# 企业微信客户专属群通知接入

当前绑定模型为：一个系统登录客户（`user` 表）对应一个企业微信客户群。代码提供连接、绑定、回调和发送服务，但尚未挂接任何订单触发逻辑。

## 企业微信管理端设置

1. 在「客户与上下游 → 客户联系 → API」中，把本系统使用的自建应用加入可调用应用。
2. 记录企业 `CorpId` 和「客户联系」`Secret`。
3. 将部署服务器公网出口 IP 加入企业可信 IP。
4. 确保专属群群主在客户联系使用范围中。
5. 将客户联系事件回调设置为 `https://你的公网域名/wecom/callback`。

## 本地配置

开发环境使用 User Secrets，不要把真实 Secret 提交到配置文件：

```powershell
dotnet user-secrets --project EplusCore set "WeCom:Enabled" "true"
dotnet user-secrets --project EplusCore set "WeCom:CompanyId" "1"
dotnet user-secrets --project EplusCore set "WeCom:CorpId" "wwxxxxxxxxxxxxxxxx"
dotnet user-secrets --project EplusCore set "WeCom:CustomerContactSecret" "你的客户联系Secret"
dotnet user-secrets --project EplusCore set "WeCom:CallbackToken" "企微后台填写的Token"
dotnet user-secrets --project EplusCore set "WeCom:CallbackEncodingAesKey" "企微后台生成的43位EncodingAESKey"
```

执行 `Persistence/Sql/20260904_add_wecom_customer_group_binding.sql` 创建绑定表。

## 一一对应规则

绑定表保存：

```text
User.Id ↔ User.OrderStartNumber（如 3106）↔ 企业微信客户群 chat_id ↔ 群主 userid
```

两个唯一索引确保一个客户账号只有一个专属群，一个群也不能绑定多个系统客户账号。群名只用于展示，不能作为稳定标识。

企业微信的客户群创建回调只提供 `ChatId`，不提供系统的 `User.Id`。因此第一次绑定应通过管理端明确选择“系统客户账号 + 企微客户群”，调用 `BindCustomerGroupAsync`。例如群名 `3106服务群` 可用 `user.OrderStartNumber = 3106` 自动建议匹配，但必须由管理员确认，最终以保存的 `chat_id` 为准。

回调会监听 `change_external_chat`；专属群被解散时，对应绑定会自动停用。

## 以后从业务接口发送

注入 `IWeComCustomerMessagingService`，传一个或多个系统客户 ID：

```csharp
var tasks = await _weComMessaging.CreateCustomerMessageTasksAsync(
    companyId,
    new[] { userId1, userId2 },
    "您的订单已发货，请登录网站查看物流进度。");
```

服务会把客户账号 `User.Id` 转成专属群 `chat_id`，再按照群主 `userid` 自动拆分为企业微信群发任务。接口成功只表示任务建立成功，群主仍需在企业微信中确认发送。

## 下一步

确定后台页面或业务接口后，再增加：

- 客户与专属群的选择、绑定、换绑和解绑页面。
- 客户群同步功能，用企业微信客户群列表填充选择框。
- 订单状态触发入口，以及 `msgid`、员工确认结果和发送结果日志。

## 联调接口

下列接口都经过现有 `AuthCode` 中间件保护，请求必须携带项目现有的 `AuthCode` Header。

```text
GET  /api/wecom/status
GET  /api/wecom/groups?name=3106
GET  /api/wecom/groups/suggest/{userId}
POST /api/wecom/bindings
GET  /api/wecom/bindings?companyId=1
POST /api/wecom/test-message
```

确认绑定请求：

```json
{
  "companyId": 1,
  "userId": 3106在user表中的真实Id,
  "chatId": "企业微信返回的chat_id"
}
```

测试发送请求（会真实创建企微群发任务）：

```json
{
  "companyId": 1,
  "userIds": [3106在user表中的真实Id],
  "text": "这是一条系统连接测试消息"
}
```

调用 `test-message` 后还需要对应的企业微信群主在群发助手中确认，消息才会进入客户群。
