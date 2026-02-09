using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Persistence.Data
{
    public partial class EplusDbContext : DbContext
    {
        public EplusDbContext()
        {
        }

        public EplusDbContext(DbContextOptions<EplusDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountIntegralRecord> AccountIntegralRecords { get; set; }
        public virtual DbSet<AccountPasscardRecord> AccountPasscardRecords { get; set; }
        public virtual DbSet<AccountShopTransaction> AccountShopTransactions { get; set; }
        public virtual DbSet<AccountTransactionRecord> AccountTransactionRecords { get; set; }
        public virtual DbSet<AccountWithdrawExamine> AccountWithdrawExamines { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<BalanceHistory> BalanceHistories { get; set; }
        public virtual DbSet<BannedUserRoute> BannedUserRoutes { get; set; }
        public virtual DbSet<BaseAdvert> BaseAdverts { get; set; }
        public virtual DbSet<BaseAlbum> BaseAlbums { get; set; }
        public virtual DbSet<BaseArea> BaseAreas { get; set; }
        public virtual DbSet<BaseBanner> BaseBanners { get; set; }
        public virtual DbSet<BaseNotice> BaseNotices { get; set; }
        public virtual DbSet<BasePayConfig> BasePayConfigs { get; set; }
        public virtual DbSet<BaseRechargeSet> BaseRechargeSets { get; set; }
        public virtual DbSet<BaseSeting> BaseSetings { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<BatchPallet> BatchPallets { get; set; }
        public virtual DbSet<BatchWarehouseReceive> BatchWarehouseReceives { get; set; }
        public virtual DbSet<BatchBox> BatchBoxes { get; set; }
        public virtual DbSet<BatchBoxMap> BatchBoxMaps { get; set; }
        public virtual DbSet<BatchBoxOrderMap> BatchBoxOrderMaps { get; set; }
        public virtual DbSet<BatchOrderMap> BatchOrderMaps { get; set; }
        public virtual DbSet<BatchOtherOrder> BatchOtherOrders { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ChatFriend> ChatFriends { get; set; }
        public virtual DbSet<ChatLog> ChatLogs { get; set; }
        public virtual DbSet<ChatUser> ChatUsers { get; set; }
        public virtual DbSet<ChinaItem> ChinaItems { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponBatch> CouponBatches { get; set; }
        public virtual DbSet<CouponStatus> CouponStatuses { get; set; }
        public virtual DbSet<DeliverProgress> DeliverProgresses { get; set; }
        public virtual DbSet<Dict> Dicts { get; set; }
        public virtual DbSet<DictDetail> DictDetails { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentComment> DocumentComments { get; set; }
        public virtual DbSet<EmailData> EmailDatas { get; set; }
        public virtual DbSet<EmailDataInWarehouse> EmailDataInWarehouses { get; set; }
        public virtual DbSet<ExpressCompany> ExpressCompanies { get; set; }
        public virtual DbSet<ExpressConfig> ExpressConfigs { get; set; }
        public virtual DbSet<ExpressTransArea> ExpressTransAreas { get; set; }
        public virtual DbSet<ExpressTransport> ExpressTransports { get; set; }
        public virtual DbSet<GoodsBrand> GoodsBrands { get; set; }
        public virtual DbSet<GoodsCartShop> GoodsCartShops { get; set; }
        public virtual DbSet<GoodsCategory> GoodsCategories { get; set; }
        public virtual DbSet<GoodsDetail> GoodsDetails { get; set; }
        public virtual DbSet<GoodsExamine> GoodsExamines { get; set; }
        public virtual DbSet<GoodsOrder> GoodsOrders { get; set; }
        public virtual DbSet<GoodsOrderChild> GoodsOrderChildren { get; set; }
        public virtual DbSet<GoodsOrderInvoice> GoodsOrderInvoices { get; set; }
        public virtual DbSet<GoodsOrderRefund> GoodsOrderRefunds { get; set; }
        public virtual DbSet<GoodsOrderShop> GoodsOrderShops { get; set; }
        public virtual DbSet<GoodsShoppingCart> GoodsShoppingCarts { get; set; }
        public virtual DbSet<GoodsSku> GoodsSkus { get; set; }
        public virtual DbSet<GoodsSpecification> GoodsSpecifications { get; set; }
        public virtual DbSet<IdCard> IdCards { get; set; }
        public virtual DbSet<IntegrationIdCard> IntegrationIdCards { get; set; }
        public virtual DbSet<IntegrationUser> IntegrationUsers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemPhoto> ItemPhotos { get; set; }
        public virtual DbSet<LoadDeliveryBatch> LoadDeliveryBatches { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Log1> Logs1 { get; set; }
        public virtual DbSet<MarketingGoodsCategory> MarketingGoodsCategories { get; set; }
        public virtual DbSet<MarketingGoodsIntegral> MarketingGoodsIntegrals { get; set; }
        public virtual DbSet<MarketingGroupDetail> MarketingGroupDetails { get; set; }
        public virtual DbSet<MarketingGroupOrder> MarketingGroupOrders { get; set; }
        public virtual DbSet<MarketingReward> MarketingRewards { get; set; }
        public virtual DbSet<MarketingRewardRecord> MarketingRewardRecords { get; set; }
        public virtual DbSet<MarketingShareprofitRecord> MarketingShareprofitRecords { get; set; }
        public virtual DbSet<MarketingShareprofitUser> MarketingShareprofitUsers { get; set; }
        public virtual DbSet<OrchardGame> OrchardGames { get; set; }
        public virtual DbSet<OrderActionHistory> OrderActionHistories { get; set; }
        public virtual DbSet<OrderBaggage> OrderBaggages { get; set; }
        public virtual DbSet<OrderComment> OrderComments { get; set; }
        public virtual DbSet<OrderFlow> OrderFlows { get; set; }
        public virtual DbSet<OrderIntegral> OrderIntegrals { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderPhoto> OrderPhotos { get; set; }
        public virtual DbSet<OrderRefund> OrderRefunds { get; set; }
        public virtual DbSet<OrderRefundFlow> OrderRefundFlows { get; set; }
        public virtual DbSet<OrderScanStatus> OrderScanStatus { get; set; }
        public virtual DbSet<OrderSharingRatio> OrderSharingRatios { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<OrderStatusInternal> OrderInternalStatuses { get; set; }
        public virtual DbSet<OrderUserAction> OrderUserActions { get; set; }
        public virtual DbSet<PayMethod> PayMethods { get; set; }
        public virtual DbSet<PendingUser> PendingUsers { get; set; }
        public virtual DbSet<PickUpLocation> PickUpLocations { get; set; }
        public virtual DbSet<QiniuConfig> QiniuConfigs { get; set; }
        public virtual DbSet<QiniuContent> QiniuContents { get; set; }
        public virtual DbSet<QiniuWatermark> QiniuWatermarks { get; set; }
        public virtual DbSet<RecordBalanceHistory> RecordBalanceHistories { get; set; }
        public virtual DbSet<RecordExpressTransport> RecordExpressTransports { get; set; }
        public virtual DbSet<RingCentralCredential> RingCentralCredentials { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<ShopAudit> ShopAudits { get; set; }
        public virtual DbSet<ShopCategory> ShopCategories { get; set; }
        public virtual DbSet<ShopInfo> ShopInfos { get; set; }
        public virtual DbSet<ShopOffline> ShopOfflines { get; set; }
        public virtual DbSet<ShopReceiveAddress> ShopReceiveAddresses { get; set; }
        public virtual DbSet<ShopService> ShopServices { get; set; }
        public virtual DbSet<ShopSupplier> ShopSuppliers { get; set; }
        public virtual DbSet<SMSLog> SMSLogs { get; set; }
        public virtual DbSet<SmsMessageConfig> SmsMessageConfigs { get; set; }
        public virtual DbSet<SmsMessageHistory> SmsMessageHistories { get; set; }
        public virtual DbSet<SmsMessageTemplate> SmsMessageTemplates { get; set; }
        public virtual DbSet<SubscribeHistory> SubscribeHistories { get; set; }
        public virtual DbSet<SupportUser> SupportUsers { get; set; }
        public virtual DbSet<SysAppuser> SysAppusers { get; set; }
        public virtual DbSet<SysAppuserRecommend> SysAppuserRecommends { get; set; }
        public virtual DbSet<SysBankCard> SysBankCards { get; set; }
        public virtual DbSet<SysFavorite> SysFavorites { get; set; }
        public virtual DbSet<SysInterfaceConfig> SysInterfaceConfigs { get; set; }
        public virtual DbSet<SysInvoiceQualification> SysInvoiceQualifications { get; set; }
        public virtual DbSet<SysMenu> SysMenus { get; set; }
        public virtual DbSet<SysOwnerAudit> SysOwnerAudits { get; set; }
        public virtual DbSet<SysRole> SysRoles { get; set; }
        public virtual DbSet<SysRolesMenu> SysRolesMenus { get; set; }
        public virtual DbSet<SysShippingAddress> SysShippingAddresses { get; set; }
        public virtual DbSet<SysUser> SysUsers { get; set; }
        public virtual DbSet<SysUsersRole> SysUsersRoles { get; set; }
        public virtual DbSet<SysWechatUserinfo> SysWechatUserinfos { get; set; }
        public virtual DbSet<SystemPhoto> SystemPhotos { get; set; }
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }
        public virtual DbSet<TodoItem> TodoItem { get; set; }
        public virtual DbSet<TodoItemAssignee> TodoItemAssignees { get; set; }
        public virtual DbSet<TodoItemCustomer> TodoItemCustomers { get; set; }
        public virtual DbSet<TodoItemOrder> TodoItemOrders { get; set; }
        public virtual DbSet<ToolsGetui> ToolsGetuis { get; set; }
        public virtual DbSet<TransportOrder> TransportOrders { get; set; }
        public virtual DbSet<TransportOrderAudit> TransportOrderAudits { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }
        public virtual DbSet<YoudumallUser> YoudumallUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.HasComment("账户表\r\n")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.UserId, "index2");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AccountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("account_type")
                    .HasComment("账户类型：0->平台,1->线上店，2->用户，3->线下店，4->供应商");

                entity.Property(e => e.AvailableBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("available_balance")
                    .HasComment("可用余额");

                entity.Property(e => e.CautionMoney)
                    .HasPrecision(10, 2)
                    .HasColumnName("caution_money")
                    .HasComment("保证金");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.FreezeAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("freeze_amount")
                    .HasComment("冻结金额");

                entity.Property(e => e.Integral)
                    .HasPrecision(10, 2)
                    .HasColumnName("integral")
                    .HasComment("积分");

                entity.Property(e => e.IntegralAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("integral_amount")
                    .HasComment("积分总额");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.IsSign)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_sign")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否签到");

                entity.Property(e => e.IsWatchVideo)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_watch_video")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否观看完整视频");

                entity.Property(e => e.PasscardAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("passcard_amount")
                    .HasComment("通证总额");

                entity.Property(e => e.PasscardBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("passcard_balance")
                    .HasComment("通证余额");

                entity.Property(e => e.SignTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("sign_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("签到时间");

                entity.Property(e => e.TodayBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("today_balance")
                    .HasComment("今日芝麻粒");

                entity.Property(e => e.TodayIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("today_integral")
                    .HasComment("今日芝麻花");

                entity.Property(e => e.UnliquidatedAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("unliquidated_amount")
                    .HasComment("未结算金额");

                entity.Property(e => e.UnliquidatedIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("unliquidated_integral")
                    .HasComment("未结算积分");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("用户/商家/会员id");

                entity.Property(e => e.WatchVideoTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("watch_video_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("签到观看视频时间");

                entity.Property(e => e.WithdrawTotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("withdraw_total")
                    .HasComment("提现总额");
            });

            modelBuilder.Entity<AccountIntegralRecord>(entity =>
            {
                entity.ToTable("account_integral_record");

                entity.HasComment("账户积分记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("account_id")
                    .HasComment("账号id");

                entity.Property(e => e.AccountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("account_type")
                    .HasComment("账户类型：0->平台,1->线上店，2->用户，3->线下店，4->供应商");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount")
                    .HasComment("交易额");

                entity.Property(e => e.BillNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("bill_number")
                    .HasComment("单据编号");

                entity.Property(e => e.BusinessType)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("business_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("业务类型：100->充值预存款赠送芝麻花，110->提现，120->商品支付，121->商品退款，122->商品收货，130->平台变更，140->果园游戏开通，150->积分商品支付，160->签到,172->线上店铺给用户转账芝麻花,173->线下店铺给用户转账芝麻花 ,180->抽奖奖励（芝麻粒抽奖）,181->抽奖扣除（芝麻粒抽奖）,190->分红扣除");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.InOut)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("in_out")
                    .HasComment("交易方向：0->转入，1->转出");

                entity.Property(e => e.IntegralBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("integral_balance")
                    .HasComment("积分余额/未结算积分");

                entity.Property(e => e.OperatorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("operator_id")
                    .HasComment("操作员id");

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(50)
                    .HasColumnName("operator_name")
                    .HasComment("操作员名称");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_id")
                    .HasComment("相关订单id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(255)
                    .HasColumnName("order_number")
                    .HasComment("相关订单编号");

                entity.Property(e => e.OrderType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_type")
                    .HasComment("订单类型：0->商品总订单，1->商品店铺订单，10->积分商品订单");

                entity.Property(e => e.OtherAccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("other_account_id")
                    .HasComment("对方账号id");

                entity.Property(e => e.OtherAccountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("other_account_type")
                    .HasComment("账户类型：0->平台，1->店铺，2->用户");

                entity.Property(e => e.OtherUserPhone)
                    .HasMaxLength(255)
                    .HasColumnName("other_user_phone")
                    .HasComment("对方会员（或者店主）账号");

                entity.Property(e => e.Remark)
                    .HasMaxLength(200)
                    .HasColumnName("remark")
                    .HasComment("备注");

                entity.Property(e => e.TransactionType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("transaction_type")
                    .HasComment("交易类型：0->账户积分，1->未结算积分");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(255)
                    .HasColumnName("user_phone")
                    .HasComment("会员（或者店主）账号");
            });

            modelBuilder.Entity<AccountPasscardRecord>(entity =>
            {
                entity.ToTable("account_passcard_record");

                entity.HasComment("账户通证变更记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("account_id")
                    .HasComment("账号id");

                entity.Property(e => e.AccountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("account_type")
                    .HasComment("账户类型：0->平台，1->店铺，2->用户");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount")
                    .HasComment("交易额");

                entity.Property(e => e.BillNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("bill_number")
                    .HasComment("单据编号");

                entity.Property(e => e.BusinessType)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("business_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("业务类型：100->手动变更，110->账户分红，120->积分商品支付，130->签到后通证转芝麻粒");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.InOut)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("in_out")
                    .HasComment("交易方向：0->转入，1->转出");

                entity.Property(e => e.OperatorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("operator_id")
                    .HasComment("操作员id");

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(50)
                    .HasColumnName("operator_name")
                    .HasComment("操作员名称");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_id")
                    .HasComment("相关订单id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(100)
                    .HasColumnName("order_number")
                    .HasComment("相关订单编号");

                entity.Property(e => e.OrderType)
                    .HasMaxLength(255)
                    .HasColumnName("order_type")
                    .HasComment("订单类型：0->商品总订单，1->商品店铺订单，2->积分商品订单");

                entity.Property(e => e.OtherAccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("other_account_id")
                    .HasComment("对方账号id");

                entity.Property(e => e.OtherAccountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("other_account_type")
                    .HasComment("账户类型：0->平台，1->店铺，2->用户");

                entity.Property(e => e.PasscardAccount)
                    .HasPrecision(10, 2)
                    .HasColumnName("passcard_account")
                    .HasComment("通证总额");

                entity.Property(e => e.PasscardBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("passcard_balance")
                    .HasComment("通证余额");

                entity.Property(e => e.RecordType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("record_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("交易类型：1->账户余额，2账户总额");

                entity.Property(e => e.Remark)
                    .HasMaxLength(200)
                    .HasColumnName("remark")
                    .HasComment("备注");

                entity.Property(e => e.TransactionType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("transaction_type")
                    .HasComment("交易类型：0->账户通证");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(255)
                    .HasColumnName("user_phone")
                    .HasComment("会员（或者店主）账号");
            });

            modelBuilder.Entity<AccountShopTransaction>(entity =>
            {
                entity.ToTable("account_shop_transaction");

                entity.HasComment("店铺对账单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.EndBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("end_balance")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("本期期末");

                entity.Property(e => e.ExpendBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("expend_balance")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("支出/提现总额");

                entity.Property(e => e.ExpendNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("expend_number")
                    .HasComment("支出/提现笔数");

                entity.Property(e => e.IncomeBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("income_balance")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("收入/充值总额");

                entity.Property(e => e.IncomeNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("income_number")
                    .HasComment("收入/充值笔数");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.StartBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("start_balance")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("本期期初");

                entity.Property(e => e.TransactionType)
                    .HasColumnName("transaction_type")
                    .HasComment("交易类型:1,收入支出,2,充值提现");
            });

            modelBuilder.Entity<AccountTransactionRecord>(entity =>
            {
                entity.ToTable("account_transaction_record");

                entity.HasComment("账户交易记录表")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("主键");

                entity.Property(e => e.AccountBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("account_balance")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("账户余额/未结算金额");

                entity.Property(e => e.AccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("account_id")
                    .HasComment("用户账号id");

                entity.Property(e => e.AccountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("account_type")
                    .HasComment("账户类型：0->平台,1->线上店，2->用户，3->线下店，4->供应商");

                entity.Property(e => e.BillNumber)
                    .HasMaxLength(255)
                    .HasColumnName("bill_number")
                    .HasComment("单据编码");

                entity.Property(e => e.BusinessType)
                    .HasColumnType("int(11)")
                    .HasColumnName("business_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("业务类型：100->充值预存款，101->充值保障金，110->提现，120->商品支付，121->商品退款，122->商品收货，123->推荐一级会员购买商品返利，124->推荐二级会员购买商品返利，125->推荐一级网店卖商品返利，126->推荐一级供应商卖商品返利，127->推荐二级网店卖商品返利，128->推荐二级供应商卖商品返利，130->平台变更，160->拼团活动支付，161->拼团失败返还，162->拼团未中奖返还，163->拼团中奖,170->用户给线上店铺转账（芝麻粒）,171->用户给线下店铺转账（芝麻粒）,180->抽奖奖励（抽奖）,181->抽奖扣除（抽奖），190->录单给自己充值，191->录单给他人充值");

                entity.Property(e => e.Cardid)
                    .HasMaxLength(255)
                    .HasColumnName("cardid")
                    .HasComment("提现银行卡号/微信账户号/支付宝账户");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.InOut)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("in_out")
                    .HasComment("交易方向：0->转入，1->转出");

                entity.Property(e => e.IsExport)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_export")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否导出报表：0->否，1->是");

                entity.Property(e => e.Money)
                    .HasPrecision(10, 2)
                    .HasColumnName("money")
                    .HasComment("交易额");

                entity.Property(e => e.OperatorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("operator_id")
                    .HasComment("操作员id");

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(50)
                    .HasColumnName("operator_name")
                    .HasComment("操作员名称");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_id")
                    .HasComment("订单id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(255)
                    .HasColumnName("order_number")
                    .HasComment("订单编号");

                entity.Property(e => e.OrderType)
                    .HasColumnName("order_type")
                    .HasComment("订单类型：0->商品总订单，1->商品店铺订单，2->拼团订单");

                entity.Property(e => e.OtherAccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("other_account_id")
                    .HasComment("对方账号id");

                entity.Property(e => e.OtherAccountType)
                    .HasColumnName("other_account_type")
                    .HasComment("对方账户类型：-1->未知，0->平台，1->店铺，2->用户");

                entity.Property(e => e.OtherOrderId)
                    .HasMaxLength(255)
                    .HasColumnName("other_order_id")
                    .HasComment("支付宝/微信支付流水号");

                entity.Property(e => e.OtherUserPhone)
                    .HasMaxLength(255)
                    .HasColumnName("other_user_phone")
                    .HasComment("对方会员（或者店主）账号");

                entity.Property(e => e.PayType)
                    .HasColumnName("pay_type")
                    .HasComment("支付类型：0->支付宝，1->1微信，2->预存款");

                entity.Property(e => e.RecommendedId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("recommended_id")
                    .HasComment("被推荐者id(推荐和果园游戏业务)");

                entity.Property(e => e.Remark)
                    .HasMaxLength(200)
                    .HasColumnName("remark")
                    .HasComment("备注");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'")
                    .HasComment("状态：0->提交成功，1->操作成功，2->操作失败");

                entity.Property(e => e.TransactionType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("transaction_type")
                    .HasComment("交易类型：0->账户金额，1->未结算金额");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("入账时间");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(255)
                    .HasColumnName("user_phone")
                    .HasComment("会员（或者店主）账号");
            });

            modelBuilder.Entity<AccountWithdrawExamine>(entity =>
            {
                entity.ToTable("account_withdraw_examine");

                entity.HasComment("账户提现审核记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AccountBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("account_balance")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("申请前芝麻粒（账户余额）");

                entity.Property(e => e.AccountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("account_type")
                    .HasComment("账户类型;0->用户，1->网店，2->门店，3->供应商");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("提现会员id");

                entity.Property(e => e.AppUserPhone)
                    .HasMaxLength(255)
                    .HasColumnName("app_user_phone")
                    .HasComment("会员手机号");

                entity.Property(e => e.BankCardBelongs)
                    .HasMaxLength(32)
                    .HasColumnName("bank_card_belongs")
                    .HasComment("银行卡所属行");

                entity.Property(e => e.BankCardNumber)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("bank_card_number")
                    .HasComment("银行卡号");

                entity.Property(e => e.BillNumber)
                    .HasMaxLength(255)
                    .HasColumnName("bill_number")
                    .HasComment("会员提现编号");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("申请日期");

                entity.Property(e => e.ExamineDetails)
                    .HasMaxLength(255)
                    .HasColumnName("examine_details")
                    .HasComment(" 反馈详情");

                entity.Property(e => e.OperatorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("operator_id")
                    .HasComment("操作员id");

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(50)
                    .HasColumnName("operator_name")
                    .HasComment("操作员名称");

                entity.Property(e => e.RecordId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("record_id")
                    .HasComment("账户交易记录表id");

                entity.Property(e => e.Remark)
                    .HasMaxLength(200)
                    .HasColumnName("remark")
                    .HasComment("备注");

                entity.Property(e => e.TransformIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("transform_integral")
                    .HasComment("转换芝麻花");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("审核时间");

                entity.Property(e => e.WithdrawalAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("withdrawal_amount")
                    .HasComment("提现芝麻粒");

                entity.Property(e => e.WithdrawalMoney)
                    .HasPrecision(10, 2)
                    .HasColumnName("withdrawal_money")
                    .HasComment("提现金额");

                entity.Property(e => e.WithdrawalStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("withdrawal_status")
                    .HasComment("提现状态（审核结果）:0->已提交,1->已通过,2->失败");

                entity.Property(e => e.WithdrawalType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("withdrawal_type")
                    .HasComment("提现类型;0-> 银行卡，1->微信，2->支付宝");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.HasComment("北美州/省")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("Id");

                entity.Property(e => e.ShortName)
                    .HasColumnType("varchar(8)")
                    .HasColumnName("ShortName");

                entity.Property(e => e.FullName)
                    .HasColumnType("varchar(50)")
                    .HasColumnName("FullName");

                entity.Property(e => e.CountryCode)
                    .HasColumnType("varchar(4)")
                    .HasColumnName("CountryCode");
            });

            modelBuilder.Entity<BalanceHistory>(entity =>
            {
                entity.ToTable("balance_history");

                entity.HasComment("账单");

                entity.HasIndex(e => e.FromUserId, "IX_dbo.BalanceHistory_FromUserId");

                entity.HasIndex(e => e.OrderId, "IX_dbo.BalanceHistory_OrderId");

                entity.HasIndex(e => e.ToUserId, "IX_dbo.BalanceHistory_ToUserId");

                entity.HasIndex(e => e.BatchId, "IX_dbo.BalanceHistoryh_BatchId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasPrecision(19, 4)
                    .HasComment("金额");

                entity.Property(e => e.BatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasComment("创建日期");

                entity.Property(e => e.Discount)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("折扣");

                entity.Property(e => e.ExchangeRate)
                    .HasPrecision(10, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("优惠");

                entity.Property(e => e.FromUserCurrentBalance)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("来自用户当前余额");

                entity.Property(e => e.FromUserDisplayAmount)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("来自用户展示金额");

                entity.Property(e => e.FromUserId)
                    .HasColumnType("int(11)")
                    .HasComment("来自用户");

                entity.Property(e => e.Method).HasMaxLength(100);

                entity.Property(e => e.Notes)
                    .HasColumnType("text")
                    .HasComment("信息");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id");

                entity.Property(e => e.Rmb)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("人民币");

                entity.Property(e => e.ToUserActualAmount)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("对方用户实际金额");

                entity.Property(e => e.ToUserCurrentBalance)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("对方用户当前余额");

                entity.Property(e => e.ToUserId)
                    .HasColumnType("int(11)")
                    .HasComment("对方用户");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasComment("类型：0->用余额支付运单,1->存款,2->用现金支付运单时先存款,3->用现金支付运单时的扣款,4->扣款，5->给自己充值，6->给他人充值");

                entity.Property(e => e.TransactionGuid)
                    .HasColumnName("transaction_guid")
                    .HasColumnType("text")
                    .HasComment("交易ID");

                entity.Property(e => e.ActualAmount)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("实际支付金额");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BalanceHistories)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK_dbo.BalanceHistory_dbo.Batch_BatchId");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.BalanceHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BalanceHistory_dbo.User_FromUserId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.BalanceHistories)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_dbo.BalanceHistory_dbo.Order_OrderId");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.BalanceHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BalanceHistory_dbo.User_ToUserId");
            });

            modelBuilder.Entity<BannedUserRoute>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RouteId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("banned_user_route");

                entity.HasComment("用户限制线路");

                entity.HasIndex(e => e.RouteId, "IX_dbo.BannedUserRoute_RouteId");

                entity.HasIndex(e => e.UserId, "IX_dbo.BannedUserRoute_UserId");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("用户id");

                entity.Property(e => e.RouteId)
                    .HasColumnType("int(11)")
                    .HasComment("线路id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.BannedUserRoutes)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BannedUserRoute_dbo.Route_RouteId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BannedUserRoutes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BannedUserRoute_dbo.User_UserId");
            });

            modelBuilder.Entity<BaseAdvert>(entity =>
            {
                entity.ToTable("base_advert");

                entity.HasComment("广告位")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AdPictureKey)
                    .HasMaxLength(50)
                    .HasColumnName("ad_picture_key")
                    .HasComment("广告图片key");

                entity.Property(e => e.AdPostiton)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("ad_postiton")
                    .HasComment("广告位置(1：1号位/左上，2：二号位/右上，3：三号位/左下，4：四号位/右下)");

                entity.Property(e => e.AdSize)
                    .HasMaxLength(32)
                    .HasColumnName("ad_size")
                    .HasComment("尺寸大小");

                entity.Property(e => e.AdType)
                    .HasColumnName("ad_type")
                    .HasComment("广告类型：0 首页banner,1 首页普惠专区,2首页扶贫专区，3拼团banner");

                entity.Property(e => e.AdUrl)
                    .HasMaxLength(255)
                    .HasColumnName("ad_url")
                    .HasComment(" 广告链接");

                entity.Property(e => e.ClickType)
                    .HasColumnName("click_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("点击类型 0：商品,1：专题链接");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.GoodsId)
                    .HasMaxLength(32)
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsShow)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_show")
                    .HasDefaultValueSql("'1'")
                    .HasComment("是否展示(0否，1是)");

                entity.Property(e => e.Sort)
                    .HasColumnType("int(1)")
                    .HasColumnName("sort")
                    .HasComment("序号");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyId")
                    .HasComment("公司ID");
            });

            modelBuilder.Entity<BaseAlbum>(entity =>
            {
                entity.ToTable("base_album");

                entity.HasComment("相册库")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.AlblumInfo)
                    .HasColumnName("alblum_info")
                    .HasComment(" 相册说明");

                entity.Property(e => e.AlbumDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("album_default")
                    .HasDefaultValueSql("b'0'")
                    .HasComment(" 是否默认相册，系统只有一个默认相册:0,false,1,true");

                entity.Property(e => e.AlbumName)
                    .HasMaxLength(255)
                    .HasColumnName("album_name")
                    .HasComment(" 相册名称");

                entity.Property(e => e.AlbumSequence)
                    .HasColumnType("int(11)")
                    .HasColumnName("album_sequence")
                    .HasDefaultValueSql("'0'")
                    .HasComment(" 相册序号");

                entity.Property(e => e.AlbumStoreId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("album_store_id")
                    .HasComment("相册对应店铺id");

                entity.Property(e => e.AlbumType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("album_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("相册类型:0,平台,1,线上店铺");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.PictureKeys)
                    .HasMaxLength(500)
                    .HasColumnName("picture_keys")
                    .HasComment("相册封面");
            });

            modelBuilder.Entity<BaseArea>(entity =>
            {
                entity.ToTable("base_area");

                entity.HasComment("区域")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11) unsigned")
                    .HasColumnName("id");

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("area_name")
                    .HasDefaultValueSql("''")
                    .HasComment("名称");

                entity.Property(e => e.AreaStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("area_status")
                    .HasComment("地区状态:0:默认,1:新增,2:删除3,修改");

                entity.Property(e => e.AreaType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("area_type")
                    .HasComment("地址类型：0->中国，1->加拿大");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .HasColumnName("code")
                    .HasDefaultValueSql("''")
                    .HasComment("行政代码");

                entity.Property(e => e.FirstWord)
                    .HasMaxLength(255)
                    .HasColumnName("first_word")
                    .HasDefaultValueSql("''")
                    .HasComment("首字母");

                entity.Property(e => e.Level)
                    .HasColumnType("int(11)")
                    .HasColumnName("level")
                    .HasComment("城市等级");

                entity.Property(e => e.ParentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("parent_id")
                    .HasComment("父id");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(50)
                    .HasColumnName("postal_code")
                    .HasDefaultValueSql("''")
                    .HasComment("邮政编码");
            });

            modelBuilder.Entity<BaseBanner>(entity =>
            {
                entity.ToTable("base_banner");

                entity.HasComment("导航栏")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.BannerType)
                    .HasColumnName("banner_type")
                    .HasComment("banner类型 0：首页,1：装修");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.GoodsCategoryId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("goods_category_id")
                    .HasComment("商品分类id");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsShow)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_show")
                    .HasComment("是否展示(0否，1是)");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name")
                    .HasComment("名称");

                entity.Property(e => e.OrderNum)
                    .HasColumnType("int(1)")
                    .HasColumnName("order_num")
                    .HasComment("序号");

                entity.Property(e => e.PictureKey)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("picture_key")
                    .HasComment("图片key");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("类型：0 商品分类,1 专题链接");

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .HasColumnName("url")
                    .HasComment("专题链接");
            });

            modelBuilder.Entity<BaseNotice>(entity =>
            {
                entity.ToTable("base_notice");

                entity.HasComment("平台公告")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .HasComment("公告内容");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsShow)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_show")
                    .HasComment("是否展示(0否，1是)");

                entity.Property(e => e.OperatorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("operator_id")
                    .HasComment("发布人id");

                entity.Property(e => e.OperatorName)
                    .HasMaxLength(50)
                    .HasColumnName("operator_name")
                    .HasComment("发布人名称");

                entity.Property(e => e.Sort)
                    .HasColumnType("int(1)")
                    .HasColumnName("sort")
                    .HasComment("序号");

                entity.Property(e => e.Title)
                    .HasMaxLength(32)
                    .HasColumnName("title")
                    .HasComment("公告标题");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("公告类型：0 首页");
            });

            modelBuilder.Entity<BasePayConfig>(entity =>
            {
                entity.ToTable("base_pay_config");

                entity.HasComment("支付配置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("支付配置ID");

                entity.Property(e => e.AppId)
                    .HasMaxLength(255)
                    .HasColumnName("app_id")
                    .HasComment("应用ID");

                entity.Property(e => e.GatewayUrl)
                    .HasMaxLength(255)
                    .HasColumnName("gateway_url")
                    .HasComment("网关地址");

                entity.Property(e => e.IsEnable)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_enable")
                    .HasDefaultValueSql("'1'")
                    .HasComment("是否启用");

                entity.Property(e => e.MchId)
                    .HasMaxLength(255)
                    .HasColumnName("mch_id")
                    .HasComment("商户号");

                entity.Property(e => e.NotifyUrl)
                    .HasMaxLength(255)
                    .HasColumnName("notify_url")
                    .HasComment("异步回调");

                entity.Property(e => e.PayAccount)
                    .HasMaxLength(255)
                    .HasColumnName("pay_account")
                    .HasComment("支付账户");

                entity.Property(e => e.PayDescribe)
                    .HasMaxLength(500)
                    .HasColumnName("pay_describe")
                    .HasComment("支付方式描述");

                entity.Property(e => e.PayMode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("pay_mode")
                    .HasComment("支付方式 0:支付宝app支付，1:微信app支付,2,ott支付");

                entity.Property(e => e.PayName)
                    .HasMaxLength(255)
                    .HasColumnName("pay_name")
                    .HasComment("支付名称");

                entity.Property(e => e.PrivateKey)
                    .HasColumnType("text")
                    .HasColumnName("private_key")
                    .HasComment("私钥");

                entity.Property(e => e.PublicKey)
                    .HasColumnType("text")
                    .HasColumnName("public_key")
                    .HasComment("公钥");

                entity.Property(e => e.ReturnUrl)
                    .HasMaxLength(255)
                    .HasColumnName("return_url")
                    .HasComment("回调地址");

                entity.Property(e => e.SignType)
                    .HasMaxLength(255)
                    .HasColumnName("sign_type")
                    .HasComment("签名方式");
            });

            modelBuilder.Entity<BaseRechargeSet>(entity =>
            {
                entity.ToTable("base_recharge_set");

                entity.HasComment("充值赠送设置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.IsDel, "index_is_del");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GiveBalance)
                    .HasPrecision(10, 2)
                    .HasColumnName("give_balance")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("赠送余额（芝麻粒）");

                entity.Property(e => e.GiveIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("give_integral")
                    .HasComment("赠送积分（芝麻花）");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.RechargeAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("recharge_amount")
                    .HasComment("充值金额");

                entity.Property(e => e.RechargeType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("recharge_type")
                    .HasComment("账户类型：0->会员充值,1->商家充值");
            });

            modelBuilder.Entity<BaseSeting>(entity =>
            {
                entity.ToTable("base_seting");

                entity.HasComment("基础设置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.SetKey, "Index_key")
                    .IsUnique();

                entity.HasIndex(e => e.Type, "Index_type");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.Remark)
                    .HasMaxLength(200)
                    .HasColumnName("remark")
                    .HasComment("备注");

                entity.Property(e => e.SetKey)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("set_key")
                    .HasComment("键");

                entity.Property(e => e.SetValue)
                    .HasColumnName("set_value")
                    .HasComment("值");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("type")
                    .HasComment("类型");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("修改时间");

                entity.Property(e => e.ValueType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("value_type")
                    .HasComment("值类型 0：文本，1：文件");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.ToTable("batch");

                entity.HasComment("批次");

                entity.HasIndex(e => e.RouteId, "FK_dbo.Batch_dbo.Route_RouteId");

                entity.HasIndex(e => e.BelongsToUserId, "IX_dbo.Batch_BelongsToUserId");

                entity.HasIndex(e => e.GroupType, "IX_dbo.Batch_GroupType");

                entity.HasIndex(e => e.MasterBatchId, "IX_dbo.Batch_MasterBatchId");

                entity.HasIndex(e => e.ProgressId, "IX_dbo.Batch_ProgressId");

                entity.HasIndex(e => e.RecipientUserId, "IX_dbo.Batch_RecipientUserId");

                entity.HasIndex(e => e.UserId, "IX_dbo.Batch_UserId");

                entity.HasIndex(e => e.WarehouseId, "IX_dbo.Batch_WarehouseId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddOnCost)
                    .HasPrecision(19, 4)
                    .HasComment("运单附加费");

                entity.Property(e => e.AeroNumber)
                    .HasMaxLength(100)
                    .HasComment("国际运单号");

                entity.Property(e => e.AeroShippingCost)
                    .HasPrecision(19, 4)
                    .HasComment("航空运费/单/kg\r\n");

                entity.Property(e => e.BelongsToUserId)
                    .HasColumnType("int(11)")
                    .HasComment("分发账户");

                entity.Property(e => e.BoxInfo).HasComment("箱号信息(箱号与运单对应关系以json格式存储\r\n)");

                entity.Property(e => e.ClearingPortFee)
                    .HasPrecision(19, 4)
                    .HasComment("清关费/单");

                entity.Property(e => e.Commission).HasPrecision(19, 4);

                entity.Property(e => e.Cost)
                    .HasPrecision(19, 4)
                    .HasComment("成本");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasComment("日期");

                entity.Property(e => e.DateEntered).HasColumnType("datetime");

                entity.Property(e => e.DeliveryCost)
                    .HasPrecision(19, 4)
                    .HasDefaultValueSql("'0.0000'")
                    .HasComment("派送费");

                entity.Property(e => e.Discount).HasPrecision(19, 4);

                entity.Property(e => e.DistrictAdditionalCost)
                    .HasPrecision(19, 4)
                    .HasComment("地区附加费");

                entity.Property(e => e.Duty)
                    .HasPrecision(19, 4)
                    .HasComment("关税");

                entity.Property(e => e.GroupType).HasColumnType("int(11)");

                entity.Property(e => e.HeBaoCost)
                    .HasPrecision(19, 4)
                    .HasComment("包运费");

                entity.Property(e => e.InsuranceFee)
                    .HasPrecision(19, 4)
                    .HasComment("保险");

                entity.Property(e => e.IntCarrier)
                    .HasMaxLength(40)
                    .HasComment("国际快递公司");

                entity.Property(e => e.IntNumber).HasMaxLength(100);

                entity.Property(e => e.IsConfirmed)
                    .HasColumnType("tinyint(4)")
                    .HasComment("已确认");

                entity.Property(e => e.IsForCreation)
                    .HasColumnType("tinyint(4)")
                    .HasComment("是做单用(个别线路的单会自动加入该批次\r\n)");

                entity.Property(e => e.IsFromChina)
                    .HasColumnType("tinyint(4)")
                    .HasComment("从中国到加拿大");

                entity.Property(e => e.MasterBatchId).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.PaidWeightKg)
                    .HasPrecision(16, 2)
                    .HasComment("收费重量Kg");

                entity.Property(e => e.PickType)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("提货类型：0-货运提货，1-商城团购提货，2，团购线到货");

                entity.Property(e => e.PickUpLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("pick_up_location_id")
                    .HasComment("自提点id");

                entity.Property(e => e.ProgressId).HasColumnType("int(11)");

                entity.Property(e => e.RecipientAddressId)
                    .HasColumnType("int(11)")
                    .HasComment("收件地址id");

                entity.Property(e => e.RecipientUserId)
                    .HasColumnType("int(11)")
                    .HasComment("收件人");

                entity.Property(e => e.RouteId)
                    .HasColumnType("int(11)")
                    .HasComment("线路id");

                entity.Property(e => e.ShippingCost)
                    .HasPrecision(19, 4)
                    .HasComment("运费");

                entity.Property(e => e.Stage).HasColumnType("int(11)");

                entity.Property(e => e.StorageCost)
                    .HasPrecision(19, 4)
                    .HasComment("仓库附加费");

                entity.Property(e => e.TargetWeightKg).HasPrecision(16, 2);

                entity.Property(e => e.TotalExpense).HasPrecision(19, 4);

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasComment("类型");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("用户id");

                entity.Property(e => e.WarehouseId).HasColumnType("int(11)");

                entity.Property(e => e.Note)
                    .HasColumnType("text")
                    .HasColumnName("note")
                    .HasComment("备注");

                entity.Property(e => e.CompanyId).HasColumnType("int");

                entity.HasOne(d => d.BelongsToUser)
                    .WithMany(p => p.BatchBelongsToUsers)
                    .HasForeignKey(d => d.BelongsToUserId)
                    .HasConstraintName("FK_dbo.Batch_dbo.User_BelongsToUserId");

                entity.HasOne(d => d.MasterBatch)
                    .WithMany(p => p.InverseMasterBatch)
                    .HasForeignKey(d => d.MasterBatchId)
                    .HasConstraintName("FK_dbo.Batch_dbo.Batch_MasterBatchId");

                entity.HasOne(d => d.Progress)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.ProgressId)
                    .HasConstraintName("FK_dbo.Batch_dbo.DeliverProgress_ProgressId");

                entity.HasOne(d => d.RecipientUser)
                    .WithMany(p => p.BatchRecipientUsers)
                    .HasForeignKey(d => d.RecipientUserId)
                    .HasConstraintName("FK_dbo.Batch_dbo.User_RecipientUserId");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_dbo.Batch_dbo.Route_RouteId");

                entity.HasOne(d => d.PickUpLocation)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.PickUpLocationId)
                    .HasConstraintName("FK_dbo.Batch_dbo.PickUpLocation_PickUpLocationId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.BatchUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.Batch_dbo.User_UserId");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_dbo.Batch_dbo.Warehouse_WarehouseId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Batch_CompanyId");
            });

            modelBuilder.Entity<BatchPallet>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.ToTable("batch_pallet");

                entity.HasComment("托盘订单二级表");

                entity.HasIndex(e => e.BatchId, "IX_dbo.Batch_BatchId");

                entity.Property(e => e.BatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次id");

                entity.Property(e => e.WarehouseId)
                    .HasColumnType("int(11)")
                    .HasComment("仓库id");

                entity.Property(e => e.Length)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("Length")
                    .HasComment("托盘长度");

                entity.Property(e => e.Width)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("Width")
                    .HasComment("托盘宽度");

                entity.Property(e => e.Height)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("Height")
                    .HasComment("托盘高度");

                entity.Property(e => e.WeightKg)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("WeightKg")
                    .HasComment("托盘重量(千克)");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.BatchPallets)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchPallet_dbo.Warehouse_WarehouseId");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchPallets)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchPallet_dbo.Batch_BatchId");
            });

            modelBuilder.Entity<BatchWarehouseReceive>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.ToTable("batch_warehouse_receive");

                entity.HasComment("仓库收货二级表");

                entity.HasIndex(e => e.BatchId, "IX_dbo.Batch_BatchId");

                entity.Property(e => e.BatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次id");

                entity.Property(e => e.WarehouseId)
                    .HasColumnType("int(11)")
                    .HasComment("仓库id");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.BatchWarehouseReceives)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchWarehouseReceive_dbo.Warehouse_WarehouseId");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchWarehouseReceives)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchWarehouseReceive_dbo.Batch_BatchId");
            });

            modelBuilder.Entity<BatchBox>(entity =>
            {
                entity.ToTable("batch_box");

                entity.HasComment("批次箱");

                entity.HasIndex(e => e.BatchId, "IX_dbo.BatchBox_BatchId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次id");

                entity.Property(e => e.Number)
                    .HasColumnType("int(11)")
                    .HasComment("数量");

                entity.Property(e => e.Name)
                    .HasColumnType("varchar(32)")
                    .HasComment("箱名称，格式为 原始批次ID - 序号");

                entity.Property(e => e.Length)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("Length")
                    .HasComment("长");

                entity.Property(e => e.Width)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("Width")
                    .HasComment("宽");

                entity.Property(e => e.Height)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("Height")
                    .HasComment("高");

                entity.Property(e => e.ActualWeightKg)
                    .HasColumnType("decimal(18,0)")
                    .HasColumnName("ActualWeightKg")
                    .HasComment("实际重量(kg)");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchBoxes)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchBox_dbo.BatchId_BatchId");
            });

            modelBuilder.Entity<BatchBoxMap>(entity =>
            {
                entity.HasKey(e => new { e.Id })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("batch_box_map");

                entity.HasComment("批次箱与箱号");

                entity.HasIndex(e => e.BatchId, "IX_dbo.BatchBoxMap_BatchId");

                entity.HasIndex(e => e.BoxId, "IX_dbo.BatchBoxMap_OrderId");

                entity.Property(e => e.BoxId)
                    .HasColumnType("int(11)")
                    .HasComment("批次箱id");

                entity.Property(e => e.BatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次id");

                entity.HasOne(d => d.BatchBox)
                    .WithMany(p => p.BatchBoxMaps)
                    .HasForeignKey(d => d.BoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchBoxMap_dbo.BatchBox_BoxId");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchBoxMaps)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchBoxMap_dbo.Batch_BatchId");
            });

            modelBuilder.Entity<BatchBoxOrderMap>(entity =>
            {
                entity.HasKey(e => new { e.BatchBoxId, e.OrderId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("batch_box_order_map");

                entity.HasComment("批次箱与订单");

                entity.HasIndex(e => e.BatchBoxId, "IX_dbo.BatchBoxOrderMap_BatchId");

                entity.HasIndex(e => e.OrderId, "IX_dbo.BatchBoxOrderMap_OrderId");

                entity.Property(e => e.BatchBoxId)
                    .HasColumnType("int(11)")
                    .HasComment("批次箱id");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("订单id");

                entity.HasOne(d => d.BatchBox)
                    .WithMany(p => p.BatchBoxOrderMaps)
                    .HasForeignKey(d => d.BatchBoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchBoxOrderMap_dbo.Batch_BatchId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.BatchBoxOrderMaps)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchBoxOrderMap_dbo.Order_OrderId");
            });

            modelBuilder.Entity<BatchOrderMap>(entity =>
            {
                entity.HasKey(e => new { e.BatchId, e.OrderId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("batch_order_map");

                entity.HasComment("批次订单");

                entity.HasIndex(e => e.BatchId, "IX_dbo.BatchOrderMap_BatchId");

                entity.HasIndex(e => e.OrderId, "IX_dbo.BatchOrderMap_OrderId");

                entity.Property(e => e.BatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次id");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("订单id");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchOrderMaps)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchOrderMap_dbo.Batch_BatchId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.BatchOrderMaps)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchOrderMap_dbo.Order_OrderId");
            });

            modelBuilder.Entity<BatchOtherOrder>(entity =>
            {
                entity.HasKey(e => new { e.BatchId, e.OtherOrder })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("batch_other_order");

                entity.Property(e => e.BatchId).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime")
                    .HasComment("日期");

                entity.Property(e => e.OtherOrder).HasMaxLength(200);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.BatchOtherOrders)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchOtherOrder_dbo.Batch_BatchId");

                entity.HasOne(d => d.Creator)
                    .WithMany(c => c.BatchOtherOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.BatchOtherOrder_dbo.User_UserId");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.HasComment("录单商品");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasComment("id");

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(100)
                    .HasComment("英文名");

                entity.Property(e => e.HsCode)
                    .HasMaxLength(100)
                    .HasComment("编号");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasComment("是否删除");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasComment("类型");
            });

            modelBuilder.Entity<ChatFriend>(entity =>
            {
                entity.ToTable("chat_friend");

                entity.HasComment("聊天朋友")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.FriendId, "index_friend_id");

                entity.HasIndex(e => e.UserId, "index_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("Id");

                entity.Property(e => e.FriendId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("friend_id")
                    .HasComment("朋友Id");

                entity.Property(e => e.FriendShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("friend_shop_id")
                    .HasComment("朋友店铺Id");

                entity.Property(e => e.FriendType)
                    .HasColumnName("friend_type")
                    .HasComment("朋友类型：0->app用户，1->客服");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("用户Id");

                entity.Property(e => e.UserShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_shop_id")
                    .HasComment("用户店铺Id");

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasComment("用户类型：0->app用户，1->客服");
            });

            modelBuilder.Entity<ChatLog>(entity =>
            {
                entity.ToTable("chat_log");

                entity.HasComment("聊天记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.FromTo, "index_from_to");

                entity.HasIndex(e => e.MsgType, "index_msg_type");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("Id");

                entity.Property(e => e.ChatFrom)
                    .HasMaxLength(20)
                    .HasColumnName("chat_from")
                    .HasComment("来源");

                entity.Property(e => e.ChatId)
                    .HasMaxLength(32)
                    .HasColumnName("chat_id")
                    .HasComment("聊天id");

                entity.Property(e => e.ChatTo)
                    .HasMaxLength(20)
                    .HasColumnName("chat_to")
                    .HasComment("目标");

                entity.Property(e => e.ChatType)
                    .HasColumnName("chat_type")
                    .HasComment("聊天类型(0:未知,1:公聊,2:私聊)");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content")
                    .HasComment("聊天记录");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.Extras)
                    .HasMaxLength(255)
                    .HasColumnName("extras")
                    .HasComment("扩展字段");

                entity.Property(e => e.FromAvatar)
                    .HasMaxLength(255)
                    .HasColumnName("from_avatar")
                    .HasComment("来源头像");

                entity.Property(e => e.FromTo)
                    .HasMaxLength(50)
                    .HasColumnName("from_to")
                    .HasComment("来源-目标");

                entity.Property(e => e.IsRead)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_read")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否已读");

                entity.Property(e => e.MsgType)
                    .HasColumnName("msg_type")
                    .HasComment("消息类型(0:text、1:image、2:voice、3:vedio、4:music、5:news)");

                entity.Property(e => e.ToAvatar)
                    .HasMaxLength(255)
                    .HasColumnName("to_avatar")
                    .HasComment("目标头像");
            });

            modelBuilder.Entity<ChatUser>(entity =>
            {
                entity.ToTable("chat_user");

                entity.HasComment("客服人员")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Name, "index_name");

                entity.HasIndex(e => e.ShopId, "index_shop_id");

                entity.HasIndex(e => e.Type, "index_type");

                entity.HasIndex(e => e.UserId, "index_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("Id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .HasComment("客服头像");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasComment("客服名称");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺Id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("客服类型：0->平台，1->店铺");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("用户Id(客服对应管理员)");
            });

            modelBuilder.Entity<ChinaItem>(entity =>
            {
                entity.ToTable("china_item");

                entity.HasComment("中国商品（中国到加拿大商品）");

                entity.HasIndex(e => e.OrderId, "IX_dbo.ChinaItem_OrderId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .HasComment("品牌");

                entity.Property(e => e.Category)
                    .HasMaxLength(200)
                    .HasComment("种类");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasComment("种类id（对应category表）");

                entity.Property(e => e.ChineseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("中文名");

                entity.Property(e => e.ClaimPrice)
                    .HasPrecision(19, 4)
                    .HasComment("申报价格");

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(100)
                    .HasComment("英文名");

                entity.Property(e => e.Height)
                    .HasPrecision(18)
                    .HasComment("高");

                entity.Property(e => e.Length)
                    .HasPrecision(18)
                    .HasComment("长");

                entity.Property(e => e.Material)
                    .HasMaxLength(100)
                    .HasComment("材质");

                entity.Property(e => e.OrderBaggageId)
                    .HasColumnType("int(11)")
                    .HasComment("运单包裹id（对应OrderBaggage表）");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id（对应order表）");

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(200)
                    .HasComment("图片url");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasComment("数量");

                entity.Property(e => e.WeightPound)
                    .HasPrecision(18)
                    .HasComment("重量磅");

                entity.Property(e => e.Width)
                    .HasPrecision(18)
                    .HasComment("宽");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ChinaItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.ChinaItem_dbo.Order_OrderId");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasIndex(e => e.BelongsToUserId, "IX_dbo.Customer_BelongsToUserId");

                entity.HasIndex(e => e.IdCardId, "IX_dbo.Customer_IdCardId");

                entity.HasIndex(e => new { e.Name, e.PhoneNumber, e.BelongsToUserId }, "IX_dbo.Customer_Name_PhoneNumber_BelongsToUserId")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "Id");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Address).HasComment("详细地址");

                entity.Property(e => e.BelongsToUserId)
                    .HasColumnType("int(11)")
                    .HasComment("所属用户（对应user表）");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .HasComment("市");

                entity.Property(e => e.District)
                    .HasMaxLength(20)
                    .HasComment("区");

                entity.Property(e => e.IdCardBackUrl).HasComment("身份证反面照片链接（淘汰）");

                entity.Property(e => e.IdCardFrontUrl).HasComment("身份证正面照片连接（淘汰）");

                entity.Property(e => e.IdCardId)
                    .HasColumnType("int(11)")
                    .HasComment("身份证id");

                entity.Property(e => e.IdCardNumber)
                    .HasMaxLength(50)
                    .HasComment("身份证号（淘汰）");

                entity.Property(e => e.IntegrationId)
                    .HasMaxLength(200)
                    .HasComment("第三方系统id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasComment("姓名");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("手机号");

                entity.Property(e => e.PhotosExported).HasColumnType("tinyint(4)");

                entity.Property(e => e.Province)
                    .HasMaxLength(20)
                    .HasComment("省");

                entity.HasOne(d => d.BelongsToUser)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BelongsToUserId)
                    .HasConstraintName("FK_dbo.Customer_dbo.User_BelongsToUserId");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasComment("公司");

                entity.HasIndex(e => e.Id, "Id");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("公司名");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("公司代码");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("coupon");

                entity.HasComment("运单优惠券");

                entity.HasIndex(e => e.CouponNumber, "unique_idx_coupon_number")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "Id");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnType("int(11)");

                entity.Property(e => e.ShippingCost)
                    .HasPrecision(19, 4)
                    .HasComment("折扣金额");

                entity.Property(e => e.CouponNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("折扣单号");

                entity.Property(e => e.DomesticNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("折扣单国内单号");

                entity.Property(e => e.CreatedById)
                    .IsRequired()
                    .HasColumnType("int(11)")
                    .HasComment("创建人");

                entity.Property(e => e.CreateTime)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasComment("创建日期");

                entity.Property(e => e.CouponBatchId)
                    .IsRequired()
                    .HasColumnType("int(11)")
                    .HasComment("属于的批次号");

                entity.Property(e => e.AssignedUserId)
                    .IsRequired(false)
                    .HasColumnType("int(11)")
                    .HasComment("分配的用户ID");

                entity.Property(e => e.MinimumPrice)
                    .HasColumnName("MinimumPrice")
                    .HasPrecision(19, 4)
                    .HasComment("最低消费额");

                entity.Property(e => e.CouponType)
                    .HasColumnName("CouponType")
                    .HasColumnType("tinyint(4)")
                    .HasComment("0代表未设定，1代表不记名优惠券类型，2代表记名优惠券类型");

                entity.Property(e => e.ConsumedUserId)
                    .IsRequired(false)
                    .HasColumnType("int(11)")
                    .HasComment("使用的用户ID");

                entity
                    .HasOne(e => e.CreatedBy)
                    .WithMany(c => c.Coupons)
                    .HasForeignKey(c => c.CreatedById)
                    .HasConstraintName("fk_coupon_created_by_id");

                entity
                    .HasOne(e => e.CouponBatch)
                    .WithMany(b => b.Coupons)
                    .HasForeignKey(c => c.CouponBatchId)
                    .HasConstraintName("fk_coupon_coupon_batch_id");
                    
                entity
                    .HasOne(e => e.AssignedUser)
                    .WithMany(c => c.CouponAssignedUsers)
                    .HasForeignKey(c => c.AssignedUserId)
                    .HasConstraintName("fk_coupon_assigned_user_id");

                entity
                    .HasOne(e => e.ConsumedUser)
                    .WithMany(c => c.CouponConsumedUsers)
                    .HasForeignKey(c => c.ConsumedUserId)
                    .HasConstraintName("fk_coupon_consumed_user_id");
            });

            modelBuilder.Entity<CouponBatch>(entity =>
            {
                entity.ToTable("coupon_batch");

                entity.HasComment("优惠券批次");

                entity.HasIndex(e => e.Id, "Id");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasColumnName("Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasMaxLength(50)
                    .HasComment("批次名");

                entity.Property(e => e.CreatedById)
                    .IsRequired()
                    .HasColumnName("CreatedById")
                    .HasColumnType("int(11)")
                    .HasComment("创建人");

                entity.Property(e => e.CreateTime)
                    .IsRequired()
                    .HasColumnName("CreateTime")
                    .HasColumnType("datetime")
                    .HasComment("创建日期");

                entity.Property(e => e.Anonymous)
                    .IsRequired(false)
                    .HasColumnName("Anonymous")
                    .HasColumnType("bit")
                    .HasComment("1-不记名优惠券; 0-记名优惠券; NULL-未设定");

                entity.Property(e => e.PhotoUrl)
                    .IsRequired(false)
                    .HasColumnName("PhotoUrl")
                    .HasColumnType("LongText")
                    .HasComment("模板照片链接");

                entity.Property(e => e.EmailContent)
                    .IsRequired(false)
                    .HasColumnName("EmailContent")
                    .HasColumnType("LongText")
                    .HasComment("邮件内容(包含换行符)");

                entity.Property(e => e.SmsContent)
                    .IsRequired(false)
                    .HasColumnName("SmsContent")
                    .HasColumnType("LongText")
                    .HasComment("短信内容(包含换行符)");

                entity
                    .HasOne(e => e.CreatedBy)
                    .WithMany(c => c.CouponBatches)
                    .HasForeignKey(c => c.CreatedById)
                    .HasConstraintName("fk_coupon_batch_created_by_id");
            });

            
            modelBuilder.Entity<CouponStatus>(entity =>
            {
                entity.ToTable("coupon_status");

                entity.HasComment("优惠券状态");

                entity.HasIndex(e => e.CouponId, "IX_dbo.CouponStatus_CouponId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasComment("日期");

                entity.Property(e => e.CouponId)
                    .HasColumnType("int(11)")
                    .HasComment("优惠券id(对应coupon表)");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasComment("状态(1-已创建,10-已打印,11-已寄送,21-已)");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("10运单已创建\r\n13,请联系国内快递公司/国内仓库负责人\r\n14,等待核对包裹状态\r\n15,录单晚请联系仓库负责人确认仓库是否收货\r\n16,货物已被{0}接收\r\n17,货物已发往下一站\r\n18,货物已发往货站\r\n20,已打包封装等待发出\r\n21,货物已入库\r\n22,包惠已封装准备发出\r\n23,包裹需要付款\r\n24,包裹建立等待称重\r\n25,包衷已进入邮政运输阶段（请在系统内查看单号)\r\n30,收件人信息缺失/错误货物进入待发区\r\n31,航班延误\r\n32,单号信息有误/运单状态还未更新请更新单号避免仓库无法收货\r\n40,重名件待发货\r\n41,移出迸入待发区\r\n42,包裹移出待发区\r\n43,包裹已入库（请核对包裹数量）\r\n44,包裹已退回\r\n50,货物已退回给客户\r\n60,包裹已打包\r\n61,货物已发往机场\r\n62,货物已接收等待打包封装\r\n63,包裹已发往各取货点\r\n64,包裹已封装\r\n65,货物已起航（请联系所在群群主充值）\r\n66,包裹到达多伦多\r\n67,客户已付款\r\n68,货物已确认\r\n69,货物已二次确认\r\n72,货物已三次确认\r\n70,货物已飞往中国\r\n71,货物开始国内段运输\r\n80,货物已抵达海关等待清关\r\n81,货物到达船运公司仓库\r\n90,货物开始国内派送\r\n91,货物开始国际段运检\r\n95,要求送货\r\n92,包裹已发出\r\n100,货物已被海关退回\r\n101,货物已到达加拿大清关中\r\n102,货物已到达多佗多仓库\r\n700,正在派送\r\n1000,已签收\r\n1100,客户已取货\r\n2000,已确认");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponStatuses)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coupon_status_coupon_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CouponStatuses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_coupon_status_user_id");
            });

            modelBuilder.Entity<DeliverProgress>(entity =>
            {
                entity.ToTable("deliver_progress");

                entity.HasComment("交货进度");

                entity.HasIndex(e => e.RouteId, "IX_dbo.DeliverProgress_RouteId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasComment("描述");

                entity.Property(e => e.Hide)
                    .HasColumnType("tinyint(4)")
                    .HasComment("是否隐藏");

                entity.Property(e => e.IsMain)
                    .HasColumnType("tinyint(4)")
                    .HasComment("是否主单");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("名称");

                entity.Property(e => e.Percent)
                    .HasPrecision(6, 3)
                    .HasComment("百分比");

                entity.Property(e => e.RouteId)
                    .HasColumnType("int(11)")
                    .HasComment("线路id");

                entity.Property(e => e.Sequence)
                    .HasColumnType("int(11)")
                    .HasComment("排序");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.DeliverProgresses)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.DeliverProgresse_dbo.Route_RouteId");
            });

            modelBuilder.Entity<Dict>(entity =>
            {
                entity.ToTable("dict");

                entity.HasComment("数据字典")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("创建日期");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("字典名称");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .HasColumnName("remark")
                    .HasComment("描述");
            });

            modelBuilder.Entity<DictDetail>(entity =>
            {
                entity.ToTable("dict_detail");

                entity.HasComment("数据字典详情")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.DictId, "FK5tpkputc6d9nboxojdbgnpmyb");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("创建日期");

                entity.Property(e => e.DictId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("dict_id")
                    .HasComment("字典id");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("label")
                    .HasComment("字典标签");

                entity.Property(e => e.Sort)
                    .HasMaxLength(255)
                    .HasColumnName("sort")
                    .HasComment("排序");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("value")
                    .HasComment("字典值");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.HasIndex(e => e.CreatedById, "IX_dbo.Document_CreatedById");

                entity.HasIndex(e => e.ModifiedById, "IX_dbo.Document_ModifiedById");

                entity.HasIndex(e => e.VisibleUserId, "IX_dbo.Document_VisibleUserId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedById).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.ModifiedById).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PinToTop).HasColumnType("tinyint(4)");

                entity.Property(e => e.Url).IsRequired();

                entity.Property(e => e.VisibleUserId).HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.DocumentCreatedBies)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Document_dbo.User_CreatedById");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.DocumentModifiedBies)
                    .HasForeignKey(d => d.ModifiedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Document_dbo.User_ModifiedById");

                entity.HasOne(d => d.VisibleUser)
                    .WithMany(p => p.DocumentVisibleUsers)
                    .HasForeignKey(d => d.VisibleUserId)
                    .HasConstraintName("FK_dbo.Document_dbo.User_VisibleUserId");
            });

            modelBuilder.Entity<DocumentComment>(entity =>
            {
                entity.ToTable("document_comment");

                entity.HasIndex(e => e.CreatedById, "IX_dbo.DocumentComment_CreatedById");

                entity.HasIndex(e => e.DocumentId, "IX_dbo.DocumentComment_DocumentId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment).IsRequired();

                entity.Property(e => e.CreatedById).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DocumentId).HasColumnType("int(11)");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.DocumentComments)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.DocumentComment_dbo.User_CreatedById");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentComments)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.DocumentComment_dbo.Comment_DocumentId");
            });

            modelBuilder.Entity<EmailData>(entity =>
            {
                entity.ToTable("email_data");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.OrderId).HasColumnType("int(11)");

                entity.Property(e => e.RecipientUserId).HasColumnType("int(11)");
                
                entity.Property(e => e.DateCreated).IsRequired(true).HasColumnType("datetime");

                entity.Property(e => e.DateSent).IsRequired(false).HasColumnType("datetime");

                entity.Property(e => e.DateSentSms).IsRequired(false).HasColumnType("datetime");

                entity.Property(e => e.BatchId).IsRequired(false).HasColumnType("int(11)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.EmailDatas)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("fk_email_data_order_id_order_id");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.EmailDataRecipients)
                    .HasForeignKey(d => d.RecipientUserId)
                    .HasConstraintName("fk_email_data_recipient_user_id_user_id");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.EmailDataSenders)
                    .HasForeignKey(d => d.SenderUserId)
                    .HasConstraintName("fk_email_data_sender_user_id_user_id");

                entity.HasOne(d => d.Batch)
                    .WithMany(b => b.EmailDatas)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("fk_email_data_batch_id_batch_id");
            });

            modelBuilder.Entity<EmailDataInWarehouse>(entity =>
            {
                entity.ToTable("email_data_in_warehouse");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.OrderId).HasColumnType("int(11)");

                entity.Property(e => e.RecipientUserId).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).IsRequired(true).HasColumnType("datetime");

                entity.Property(e => e.DateSentEmail).IsRequired(false).HasColumnType("datetime");
            });

            modelBuilder.Entity<ExpressCompany>(entity =>
            {
                entity.ToTable("express_company");

                entity.HasComment("快递公司")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Code, "index_code")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "index_name");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("code")
                    .HasComment("公司编码");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.IsShow)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_show")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否显示");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("公司名称");

                entity.Property(e => e.OrderNum)
                    .HasColumnType("int(11)")
                    .HasColumnName("order_num")
                    .HasDefaultValueSql("'999'")
                    .HasComment("序号");
            });

            modelBuilder.Entity<ExpressConfig>(entity =>
            {
                entity.ToTable("express_config");

                entity.HasComment("快递配置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id，这里为自增类型");

                entity.Property(e => e.ChargeCustomer)
                    .HasMaxLength(255)
                    .HasColumnName("charge_customer")
                    .HasComment("收费公司编号(快递100企业后台查看)");

                entity.Property(e => e.ChargeKey)
                    .HasColumnName("charge_key")
                    .HasComment("收费key(快递100企业后台查看)");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.ExpressType)
                    .HasColumnType("int(11)")
                    .HasColumnName("express_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment(" 快递100类型，0为免费版快递查询，1为收费版快递查询，默认为免费版快递查询");

                entity.Property(e => e.FreeCustomer)
                    .HasMaxLength(255)
                    .HasColumnName("free_customer")
                    .HasComment("免费公司编号(快递100企业后台查看)");

                entity.Property(e => e.FreeKey)
                    .HasColumnName("free_key")
                    .HasComment("免费key(快递100企业后台查看)");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");
            });

            modelBuilder.Entity<ExpressTransArea>(entity =>
            {
                entity.ToTable("express_trans_area");

                entity.HasComment("运费区域")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.ParentId, "parent_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(255)
                    .HasColumnName("areaName")
                    .HasComment(" 区域名称");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.Level)
                    .HasColumnType("int(11)")
                    .HasColumnName("level")
                    .HasComment(" 层级");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("parent_id")
                    .HasComment(" 上级区域");

                entity.Property(e => e.Sequence)
                    .HasColumnType("int(11)")
                    .HasColumnName("sequence")
                    .HasComment(" 序号");
            });

            modelBuilder.Entity<ExpressTransport>(entity =>
            {
                entity.ToTable("express_transport");

                entity.HasComment("运费模板")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.StoreId, "store_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.FreePostage)
                    .HasPrecision(19, 2)
                    .HasColumnName("free_postage");

                entity.Property(e => e.FreePostageStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("free_postage_status")
                    .HasDefaultValueSql("'0'")
                    .HasComment("0否 1是");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.StoreId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("store_id");

                entity.Property(e => e.TransEms)
                    .HasColumnType("bit(1)")
                    .HasColumnName("trans_ems")
                    .HasComment(" EMS信息,使用json管理[{\"city_id\":-1,\"city_name\":\"全国\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weight\":1,\"trans_add_fee\":2},{\"city_id\":1,\"city_name\":\"沈阳\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weig");

                entity.Property(e => e.TransEmsInfo)
                    .HasColumnName("trans_ems_info")
                    .HasComment(" EMS信息,使用json管理[{\"city_id\":-1,\"city_name\":\"全国\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weight\":1,\"trans_add_fee\":2},{\"city_id\":1,\"city_name\":\"沈阳\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weig");

                entity.Property(e => e.TransExpress)
                    .HasColumnType("bit(1)")
                    .HasColumnName("trans_express")
                    .HasComment(" 快递信息,使用json管理[{\"city_id\":-1,\"city_name\":\"全国\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weight\":1,\"trans_add_fee\":2},{\"city_id\":1,\"city_name\":\"沈阳\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weigh");

                entity.Property(e => e.TransExpressInfo)
                    .HasColumnName("trans_express_info")
                    .HasComment(" 快递信息,使用json管理[{\"city_id\":-1,\"city_name\":\"全国\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weight\":1,\"trans_add_fee\":2},{\"city_id\":1,\"city_name\":\"沈阳\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weigh");

                entity.Property(e => e.TransMail)
                    .HasColumnType("bit(1)")
                    .HasColumnName("trans_mail")
                    .HasComment(" 平邮信息,使用json管理[{\"city_id\":-1,\"city_name\":\"全国\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weight\":1,\"trans_add_fee\":2},{\"city_id\":1,\"city_name\":\"沈阳\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weigh");

                entity.Property(e => e.TransMailInfo)
                    .HasColumnName("trans_mail_info")
                    .HasComment(" 平邮信息,使用json管理[{\"city_id\":-1,\"city_name\":\"全国\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weight\":1,\"trans_add_fee\":2},{\"city_id\":1,\"city_name\":\"沈阳\",\"trans_weight\":1,\"trans_fee\":13.5,\"trans_add_weigh");

                entity.Property(e => e.TransName)
                    .HasMaxLength(255)
                    .HasColumnName("trans_name")
                    .HasComment(" 运费模板名称");

                entity.Property(e => e.TransTime)
                    .HasColumnType("int(11)")
                    .HasColumnName("trans_time")
                    .HasDefaultValueSql("'0'")
                    .HasComment(" 发货时间");

                entity.Property(e => e.TransType)
                    .HasColumnType("int(11)")
                    .HasColumnName("trans_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment(" 0按件数，1按重量，2按体积");

                entity.Property(e => e.TransUser)
                    .HasColumnType("int(11)")
                    .HasColumnName("trans_user")
                    .HasDefaultValueSql("'0'")
                    .HasComment(" 运费模板类型，0为自营模板，1为商家模板");
            });

            modelBuilder.Entity<GoodsBrand>(entity =>
            {
                entity.ToTable("goods_brand");

                entity.HasComment("商品品牌")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.BrandDisplay)
                    .HasColumnType("bit(1)")
                    .HasColumnName("brand_display")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否显示:0,否,1,是");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(255)
                    .HasColumnName("brand_name")
                    .HasComment(" 品牌名称");

                entity.Property(e => e.BrandPictureKey)
                    .HasMaxLength(255)
                    .HasColumnName("brand_picture_key")
                    .HasComment(" 品牌logo");

                entity.Property(e => e.BrandRecommend)
                    .HasColumnType("bit(1)")
                    .HasColumnName("brand_recommend")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否推荐:0,否,1,是");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.FirstWord)
                    .HasMaxLength(255)
                    .HasColumnName("first_word")
                    .HasComment(" 品牌首字母，后台管理添加");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");
            });

            modelBuilder.Entity<GoodsCartShop>(entity =>
            {
                entity.ToTable("goods_cart_shop");

                entity.HasComment("购物车店铺信息")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(10)")
                    .HasColumnName("client_id")
                    .HasComment("客户ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(255)
                    .HasColumnName("shop_name")
                    .HasComment("店铺名称");
            });

            modelBuilder.Entity<GoodsCategory>(entity =>
            {
                entity.ToTable("goods_category");

                entity.HasComment("商品分类")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.ParentId, "parent_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CategoryDescribe)
                    .HasMaxLength(255)
                    .HasColumnName("category_describe")
                    .HasComment("分类描述");

                entity.Property(e => e.CategoryDisplay)
                    .HasColumnType("bit(1)")
                    .HasColumnName("category_display")
                    .HasDefaultValueSql("b'1'")
                    .HasComment("是否显示:0:否,1:是");

                entity.Property(e => e.CategoryLevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("category_level")
                    .HasComment("等级:1,一级分类,2,二级分类,3,三级分类");

                entity.Property(e => e.CategoryType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("category_type")
                    .HasDefaultValueSql("'0'")
                    .HasComment("分类类型:1,平台,2,店铺");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .HasColumnName("class_name")
                    .HasComment("分类名称");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IconKey)
                    .HasMaxLength(255)
                    .HasColumnName("icon_key")
                    .HasComment("上传图标");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("parent_id")
                    .HasComment("父id");

                entity.Property(e => e.SerialNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("serial_number")
                    .HasComment("排序序号");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");
            });

            modelBuilder.Entity<GoodsDetail>(entity =>
            {
                entity.ToTable("goods_details");

                entity.HasComment("商品详情\r\n")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.AllSales)
                    .HasColumnType("int(11)")
                    .HasColumnName("all_sales")
                    .HasDefaultValueSql("'0'")
                    .HasComment("总销量(展示销量+实际销量)");

                entity.Property(e => e.BrandId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("brand_id")
                    .HasComment("品牌id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.DeliveryMode)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("delivery_mode")
                    .HasDefaultValueSql("'0'")
                    .HasComment("配送方式:1,快递,2,同城物流");

                entity.Property(e => e.DistributionExplain)
                    .HasMaxLength(255)
                    .HasColumnName("distribution_explain")
                    .HasComment("配送说明");

                entity.Property(e => e.DistributionPhone)
                    .HasMaxLength(255)
                    .HasColumnName("distribution_phone")
                    .HasComment("配送电话");

                entity.Property(e => e.FixedFreight)
                    .HasPrecision(18, 2)
                    .HasColumnName("fixed_freight")
                    .HasComment("固定运费");

                entity.Property(e => e.FreightBear)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("freight_bear")
                    .HasDefaultValueSql("'0'")
                    .HasComment("承担运费:1商家承担运费（免运费）,2,买家承担");

                entity.Property(e => e.FreightMode)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("freight_mode")
                    .HasDefaultValueSql("'0'")
                    .HasComment("运费方式:1,使用运费模板,2,固定运费");

                entity.Property(e => e.GoodsAdvertisement)
                    .HasMaxLength(255)
                    .HasColumnName("goods_advertisement")
                    .HasComment("商品广告语");

                entity.Property(e => e.GoodsCategoryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_category_id")
                    .HasComment("平台商品分类id");

                entity.Property(e => e.GoodsDetails)
                    .HasColumnName("goods_details")
                    .HasComment("商品详情");

                entity.Property(e => e.GoodsName)
                    .HasMaxLength(255)
                    .HasColumnName("goods_name")
                    .HasComment("商品名称");

                entity.Property(e => e.GoodsPicture)
                    .HasMaxLength(255)
                    .HasColumnName("goods_picture")
                    .HasComment("商品图片");

                entity.Property(e => e.GoodsPrice)
                    .HasPrecision(18, 2)
                    .HasColumnName("goods_price")
                    .HasComment("商品价格");

                entity.Property(e => e.GoodsSort)
                    .HasColumnType("int(11)")
                    .HasColumnName("goods_sort")
                    .HasComment("排序");

                entity.Property(e => e.GoodsStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("goods_status")
                    .HasDefaultValueSql("'0'")
                    .HasComment("商品发布后状态:\r\n-2.违规下架\r\n-1,审核不通过\r\n0,待审核,\r\n1,未上架(仓库中)\r\n2,上架\r\n3,售罄");

                entity.Property(e => e.GoodsStock)
                    .HasColumnType("int(11)")
                    .HasColumnName("goods_stock")
                    .HasComment("商品库存");

                entity.Property(e => e.GoodsType)
                    .HasColumnName("goods_type")
                    .HasComment("商品类型:0->普通，1->vip，2->普惠，3->扶贫");

                entity.Property(e => e.GoodsVolume)
                    .HasPrecision(18, 2)
                    .HasColumnName("goods_volume")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("商品体积");

                entity.Property(e => e.GoodsWarning)
                    .HasColumnType("int(11)")
                    .HasColumnName("goods_warning")
                    .HasComment("库存预警值");

                entity.Property(e => e.GoodsWeight)
                    .HasPrecision(18, 2)
                    .HasColumnName("goods_weight")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("商品重量");

                entity.Property(e => e.HighPraiseRate)
                    .HasPrecision(18, 2)
                    .HasColumnName("high_praise_rate")
                    .HasComment("好评率");

                entity.Property(e => e.Integral)
                    .HasPrecision(18, 2)
                    .HasColumnName("integral")
                    .HasComment("积分");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.IsNew)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_new")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否新品(店铺):0,否,1,是");

                entity.Property(e => e.IsRecommend)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_recommend")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否推荐:0,否,1,是");

                entity.Property(e => e.IsShelf)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_shelf")
                    .HasComment("是否上架:0,否,1,是");

                entity.Property(e => e.IsSupportInvoice)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_support_invoice")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否支持开发票：0->否，1->是");

                entity.Property(e => e.IsWarning)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_warning")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否预警:0,否,1,是");

                entity.Property(e => e.MarketPrice)
                    .HasPrecision(18, 2)
                    .HasColumnName("market_price")
                    .HasComment("市场价格");

                entity.Property(e => e.MeasurementUnit)
                    .HasMaxLength(255)
                    .HasColumnName("measurement_unit")
                    .HasComment("计量单位");

                entity.Property(e => e.OtherBrand)
                    .HasMaxLength(255)
                    .HasColumnName("other_brand")
                    .HasComment("其他品牌");

                entity.Property(e => e.PlatformRecommend)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("platform_recommend")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否推荐(平台):0,否,1,是");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(255)
                    .HasColumnName("product_code")
                    .HasComment("商品货号");

                entity.Property(e => e.SalesVolume)
                    .HasColumnType("int(11)")
                    .HasColumnName("sales_volume")
                    .HasDefaultValueSql("'0'")
                    .HasComment("实际销量");

                entity.Property(e => e.SeoDescription)
                    .HasColumnName("seo_description")
                    .HasComment(" seo描述");

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(255)
                    .HasColumnName("seo_keywords")
                    .HasComment(" seo关键字");

                entity.Property(e => e.ShopCategoryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_category_id")
                    .HasComment("店铺商品分类id");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(255)
                    .HasColumnName("shop_name")
                    .HasComment("店铺名称");

                entity.Property(e => e.ShowSales)
                    .HasColumnType("int(11)")
                    .HasColumnName("show_sales")
                    .HasDefaultValueSql("'0'")
                    .HasComment("展示销量");

                entity.Property(e => e.SpecificationsType)
                    .HasColumnName("specifications_type")
                    .HasDefaultValueSql("'1'")
                    .HasComment("规格类型:1->单规格,2->多规格");

                entity.Property(e => e.SupplierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("supplier_id")
                    .HasComment("供应商id");

                entity.Property(e => e.TotalCommentLevel)
                    .HasColumnType("int(10)")
                    .HasColumnName("total_comment_level")
                    .HasDefaultValueSql("'0'")
                    .HasComment("总星级数");

                entity.Property(e => e.TransportId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("transport_id")
                    .HasComment("运费模板id");

                entity.Property(e => e.VideoPicture)
                    .HasMaxLength(255)
                    .HasColumnName("video_picture")
                    .HasComment("视频图片");

                entity.Property(e => e.Videos)
                    .HasMaxLength(255)
                    .HasColumnName("videos")
                    .HasComment("视频");
            });

            modelBuilder.Entity<GoodsExamine>(entity =>
            {
                entity.ToTable("goods_examine");

                entity.HasComment("商品审核")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.ExamineDetails)
                    .HasMaxLength(255)
                    .HasColumnName("examine_details")
                    .HasComment(" 反馈详情");

                entity.Property(e => e.ExaminePersonnel)
                    .HasMaxLength(255)
                    .HasColumnName("examine_personnel")
                    .HasComment("审核人员");

                entity.Property(e => e.ExamineResult)
                    .HasColumnType("bit(1)")
                    .HasColumnName("examine_result")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否审核成功:0,不成功,1,成功");

                entity.Property(e => e.ExamineTime)
                    .HasColumnType("datetime")
                    .HasColumnName("examine_time")
                    .HasComment("审核时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.GoodsName)
                    .HasMaxLength(255)
                    .HasColumnName("goods_name")
                    .HasComment("商品名称");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");
            });

            modelBuilder.Entity<GoodsOrder>(entity =>
            {
                entity.ToTable("goods_order");

                entity.HasComment("商品总订单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.ClientId, "index_client_id");

                entity.HasIndex(e => e.ClientPhone, "index_client_phone");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.OrderNumber, "index_order_number")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id")
                    .HasComment("总订单id");

                entity.Property(e => e.ClientAvatar)
                    .HasMaxLength(200)
                    .HasColumnName("client_avatar")
                    .HasDefaultValueSql("''")
                    .HasComment("客户头像");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("client_id")
                    .HasComment("客户id");

                entity.Property(e => e.ClientIsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("client_is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("客户是否删除订单");

                entity.Property(e => e.ClientNickName)
                    .HasMaxLength(50)
                    .HasColumnName("client_nick_name")
                    .HasComment("客户昵称");

                entity.Property(e => e.ClientPhone)
                    .HasMaxLength(20)
                    .HasColumnName("client_phone")
                    .HasDefaultValueSql("''")
                    .HasComment("客户手机");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.InvoBankAccount)
                    .HasMaxLength(100)
                    .HasColumnName("invo_bank_account")
                    .HasComment("发票开户银行账户");

                entity.Property(e => e.InvoBankName)
                    .HasMaxLength(100)
                    .HasColumnName("invo_bank_name")
                    .HasComment("发票开户行");

                entity.Property(e => e.InvoGeneralType)
                    .HasColumnName("invo_general_type")
                    .HasComment("普通发票类型：0->个人，1->单位");

                entity.Property(e => e.InvoIsOpen)
                    .HasColumnType("bit(1)")
                    .HasColumnName("invo_is_open")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("发票是否开票");

                entity.Property(e => e.InvoName)
                    .HasMaxLength(100)
                    .HasColumnName("invo_name")
                    .HasComment("发票个人/单位名称");

                entity.Property(e => e.InvoRegisterAddress)
                    .HasMaxLength(100)
                    .HasColumnName("invo_register_address")
                    .HasComment("发票注册地址");

                entity.Property(e => e.InvoRegisterPhone)
                    .HasMaxLength(100)
                    .HasColumnName("invo_register_phone")
                    .HasComment("发票注册电话");

                entity.Property(e => e.InvoTaxpayersNum)
                    .HasMaxLength(100)
                    .HasColumnName("invo_taxpayers_num")
                    .HasComment("发票纳税人识别号");

                entity.Property(e => e.InvoType)
                    .HasColumnName("invo_type")
                    .HasComment("发票类型：0->普通发票，1->增值税专用发票，2->无需发票");

                entity.Property(e => e.OrderGoodsType)
                    .HasColumnName("order_goods_type")
                    .HasComment("商品类型：0->大众商品，1->vip商品");

                entity.Property(e => e.OrderIntegralTotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_integral_total")
                    .HasComment("订单总积分");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("order_number")
                    .HasComment("总订单号");

                entity.Property(e => e.OrderPayTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_pay_time")
                    .HasComment("支付时间");

                entity.Property(e => e.OrderPayType)
                    .HasColumnName("order_pay_type")
                    .HasComment("支付类型：0->支付宝，1->微信，2->预存款支付");

                entity.Property(e => e.OrderRemark)
                    .HasColumnName("order_remark")
                    .HasComment("订单备注");

                entity.Property(e => e.OrderTotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_total")
                    .HasComment("订单总金额");
            });

            modelBuilder.Entity<GoodsOrderChild>(entity =>
            {
                entity.ToTable("goods_order_children");

                entity.HasComment("商品订单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.GoodsId, "index_goods_id");

                entity.HasIndex(e => e.GoodsName, "index_goods_name");

                entity.HasIndex(e => e.GoodsOrderNumber, "index_order_number")
                    .IsUnique();

                entity.HasIndex(e => e.ShopOrderId, "index_shop_order_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("商品订单id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.GoodsName)
                    .IsRequired()
                    .HasColumnName("goods_name")
                    .HasComment("商品名称");

                entity.Property(e => e.GoodsNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("goods_number")
                    .HasComment("商品数量");

                entity.Property(e => e.GoodsOrderNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("goods_order_number")
                    .HasComment("商品订单号");

                entity.Property(e => e.GoodsPicture)
                    .HasMaxLength(255)
                    .HasColumnName("goods_picture")
                    .HasComment("商品图片");

                entity.Property(e => e.GoodsPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("goods_price")
                    .HasComment("商品价格");

                entity.Property(e => e.GoodsType)
                    .HasColumnName("goods_type")
                    .HasComment("商品类型:0->普通，1->vip，2->普惠，3->扶贫");

                entity.Property(e => e.OrderActualPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_actual_price")
                    .HasComment("订单实付金额");

                entity.Property(e => e.OrderIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_integral")
                    .HasComment("订单积分");

                entity.Property(e => e.OrderPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_price")
                    .HasComment("订单金额");

                entity.Property(e => e.ShopOrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_order_id")
                    .HasComment("店铺订单id");

                entity.Property(e => e.ShopOrderNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("shop_order_number")
                    .HasComment("店铺订单号");

                entity.Property(e => e.SkuId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("sku_id")
                    .HasComment("SKUId");

                entity.Property(e => e.SkuNumber)
                    .HasMaxLength(255)
                    .HasColumnName("sku_number")
                    .HasComment("SKU编号");

                entity.Property(e => e.SkuSpec)
                    .HasMaxLength(500)
                    .HasColumnName("sku_spec")
                    .HasComment("SKU规格(颜色:红色;尺寸:L;)");
            });

            modelBuilder.Entity<GoodsOrderInvoice>(entity =>
            {
                entity.ToTable("goods_order_invoice");

                entity.HasComment("商品订单店铺发票")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.ClientId, "index_client_id");

                entity.HasIndex(e => e.ClientPhone, "index_client_phone");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.ShopOrderNumber, "index_order_number")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.BankAccount)
                    .HasMaxLength(100)
                    .HasColumnName("bank_account")
                    .HasComment("发票开户银行账户");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .HasColumnName("bank_name")
                    .HasComment("发票开户行");

                entity.Property(e => e.ClientAvatar)
                    .HasMaxLength(100)
                    .HasColumnName("client_avatar")
                    .HasDefaultValueSql("''")
                    .HasComment("客户头像");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("client_id")
                    .HasComment("客户id");

                entity.Property(e => e.ClientNickName)
                    .HasMaxLength(50)
                    .HasColumnName("client_nick_name")
                    .HasComment("客户昵称");

                entity.Property(e => e.ClientPhone)
                    .HasMaxLength(20)
                    .HasColumnName("client_phone")
                    .HasDefaultValueSql("''")
                    .HasComment("客户手机");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GeneralType)
                    .HasColumnName("general_type")
                    .HasComment("普通发票类型：0->个人，1->单位");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.IsOpen)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_open")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否开票");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("发票个人/单位名称");

                entity.Property(e => e.OpenInvoTime)
                    .HasColumnType("datetime")
                    .HasColumnName("open_invo_time")
                    .HasComment("开票时间");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_id")
                    .HasComment("总订单id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(100)
                    .HasColumnName("order_number")
                    .HasComment("总订单号");

                entity.Property(e => e.RegisterAddress)
                    .HasMaxLength(100)
                    .HasColumnName("register_address")
                    .HasComment("发票注册地址");

                entity.Property(e => e.RegisterPhone)
                    .HasMaxLength(100)
                    .HasColumnName("register_phone")
                    .HasComment("发票注册电话");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.ShopOrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_order_id")
                    .HasComment("店铺订单id");

                entity.Property(e => e.ShopOrderNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("shop_order_number")
                    .HasComment("店铺订单号");

                entity.Property(e => e.TaxpayersNum)
                    .HasMaxLength(100)
                    .HasColumnName("taxpayers_num")
                    .HasComment("发票纳税人识别号");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("发票类型：0->普通发票，1->增值税专用发票");
            });

            modelBuilder.Entity<GoodsOrderRefund>(entity =>
            {
                entity.ToTable("goods_order_refund");

                entity.HasComment("商品订单退单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.GoodsOrderId, "index_goods_order_id");

                entity.HasIndex(e => e.OrderRefundId, "index_order_refund_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.GoodsOrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_order_id")
                    .HasComment("商品订单id");

                entity.Property(e => e.OrderRefundId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_refund_id")
                    .HasComment("退单id");
            });

            modelBuilder.Entity<GoodsOrderShop>(entity =>
            {
                entity.ToTable("goods_order_shop");

                entity.HasComment("商品店铺订单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.OrderId, "index_goods_order_id");

                entity.HasIndex(e => e.OrderNumber, "index_goods_order_number");

                entity.HasIndex(e => e.ShopId, "index_shop_id");

                entity.HasIndex(e => e.ShopName, "index_shop_name");

                entity.HasIndex(e => e.TransportCompany, "index_transport_company");

                entity.HasIndex(e => e.TransportCompanyCode, "index_transport_company_code");

                entity.HasIndex(e => e.TransportNumber, "index_transport_number");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("店铺订单id");

                entity.Property(e => e.AddrAreaCode)
                    .HasMaxLength(10)
                    .HasColumnName("addr_area_code")
                    .HasComment("收货邮编");

                entity.Property(e => e.AddrAreaId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("addr_area_id")
                    .HasComment("收货省市区id");

                entity.Property(e => e.AddrAreaNames)
                    .HasMaxLength(20)
                    .HasColumnName("addr_area_names")
                    .HasComment("省市区");

                entity.Property(e => e.AddrConsignee)
                    .HasMaxLength(20)
                    .HasColumnName("addr_consignee")
                    .HasComment("收货人");

                entity.Property(e => e.AddrDetailArea)
                    .HasMaxLength(255)
                    .HasColumnName("addr_detail_area")
                    .HasComment("收货详细地址");

                entity.Property(e => e.AddrMobile)
                    .HasMaxLength(20)
                    .HasColumnName("addr_mobile")
                    .HasComment("收货电话");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.OrderGoodsSubtotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_goods_subtotal")
                    .HasComment("店铺订单商品金额小计");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_id")
                    .HasComment("总订单id");

                entity.Property(e => e.OrderIntegralSubtotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_integral_subtotal")
                    .HasComment("店铺订单积分小计");

                entity.Property(e => e.OrderIsFinish)
                    .HasColumnType("bit(1)")
                    .HasColumnName("order_is_finish")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("订单是否完结");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("order_number")
                    .HasComment("总订单编号");

                entity.Property(e => e.OrderSendGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_send_goods_time")
                    .HasComment("发货时间");

                entity.Property(e => e.OrderShopSubtotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_shop_subtotal")
                    .HasComment("店铺订单金额小计");

                entity.Property(e => e.OrderStatus)
                    .HasColumnName("order_status")
                    .HasComment("订单状态：0->交易关闭，1->待付款，2->待发货，3->已退款，4->已评价，5->已完成，7->待收货，8->待评价");

                entity.Property(e => e.OrderTakeGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_take_goods_time")
                    .HasComment("收货时间");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasColumnName("shop_name")
                    .HasComment("店铺名称");

                entity.Property(e => e.ShopOrderNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("shop_order_number")
                    .HasComment("店铺订单号");

                entity.Property(e => e.SupplierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("supplier_id")
                    .HasComment("供应商id");

                entity.Property(e => e.TransportCompany)
                    .HasColumnName("transport_company")
                    .HasComment("配送公司");

                entity.Property(e => e.TransportCompanyCode)
                    .HasMaxLength(20)
                    .HasColumnName("transport_company_code")
                    .HasComment("配送公司编码");

                entity.Property(e => e.TransportCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("transport_cost")
                    .HasComment("运费");

                entity.Property(e => e.TransportExplain)
                    .HasMaxLength(1000)
                    .HasColumnName("transport_explain")
                    .HasComment("配送说明");

                entity.Property(e => e.TransportIsSupplier)
                    .HasColumnType("bit(1)")
                    .HasColumnName("transport_is_supplier")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否供应商发货");

                entity.Property(e => e.TransportNumber)
                    .HasMaxLength(50)
                    .HasColumnName("transport_number")
                    .HasComment("配送单号");

                entity.Property(e => e.TransportNumberImage)
                    .HasMaxLength(100)
                    .HasColumnName("transport_number_image")
                    .HasComment("配送单号照片");

                entity.Property(e => e.TransportPhone)
                    .HasMaxLength(20)
                    .HasColumnName("transport_phone")
                    .HasComment("配送电话");

                entity.Property(e => e.TransportType)
                    .HasColumnName("transport_type")
                    .HasComment("配送类型：1->快递，2->物流");
            });

            modelBuilder.Entity<GoodsShoppingCart>(entity =>
            {
                entity.ToTable("goods_shopping_cart");

                entity.HasComment("商品购物车")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(10)")
                    .HasColumnName("client_id")
                    .HasComment("客户ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.GoodsNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("goods_number")
                    .HasDefaultValueSql("'0'")
                    .HasComment("商品数量");

                entity.Property(e => e.GoodsPicture)
                    .HasMaxLength(255)
                    .HasColumnName("goods_picture")
                    .HasComment("商品图片");

                entity.Property(e => e.GoodsSkuId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_sku_id")
                    .HasComment("商品库存id");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.SpecName)
                    .HasMaxLength(255)
                    .HasColumnName("spec_name")
                    .HasComment("规格值");
            });

            modelBuilder.Entity<GoodsSku>(entity =>
            {
                entity.ToTable("goods_sku");

                entity.HasComment("商品sku库存")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.GoodsPrice)
                    .HasPrecision(18, 2)
                    .HasColumnName("goods_price")
                    .HasComment("商品价格");

                entity.Property(e => e.Integral)
                    .HasPrecision(18, 2)
                    .HasColumnName("integral")
                    .HasComment("积分");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.SkuNumber)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("sku_number")
                    .HasComment("sku编号");

                entity.Property(e => e.SkuPictureKey)
                    .HasMaxLength(255)
                    .HasColumnName("sku_picture_key")
                    .HasComment(" sku商品图片key");

                entity.Property(e => e.SpecSkuId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("spec_sku_id")
                    .HasComment("规格id组合");

                entity.Property(e => e.StockNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("stock_number")
                    .HasComment("库存数量");

                entity.Property(e => e.WarningNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_number")
                    .HasComment("预警数量");
            });

            modelBuilder.Entity<GoodsSpecification>(entity =>
            {
                entity.ToTable("goods_specifications");

                entity.HasComment("商品规格")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.ParentId, "parent_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("parent_id")
                    .HasComment("父id");

                entity.Property(e => e.SpecName)
                    .HasMaxLength(255)
                    .HasColumnName("spec_name")
                    .HasComment("规格名称");

                entity.Property(e => e.SpecType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("spec_type")
                    .HasComment("规格类型:1,一级.2.二级");
            });

            modelBuilder.Entity<IdCard>(entity =>
            {
                entity.ToTable("id_card");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.BackUrl).IsRequired();

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.FrontUrl).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<IntegrationIdCard>(entity =>
            {
                entity.ToTable("integration_id_card");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.IdCardNumber).HasMaxLength(50);

                entity.Property(e => e.IntegrationId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.IssueDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<IntegrationUser>(entity =>
            {
                entity.ToTable("integration_user");

                entity.HasIndex(e => e.IdCardId, "IX_dbo.IntegrationUser_IdCardId");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdCardId).HasColumnType("int(11)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.IdCard)
                    .WithMany(p => p.IntegrationUsers)
                    .HasForeignKey(d => d.IdCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.IntegrationUser_dbo.IdCard_IdCardId");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.HasComment("商品（加拿大到中国商品）");

                entity.HasIndex(e => e.CategoryId, "IX_dbo.Item_CateogryId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasComment("中文品牌");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasComment("种类id（对应category表）");

                entity.Property(e => e.ClaimPrice)
                    .HasPrecision(19, 4)
                    .HasComment("申报价格");

                entity.Property(e => e.Count)
                    .HasMaxLength(50)
                    .HasComment("计数");

                entity.Property(e => e.Details)
                    .HasMaxLength(200)
                    .HasComment("详细分类");

                entity.Property(e => e.EnglishBrand)
                    .HasMaxLength(200)
                    .HasComment("英文品牌");

                entity.Property(e => e.EnglishCount)
                    .HasMaxLength(50)
                    .HasComment("英文计数");

                entity.Property(e => e.EnglishFormat)
                    .HasMaxLength(200)
                    .HasComment("英文规格");

                entity.Property(e => e.EnglishName)
                    .HasMaxLength(200)
                    .HasComment("英文名");

                entity.Property(e => e.EnglishType)
                    .HasMaxLength(50)
                    .HasComment("英文型号");

                entity.Property(e => e.EnglishUnit)
                    .HasMaxLength(50)
                    .HasComment("英文单位");

                entity.Property(e => e.Format)
                    .HasMaxLength(50)
                    .HasComment("规格");

                entity.Property(e => e.HsCode)
                    .HasMaxLength(100)
                    .HasComment("HsCode代码");

                entity.Property(e => e.IsDeleted)
                    .HasColumnType("tinyint(4)")
                    .HasComment("已删除");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("名称");

                entity.Property(e => e.OriginalPrice)
                    .HasPrecision(19, 4)
                    .HasComment("原价");

                entity.Property(e => e.Point)
                    .HasPrecision(16, 2)
                    .HasComment("分值");

                entity.Property(e => e.SellingPrice)
                    .HasPrecision(19, 4)
                    .HasComment("售价");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasComment("型号");

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .HasComment("单位");

                entity.Property(e => e.Upc)
                    .HasMaxLength(50)
                    .HasComment("Upc代码");

                entity.Property(e => e.Weight)
                    .HasPrecision(16, 2)
                    .HasComment("净重");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_dbo.Item_dbo.Category_CategoryId");
            });

            modelBuilder.Entity<ItemPhoto>(entity =>
            {
                entity.ToTable("item_photo");

                entity.HasComment("商品图片");

                entity.HasIndex(e => e.ItemId, "IX_dbo.ItemPhoto_ItemId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ItemId).HasColumnType("int(11)");

                entity.Property(e => e.Url).IsRequired();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ItemPhotos)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dbo.ItemPhoto_dbo.Item_ItemId");
            });

            modelBuilder.Entity<LoadDeliveryBatch>(entity =>
            {
                entity.ToTable("load_delivery_batch");

                entity.HasComment("装车发货批次二级表");

                entity.Property(e => e.Id).HasColumnType("int");

                entity.Property(e => e.FlightInfo)
                    .HasMaxLength(32)
                    .HasColumnName("FlightInfo");

                entity.Property(e => e.CargoNumber)
                    .HasMaxLength(32)
                    .HasColumnName("CargoNumber");

                entity.Property(e => e.ArrivalTime)
                    .HasColumnType("datetime")
                    .HasColumnName("ArrivalTime");

                entity.Property(e => e.WarehouseId)
                    .HasColumnType("int(11)")
                    .HasComment("仓库id");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.LoadDeliveryBatches)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.LoadDeliveryBatch_dbo.Warehouse_WarehouseId");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.LoadDeliveryBatches)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_dbo.LoadDeliveryBatch_dbo.Batch_Id");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.HasComment("系统日志")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Browser)
                    .HasMaxLength(255)
                    .HasColumnName("browser");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.ExceptionDetail)
                    .HasColumnType("text")
                    .HasColumnName("exception_detail");

                entity.Property(e => e.LogType)
                    .HasMaxLength(255)
                    .HasColumnName("log_type");

                entity.Property(e => e.Method)
                    .HasMaxLength(255)
                    .HasColumnName("method");

                entity.Property(e => e.Params)
                    .HasColumnType("text")
                    .HasColumnName("params");

                entity.Property(e => e.RequestIp)
                    .HasMaxLength(255)
                    .HasColumnName("request_ip");

                entity.Property(e => e.Time)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("time");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Log1>(entity =>
            {
                entity.ToTable("logs");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            modelBuilder.Entity<MarketingGoodsCategory>(entity =>
            {
                entity.ToTable("marketing_goods_category");

                entity.HasComment("积分商品分类")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.HasIndex(e => e.ParentId, "parent_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CategoryDescribe)
                    .HasMaxLength(255)
                    .HasColumnName("category_describe")
                    .HasComment("分类描述");

                entity.Property(e => e.CategoryDisplay)
                    .HasColumnType("bit(1)")
                    .HasColumnName("category_display")
                    .HasDefaultValueSql("b'1'")
                    .HasComment("是否显示:0:否,1:是");

                entity.Property(e => e.CategoryLevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("category_level")
                    .HasComment("等级:1,一级分类,2,二级分类");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(255)
                    .HasColumnName("class_name")
                    .HasComment("分类名称");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IconKey)
                    .HasMaxLength(255)
                    .HasColumnName("icon_key")
                    .HasComment("上传图标");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("parent_id")
                    .HasComment("父id");

                entity.Property(e => e.SerialNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("serial_number")
                    .HasComment("排序序号");
            });

            modelBuilder.Entity<MarketingGoodsIntegral>(entity =>
            {
                entity.ToTable("marketing_goods_integral");

                entity.HasComment("积分商品")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.IsDel, "index_is_del");

                entity.HasIndex(e => e.IsRecommend, "index_is_recommend");

                entity.HasIndex(e => e.IsShelf, "index_is_shelf");

                entity.HasIndex(e => e.ShopId, "index_shop_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("category_id")
                    .HasComment("积分商品分类id");

                entity.Property(e => e.ChangePrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("change_price")
                    .HasComment("兑换价");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GoodsDetails)
                    .HasColumnName("goods_details")
                    .HasComment("商品详情");

                entity.Property(e => e.GoodsInventory)
                    .HasColumnType("int(11)")
                    .HasColumnName("goods_inventory")
                    .HasComment("库存");

                entity.Property(e => e.GoodsName)
                    .HasMaxLength(255)
                    .HasColumnName("goods_name")
                    .HasComment("商品名称");

                entity.Property(e => e.GoodsPicture)
                    .HasMaxLength(255)
                    .HasColumnName("goods_picture")
                    .HasComment("商品图片");

                entity.Property(e => e.GoodsPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("goods_price")
                    .HasComment("商品价格");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.IsRecommend)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_recommend")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否推荐");

                entity.Property(e => e.IsShelf)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_shelf")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否上架");

                entity.Property(e => e.SalesVolume)
                    .HasColumnType("int(11)")
                    .HasColumnName("sales_volume")
                    .HasComment("销量");

                entity.Property(e => e.SeoDescription)
                    .HasMaxLength(255)
                    .HasColumnName("seo_description")
                    .HasComment("seo描述");

                entity.Property(e => e.SeoKeywords)
                    .HasMaxLength(100)
                    .HasColumnName("seo_keywords")
                    .HasComment("seo关键字");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("修改时间");
            });

            modelBuilder.Entity<MarketingGroupDetail>(entity =>
            {
                entity.ToTable("marketing_group_details");

                entity.HasComment("拼团活动详情")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.IsDel, "index_is_del");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(255)
                    .HasColumnName("activity_name")
                    .HasComment("活动名称");

                entity.Property(e => e.ActivityStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("activity_status")
                    .HasComment("活动状态(0->未开始,1->已开始,2->已结束)");

                entity.Property(e => e.AllNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("all_number")
                    .HasComment("成团人数");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time")
                    .HasComment("结束时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("拼团商品id");

                entity.Property(e => e.GoodsName)
                    .HasMaxLength(255)
                    .HasColumnName("goods_name")
                    .HasComment("拼团商品名称");

                entity.Property(e => e.GoodsPicture)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("goods_picture")
                    .HasComment("拼团商品图片");

                entity.Property(e => e.GoodsPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("goods_price")
                    .HasComment("商品价格(团购价格)");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.IsSuccess)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_success")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否成功:0.失败.1.成功");

                entity.Property(e => e.JoinNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("join_number")
                    .HasDefaultValueSql("'0'")
                    .HasComment("已参团人数");

                entity.Property(e => e.RebateMoney)
                    .HasPrecision(10, 2)
                    .HasColumnName("rebate_money")
                    .HasComment("成团退芝麻粒");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time")
                    .HasComment("开始时间");

                entity.Property(e => e.WinNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("win_number")
                    .HasComment("中奖人数");
            });

            modelBuilder.Entity<MarketingGroupOrder>(entity =>
            {
                entity.ToTable("marketing_group_order");

                entity.HasComment("拼团活动订单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.IsDel, "index_is_del");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ActivityId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("activity_id")
                    .HasComment("拼团活动id");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(255)
                    .HasColumnName("activity_name")
                    .HasComment("活动名称");

                entity.Property(e => e.ActivityStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("activity_status")
                    .HasComment("活动状态:,0->待付款，1->待开奖,2->拼团失败,3->中奖,4->未中奖");

                entity.Property(e => e.AddrAreaCode)
                    .HasMaxLength(10)
                    .HasColumnName("addr_area_code")
                    .HasComment("收货邮编");

                entity.Property(e => e.AddrAreaId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("addr_area_id")
                    .HasComment("收货省市区id");

                entity.Property(e => e.AddrAreaNames)
                    .HasMaxLength(20)
                    .HasColumnName("addr_area_names")
                    .HasComment("省市区");

                entity.Property(e => e.AddrConsignee)
                    .HasMaxLength(20)
                    .HasColumnName("addr_consignee")
                    .HasComment("收货人");

                entity.Property(e => e.AddrDetailArea)
                    .HasMaxLength(255)
                    .HasColumnName("addr_detail_area")
                    .HasComment("收货详细地址");

                entity.Property(e => e.AddrMobile)
                    .HasMaxLength(20)
                    .HasColumnName("addr_mobile")
                    .HasComment("收货电话");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("用户id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GeneralType)
                    .HasColumnName("general_type")
                    .HasComment("普通发票类型：0->个人，1->单位");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("拼团商品id");

                entity.Property(e => e.GoodsName)
                    .HasMaxLength(255)
                    .HasColumnName("goods_name")
                    .HasComment("拼团商品名称");

                entity.Property(e => e.GoodsPicture)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("goods_picture")
                    .HasComment("拼团商品图片");

                entity.Property(e => e.GoodsPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("goods_price")
                    .HasComment("拼团商品价格");

                entity.Property(e => e.Integral)
                    .HasPrecision(18, 2)
                    .HasColumnName("integral")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("积分（拼团中奖返积分）");

                entity.Property(e => e.InvoiceId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("invoice_id")
                    .HasComment("发票id");

                entity.Property(e => e.InvoiceName)
                    .HasMaxLength(100)
                    .HasColumnName("invoice_name")
                    .HasComment("发票个人/单位名称");

                entity.Property(e => e.InvoiceType)
                    .IsRequired()
                    .HasColumnName("invoice_type")
                    .HasDefaultValueSql("'2'")
                    .HasComment("发票类型：0->普通发票，1->增值税专用发票,2,->无发票");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.NickName)
                    .HasMaxLength(255)
                    .HasColumnName("nick_name")
                    .HasComment("会员昵称");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(255)
                    .HasColumnName("order_number")
                    .HasComment("订单编号");

                entity.Property(e => e.OrderPayType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_pay_type")
                    .HasComment("支付方式：0->余额(芝麻粒)，1->支付宝,2->微信");

                entity.Property(e => e.OrderRemark)
                    .HasMaxLength(500)
                    .HasColumnName("order_remark")
                    .HasComment("订单备注");

                entity.Property(e => e.OrderSendGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_send_goods_time")
                    .HasComment("发货时间");

                entity.Property(e => e.OrderStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_status")
                    .HasComment("订单状态:0->待付款,1->待发货,2->待收货,3->已完成,4->已退款");

                entity.Property(e => e.OrderTakeGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_take_goods_time")
                    .HasComment("收货时间");

                entity.Property(e => e.OrderTotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("order_total")
                    .HasComment("订单总额");

                entity.Property(e => e.PayNumber)
                    .HasMaxLength(255)
                    .HasColumnName("pay_number")
                    .HasComment("付款单号");

                entity.Property(e => e.PayTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pay_time")
                    .HasComment("支付时间");

                entity.Property(e => e.PayType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("pay_type")
                    .HasComment("支付类型");

                entity.Property(e => e.RefundNumber)
                    .HasMaxLength(255)
                    .HasColumnName("refund_number")
                    .HasComment("退款编号");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(255)
                    .HasColumnName("shop_name")
                    .HasComment("店铺名称");

                entity.Property(e => e.SupplierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("supplier_id")
                    .HasComment("供应商id");

                entity.Property(e => e.TaxpayersNum)
                    .HasMaxLength(100)
                    .HasColumnName("taxpayers_num")
                    .HasComment("发票纳税人识别号");

                entity.Property(e => e.TransportCompany)
                    .HasMaxLength(255)
                    .HasColumnName("transport_company")
                    .HasComment("配送公司");

                entity.Property(e => e.TransportCompanyCode)
                    .HasMaxLength(20)
                    .HasColumnName("transport_company_code")
                    .HasComment("配送公司编码");

                entity.Property(e => e.TransportCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("transport_cost")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("运费");

                entity.Property(e => e.TransportNumber)
                    .HasMaxLength(50)
                    .HasColumnName("transport_number")
                    .HasComment("配送单号");

                entity.Property(e => e.UserAvatar)
                    .HasMaxLength(255)
                    .HasColumnName("user_avatar")
                    .HasComment("会员头像");

                entity.Property(e => e.UserNumber)
                    .HasMaxLength(255)
                    .HasColumnName("user_number")
                    .HasComment("会员编号");
            });

            modelBuilder.Entity<MarketingReward>(entity =>
            {
                entity.ToTable("marketing_reward");

                entity.HasComment("抽奖参数")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.Location)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("location")
                    .HasComment("位置(1:上左,2:上中，3上右，4中左，5中右,6:下左,7:下中，8下右)");

                entity.Property(e => e.MarkedWords)
                    .HasMaxLength(32)
                    .HasColumnName("marked_words")
                    .HasComment("提示语");

                entity.Property(e => e.RewardCount)
                    .HasPrecision(10)
                    .HasColumnName("reward_count")
                    .HasComment("中奖数量");

                entity.Property(e => e.RewardName)
                    .HasMaxLength(64)
                    .HasColumnName("reward_name")
                    .HasComment("奖品名称");

                entity.Property(e => e.RewardPicture)
                    .HasMaxLength(64)
                    .HasColumnName("reward_picture")
                    .HasComment("奖品图片");

                entity.Property(e => e.RewardProbability)
                    .HasMaxLength(32)
                    .HasColumnName("reward_probability")
                    .HasComment("中奖概率");

                entity.Property(e => e.RewardType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("reward_type")
                    .HasComment("中奖方式（0:无，1芝麻花，2芝麻粒，3商品）");

                entity.Property(e => e.SerialNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("serial_number")
                    .HasComment("排序序号");
            });

            modelBuilder.Entity<MarketingRewardRecord>(entity =>
            {
                entity.ToTable("marketing_reward_record");

                entity.HasComment("抽奖记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AddrAreaId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("addr_area_id")
                    .HasComment("收货省市区id");

                entity.Property(e => e.AddrAreaNames)
                    .HasMaxLength(20)
                    .HasColumnName("addr_area_names")
                    .HasComment("省市区");

                entity.Property(e => e.AddrConsignee)
                    .HasMaxLength(20)
                    .HasColumnName("addr_consignee")
                    .HasComment("收货人");

                entity.Property(e => e.AddrDetailArea)
                    .HasMaxLength(255)
                    .HasColumnName("addr_detail_area")
                    .HasComment("收货详细地址");

                entity.Property(e => e.AddrMobile)
                    .HasMaxLength(20)
                    .HasColumnName("addr_mobile")
                    .HasComment("收货电话");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(64)
                    .HasColumnName("order_number")
                    .HasComment("订单编号");

                entity.Property(e => e.OrderSendGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_send_goods_time")
                    .HasComment("发货时间");

                entity.Property(e => e.OrderTakeGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_take_goods_time")
                    .HasComment("收货时间");

                entity.Property(e => e.RewardId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("reward_id")
                    .HasComment("奖品id");

                entity.Property(e => e.RewardStaus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("reward_staus")
                    .HasDefaultValueSql("'0'")
                    .HasComment("中奖状态（0:待发货，1待收货，2已完成，3已到账）");

                entity.Property(e => e.RewardType)
                    .HasColumnType("int(11)")
                    .HasColumnName("reward_type")
                    .HasComment("抽奖类型（1免费抽奖，2芝麻粒抽奖）");

                entity.Property(e => e.SerialNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("serial_number")
                    .HasComment("排序序号");

                entity.Property(e => e.TransportCompany)
                    .HasMaxLength(255)
                    .HasColumnName("transport_company")
                    .HasComment("配送公司");

                entity.Property(e => e.TransportCompanyCode)
                    .HasMaxLength(20)
                    .HasColumnName("transport_company_code")
                    .HasComment("配送公司编码");

                entity.Property(e => e.TransportExplain)
                    .HasMaxLength(1000)
                    .HasColumnName("transport_explain")
                    .HasComment("配送说明");

                entity.Property(e => e.TransportNumber)
                    .HasMaxLength(50)
                    .HasColumnName("transport_number")
                    .HasComment("配送单号");

                entity.Property(e => e.TransportPhone)
                    .HasMaxLength(20)
                    .HasColumnName("transport_phone")
                    .HasComment("配送电话");

                entity.Property(e => e.TransportType)
                    .HasColumnName("transport_type")
                    .HasDefaultValueSql("'1'")
                    .HasComment("配送类型：1->快递，2->物流");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("user_id")
                    .HasComment("中奖人");
            });

            modelBuilder.Entity<MarketingShareprofitRecord>(entity =>
            {
                entity.ToTable("marketing_shareprofit_record");

                entity.HasComment("分红历史记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.ExpendIntegral)
                    .HasPrecision(32, 2)
                    .HasColumnName("expend_integral")
                    .HasComment("参与分红积分");

                entity.Property(e => e.OwePasscard)
                    .HasPrecision(20, 2)
                    .HasColumnName("owe_passcard")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("亏欠通证");

                entity.Property(e => e.RealPasscardProfits)
                    .HasPrecision(20, 2)
                    .HasColumnName("real_passcard_profits")
                    .HasComment("实际通证分红");

                entity.Property(e => e.RemainIntegral)
                    .HasPrecision(32, 2)
                    .HasColumnName("remain_integral")
                    .HasComment("剩余积分");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("type")
                    .HasComment("1店铺，2会员");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("user_id")
                    .HasComment("会员或店铺id");
            });

            modelBuilder.Entity<MarketingShareprofitUser>(entity =>
            {
                entity.ToTable("marketing_shareprofit_user");

                entity.HasComment("用户-积分(今日榜单）")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.ExpendIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("expend_integral")
                    .HasComment("参与分红积分");

                entity.Property(e => e.OweIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("owe_integral")
                    .HasComment("亏欠通证");

                entity.Property(e => e.RemainIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("remain_integral")
                    .HasComment("剩余积分");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("type")
                    .HasDefaultValueSql("'2'")
                    .HasComment("1店铺，2会员");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("update_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("会员或店铺id");
            });

            modelBuilder.Entity<OrchardGame>(entity =>
            {
                entity.ToTable("orchard_game");

                entity.HasComment("果园游戏用户信息")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.AppUserId, "app_user_id")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("app用户id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GameLevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("game_level")
                    .HasComment("游戏等级:0->初始等级,1->一级,2->二级.......9->九级");

                entity.Property(e => e.RebateCost)
                    .HasPrecision(18, 2)
                    .HasColumnName("rebate_cost")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("返利收益");

                entity.Property(e => e.UserStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("user_status")
                    .HasComment("用户状态:0->未参与游戏  1->已参与游戏,2->出局");
            });

            modelBuilder.Entity<OrderActionHistory>(entity =>
            {
                entity.ToTable("order_action_history");

                entity.HasComment("运单操作记录");

                entity.HasIndex(e => e.OrderId, "IX_dbo.OrderActionHistory_OrderId");

                entity.HasIndex(e => e.UserId, "IX_dbo.OrderActionHistory_UserId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderActionHistories)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OrderActionHistory_dbo.Order_OrderId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderActionHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OrderActionHistory_dbo.User_UserId");
            });

            modelBuilder.Entity<OrderBaggage>(entity =>
            {
                entity.ToTable("order_baggage");

                entity.HasIndex(e => e.OrderId, "IX_dbo.OrderBaggage_OrderId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Height)
                    .HasPrecision(16, 2)
                    .HasComment("高");

                entity.Property(e => e.Length)
                    .HasPrecision(16, 2)
                    .HasComment("长");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id（对应order表）");

                entity.Property(e => e.WeightKg)
                    .HasPrecision(16, 2)
                    .HasComment("重量Kg");

                entity.Property(e => e.Width)
                    .HasPrecision(16, 2)
                    .HasComment("宽");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderBaggages)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OrderBaggage_dbo.Order_OrderId");
            });

            modelBuilder.Entity<OrderComment>(entity =>
            {
                entity.ToTable("order_comment");

                entity.HasComment("订单评论")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.OrderNumber, "Index_order_number")
                    .IsUnique();

                entity.HasIndex(e => e.ClientId, "index_client_id");

                entity.HasIndex(e => e.GoodsId, "index_goods_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("评论ID");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("client_id")
                    .HasComment("客户ID");

                entity.Property(e => e.CommentContent)
                    .HasColumnName("comment_content")
                    .HasComment("评价内容");

                entity.Property(e => e.CommentImages)
                    .HasMaxLength(500)
                    .HasColumnName("comment_images")
                    .HasComment("评价图片");

                entity.Property(e => e.CommentLevel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("comment_level")
                    .HasComment("评价星级");

                entity.Property(e => e.CommentType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("comment_type")
                    .HasComment("评价类型：0->好评，1->中评，2->差评");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsReply)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_reply")
                    .HasComment("是否已回复：0->否，1->是");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("order_number")
                    .HasComment("订单编号");

                entity.Property(e => e.OrderType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_type")
                    .HasComment("订单类型：0->装修套餐预定，1->工人预定,2-商品订单");

                entity.Property(e => e.ReplyContent)
                    .HasColumnName("reply_content")
                    .HasComment("回复内容");

                entity.Property(e => e.ReplyTime)
                    .HasColumnType("datetime")
                    .HasColumnName("reply_time")
                    .HasComment("回复时间");

                entity.Property(e => e.ReplyUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("reply_user_id")
                    .HasComment("回复者ID");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.SkuId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("sku_id")
                    .HasComment("skuid");
            });

            modelBuilder.Entity<OrderFlow>(entity =>
            {
                entity.ToTable("order_flow");

                entity.HasComment("订单流程")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.OrderNumber, "index_order_number");

                entity.HasIndex(e => e.OrderStatus, "index_order_status");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("订单流程ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("order_number")
                    .HasComment("订单编号");

                entity.Property(e => e.OrderStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_status")
                    .HasComment("流程状态：0->交易关闭，1->已下单，2->已支付，3->已退款，4->已评价，5->已完成，6->已施工，7->已发货，8->已收货");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("操作用户ID");

                entity.Property(e => e.UserType)
                    .HasColumnName("user_type")
                    .HasComment("操作用户类型：0->app用户，1->pc用户");
            });

            modelBuilder.Entity<OrderIntegral>(entity =>
            {
                entity.ToTable("order_integral");

                entity.HasComment("积分订单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.OrderNumber, "index_goods_order_number");

                entity.HasIndex(e => e.ShopId, "index_shop_id");

                entity.HasIndex(e => e.ShopName, "index_shop_name");

                entity.HasIndex(e => e.TransportCompany, "index_transport_company");

                entity.HasIndex(e => e.TransportCompanyCode, "index_transport_company_code");

                entity.HasIndex(e => e.TransportNumber, "index_transport_number");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AddrAreaCode)
                    .HasMaxLength(10)
                    .HasColumnName("addr_area_code")
                    .HasComment("收货邮编");

                entity.Property(e => e.AddrAreaId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("addr_area_id")
                    .HasComment("收货省市区id");

                entity.Property(e => e.AddrAreaNames)
                    .HasMaxLength(20)
                    .HasColumnName("addr_area_names")
                    .HasComment("省市区");

                entity.Property(e => e.AddrConsignee)
                    .HasMaxLength(20)
                    .HasColumnName("addr_consignee")
                    .HasComment("收货人");

                entity.Property(e => e.AddrDetailArea)
                    .HasMaxLength(255)
                    .HasColumnName("addr_detail_area")
                    .HasComment("收货详细地址");

                entity.Property(e => e.AddrMobile)
                    .HasMaxLength(20)
                    .HasColumnName("addr_mobile")
                    .HasComment("收货电话");

                entity.Property(e => e.ClientAvatar)
                    .HasMaxLength(255)
                    .HasColumnName("client_avatar")
                    .HasComment("会员头像");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("client_id")
                    .HasComment("会员id");

                entity.Property(e => e.ClientNickname)
                    .HasMaxLength(20)
                    .HasColumnName("client_nickname")
                    .HasComment("会员昵称");

                entity.Property(e => e.ClientNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("client_number")
                    .HasComment("会员编号");

                entity.Property(e => e.ClientPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("client_phone")
                    .HasComment("会员手机号");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.GoodsChangePrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("goods_change_price")
                    .HasComment("商品兑换金额");

                entity.Property(e => e.GoodsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("goods_id")
                    .HasComment("商品id");

                entity.Property(e => e.GoodsName)
                    .HasMaxLength(255)
                    .HasColumnName("goods_name")
                    .HasComment("商品名称");

                entity.Property(e => e.GoodsPicture)
                    .HasMaxLength(255)
                    .HasColumnName("goods_picture")
                    .HasComment("商品图片");

                entity.Property(e => e.GoodsPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("goods_price")
                    .HasComment("商品价格");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("order_number")
                    .HasComment("总订单编号");

                entity.Property(e => e.OrderPayType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_pay_type")
                    .HasComment("支付方式：0->积分，1->通证");

                entity.Property(e => e.OrderRemark)
                    .HasMaxLength(500)
                    .HasColumnName("order_remark")
                    .HasComment("订单备注");

                entity.Property(e => e.OrderSendGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_send_goods_time")
                    .HasComment("发货时间");

                entity.Property(e => e.OrderStatus)
                    .HasColumnName("order_status")
                    .HasComment("订单状态：1->已下单，5->已完成，7->已发货");

                entity.Property(e => e.OrderTakeGoodsTime)
                    .HasColumnType("datetime")
                    .HasColumnName("order_take_goods_time")
                    .HasComment("收货时间");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasColumnName("shop_name")
                    .HasComment("店铺名称");

                entity.Property(e => e.TransportCompany)
                    .HasColumnName("transport_company")
                    .HasComment("配送公司");

                entity.Property(e => e.TransportCompanyCode)
                    .HasMaxLength(20)
                    .HasColumnName("transport_company_code")
                    .HasComment("配送公司编码");

                entity.Property(e => e.TransportNumber)
                    .HasMaxLength(50)
                    .HasColumnName("transport_number")
                    .HasComment("配送单号");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("order_item");

                entity.HasIndex(e => e.ItemId, "IX_dbo.NewOrderItem_ItemId");

                entity.HasIndex(e => e.OrderId, "IX_dbo.NewOrderItem_OrderId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ItemId)
                    .HasColumnType("int(11)")
                    .HasComment("商品id（对应item表）");

                entity.Property(e => e.OrderBaggageId)
                    .HasColumnType("int(11)")
                    .HasComment("运单包裹id（对应OrderBaggage表）");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id（对应order表）");

                entity.Property(e => e.Quantity)
                    .HasColumnType("int(11)")
                    .HasComment("数量");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.NewOrderItem_dbo.Item_ItemId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.NewOrderItem_dbo.Order_OrderId");
            });

            modelBuilder.Entity<OrderPhoto>(entity =>
            {
                entity.ToTable("order_photo");

                entity.HasIndex(e => e.OrderId, "IX_dbo.OrderPhoto_OrderId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasComment("创建时间");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("订单id");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasComment("图片地址");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPhotos)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OrderPhoto_dbo.Order_OrderId");
            });

            modelBuilder.Entity<OrderRefund>(entity =>
            {
                entity.ToTable("order_refund");

                entity.HasComment("退单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Accountant, "index_accountant");

                entity.HasIndex(e => e.Auditor, "index_auditor");

                entity.HasIndex(e => e.ClientId, "index_client_id");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.OrderNumber, "index_order_number");

                entity.HasIndex(e => e.OrderType, "index_order_type");

                entity.HasIndex(e => e.RefundNumber, "index_refund_number");

                entity.HasIndex(e => e.RefundStatus, "index_refund_status");

                entity.HasIndex(e => e.ShopId, "index_shop_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("退单ID");

                entity.Property(e => e.Accountant)
                    .HasColumnType("bigint(10)")
                    .HasColumnName("accountant")
                    .HasComment("打款人");

                entity.Property(e => e.AddressIsSupplier)
                    .HasColumnType("bit(1)")
                    .HasColumnName("address_is_supplier")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否供应商收货");

                entity.Property(e => e.AreaCode)
                    .HasMaxLength(10)
                    .HasColumnName("area_code")
                    .HasComment("退货邮编");

                entity.Property(e => e.AuditTime)
                    .HasColumnType("datetime")
                    .HasColumnName("audit_time")
                    .HasComment("审核时间");

                entity.Property(e => e.Auditor)
                    .HasColumnType("bigint(10)")
                    .HasColumnName("auditor")
                    .HasComment("审核人");

                entity.Property(e => e.AuditorType)
                    .HasColumnName("auditor_type")
                    .HasComment("审核人类型：0->app用户，1->pc用户");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(10)")
                    .HasColumnName("client_id")
                    .HasComment("客户id");

                entity.Property(e => e.Consignee)
                    .HasMaxLength(20)
                    .HasColumnName("consignee")
                    .HasComment("退货联系人");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.DetailArea)
                    .HasMaxLength(255)
                    .HasColumnName("detail_area")
                    .HasComment("退货详细地址");

                entity.Property(e => e.ExpressCompanyCode)
                    .HasMaxLength(50)
                    .HasColumnName("express_company_code")
                    .HasComment("快递公司编码");

                entity.Property(e => e.ExpressNumber)
                    .HasMaxLength(100)
                    .HasColumnName("express_number")
                    .HasComment("快递单号");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsIntervene)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_intervene")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否平台介入(废弃)");

                entity.Property(e => e.IsReturnGoods)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_return_goods")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否退货");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .HasColumnName("mobile")
                    .HasComment("退货电话号码");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_id")
                    .HasComment("订单id");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(100)
                    .HasColumnName("order_number")
                    .HasComment("订单编号");

                entity.Property(e => e.OrderType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_type")
                    .HasComment("订单类型：2->商品订单");

                entity.Property(e => e.RealRefundIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("real_refund_integral")
                    .HasComment("真实退单积分");

                entity.Property(e => e.RealRefundPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("real_refund_price")
                    .HasComment("真实退款金额");

                entity.Property(e => e.RefundCause)
                    .HasColumnName("refund_cause")
                    .HasComment("退款原因");

                entity.Property(e => e.RefundImages)
                    .HasMaxLength(500)
                    .HasColumnName("refund_images")
                    .HasComment("退款图片");

                entity.Property(e => e.RefundIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("refund_integral")
                    .HasComment("退单积分");

                entity.Property(e => e.RefundNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("refund_number")
                    .HasComment("退单编号");

                entity.Property(e => e.RefundPayType)
                    .HasColumnName("refund_pay_type")
                    .HasComment("退款支付类型：0->支付宝，1->微信，2->预存款");

                entity.Property(e => e.RefundPrice)
                    .HasPrecision(10, 2)
                    .HasColumnName("refund_price")
                    .HasComment("退单金额");

                entity.Property(e => e.RefundStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("refund_status")
                    .HasComment("退单状态：0->申请中，1->取消退款，2->拒绝退款，3->同意退款，4->已退款，5->同意退货，6->平台待处理");

                entity.Property(e => e.RefundTime)
                    .HasColumnType("datetime")
                    .HasColumnName("refund_time")
                    .HasComment("打款时间");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasColumnName("remark")
                    .HasComment("备注");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");
            });

            modelBuilder.Entity<OrderRefundFlow>(entity =>
            {
                entity.ToTable("order_refund_flow");

                entity.HasComment("退单流程")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CreateTime, "index_create_time");

                entity.HasIndex(e => e.RefundNumber, "index_refund_number");

                entity.HasIndex(e => e.RefundStatus, "index_refund_status");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("退单流程ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.OptionMessage)
                    .HasColumnName("option_message")
                    .HasComment("操作消息");

                entity.Property(e => e.RefundNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("refund_number")
                    .HasComment("退单编号");

                entity.Property(e => e.RefundStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("refund_status")
                    .HasComment("退单状态：0->申请中，1->取消退款，2->拒绝退款，3->同意退款，4->已退款，5->同意退货，6->平台待处理");

                entity.Property(e => e.Remark)
                    .HasMaxLength(500)
                    .HasColumnName("remark")
                    .HasComment("备注");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("操作用户ID");

                entity.Property(e => e.UserType)
                    .HasColumnType("tinyint(20)")
                    .HasColumnName("user_type")
                    .HasComment("操作用户类型：0->app用户，1->pc用户");
            });

            modelBuilder.Entity<OrderScanStatus>(entity =>
            {
                entity.ToTable("order_scan_status");

                entity.HasComment("订单扫描状态");

                entity.HasIndex(e => e.OrderId);
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id（对应order表）");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasComment("状态");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasComment("扫描时间");
                
                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("操作员Id");
            });

            modelBuilder.Entity<OrderSharingRatio>(entity =>
            {
                entity.ToTable("order_sharing_ratio");

                entity.HasComment("订单分成比例")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("client_id")
                    .HasComment("客户id");

                entity.Property(e => e.ClientIntegral)
                    .HasPrecision(10, 2)
                    .HasColumnName("client_integral")
                    .HasComment("客户积分");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.MemberFirstAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("member_first_amount")
                    .HasComment("会员一级推荐金额");

                entity.Property(e => e.MemberFirstId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("member_first_id")
                    .HasComment("会员一级推荐id");

                entity.Property(e => e.MemberSecondAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("member_second_amount")
                    .HasComment("会员二级推荐金额");

                entity.Property(e => e.MemberSecondId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("member_second_id")
                    .HasComment("会员二级推荐id");

                entity.Property(e => e.OnlineFirstAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("online_first_amount")
                    .HasComment("网店一级推荐金额");

                entity.Property(e => e.OnlineFirstId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("online_first_id")
                    .HasComment("网店一级推荐id");

                entity.Property(e => e.OnlineSecondAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("online_second_amount")
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("网店二级推荐金额");

                entity.Property(e => e.OnlineSecondId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("online_second_id")
                    .HasComment("网店二级推荐id");

                entity.Property(e => e.OrderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order_id")
                    .HasComment("商品店铺订单id");

                entity.Property(e => e.OrderType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("order_type")
                    .HasComment("订单类型：0-商品店铺订单，1-拼团订单");

                entity.Property(e => e.PlatformAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("platform_amount")
                    .HasComment("平台金额");

                entity.Property(e => e.ShareStatus)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("share_status")
                    .HasComment("分成状态：0->待分成，1->已分成，2->不分成");

                entity.Property(e => e.ShopAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("shop_amount")
                    .HasComment("店铺金额");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.SupplierFirstAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("supplier_first_amount")
                    .HasComment("供应商一级推荐金额");

                entity.Property(e => e.SupplierFirstId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("supplier_first_id")
                    .HasComment("供应商一级推荐id");

                entity.Property(e => e.SupplierSecondAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("supplier_second_amount")
                    .HasComment("供应商二级推荐金额");

                entity.Property(e => e.SupplierSecondId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("supplier_second_id")
                    .HasComment("供应商二级推荐id");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("order_status");

                entity.HasComment("订单状态");

                entity.HasIndex(e => e.OrderId, "IX_dbo.OrderStatus_OrderId");

                entity.HasIndex(e => e.UserId, "IX_dbo.OrderStatus_UserId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasComment("日期");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id（对应order表）");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasComment("状态");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("10运单已创建\r\n13,请联系国内快递公司/国内仓库负责人\r\n14,等待核对包裹状态\r\n15,录单晚请联系仓库负责人确认仓库是否收货\r\n16,货物已被{0}接收\r\n17,货物已发往下一站\r\n18,货物已发往货站\r\n20,已打包封装等待发出\r\n21,货物已入库\r\n22,包惠已封装准备发出\r\n23,包裹需要付款\r\n24,包裹建立等待称重\r\n25,包衷已进入邮政运输阶段（请在系统内查看单号)\r\n30,收件人信息缺失/错误货物进入待发区\r\n31,航班延误\r\n32,单号信息有误/运单状态还未更新请更新单号避免仓库无法收货\r\n40,重名件待发货\r\n41,移出迸入待发区\r\n42,包裹移出待发区\r\n43,包裹已入库（请核对包裹数量）\r\n44,包裹已退回\r\n50,货物已退回给客户\r\n60,包裹已打包\r\n61,货物已发往机场\r\n62,货物已接收等待打包封装\r\n63,包裹已发往各取货点\r\n64,包裹已封装\r\n65,货物已起航（请联系所在群群主充值）\r\n66,包裹到达多伦多\r\n67,客户已付款\r\n68,货物已确认\r\n69,货物已二次确认\r\n72,货物已三次确认\r\n70,货物已飞往中国\r\n71,货物开始国内段运输\r\n80,货物已抵达海关等待清关\r\n81,货物到达船运公司仓库\r\n90,货物开始国内派送\r\n91,货物开始国际段运检\r\n95,要求送货\r\n92,包裹已发出\r\n100,货物已被海关退回\r\n101,货物已到达加拿大清关中\r\n102,货物已到达多佗多仓库\r\n700,正在派送\r\n1000,已签收\r\n1100,客户已取货\r\n2000,已确认");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderStatuses)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OrderStatus_dbo.Order_OrderId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderStatuses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.OrderStatus_dbo.User_UserId");
            });

            modelBuilder.Entity<OrderStatusInternal>(entity =>
            {
                entity.ToTable("order_status_internal");

                entity.HasComment("订单后台操作记录");

                entity.HasIndex(e => e.OrderId, "IX_dbo.OrderStatusInternal_OrderId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasComment("日期");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id（对应order表）");

                entity.Property(e => e.Status)
                    .HasColumnType("int(11)")
                    .HasComment("状态");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("13装箱打包扫描\r\n14,出库扫描\r\n15,装车扫描\r\n21,到货扫描\r\n22,确认扫描");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderInternalStatuses)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OrderStatusInternal_dbo.Order_OrderId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderInternalStatuses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.OrderStatusInternal_dbo.User_UserId");
            });

            modelBuilder.Entity<OrderUserAction>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.UserId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("order_user_action");

                entity.HasIndex(e => e.OrderId, "IX_dbo.OrderUserAction_OrderId");

                entity.HasIndex(e => e.UserId, "IX_dbo.OrderUserAction_UserId");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasComment("运单id（对应order表）");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("用户id（对应user表）");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasComment("日期");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderUserActions)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.OrderUserAction_dbo.Order_OrderId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderUserActions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.UserBatchAction_dbo.User_UserId");
            });

            modelBuilder.Entity<PayMethod>(entity =>
            {
                entity.ToTable("pay_method");

                entity.HasComment("支付方式")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("名称");

                entity.Property(e => e.ServiceCharge)
                    .HasPrecision(19, 4)
                    .HasColumnName("service_charge")
                    .HasComment("手续费率/%");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("类型：0->余额，1->微信");
            });

            modelBuilder.Entity<PendingUser>(entity =>
            {
                entity.ToTable("pending_user");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BelongsTo).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(40);

                entity.Property(e => e.District).HasMaxLength(40);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Province).HasMaxLength(40);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.WeChat)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PickUpLocation>(entity =>
            {
                entity.ToTable("pick_up_location");

                entity.HasComment("自提点")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BelongsToId, "fk_pick_up_location_user_belongs_to_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.BelongsToId)
                    .HasColumnType("int(11)")
                    .HasColumnName("belongs_to_id")
                    .HasComment("属高级用户id");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasComment("类别");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.DetailArea)
                    .HasMaxLength(255)
                    .HasColumnName("detail_area")
                    .HasComment("详细地址");

                entity.Property(e => e.DistrictAdditionalCost)
                    .HasPrecision(19, 4)
                    .HasColumnName("district_additional_cost")
                    .HasComment("地区附加费");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.LatAndLng)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("lat_and_lng")
                    .HasComment("坐标(经纬度)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("名称");

                entity.Property(e => e.Number)
                    .HasColumnType("int(11)")
                    .HasColumnName("number")
                    .HasComment("编号");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone")
                    .HasComment("电话");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(255)
                    .HasColumnName("postal_code")
                    .HasComment("邮编");

                entity.Property(e => e.StorageCost)
                    .HasPrecision(19, 4)
                    .HasColumnName("storage_cost")
                    .HasComment("仓储费用");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("类型：0-》平台，1-》高级用户");

                entity.Property(e => e.AreaId)
                    .IsRequired(false)
                    .HasColumnName("AreaId")
                    .HasColumnType("int(11)")
                    .HasComment("地区类型, 参见Area表");

                entity.Property(e => e.Visible)
                    .IsRequired(true)
                    .HasColumnName("Visible")
                    .HasColumnType("bit")
                    .HasComment("是否显示");

                entity.Property(e => e.Version)
                    .IsRequired(true)
                    .HasColumnName("version")
                    .HasColumnType("tinyint")
                    .HasComment("版本，从1开始");

                entity.Property(e => e.Note)
                    .IsRequired(true)
                    .HasColumnName("note")
                    .HasColumnType("text")
                    .HasComment("备注");

                entity.Property(e => e.CompanyId).HasColumnType("int");

                entity.HasOne(d => d.BelongsTo)
                    .WithMany(p => p.PickUpLocations)
                    .HasForeignKey(d => d.BelongsToId)
                    .HasConstraintName("fk_pick_up_location_user_belongs_to_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.PickUpLocations)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_PickUpLocation_CompanyId");
            });

            modelBuilder.Entity<QiniuConfig>(entity =>
            {
                entity.ToTable("qiniu_config");

                entity.HasComment("七牛云配置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AccessKey)
                    .HasColumnType("text")
                    .HasColumnName("access_key")
                    .HasComment("accessKey");

                entity.Property(e => e.Bucket)
                    .HasMaxLength(255)
                    .HasColumnName("bucket")
                    .HasComment("Bucket 识别符");

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("host")
                    .HasComment("外链域名");

                entity.Property(e => e.SecretKey)
                    .HasColumnType("text")
                    .HasColumnName("secret_key")
                    .HasComment("secretKey");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type")
                    .HasComment("空间类型");

                entity.Property(e => e.Zone)
                    .HasMaxLength(255)
                    .HasColumnName("zone")
                    .HasComment("机房");
            });

            modelBuilder.Entity<QiniuContent>(entity =>
            {
                entity.ToTable("qiniu_content");

                entity.HasComment("七牛云文件存储")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AlbumId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("album_id")
                    .HasComment("相册id");

                entity.Property(e => e.Bucket)
                    .HasMaxLength(255)
                    .HasColumnName("bucket")
                    .HasComment("Bucket 识别符");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .HasColumnName("category")
                    .HasComment("文件种类");

                entity.Property(e => e.ContentKey)
                    .HasMaxLength(255)
                    .HasColumnName("content_key")
                    .HasComment("文件存储在七牛云唯一标识");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("文件名称:用于后台展示");

                entity.Property(e => e.Size)
                    .HasMaxLength(255)
                    .HasColumnName("size")
                    .HasComment("文件大小");

                entity.Property(e => e.Suffix)
                    .HasMaxLength(255)
                    .HasColumnName("suffix")
                    .HasComment("后缀");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type")
                    .HasComment("文件类型：私有或公开");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("上传或同步的时间");
            });

            modelBuilder.Entity<QiniuWatermark>(entity =>
            {
                entity.ToTable("qiniu_watermark");

                entity.HasComment("图片、文字水印")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment(" 水印对应的店铺");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment(" 水印对应的自营管理员用户");

                entity.Property(e => e.WmImageAlpha)
                    .HasColumnType("int(11)")
                    .HasColumnName("wm_image_alpha")
                    .HasComment(" 水印图片透明度");

                entity.Property(e => e.WmImageId)
                    .HasMaxLength(255)
                    .HasColumnName("wm_image_id")
                    .HasComment(" 水印图片");

                entity.Property(e => e.WmImageOpen)
                    .HasColumnType("bit(1)")
                    .HasColumnName("wm_image_open")
                    .HasComment(" 是否开启图片水印");

                entity.Property(e => e.WmImagePos)
                    .HasMaxLength(64)
                    .HasColumnName("wm_image_pos")
                    .HasComment(" 水印图片位置");

                entity.Property(e => e.WmText)
                    .HasMaxLength(255)
                    .HasColumnName("wm_text")
                    .HasComment(" 水印文字");

                entity.Property(e => e.WmTextColor)
                    .HasMaxLength(255)
                    .HasColumnName("wm_text_color")
                    .HasComment(" 水印文字颜色");

                entity.Property(e => e.WmTextFont)
                    .HasMaxLength(255)
                    .HasColumnName("wm_text_font")
                    .HasComment(" 水印文字字体");

                entity.Property(e => e.WmTextFontSize)
                    .HasColumnType("int(11)")
                    .HasColumnName("wm_text_font_size")
                    .HasComment(" 水印文字字号");

                entity.Property(e => e.WmTextOpen)
                    .HasColumnType("bit(1)")
                    .HasColumnName("wm_text_open")
                    .HasComment(" 是否开启");

                entity.Property(e => e.WmTextPos)
                    .HasMaxLength(64)
                    .HasColumnName("wm_text_pos")
                    .HasComment(" 水印文字位置");
            });

            modelBuilder.Entity<RecordBalanceHistory>(entity =>
            {
                entity.ToTable("record_balance_history");

                entity.HasComment("录单账单统计\r\n")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.UserId, "index2");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AccountOut)
                    .HasPrecision(10, 2)
                    .HasColumnName("account_out")
                    .HasComment("出账");

                entity.Property(e => e.BalanceEntry)
                    .HasPrecision(10, 2)
                    .HasColumnName("balance_entry")
                    .HasComment("余额进账");

                entity.Property(e => e.CashEntry)
                    .HasPrecision(10, 2)
                    .HasColumnName("cash_entry")
                    .HasComment("现金进账");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.Cumulative)
                    .HasPrecision(10, 2)
                    .HasColumnName("cumulative")
                    .HasComment("累计");

                entity.Property(e => e.Date)
                    .HasMaxLength(100)
                    .HasColumnName("date")
                    .HasComment("日期")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.IsConfirm)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_confirm")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否确认：0，否，1，确认");

                entity.Property(e => e.RecordType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("record_type")
                    .HasComment("账户类型：1->月账单，2->日账单");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("更新时间");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("用户d");
            });

            modelBuilder.Entity<RecordExpressTransport>(entity =>
            {
                entity.ToTable("record_express_transport");

                entity.HasComment("录单运费模板")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.DeliveryAreaId, "IX_dbo.base_area_areaid");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.DeliveryAreaId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("delivery_area_id")
                    .HasComment("配送城市id");

                entity.Property(e => e.FirstSectionCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("first_section_cost")
                    .HasComment("第一区间费用");

                entity.Property(e => e.FirstValue)
                    .HasPrecision(10, 2)
                    .HasColumnName("first_value")
                    .HasComment("第一个值");

                entity.Property(e => e.FirstWeightCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("first_weight_cost")
                    .HasComment("首重费用");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.PickUpLocation)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("pick_up_location")
                    .HasComment("取货地点")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.SecondSectionCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("second_section_cost")
                    .HasComment("第二区间费用 ");

                entity.Property(e => e.SecondValue)
                    .HasPrecision(10, 2)
                    .HasColumnName("second_value")
                    .HasComment("第二个值");

                entity.Property(e => e.ThirdSectionCost)
                    .HasPrecision(10, 2)
                    .HasColumnName("third_section_cost")
                    .HasComment("第三区间费用");

                entity.Property(e => e.TransName)
                    .HasMaxLength(255)
                    .HasColumnName("trans_name")
                    .HasComment(" 运费模板名称");
            });

            
            modelBuilder.Entity<RingCentralCredential>(entity =>
            {
                entity.ToTable("ringcentral_credential");

                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.HasComment("RingCentral账号密码")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserId")
                    .HasComment("User ID");
                
                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("RingCentral账户名称");

                entity.Property(e => e.ClientID)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("Client ID");

                entity.Property(e => e.ClientSecret)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasComment("Client Secret");
                    
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasComment("用户名，是电话号码");
                    
                entity.Property(e => e.Extension)
                    .IsRequired()
                    .HasMaxLength(8)
                    .HasComment("Extension");
                    
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasComment("密码");
                    
                entity.Property(e => e.FromNumber)
                    .IsRequired()
                    .HasMaxLength(16)
                    .HasComment("发送方号码");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasComment("权限");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(64)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("roleid")
                    .HasColumnType("int");

                entity.Property(e => e.IsInternal)
                    .HasColumnName("is_internal")
                    .HasColumnType("bit");

                entity.Property(e => e.DisplayOrder)
                    .HasColumnName("display_order")
                    .HasColumnType("int");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("route");

                entity.HasComment("线路");

                entity.HasIndex(e => e.WarehouseId, "IX_dbo.Route_WarehouseId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("代码");

                entity.Property(e => e.Description).HasComment("描述");

                entity.Property(e => e.DisplaySequence).HasColumnType("int(11)");

                entity.Property(e => e.FixedPrice).HasPrecision(16, 2);

                entity.Property(e => e.IsDeleted).HasColumnType("tinyint(4)");

                entity.Property(e => e.IsFromChina)
                    .HasColumnType("tinyint(4)")
                    .HasComment("是从中国到加拿大线路");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("线路名");

                entity.Property(e => e.Price).IsRequired();

                entity.Property(e => e.SupportWechat)
                    .HasMaxLength(50)
                    .HasComment("客服");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasComment("类型");

                entity.Property(e => e.Type1Price).HasPrecision(16, 2);

                entity.Property(e => e.Type2Price).HasPrecision(16, 2);

                entity.Property(e => e.Type3Price).HasPrecision(16, 2);

                entity.Property(e => e.Type4Price).HasPrecision(16, 2);

                entity.Property(e => e.WarehouseId).HasColumnType("int(11)");

                entity.Property(e => e.IsRegular).HasColumnType("bit");

                entity.Property(e => e.CompanyId).HasColumnType("int");

                entity.Property(e => e.NeedInsurance).HasColumnType("bit");

                entity.Property(e => e.InsuranceRatio).HasColumnType("decimal(16,3)");

                entity.Property(e => e.VolumeWeightRatio).HasColumnType("decimal(16,3)");


                entity.Property(e => e.Destination)
                    .HasColumnType("varchar(64)")
                    .HasColumnName("Destination");

                entity.Property(e => e.Departure)
                    .HasColumnType("varchar(64)")
                    .HasColumnName("Departure");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.WarehouseId)
                    .HasConstraintName("FK_dbo.Route_dbo.Warehouse_WarehouseId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Route_CompanyId");
            });

            modelBuilder.Entity<ShopAudit>(entity =>
            {
                entity.ToTable("shop_audit");

                entity.HasComment("店铺审核记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.AuditState, "Index_audit_state");

                entity.HasIndex(e => e.Type, "Index_type");

                entity.HasIndex(e => e.ShopId, "Index_worker_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AuditContent)
                    .HasMaxLength(500)
                    .HasColumnName("audit_content")
                    .HasComment("审核内容");

                entity.Property(e => e.AuditState)
                    .HasColumnName("audit_state")
                    .HasComment("审核状态 1：审核成功，2：审核失败");

                entity.Property(e => e.AuditUser)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("audit_user")
                    .HasComment("审核人");

                entity.Property(e => e.AuditUserName)
                    .HasMaxLength(255)
                    .HasColumnName("audit_user_name")
                    .HasComment("审核人昵称");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("审核类型： 0->线上店入驻审核，1->线下店入驻审核，2->经销商入驻审核,3,->门店营业审核");
            });

            modelBuilder.Entity<ShopCategory>(entity =>
            {
                entity.ToTable("shop_category");

                entity.HasComment("店铺分类")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Name, "index_name");

                entity.HasIndex(e => e.ParentId, "index_parent_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id")
                    .HasComment("店铺分类id");

                entity.Property(e => e.CategoryDescribe)
                    .HasMaxLength(255)
                    .HasColumnName("category_describe")
                    .HasComment("分类描述");

                entity.Property(e => e.CategoryType)
                    .HasColumnName("category_type")
                    .HasComment("分类类型：0->店铺,1->门店");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image")
                    .HasComment("分类图片");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsDisplay)
                    .IsRequired()
                    .HasColumnName("is_display")
                    .HasDefaultValueSql("'1'")
                    .HasComment("是否展示：0->否，1->是");

                entity.Property(e => e.Level)
                    .HasColumnName("level")
                    .HasComment("分类等级：0->一级，1->二级,");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasComment("分类名称");

                entity.Property(e => e.ParentId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("parent_id")
                    .HasComment("分类父id");

                entity.Property(e => e.SerialNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("serial_number")
                    .HasComment("排序序号");
            });

            modelBuilder.Entity<ShopInfo>(entity =>
            {
                entity.ToTable("shop_info");

                entity.HasComment("线上店铺信息")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.AppuserId, "index_appuser_id");

                entity.HasIndex(e => e.IdentityCard, "index_identity_card");

                entity.HasIndex(e => e.ShopAreaId, "index_shop_area_id");

                entity.HasIndex(e => e.ShopCategoryId, "index_shop_category_id");

                entity.HasIndex(e => e.ShopMold, "index_shop_mold_id");

                entity.HasIndex(e => e.ShopName, "index_shop_name");

                entity.HasIndex(e => e.ShopNumber, "index_shop_number")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "index_user_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("店铺id");

                entity.Property(e => e.AppuserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("appuser_id")
                    .HasComment("app用户id");

                entity.Property(e => e.AuthorizationCertificate)
                    .HasMaxLength(255)
                    .HasColumnName("authorization_certificate")
                    .HasComment("经销商授权证书");

                entity.Property(e => e.BusinessLicense)
                    .HasMaxLength(255)
                    .HasColumnName("business_license")
                    .HasComment("营业执照");

                entity.Property(e => e.CautionMoney)
                    .HasPrecision(10, 2)
                    .HasColumnName("caution_money")
                    .HasComment("保证金");

                entity.Property(e => e.CommissionProportion)
                    .HasPrecision(18, 2)
                    .HasColumnName("commission_proportion")
                    .HasComment("提成比例");

                entity.Property(e => e.CompanyAddress)
                    .HasMaxLength(255)
                    .HasColumnName("company_address")
                    .HasComment("公司地址");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("company_name")
                    .HasComment("公司名称");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.CreditCode)
                    .HasMaxLength(255)
                    .HasColumnName("credit_code")
                    .HasComment("统一社会信用代码");

                entity.Property(e => e.HighPraiseRate)
                    .HasColumnType("float(11,2)")
                    .HasColumnName("high_praise_rate")
                    .HasDefaultValueSql("'100.00'")
                    .HasComment("好评率");

                entity.Property(e => e.IdentityCard)
                    .HasColumnName("identity_card")
                    .HasComment("身份证");

                entity.Property(e => e.IdentityCardBack)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card_back")
                    .HasComment("身份证背面");

                entity.Property(e => e.IdentityCardFront)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card_front")
                    .HasComment("身份证正面");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsOperate)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_operate")
                    .HasDefaultValueSql("b'1'")
                    .HasComment("是否营业：0->否，1->是");

                entity.Property(e => e.IsRecommend)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_recommend")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否推荐:0->否，1->是");

                entity.Property(e => e.QrPath)
                    .HasMaxLength(100)
                    .HasColumnName("qr_path")
                    .HasComment("二维码");

                entity.Property(e => e.RecommenderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("recommender_id")
                    .HasComment("推荐人id");

                entity.Property(e => e.ShopAddress)
                    .HasMaxLength(255)
                    .HasColumnName("shop_address")
                    .HasComment("店铺地址");

                entity.Property(e => e.ShopArea)
                    .HasMaxLength(255)
                    .HasColumnName("shop_area")
                    .HasComment("店铺地区");

                entity.Property(e => e.ShopAreaId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_area_id")
                    .HasComment("店铺地区id");

                entity.Property(e => e.ShopAuditContent)
                    .HasMaxLength(500)
                    .HasColumnName("shop_audit_content")
                    .HasComment("审核内容");

                entity.Property(e => e.ShopAuditState)
                    .IsRequired()
                    .HasColumnName("shop_audit_state")
                    .HasDefaultValueSql("'-1'")
                    .HasComment("审核状态：0->待审核，1->已通过，2->未通过");

                entity.Property(e => e.ShopBackgroundImage)
                    .HasMaxLength(255)
                    .HasColumnName("shop_background_image")
                    .HasComment("店铺背景图");

                entity.Property(e => e.ShopCategoryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_category_id")
                    .HasComment("店铺分类id");

                entity.Property(e => e.ShopEffectiveTime)
                    .HasColumnType("datetime")
                    .HasColumnName("shop_effective_time")
                    .HasComment("店铺有效时间");

                entity.Property(e => e.ShopLnglat)
                    .HasMaxLength(255)
                    .HasColumnName("shop_lnglat")
                    .HasComment("店铺经纬度");

                entity.Property(e => e.ShopLogo)
                    .HasMaxLength(255)
                    .HasColumnName("shop_logo")
                    .HasComment("店铺logo");

                entity.Property(e => e.ShopMold)
                    .HasColumnName("shop_mold")
                    .HasComment("店铺类型：0->线上店，1->自营店");

                entity.Property(e => e.ShopName)
                    .HasColumnName("shop_name")
                    .HasComment("店铺名称");

                entity.Property(e => e.ShopNumber)
                    .IsRequired()
                    .HasColumnName("shop_number")
                    .HasComment("店铺编号");

                entity.Property(e => e.ShopPhone)
                    .HasMaxLength(255)
                    .HasColumnName("shop_phone")
                    .HasComment("店铺电话");

                entity.Property(e => e.ShopProperty)
                    .HasColumnName("shop_property")
                    .HasDefaultValueSql("'0'")
                    .HasComment("店铺属性：0->个人，1->企业");

                entity.Property(e => e.ShopScope)
                    .HasMaxLength(255)
                    .HasColumnName("shop_scope")
                    .HasComment("经营范围");

                entity.Property(e => e.ShopServices)
                    .HasMaxLength(255)
                    .HasColumnName("shop_services")
                    .HasComment("商家服务");

                entity.Property(e => e.ShopkeeperName)
                    .HasMaxLength(255)
                    .HasColumnName("shopkeeper_name")
                    .HasComment("姓名/法人名称");

                entity.Property(e => e.ShopkeeperPhone)
                    .HasMaxLength(255)
                    .HasColumnName("shopkeeper_phone")
                    .HasComment("店主电话");

                entity.Property(e => e.TotalCommentLevel)
                    .HasColumnType("int(11)")
                    .HasColumnName("total_comment_level")
                    .HasComment("总星级数");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("后台户id");

                entity.Property(e => e.Volume)
                    .HasColumnType("int(11)")
                    .HasColumnName("volume")
                    .HasComment("成交量");
            });

            modelBuilder.Entity<ShopOffline>(entity =>
            {
                entity.ToTable("shop_offline");

                entity.HasComment("线下店铺信息")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("会员id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("area_id")
                    .HasComment("省市区id");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(50)
                    .HasColumnName("area_name")
                    .HasComment("省市区名称");

                entity.Property(e => e.AuditContent)
                    .HasMaxLength(500)
                    .HasColumnName("audit_content")
                    .HasComment("审核内容");

                entity.Property(e => e.AuditState)
                    .IsRequired()
                    .HasColumnName("audit_state")
                    .HasDefaultValueSql("'-1'")
                    .HasComment("审核状态：0->待审核，1->已通过，2->未通过");

                entity.Property(e => e.BusinessLicense)
                    .HasMaxLength(255)
                    .HasColumnName("business_license")
                    .HasComment("营业执照");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("company_name")
                    .HasComment("公司名称");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.CreditCode)
                    .HasMaxLength(255)
                    .HasColumnName("credit_code")
                    .HasComment("统一社会信用代码");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time")
                    .HasComment("结束时间");

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card")
                    .HasComment("身份证");

                entity.Property(e => e.IdentityCardBack)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card_back")
                    .HasComment("身份证背面");

                entity.Property(e => e.IdentityCardFront)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card_front")
                    .HasComment("身份证正面");

                entity.Property(e => e.IsOperate)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_operate")
                    .HasDefaultValueSql("b'1'")
                    .HasComment("是否营业：0->否，1->是");

                entity.Property(e => e.IsRecommend)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_recommend")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否推荐:0,否,1,是");

                entity.Property(e => e.JuridicalPerson)
                    .HasMaxLength(255)
                    .HasColumnName("juridical_person")
                    .HasComment("姓名/法人名称");

                entity.Property(e => e.Latitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("latitude")
                    .HasComment("店铺纬度");

                entity.Property(e => e.Longitude)
                    .HasPrecision(9, 6)
                    .HasColumnName("longitude")
                    .HasComment("店铺经度");

                entity.Property(e => e.LowerPictures)
                    .HasMaxLength(255)
                    .HasColumnName("lower_pictures")
                    .HasComment("门店照片(下方展示)");

                entity.Property(e => e.OfflineDetails)
                    .HasColumnName("offline_details")
                    .HasComment("门店详细描述");

                entity.Property(e => e.OperateAuditState)
                    .IsRequired()
                    .HasColumnName("operate_audit_state")
                    .HasDefaultValueSql("'-1'")
                    .HasComment("营业审核状态 -1.未提交营业信息，0：未审核，1：审核成功，2：审核失败");

                entity.Property(e => e.OperateTime)
                    .HasMaxLength(255)
                    .HasColumnName("operate_time")
                    .HasComment("营业时间");

                entity.Property(e => e.OtherCertificate)
                    .HasMaxLength(255)
                    .HasColumnName("other_certificate")
                    .HasComment("其他证件");

                entity.Property(e => e.QrPath)
                    .HasMaxLength(100)
                    .HasColumnName("qr_path")
                    .HasComment("二维码");

                entity.Property(e => e.ShopAddress)
                    .HasMaxLength(255)
                    .HasColumnName("shop_address")
                    .HasComment("店铺地址");

                entity.Property(e => e.ShopCategoryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_category_id")
                    .HasComment("店铺分类");

                entity.Property(e => e.ShopContacts)
                    .HasMaxLength(255)
                    .HasColumnName("shop_contacts")
                    .HasComment("店铺联系人");

                entity.Property(e => e.ShopLogo)
                    .HasMaxLength(255)
                    .HasColumnName("shop_logo")
                    .HasComment("店铺logo");

                entity.Property(e => e.ShopName)
                    .HasMaxLength(255)
                    .HasColumnName("shop_name")
                    .HasComment("店铺名称");

                entity.Property(e => e.ShopNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("shop_number")
                    .HasComment("店铺编号");

                entity.Property(e => e.ShopPhone)
                    .HasMaxLength(255)
                    .HasColumnName("shop_phone")
                    .HasComment("店铺电话");

                entity.Property(e => e.ShopProperty)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("shop_property")
                    .HasComment("店铺属性：0->个人，1->企业");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time")
                    .HasComment("开店时间");

                entity.Property(e => e.UpperPictures)
                    .HasMaxLength(255)
                    .HasColumnName("upper_pictures")
                    .HasComment("门店图片(上方展示)");
            });

            modelBuilder.Entity<ShopReceiveAddress>(entity =>
            {
                entity.ToTable("shop_receive_address");

                entity.HasComment("店铺收货地址")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.ShopId, "index_shop_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.AreaCode)
                    .HasMaxLength(10)
                    .HasColumnName("area_code")
                    .HasComment("邮编");

                entity.Property(e => e.Consignee)
                    .HasMaxLength(20)
                    .HasColumnName("consignee")
                    .HasComment("收货人");

                entity.Property(e => e.DetailArea)
                    .HasMaxLength(255)
                    .HasColumnName("detail_area")
                    .HasComment("详细地址");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(20)
                    .HasColumnName("mobile")
                    .HasComment("电话号码");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("type")
                    .HasComment("类型：0->线上店/自营店，1->供应商");
            });

            modelBuilder.Entity<ShopService>(entity =>
            {
                entity.ToTable("shop_service");

                entity.HasComment("商家服务")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.ServiceImages)
                    .HasMaxLength(500)
                    .HasColumnName("service_images")
                    .HasComment("服务图片");

                entity.Property(e => e.ServiceName)
                    .HasMaxLength(500)
                    .HasColumnName("service_name")
                    .HasComment("服务名称");
            });

            modelBuilder.Entity<ShopSupplier>(entity =>
            {
                entity.ToTable("shop_supplier");

                entity.HasComment("供应商信息")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address")
                    .HasComment("详细地址");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("会员id");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("area_id")
                    .HasComment("省市区id");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(255)
                    .HasColumnName("area_name")
                    .HasComment("省市区名称");

                entity.Property(e => e.AuditContent)
                    .HasMaxLength(255)
                    .HasColumnName("audit_content")
                    .HasComment("审核内容");

                entity.Property(e => e.AuditState)
                    .IsRequired()
                    .HasColumnName("audit_state")
                    .HasDefaultValueSql("'-1'")
                    .HasComment("审核状态：0->待审核，1->已通过，2->未通过");

                entity.Property(e => e.BusinessLicense)
                    .HasMaxLength(255)
                    .HasColumnName("business_license")
                    .HasComment("营业执照");

                entity.Property(e => e.CommissionProportion)
                    .HasPrecision(18, 2)
                    .HasColumnName("commission_proportion")
                    .HasComment("提成比例");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(255)
                    .HasColumnName("company_name")
                    .HasComment("公司名称");

                entity.Property(e => e.Contacts)
                    .HasMaxLength(255)
                    .HasColumnName("contacts")
                    .HasComment("供应商联系人");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.CreditCode)
                    .HasMaxLength(255)
                    .HasColumnName("credit_code")
                    .HasComment("统一社会信用代码");

                entity.Property(e => e.IdentityCard)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card")
                    .HasComment("身份证");

                entity.Property(e => e.IdentityCardBack)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card_back")
                    .HasComment("身份证背面");

                entity.Property(e => e.IdentityCardFront)
                    .HasMaxLength(255)
                    .HasColumnName("identity_card_front")
                    .HasComment("身份证正面");

                entity.Property(e => e.IsOperate)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_operate")
                    .HasDefaultValueSql("b'1'")
                    .HasComment("是否营业：0->否，1->是");

                entity.Property(e => e.JuridicalPerson)
                    .HasMaxLength(255)
                    .HasColumnName("juridical_person")
                    .HasComment("法人名称");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("供应商名称");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("number")
                    .HasComment("供应商编号");

                entity.Property(e => e.OtherCertificate)
                    .HasMaxLength(255)
                    .HasColumnName("other_certificate")
                    .HasComment("其他证件");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone")
                    .HasComment("供应商电话");

                entity.Property(e => e.RecommenderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("recommender_id")
                    .HasComment("推荐人id");
            });

            modelBuilder.Entity<SMSLog>(entity =>
            {
                entity.ToTable("sms_log");

                entity.HasComment("SMS日志");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.BatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次id（对应batch表）");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasComment("操作用户id（对应user表）");

                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .HasComment("日志信息");

                entity.Property(e => e.Content)
                    .HasColumnType("text")
                    .HasComment("短信内容");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp")
                    .HasComment("日志时间");
            });

            modelBuilder.Entity<SmsMessageConfig>(entity =>
            {
                entity.ToTable("sms_message_config");

                entity.HasComment("短信设置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.SmsAccesskeyId)
                    .HasMaxLength(255)
                    .HasColumnName("sms_accesskeyId")
                    .HasComment("阿里云accesskeyId");

                entity.Property(e => e.SmsEnbale)
                    .HasColumnType("bit(1)")
                    .HasColumnName("sms_enbale")
                    .HasComment(" 短信平台是否开启");

                entity.Property(e => e.SmsSecret)
                    .HasMaxLength(255)
                    .HasColumnName("sms_secret")
                    .HasComment("阿里云accessKeySecret");

                entity.Property(e => e.SmsSign)
                    .HasMaxLength(32)
                    .HasColumnName("sms_sign")
                    .HasComment("短信签名(需要阿里云后台审核)");

                entity.Property(e => e.SmsTestPhone)
                    .HasMaxLength(255)
                    .HasColumnName("sms_test_phone")
                    .HasComment(" 短信发送测试");
            });

            modelBuilder.Entity<SmsMessageHistory>(entity =>
            {
                entity.ToTable("sms_message_history");

                entity.HasComment("发送短信历史")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasComment(" 短信内容");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.TemplateCode)
                    .HasMaxLength(32)
                    .HasColumnName("template_code")
                    .HasComment("模板Code");

                entity.Property(e => e.Title)
                    .HasMaxLength(32)
                    .HasColumnName("title")
                    .HasComment("标题");

                entity.Property(e => e.ToMobile)
                    .HasMaxLength(11)
                    .HasColumnName("to_mobile")
                    .HasComment("发短信对象");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(64)")
                    .HasColumnName("type")
                    .HasComment(" 短信类型(0验证码，1短信通知，2系统通知)");
            });

            modelBuilder.Entity<SmsMessageTemplate>(entity =>
            {
                entity.ToTable("sms_message_template");

                entity.HasComment("短信模板")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.Classification)
                    .HasColumnName("classification")
                    .HasComment("模板分类");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("code")
                    .HasComment("短信模板CODE");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .HasComment("模板内容");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.EnabledMessage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled_message")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否启用短信 :0否 1是");

                entity.Property(e => e.EnabledNews)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled_news")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否启用推送 :0否 1是");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .HasComment("模板名称");

                entity.Property(e => e.Remark)
                    .HasMaxLength(32)
                    .HasColumnName("remark")
                    .HasComment("短信模板申请说明。请在申请说明中描述您的业务使用场景，长度为1~100个字符。");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("模板类型：0：验证码。1：短信通知。2：推广短信。");
            });

            modelBuilder.Entity<SubscribeHistory>(entity =>
            {
                entity.ToTable("subscribe_history");

                entity.HasComment("预约记录");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address")
                    .HasComment("取件地址");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasComment("姓名");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone")
                    .HasComment("电话");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("user_id")
                    .HasComment("用户id");
            });

            modelBuilder.Entity<SysAppuser>(entity =>
            {
                entity.ToTable("sys_appuser");

                entity.HasComment("app用户")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Email, "index_email")
                    .IsUnique();

                entity.HasIndex(e => e.NickName, "index_nick_name");

                entity.HasIndex(e => e.Phone, "index_phone")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "index_username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("area_id")
                    .HasComment("所在地");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .HasComment("头像");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email")
                    .HasComment("邮箱号码")
                    .UseCollation("utf32_general_ci")
                    .HasCharSet("utf32");

                entity.Property(e => e.Enabled)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("enabled")
                    .HasComment("状态：1启用、0禁用");

                entity.Property(e => e.IdCard)
                    .HasMaxLength(255)
                    .HasColumnName("id_card")
                    .HasComment("身份证");

                entity.Property(e => e.IdCardBack)
                    .HasMaxLength(255)
                    .HasColumnName("id_card_back")
                    .HasComment("身份证背面");

                entity.Property(e => e.IdCardFront)
                    .HasMaxLength(255)
                    .HasColumnName("id_card_front")
                    .HasComment("身份证正面");

                entity.Property(e => e.IsCertification)
                    .HasColumnType("tinyint(4) unsigned zerofill")
                    .HasColumnName("is_certification")
                    .HasDefaultValueSql("'0003'")
                    .HasComment("是否认证：0未审核、1通过审核、2审核驳回、3未认证");

                entity.Property(e => e.LastPasswordResetTime)
                    .HasColumnType("datetime")
                    .HasColumnName("last_password_reset_time")
                    .HasComment("最后修改密码的日期");

                entity.Property(e => e.NickName)
                    .HasMaxLength(50)
                    .HasColumnName("nick_name")
                    .HasComment("昵称");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password")
                    .HasComment("密码");

                entity.Property(e => e.PayPassword)
                    .HasMaxLength(100)
                    .HasColumnName("pay_password")
                    .HasComment("支付密码");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone")
                    .HasComment("手机号码");

                entity.Property(e => e.QrPath)
                    .HasMaxLength(100)
                    .HasColumnName("qr_path")
                    .HasComment("二维码");

                entity.Property(e => e.RealName)
                    .HasMaxLength(32)
                    .HasColumnName("real_name")
                    .HasComment("姓名");

                entity.Property(e => e.Role)
                    .HasMaxLength(200)
                    .HasColumnName("role")
                    .HasComment("角色：会员->ROLE_USER，线上店->ROLE_ONLINE，线下店->ROLE_OFFLINE，供应商->ROLE_SUPPLIER,自营店铺->ROLE_AUTARKY");

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .HasColumnName("sex")
                    .IsFixedLength(true)
                    .HasComment("性别");

                entity.Property(e => e.Stage)
                    .HasColumnType("int(11)")
                    .HasColumnName("stage")
                    .HasComment("入驻：0->已完成，100->线上店-已选身份，101->线上店-已填个人基本信息，102->线上店-已填企业基本信息，103->线上店-已填认证信息，200->线下店-已选身份，201->线下店-已填基本信息，202->线下店-已填认证信息，300->供应商-已选身份，301->供应商-已填基本信息，302->供应商-已填认证信息");

                entity.Property(e => e.UserNumber)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("user_number")
                    .HasComment("用户编号");

                entity.Property(e => e.UserType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("user_type")
                    .HasComment("用户类型：0->会员，1->线上店，2->线下店，3->供应商,4->自营店铺");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .HasColumnName("username")
                    .HasComment("用户名");
            });

            modelBuilder.Entity<SupportUser>(entity =>
            {
                entity.ToTable("support_user");

                entity.HasComment("客服用户")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");
                
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserId")
                    .HasComment("User ID");

                entity.Property(e => e.WeChat)
                    .HasMaxLength(64)
                    .HasColumnName("WeChat")
                    .HasComment("微信号码");

                entity.Property(e => e.Warehouse)
                    .HasMaxLength(128)
                    .HasColumnName("Warehouse")
                    .HasComment("仓库");
                
                entity.HasOne(d => d.User)
                    .WithMany(p => p.SupportUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_support_user_user_id");
            });

            modelBuilder.Entity<SysAppuserRecommend>(entity =>
            {
                entity.ToTable("sys_appuser_recommend");

                entity.HasComment("用户推荐表")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("用户id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.InviteCode)
                    .HasMaxLength(100)
                    .HasColumnName("invite_code")
                    .HasComment("邀请码");

                entity.Property(e => e.InviteUrl)
                    .HasMaxLength(255)
                    .HasColumnName("invite_url")
                    .HasComment("邀请链接");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否删除");

                entity.Property(e => e.ParentUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("parent_user_id")
                    .HasDefaultValueSql("'0'")
                    .HasComment("推荐人id");

                entity.Property(e => e.QrPath)
                    .HasMaxLength(100)
                    .HasColumnName("qr_path")
                    .HasComment("二维码");
            });

            modelBuilder.Entity<SysBankCard>(entity =>
            {
                entity.ToTable("sys_bank_card");

                entity.HasComment("银行卡")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("app_user_id")
                    .HasComment("登录用户");

                entity.Property(e => e.BankCardBelongs)
                    .HasMaxLength(32)
                    .HasColumnName("bank_card_belongs")
                    .HasComment("银行卡所属行");

                entity.Property(e => e.BankCardNumber)
                    .HasMaxLength(32)
                    .HasColumnName("bank_card_number")
                    .HasComment("银行卡号");

                entity.Property(e => e.BankCardPerson)
                    .HasMaxLength(255)
                    .HasColumnName("bank_card_person")
                    .HasComment("银行卡开户人");

                entity.Property(e => e.BankCardType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("bank_card_type")
                    .HasComment("银行卡类型(0储蓄卡，1信用卡)");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.IdCard)
                    .HasMaxLength(32)
                    .HasColumnName("id_card")
                    .HasComment("身份证");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");
            });

            modelBuilder.Entity<SysFavorite>(entity =>
            {
                entity.ToTable("sys_favorite");

                entity.HasComment("商品、店铺、视频收藏")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("域模型id，这里为自增类型");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("收藏人id");

                entity.Property(e => e.AppUserName)
                    .HasMaxLength(255)
                    .HasColumnName("app_user_name")
                    .HasComment("收藏人姓名");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.FavoriteId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("favorite_id")
                    .HasComment("收藏视频/商品/店铺id");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type")
                    .HasComment("类型(0:视频，1商品，2:店铺)");
            });

            modelBuilder.Entity<SysInterfaceConfig>(entity =>
            {
                entity.ToTable("sys_interface_config");

                entity.HasComment("接口配置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id，这里为自增类型");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.IsQqConnection)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_qq_connection")
                    .HasComment("是否启用QQ互联功能\r\n\r\n是否启用QQ互联功能\r\n\r\n");

                entity.Property(e => e.IsWechatConnection)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_wechat_connection")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否启用微信互联，开启后可使用微信账户登录甲家装饰网站系统(0：否 1：是)");

                entity.Property(e => e.IsWeiboConnection)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_weibo_connection")
                    .HasComment("是否启用微博互联功能");

                entity.Property(e => e.QqApplicationIdentiy)
                    .HasMaxLength(255)
                    .HasColumnName("qq_application_identiy")
                    .HasComment("qq应用标识");

                entity.Property(e => e.QqApplicationSecretKey)
                    .HasMaxLength(255)
                    .HasColumnName("qq_application_secret_key")
                    .HasComment("qq应用密钥");

                entity.Property(e => e.QqDomain)
                    .HasMaxLength(255)
                    .HasColumnName("qq_domain")
                    .HasComment("qq域名验证消息");

                entity.Property(e => e.WechatAppid)
                    .HasMaxLength(64)
                    .HasColumnName("wechat_appid")
                    .HasComment("微信appid");

                entity.Property(e => e.WechatAppsecret)
                    .HasMaxLength(64)
                    .HasColumnName("wechat_appsecret")
                    .HasComment("微信AppSecret");

                entity.Property(e => e.WeiboApplicationIdentiy)
                    .HasMaxLength(255)
                    .HasColumnName("weibo_application_identiy")
                    .HasComment("微博应用标识");

                entity.Property(e => e.WeiboApplicationSecretKey)
                    .HasMaxLength(255)
                    .HasColumnName("weibo_application_secret_key")
                    .HasComment("微博应用密钥");

                entity.Property(e => e.WeiboDomain)
                    .HasMaxLength(255)
                    .HasColumnName("weibo_domain")
                    .HasComment("微博域名验证消息");
            });

            modelBuilder.Entity<SysInvoiceQualification>(entity =>
            {
                entity.ToTable("sys_invoice_qualification");

                entity.HasComment("增票资质")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("app_user_id")
                    .HasComment("登录用户");

                entity.Property(e => e.BankAccount)
                    .HasMaxLength(100)
                    .HasColumnName("bank_account")
                    .HasComment("开户银行账户");

                entity.Property(e => e.BankName)
                    .HasMaxLength(100)
                    .HasColumnName("bank_name")
                    .HasComment("开户行");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.EntityName)
                    .HasMaxLength(100)
                    .HasColumnName("entity_name")
                    .HasComment("单位名称");

                entity.Property(e => e.IsAuditState)
                    .HasColumnName("is_audit_state")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否审核：0未审核、1通过审核、2审核驳回");

                entity.Property(e => e.IsDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_default")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否设置默认:0否 1是");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.RegisterAddress)
                    .HasMaxLength(100)
                    .HasColumnName("register_address")
                    .HasComment("注册地址");

                entity.Property(e => e.RegisterPhone)
                    .HasMaxLength(100)
                    .HasColumnName("register_phone")
                    .HasComment("注册电话");

                entity.Property(e => e.TaxpayersNum)
                    .HasMaxLength(100)
                    .HasColumnName("taxpayers_num")
                    .HasComment("纳税人识别号");
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PRIMARY");

                entity.ToTable("sys_menu");

                entity.HasComment("系统菜单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Pid, "inx_pid");

                entity.HasIndex(e => e.Name, "uniq_name")
                    .IsUnique();

                entity.HasIndex(e => e.Title, "uniq_title");

                entity.Property(e => e.MenuId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("menu_id")
                    .HasComment("ID");

                entity.Property(e => e.Cache)
                    .HasColumnType("bit(1)")
                    .HasColumnName("cache")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("缓存");

                entity.Property(e => e.Component)
                    .HasMaxLength(255)
                    .HasColumnName("component")
                    .HasComment("组件");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(255)
                    .HasColumnName("create_by")
                    .HasComment("创建者");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("创建日期");

                entity.Property(e => e.Hidden)
                    .HasColumnType("bit(1)")
                    .HasColumnName("hidden")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("隐藏");

                entity.Property(e => e.IFrame)
                    .HasColumnType("bit(1)")
                    .HasColumnName("i_frame")
                    .HasComment("是否外链");

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .HasColumnName("icon")
                    .HasComment("图标");

                entity.Property(e => e.MenuSort)
                    .HasColumnType("int(5)")
                    .HasColumnName("menu_sort")
                    .HasComment("排序");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasComment("组件名称");

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .HasColumnName("path")
                    .HasComment("链接地址");

                entity.Property(e => e.Permission)
                    .HasMaxLength(255)
                    .HasColumnName("permission")
                    .HasComment("权限");

                entity.Property(e => e.Pid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("pid")
                    .HasComment("上级菜单ID");

                entity.Property(e => e.SubCount)
                    .HasColumnType("int(5)")
                    .HasColumnName("sub_count")
                    .HasDefaultValueSql("'0'")
                    .HasComment("子菜单数目");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasComment("菜单标题");

                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type")
                    .HasComment("菜单类型");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(255)
                    .HasColumnName("update_by")
                    .HasComment("更新者");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<SysOwnerAudit>(entity =>
            {
                entity.ToTable("sys_owner_audit");

                entity.HasComment("业主实名认证审核记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.AuditState, "Index_audit_state");

                entity.HasIndex(e => e.ClientId, "Index_worker_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("业主审核ID");

                entity.Property(e => e.AuditContent)
                    .HasMaxLength(255)
                    .HasColumnName("audit_content")
                    .HasComment("审核内容");

                entity.Property(e => e.AuditState)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("audit_state")
                    .HasComment("审核状态 1：审核成功，2：审核失败");

                entity.Property(e => e.ClientId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("client_id")
                    .HasComment("业主ID");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("审核人");
            });

            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PRIMARY");

                entity.ToTable("sys_role");

                entity.HasComment("角色表")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Name, "role_name_index")
                    .IsUnique();

                entity.Property(e => e.RoleId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("role_id")
                    .HasComment("ID");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(255)
                    .HasColumnName("create_by")
                    .HasComment("创建者");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("创建日期");

                entity.Property(e => e.DataScope)
                    .HasMaxLength(255)
                    .HasColumnName("data_scope")
                    .HasComment("数据权限");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description")
                    .HasComment("描述");

                entity.Property(e => e.Level)
                    .HasColumnType("int(255)")
                    .HasColumnName("level")
                    .HasComment("角色级别：1->超级管理员，2->平台管理员，3->线上店店主/自营店店主，4->线上店店员/自营店店员");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasComment("名称");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("角色类型：0->超级管理员，1->平台管理员，2->线上店店主，3->自营店店主，4->线上店店员，5->自营店店员");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(255)
                    .HasColumnName("update_by")
                    .HasComment("更新者");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("更新时间");

                entity.Property(e => e.Code)
                    .HasColumnType("varchar(32)")
                    .HasColumnName("code")
                    .HasComment("权限编号");
            });

            modelBuilder.Entity<SysRolesMenu>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("sys_roles_menus");

                entity.HasComment("角色菜单关联")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.RoleId, "FKcngg2qadojhi3a651a5adkvbq");

                entity.Property(e => e.MenuId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("menu_id")
                    .HasComment("菜单ID");

                entity.Property(e => e.RoleId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("role_id")
                    .HasComment("角色ID");
            });

            modelBuilder.Entity<SysShippingAddress>(entity =>
            {
                entity.ToTable("sys_shipping_address");

                entity.HasComment("收货地址")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("ID");

                entity.Property(e => e.AddressType)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'")
                    .HasComment("地址类型：0->中国，1->加拿大");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(32)")
                    .HasColumnName("app_user_id")
                    .HasComment("登录用户");

                entity.Property(e => e.AreaId)
                    .HasColumnType("bigint(11)")
                    .HasColumnName("area_id")
                    .HasComment("省市区");

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .HasComment("市")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Consignee)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("consignee")
                    .HasComment("收货人");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建日期");

                entity.Property(e => e.DetailArea)
                    .HasMaxLength(200)
                    .HasColumnName("detail_area")
                    .HasComment("详细地址");

                entity.Property(e => e.District)
                    .HasMaxLength(20)
                    .HasComment("区")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.IdCardBackUrl)
                    .HasComment("身份证反面照片链接")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.IdCardFrontUrl)
                    .HasComment("身份证正面照片链接")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.IdCardNumber)
                    .HasMaxLength(64)
                    .HasComment("身份证号码");

                entity.Property(e => e.IsDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_default")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否设置默认地址:0否 1是");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否删除：0->否，1->是");

                entity.Property(e => e.LatAndLng)
                    .HasMaxLength(100)
                    .HasColumnName("lat_and_lng")
                    .HasComment("经纬度")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(64)
                    .HasColumnName("mobile")
                    .HasComment("电话号码");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .HasComment("邮编")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Province)
                    .HasMaxLength(20)
                    .HasComment("省")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.WeChat)
                    .HasMaxLength(100)
                    .HasComment("微信")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("sys_user");

                entity.HasComment("系统用户")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Avatar, "FKpq2dhypk2qgt68nauh2by22jb");

                entity.HasIndex(e => e.Email, "UK_kpubos9gc2cvtkb0thktkbkes")
                    .IsUnique();

                entity.HasIndex(e => e.Enabled, "inx_enabled");

                entity.HasIndex(e => e.Username, "username");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("ID");

                entity.Property(e => e.Avatar)
                    .HasColumnName("avatar")
                    .HasComment("头像");

                entity.Property(e => e.CreateBy)
                    .HasMaxLength(255)
                    .HasColumnName("create_by")
                    .HasComment("创建者");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasComment("创建日期");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasComment("邮箱");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("enabled")
                    .HasComment("状态：1启用、0禁用");

                entity.Property(e => e.Gender)
                    .HasMaxLength(2)
                    .HasColumnName("gender")
                    .HasComment("性别");

                entity.Property(e => e.IsAdmin)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_admin")
                    .HasDefaultValueSql("b'0'")
                    .HasComment("是否为admin账号");

                entity.Property(e => e.IsDel)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除");

                entity.Property(e => e.NickName)
                    .HasMaxLength(255)
                    .HasColumnName("nick_name")
                    .HasComment("昵称");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password")
                    .HasComment("密码");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone")
                    .HasComment("手机号码");

                entity.Property(e => e.PwdResetTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pwd_reset_time")
                    .HasComment("修改密码的时间");

                entity.Property(e => e.ShopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shop_id")
                    .HasComment("店铺id");

                entity.Property(e => e.UpdateBy)
                    .HasMaxLength(255)
                    .HasColumnName("update_by")
                    .HasComment("更新着");

                entity.Property(e => e.UpdateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("update_time")
                    .HasComment("更新时间");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasComment("用户名");
            });

            modelBuilder.Entity<SysUsersRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleCode })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("sys_users_roles");

                entity.HasComment("用户角色关联")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.RoleId, "FKq4eq273l04bpu4efj0jd0jb98");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id")
                    .HasComment("用户ID");

                entity.Property(e => e.RoleId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("role_id")
                    .HasComment("角色ID");

                entity.Property(e => e.RoleCode)
                    .HasColumnType("varchar(32)")
                    .HasColumnName("role_code")
                    .HasComment("角色 code");
            });

            modelBuilder.Entity<SysWechatUserinfo>(entity =>
            {
                entity.ToTable("sys_wechat_userinfo");

                entity.HasComment("微信用户信息")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.AppUserId, "index_appUserId")
                    .IsUnique();

                entity.HasIndex(e => e.Openid, "index_openid")
                    .IsUnique();

                entity.HasIndex(e => e.Unionid, "index_unionid");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.AppUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("app_user_id")
                    .HasComment("app用户id");

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .HasColumnName("city")
                    .HasComment("城市");

                entity.Property(e => e.Country)
                    .HasMaxLength(100)
                    .HasColumnName("country")
                    .HasComment("国家");

                entity.Property(e => e.Headimgurl)
                    .HasMaxLength(255)
                    .HasColumnName("headimgurl")
                    .HasComment("用户头像url");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(100)
                    .HasColumnName("nickname")
                    .HasComment("用户昵称");

                entity.Property(e => e.Openid)
                    .HasMaxLength(100)
                    .HasColumnName("openid")
                    .HasComment("用户标识");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone")
                    .HasComment("app用户电话(弃用)");

                entity.Property(e => e.Privilege)
                    .HasMaxLength(100)
                    .HasColumnName("privilege")
                    .HasComment("用户特权信息");

                entity.Property(e => e.Province)
                    .HasMaxLength(100)
                    .HasColumnName("province")
                    .HasComment("省");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasComment("性别：1->男性，2->女性");

                entity.Property(e => e.Unionid)
                    .HasMaxLength(100)
                    .HasColumnName("unionid")
                    .HasComment("同一用户的唯一值");
            });

            modelBuilder.Entity<SystemPhoto>(entity =>
            {
                entity.ToTable("system_photo");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Type).HasColumnType("int(11)");

                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.ToTable("system_settings");

                entity.HasComment("录单系统设置");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.BatchConfirmMessage).IsRequired();

                entity.Property(e => e.EnableProfileUpdate).HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.ToTable("todo_item");

                entity.HasComment("待办事项")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.CreatedByUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("CreatedByUserId")
                    .HasComment("负责人Id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("DateCreated")
                    .HasComment("创建日期");

                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .HasColumnName("Message")
                    .HasComment("待办事项");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("Comment")
                    .HasComment("处理意见");

                entity.Property(e => e.Resolution)
                    .HasColumnType("text")
                    .HasColumnName("Resolution")
                    .HasComment("处理结果");

                entity.Property(e => e.OrderInfo)
                    .HasColumnType("text")
                    .HasColumnName("OrderInfo")
                    .HasComment("运单信息");

                entity.Property(e => e.CustomerInfo)
                    .HasColumnType("text")
                    .HasColumnName("CustomerInfo")
                    .HasComment("客户信息");

                entity.Property(e => e.NotifyCustomer)
                    .HasColumnType("bit")
                    .HasColumnName("NotifyCustomer")
                    .HasComment("是否通知客户");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint")
                    .HasColumnName("Status")
                    .HasComment("状态");

                entity
                    .HasOne(e => e.CreatedBy)
                    .WithMany(c => c.TodoItems)
                    .HasForeignKey(c => c.CreatedByUserId)
                    .HasConstraintName("fk_todo_item_created_by_user_id_user_id");
            });

            modelBuilder.Entity<TodoItemAssignee>(entity =>
            {
                entity.ToTable("todo_item_assignee");

                entity.HasComment("待办事项经办人")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserId");

                entity.Property(e => e.ItemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ItemId");

                entity.HasIndex(e => e.UserId, "IX_dbo.TodoItemAssignee_UserId");

                entity.HasIndex(e => e.ItemId, "IX_dbo.TodoItemAssignee_ItemId");

                entity.HasOne(d => d.TodoItem)
                    .WithMany(p => p.TodoItemAssignees)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_todo_item_assignee_item_id_item_id");

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.TodoItemAssignees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("fk_todo_item_assignee_user_id_user_id");
            });

            modelBuilder.Entity<TodoItemCustomer>(entity =>
            {
                entity.ToTable("todo_item_customer");

                entity.HasComment("待办事项客户")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserId");

                entity.Property(e => e.ItemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ItemId");

                entity.HasIndex(e => e.UserId, "IX_dbo.TodoItemCustomer_UserId");

                entity.HasIndex(e => e.ItemId, "IX_dbo.TodoItemCustomer_ItemId");

                entity.HasOne(d => d.TodoItem)
                    .WithMany(p => p.TodoItemCustomers)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_todo_item_customer_item_id_item_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TodoItemCustomers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("fk_todo_item_customer_user_id_user_id");
            });

            modelBuilder.Entity<TodoItemOrder>(entity =>
            {
                entity.ToTable("todo_item_order");

                entity.HasComment("待办事项运单")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.OrderId)
                    .HasColumnType("int(11)")
                    .HasColumnName("OrderId");

                entity.Property(e => e.ItemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("ItemId");

                entity.HasIndex(e => e.OrderId, "IX_dbo.TodoItemOrder_OrderId");

                entity.HasIndex(e => e.ItemId, "IX_dbo.TodoItemOrder_ItemId");

                entity.HasOne(d => d.TodoItem)
                    .WithMany(p => p.TodoItemOrders)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_todo_item_order_item_id_item_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.TodoItemOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("fk_todo_item_order_order_id_order_id");
            });

            modelBuilder.Entity<ToolsGetui>(entity =>
            {
                entity.ToTable("tools_getui");

                entity.HasComment("个推配置")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AppId)
                    .HasMaxLength(255)
                    .HasColumnName("app_id");

                entity.Property(e => e.AppKey)
                    .HasMaxLength(255)
                    .HasColumnName("app_key");

                entity.Property(e => e.AppSecret)
                    .HasMaxLength(255)
                    .HasColumnName("app_secret");

                entity.Property(e => e.Http)
                    .HasMaxLength(255)
                    .HasColumnName("http");

                entity.Property(e => e.Https)
                    .HasMaxLength(255)
                    .HasColumnName("https");

                entity.Property(e => e.MasterSecret)
                    .HasMaxLength(255)
                    .HasColumnName("master_secret");
            });

            modelBuilder.Entity<TransportOrder>(entity =>
            {
                entity.ToTable("transport_order");

                entity.HasComment("运单");

                entity.HasIndex(e => e.RouteId, "IX_dbo.Batch_RouteId");

                entity.HasIndex(e => e.CreatedById, "IX_dbo.Order_CreatedById");

                entity.HasIndex(e => e.DomesticNumber, "IX_dbo.Order_DomesticNumber");

                entity.HasIndex(e => e.OrderNumber, "IX_dbo.Order_OrderNumber")
                    .IsUnique();

                entity.HasIndex(e => e.OwnerId, "IX_dbo.Order_OwnerId");

                entity.HasIndex(e => e.RecipientId, "IX_dbo.Order_RecipientId");

                entity.HasIndex(e => e.SenderId, "IX_dbo.Order_SenderId");

                entity.HasIndex(e => e.PickUpLocationId, "fk_transport_order_pick_up_location_pick_up_location_id_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ActualCost)
                    .HasPrecision(19, 4)
                    .HasComment("实际价格");

                entity.Property(e => e.AeroNumber)
                    .HasMaxLength(50)
                    .HasComment("航空号");

                entity.Property(e => e.AeroShippingCost)
                    .HasPrecision(19, 4)
                    .HasComment("航空运费");

                entity.Property(e => e.AirportCharge)
                    .HasPrecision(19, 4)
                    .HasComment("机场杂费");

                entity.Property(e => e.ArriveTime)
                    .HasColumnType("datetime")
                    .HasColumnName("arrive_time")
                    .HasComment("到达提货点时间");

                entity.Property(e => e.AuditDetails)
                    .HasMaxLength(255)
                    .HasComment("审核详情")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.AuditStatus)
                    .HasColumnType("int(11)")
                    .HasComment("退运状态：0,待申请，1，待审核，3，待退运，4，已退运");

                entity.Property(e => e.BaggageNumber)
                    .HasMaxLength(50)
                    .HasComment("箱号");

                entity.Property(e => e.ClearingPort)
                    .HasMaxLength(10)
                    .HasComment("清关口岸");

                entity.Property(e => e.ClearingPortFee)
                    .HasPrecision(19, 4)
                    .HasComment("清关费");

                entity.Property(e => e.CreatedById)
                    .HasColumnType("int(11)")
                    .HasComment("（对应user表）");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Discount)
                    .HasPrecision(18)
                    .HasComment("折扣");

                entity.Property(e => e.DisplayCost)
                    .HasPrecision(19, 4)
                    .HasComment("显示价格");

                entity.Property(e => e.DistrictAdditionalCost)
                    .HasPrecision(19, 4)
                    .HasComment("地区附加费");

                entity.Property(e => e.DomesticCarrier)
                    .HasMaxLength(20)
                    .HasComment("国内快递公司（用于快递100）");

                entity.Property(e => e.DomesticNumber)
                    .HasMaxLength(50)
                    .HasComment("国内转运单号（用于快递100）");

                entity.Property(e => e.DomesticShippingCost)
                    .HasPrecision(19, 4)
                    .HasComment("派送费");

                entity.Property(e => e.Duty).HasPrecision(19, 4);

                entity.Property(e => e.Enclosure)
                    .HasMaxLength(255)
                    .HasComment("附件")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.FumigationCost)
                    .HasPrecision(19, 4)
                    .HasComment("熏蒸");

                entity.Property(e => e.Height)
                    .HasPrecision(18)
                    .HasComment("高");

                entity.Property(e => e.HiddenNotes).HasComment("管理员备注");

                entity.Property(e => e.Insurance)
                    .HasPrecision(19, 4)
                    .HasComment("保费（和保险金额用换算公式）");

                entity.Property(e => e.IsFromChina)
                    .HasColumnType("tinyint(4)")
                    .HasComment("从中国发往加拿大");

                entity.Property(e => e.ItemCost).HasPrecision(19, 4);

                entity.Property(e => e.Length)
                    .HasPrecision(18)
                    .HasComment("长");

                entity.Property(e => e.LoadDeliveryBatchName)
                    .HasMaxLength(255)
                    .HasComment("批次名称")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.LoadDeliveryBatchId)
                    .HasColumnType("int(11)")
                    .HasComment("批次号");

                entity.Property(e => e.Memo).HasComment("备注");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .HasComment("运单号");

                entity.Property(e => e.OversizeCost).HasPrecision(19, 4);

                entity.Property(e => e.OwnerId)
                    .HasColumnType("int(11)")
                    .HasComment("所属用户（基本已淘汰）");

                entity.Property(e => e.PhotoUrl).HasComment("图片url");

                entity.Property(e => e.PickUpLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("pick_up_location_id")
                    .HasComment("自提点id");

                entity.Property(e => e.PortMisCost)
                    .HasPrecision(19, 4)
                    .HasComment("港杂费");

                entity.Property(e => e.RecipientAddressId)
                    .HasColumnType("int(11)")
                    .HasComment("收件人地址id");

                entity.Property(e => e.RecipientId)
                    .HasColumnType("int(11)")
                    .HasComment("收件人id");

                entity.Property(e => e.RemoteCost)
                    .HasPrecision(18)
                    .HasComment("偏远地区收费");

                entity.Property(e => e.ReturnAddressId)
                    .HasColumnType("int(11)")
                    .HasComment("退运地址id");

                entity.Property(e => e.ReturnCarrier)
                    .HasMaxLength(20)
                    .HasComment("退运快递公司");

                entity.Property(e => e.ReturnNumber)
                    .HasMaxLength(50)
                    .HasComment("退运单号");

                entity.Property(e => e.ReturnUserName)
                    .HasMaxLength(50)
                    .HasComment("退运收件人名");

                entity.Property(e => e.Route)
                    .HasMaxLength(50)
                    .HasComment("线路名（对应Route表name）");

                entity.Property(e => e.RouteId).HasColumnType("int(11)");

                entity.Property(e => e.SecondCarrier)
                    .HasMaxLength(40)
                    .HasComment("第二段快递公司");

                entity.Property(e => e.SecondTrackNumber)
                    .HasMaxLength(100)
                    .HasComment("第二段国际运单号");

                entity.Property(e => e.SendAddressId)
                    .HasColumnType("int(11)")
                    .HasComment("发件地址id");

                entity.Property(e => e.SenderId)
                    .HasColumnType("int(11)")
                    .HasComment("发件人id（对应custoner表）");

                entity.Property(e => e.ShippingCost)
                    .HasPrecision(19, 4)
                    .HasComment("运费");

                entity.Property(e => e.State)
                    .HasColumnType("int(11)")
                    .HasComment("状态");

                entity.Property(e => e.StorageCost)
                    .HasPrecision(19, 4)
                    .HasComment("仓储费用");

                entity.Property(e => e.SuggestedCost)
                    .HasPrecision(16, 2)
                    .HasComment("参考价格");

                entity.Property(e => e.Tax)
                    .HasPrecision(19, 4)
                    .HasComment("关税");

                entity.Property(e => e.TransferNumber)
                    .HasMaxLength(50)
                    .HasComment("转运单号");

                entity.Property(e => e.VolumnCost)
                    .HasPrecision(19, 4)
                    .HasComment("体积收费");

                entity.Property(e => e.WarehouseCost).HasPrecision(19, 4);

                entity.Property(e => e.WeightKg)
                    .HasPrecision(16, 2)
                    .HasDefaultValueSql("'0.00'")
                    .HasComment("重量公斤（和重量磅有换算公式）");

                entity.Property(e => e.WeightPound)
                    .HasPrecision(16, 2)
                    .HasComment("重量磅");

                entity.Property(e => e.Width)
                    .HasPrecision(18)
                    .HasComment("宽");

                entity.Property(e => e.TotalVolume)
                    .HasPrecision(16, 2)
                    .HasComment("总体积");

                entity.Property(e => e.InsuranceCost)
                    .HasPrecision(16, 2)
                    .HasComment("保险费用");

                entity.Property(e => e.CompanyId).HasColumnType("int");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.TransportOrderCreatedBies)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.Order_dbo.User_CreatedById");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.TransportOrderOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_dbo.Order_dbo.User_OwnerId");

                entity.HasOne(d => d.PickUpLocation)
                    .WithMany(p => p.TransportOrders)
                    .HasForeignKey(d => d.PickUpLocationId)
                    .HasConstraintName("fk_transport_order_pick_up_location_pick_up_location_id");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.TransportOrderRecipients)
                    .HasForeignKey(d => d.RecipientId)
                    .HasConstraintName("FK_dbo.Order_dbo.Customer_RecipientId");

                entity.HasOne(d => d.RouteNavigation)
                    .WithMany(p => p.TransportOrders)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK_dbo.Order_dbo.Route_RouteId");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.TransportOrderSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK_dbo.Order_dbo.Customer_SenderId");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.TransportOrders)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_TransportOrder_CompanyId");

            });

            modelBuilder.Entity<TransportOrderAudit>(entity =>
            {
                entity.ToTable("transport_order_audit");

                entity.HasComment("运单审核记录")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.AuditState, "Index_audit_state");

                entity.HasIndex(e => e.Type, "Index_type");

                entity.HasIndex(e => e.BusinessId, "Index_worker_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AddressId)
                    .HasColumnType("int(11)")
                    .HasColumnName("address_id")
                    .HasComment("退运地址id");

                entity.Property(e => e.ApplyContent)
                    .HasMaxLength(500)
                    .HasColumnName("apply_content")
                    .HasComment("申请内容");

                entity.Property(e => e.AuditContent)
                    .HasMaxLength(500)
                    .HasColumnName("audit_content")
                    .HasComment("审核内容");

                entity.Property(e => e.AuditState)
                    .HasColumnName("audit_state")
                    .HasComment("审核状态 1：审核成功，2：审核失败");

                entity.Property(e => e.AuditUser)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("audit_user")
                    .HasComment("审核人");

                entity.Property(e => e.AuditUserName)
                    .HasMaxLength(255)
                    .HasColumnName("audit_user_name")
                    .HasComment("审核人昵称");

                entity.Property(e => e.BusinessId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("business_id")
                    .HasComment("业务id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasComment("创建时间");

                entity.Property(e => e.IsDel)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_del")
                    .HasComment("是否删除 0:否,1:是");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasComment("审核类型： 0->批次确认，2->运单确认,3,->申请退运");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasComment("用户（可登陆系统）");

                entity.HasIndex(e => e.BelongsToId, "IX_dbo.User_BelongsToId");

                entity.HasIndex(e => e.CustomerId, "IX_dbo.User_CustomerId");

                entity.HasIndex(e => e.DefaultBatchId, "IX_dbo.User_DefaultBatchId");

                entity.HasIndex(e => e.PickUpLocationId, "fk_user_pick_up_location_pick_up_location_id_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AddOnCost).HasPrecision(19, 4);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .HasComment("头像")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Balance)
                    .HasPrecision(19, 4)
                    .HasComment("余额");

                entity.Property(e => e.BelongsTo)
                    .HasMaxLength(100)
                    .HasComment("所属用户（淘汰）");

                entity.Property(e => e.BelongsToId)
                    .HasColumnType("int(11)")
                    .HasComment("所属用户（对应User表，普通用户可以归属与高级用户）");

                entity.Property(e => e.CanadaPhoneNumber)
                    .HasMaxLength(20)
                    .HasComment("加拿大电话");

                entity.Property(e => e.ChinaPhoneNumber)
                    .HasMaxLength(20)
                    .HasComment("中国电话");

                entity.Property(e => e.ClearingPortCost)
                    .HasPrecision(19, 4)
                    .HasComment("清关费");

                entity.Property(e => e.Credit).HasPrecision(19, 4);

                entity.Property(e => e.CustomerId)
                    .HasColumnType("int(11)")
                    .HasComment("客户id（对应客户表，用于存基本信息）");

                entity.Property(e => e.DefaultBatchId)
                    .HasColumnType("int(11)")
                    .HasComment("用户默认批次");

                entity.Property(e => e.DisplaySequence)
                    .HasColumnType("int(11)")
                    .HasComment("排序");

                entity.Property(e => e.IsPending)
                    .HasColumnType("tinyint(4)")
                    .HasComment("待批准");

                entity.Property(e => e.IsTestAccount)
                    .HasColumnType("tinyint(4)")
                    .HasComment("是测试账户");

                entity.Property(e => e.IsUpdated)
                    .HasColumnType("tinyint(4)")
                    .HasComment("已更新信息");

                entity.Property(e => e.Level)
                    .HasColumnType("int(11)")
                    .HasComment("等级");

                entity.Property(e => e.NickName)
                    .HasMaxLength(255)
                    .HasComment("昵称")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.OrderStartNumber)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasComment("运单起始标号");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .HasComment("密码");

                entity.Property(e => e.PayPassword)
                    .HasMaxLength(250)
                    .HasComment("支付密码");

                //entity.Property(e => e.PickUpLocation)
                //    .HasMaxLength(100)
                //    .HasComment("取货地点");

                entity.Property(e => e.PickUpLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("pick_up_location_id")
                    .HasComment("自提点id");

                entity.Property(e => e.PickUpPhoneNumber).HasMaxLength(50);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .HasComment("邮编");

                entity.Property(e => e.QrPath)
                    .HasMaxLength(100)
                    .HasColumnName("qr_path")
                    .HasComment("二维码")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.Role)
                    .HasColumnType("int(11)")
                    .HasComment("职权（1-》管理员2-》高级用户3-》普通用户）");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(100)
                    .HasComment("角色名称：职权（1-》管理员 ROLE_ADMIN,2-》高级用户 ROLE_AENIOR,3-》普通用户 ROLE_USER）");

                entity.Property(e => e.Settings).HasComment("设置");

                entity.Property(e => e.StorageCost).HasPrecision(19, 4);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasComment("用户名");

                entity.Property(e => e.WeChat)
                    .HasMaxLength(100)
                    .HasComment("微信");

                entity.Property(e => e.Mailbox)
                    .HasMaxLength(255)
                    .HasColumnName("mailbox")
                    .HasComment("邮箱")
                    .HasCharSet("utf8");

                entity.Property(e => e.CompanyId).HasColumnType("int");

                entity.HasOne(d => d.BelongsToNavigation)
                    .WithMany(p => p.InverseBelongsToNavigation)
                    .HasForeignKey(d => d.BelongsToId)
                    .HasConstraintName("FK_dbo.User_dbo.User_BelongsToId");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.User_dbo.Customer_CustomerId");

                entity.HasOne(d => d.DefaultBatch)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.DefaultBatchId)
                    .HasConstraintName("FK_dbo.User_dbo.Batch_BatchId");

                entity.HasOne(d => d.PickUpLocationNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PickUpLocationId)
                    .HasConstraintName("fk_user_pick_up_location_pick_up_location_id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_User_CompanyId");

                entity.HasOne(d => d.UserRole)
                    .WithMany(r => r.Users)
                    .HasForeignKey(d => d.Role)
                    .HasConstraintName("FK_dbo.User_dbo.Role_RoleId");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("warehouse");

                entity.HasComment("仓库");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Contact).HasComment("联系人");

                entity.Property(e => e.DisplaySequence).HasColumnType("int(11)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasComment("位置");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasComment("名称");

                entity.Property(e => e.Photo).HasComment("照片");

                entity.Property(e => e.CompanyId).HasColumnType("int");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Warehouse_CompanyId");
            });

            modelBuilder.Entity<YoudumallUser>(entity =>
            {
                entity.ToTable("youdumall_user");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id")
                    .HasComment("id");

                entity.Property(e => e.AvailableBalance)
                    .HasPrecision(12, 2)
                    .HasColumnName("available_balance")
                    .HasComment("可用余额");

                entity.Property(e => e.GameFlag)
                    .HasMaxLength(1)
                    .HasColumnName("game_flag")
                    .HasComment("游戏标准：0->未参与,1->已参与");

                entity.Property(e => e.Introid)
                    .HasMaxLength(20)
                    .HasColumnName("introid")
                    .HasComment("推荐人");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(15)
                    .HasColumnName("mobile")
                    .HasComment("手机号");

                entity.Property(e => e.NickName)
                    .HasMaxLength(255)
                    .HasColumnName("nick_name")
                    .HasComment("昵称");

                entity.Property(e => e.UserLevel)
                    .HasMaxLength(5)
                    .HasColumnName("user_level")
                    .HasComment("会员等级");

                entity.Property(e => e.ZmhBalance)
                    .HasPrecision(20, 2)
                    .HasColumnName("zmh_balance")
                    .HasComment("芝麻花");

                entity.Property(e => e.ZmlBalance)
                    .HasPrecision(20, 2)
                    .HasColumnName("zml_balance")
                    .HasComment("芝麻粒（作废）");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
