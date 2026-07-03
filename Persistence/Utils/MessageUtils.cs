using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Persistence.Data;

namespace Persistence.Utils
{
    public static class MessageUtils
    {
        private const string MOBILE_SITE = "http://expressh5.epluscanada.com";
        private const string CUSTOMER_SERVICE_PHONE = "6476702288";
        public static string GetBatchNotificationMessage(SmsUserInfo userInfo, string pickUpLocation, string pickUpTime)
        {
            return $@"这里是舒誉，您{userInfo.BatchName.Split(" ")[0]}的{userInfo.OrderCount}件包裹已分拣完毕
该批次总运费为{Math.Round(userInfo.ShippingCost, 1)} 当前余额{Math.Round(userInfo.Balance, 1)}
取货地点：{pickUpLocation}
取货时间：{pickUpTime}";
        }

        public const string BatchNotificationNewMessage = "DO NOT REPLY: Dear customer, your package is ready for pickup. Please login/check your email for details.";

        public const string BatchNotificationEmailSubject = "待提货";

        public const string SetCouponUserEmailSubject = "您的优惠券";

        public const string TodoItemCompleteSubject = "待办事项已完成";

        public static string GetBatchNotificationEmailBody(string batchName, decimal userBalance, string pickUpLocation, string pickUpTime, string locationPhone, List<TransportOrder> orderList)
        {
            decimal totalShippingCost = orderList.Sum(o => o.ShippingCost ?? 0);
            var text = new StringBuilder($"<p>尊敬的客人您好，这里是舒誉，您{batchName.Split(" ")[0]}的包裹已分拣完毕</p>");
            text.Append("<table><thead><tr><th></th><th style='text-align: left'>运单号</th><th style='text-align: right'>运费(加元)</th><th style='text-align: left'>取货点</th></tr></thead><tbody>");
            for (int i = 0; i < orderList.Count; i++)
            {
                var o = orderList[i];
                text.Append($"<tr><td>包裹{i+1}</td><td style='text-align: left'>{o.OrderNumber}</td><td style='text-align: right'>{o.ShippingCost ?? 0:0.00}</td><td style='text-align: left'>{o.PickUpLocation.Name}</td></tr>");
            }
            text.Append("</tbody></table>");
            text.Append($@"<p>该批次总运费为{Math.Round(totalShippingCost, 1)} 当前余额{Math.Round(userBalance, 1)}</p>
<p>取货地点：{pickUpLocation}</p>
<p>取货时间：{pickUpTime}</p>
<p>电话：{locationPhone}</p>");
            return text.ToString();
        }

        // 扫描入库 短信
        public static string GetScanMessage(string domesticNumber, string warehouse, string customerServiceWeChat)
        {
            return $@"{warehouse}提醒您单号{domesticNumber}已入库
查看详细内容请登录官网： {MOBILE_SITE} 进行查询货登录公众号进行查询
客服电话：{CUSTOMER_SERVICE_PHONE}";
        }

        public static string GetTransferMessage(decimal amount, string transferType, string transactionType, decimal balance)
        {
            if (transferType == "deposit")
            {
                return $@"舒誉提醒您：您账户内收到加币：{amount}，交易类型：{transactionType}，当前余额为：{balance}";
            }
            if (transferType == "deduct")
            {
                return $@"舒誉提醒您：您账户内被扣掉加币：{amount}，交易类型：{transactionType}，当前余额为：{balance}";
            }
            return "";
        }

        public static string GetParcelDispatchMessage(string batchName, decimal totalWeight, decimal totalCost)
        {
            return $@"舒誉提醒您：批次号 {batchName.Substring(0, 4)} 的和包包裹已称重计费。
总重量 {Math.Round(totalWeight, 2)} kg, 总费用 {Math.Round(totalCost, 1)}。
请在系统内确认运费：{MOBILE_SITE}，包裹将在确认后尽快发出";
        }

        public static string GetParcelDispatchNewMessage(string batchName, decimal totalWeight, decimal totalCost)
        {
            return $@"DO NOT REPLY: Your package {batchName.Substring(0, 4)} is {Math.Round(totalWeight, 2)} kg. Shipping fee is {Math.Round(totalCost, 1)}. Please login and make a payment in your system.";
        }

        public const string ParcelDispatchEmailSubject = "请确认运费";            

        public static string GetParcelDispatchEmailBody(string batchName, decimal totalWeight, decimal totalCost)
        {
            return $@"舒誉提醒您：批次号 {batchName.Substring(0, 4)} 的和包包裹已称重计费。
总重量 {Math.Round(totalWeight, 2)} kg, 总费用 {Math.Round(totalCost, 1)}。
请在系统内确认运费：{MOBILE_SITE}，包裹将在确认后尽快发出";            
        }

        public static string GetLoadDeliveryMessage(string internationOrderNumber, string carrier, string recipientAddress, string recipient, string recipientPhone)
        {
            return $@"舒誉提醒您：你的包裹已发出，
国际运单号：{internationOrderNumber}
承运公司：{carrier}
收件地址：{recipientAddress}
收件人：{recipient}
电话：{recipientPhone}";
        }

        public static string GetLoadDeliveryNewMessage(string internationOrderNumber, string carrier, string recipientAddress, string recipient, string recipientPhone)
        {
            return $@"Your package is sent
Tracking NO: {internationOrderNumber}
More information please login your email";
        }

        public static string GetOrderPendingConfirmationMessage(string domesticNumber, string actionReason)
        {
            return $@"舒誉提醒您：您的包裹 {domesticNumber} {actionReason}
请您在系统内查看详情http://expressh5.epluscanada.com
如有问题可联系官方客服";
        }

        public static string GetOrderPendingConfirmationNewMessage()
        {
            return "DO NOT REPLY-Eplus:Dear Customer please login website: www.epluscanada.com to confirm the status of package";
        }

        public static string GetOrderPendingConfirmationEmailSubject(string domesticNumber)
        {
            return $"待确认 - 包裹 {domesticNumber}";
        }

        public static string GetOrderPendingConfirmationEmailBody(string domesticNumber, string actionReason)
        {
            return $@"舒誉提醒您：您的包裹 {domesticNumber} {actionReason}
请您在系统内查看详情http://www.epluscanada.com
如有问题可联系官方客服";
        }
    }
}