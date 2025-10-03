ALTER TABLE transport_order
ADD COLUMN ActionReason LONGTEXT;

ALTER TABLE user
ADD COLUMN AddOnCost DECIMAL(19, 4) NOT NULL Default 0;

ALTER TABLE system_settings
ADD COLUMN SchedulePickUpText LONGTEXT;

ALTER TABLE user
ADD COLUMN Credit DECIMAL(19, 4) NOT NULL Default 0;

ALTER TABLE user
ADD COLUMN StorageCost DECIMAL(19, 4) NOT NULL Default 0;

ALTER TABLE transport_order
ADD COLUMN IsItemCostUpdated tinyint(1) NOT NULL Default 0;

ALTER TABLE pick_up_location
ADD CONSTRAINT `fk_pick_up_location_user_belongs_to_id`
  FOREIGN KEY (`belongs_to_id`)
  REFERENCES `user` (`Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;

ALTER TABLE pick_up_location
ADD INDEX `fk_pick_up_location_user_belongs_to_id_idx` (`belongs_to_id` ASC);

ALTER TABLE `user`
ADD CONSTRAINT `fk_user_pick_up_location_pick_up_location_id`
  FOREIGN KEY (`pick_up_location_id`)
  REFERENCES `pick_up_location` (`Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `user`
ADD INDEX `fk_user_pick_up_location_pick_up_location_id_idx` (`pick_up_location_id` ASC);

ALTER TABLE `transport_order`
ADD CONSTRAINT `fk_transport_order_pick_up_location_pick_up_location_id`
  FOREIGN KEY (`pick_up_location_id`)
  REFERENCES `pick_up_location` (`Id`)
  ON DELETE NO ACTION
  ON UPDATE NO ACTION;


ALTER TABLE `transport_order`
ADD INDEX `fk_transport_order_pick_up_location_pick_up_location_id_idx` (`pick_up_location_id` ASC);

ALTER TABLE `batch`
ADD COLUMN Commission DECIMAL(19, 4) NOT NULL Default 0;

ALTER TABLE `batch`
ADD COLUMN DateEntered datetime NULL;

ALTER TABLE user
ADD COLUMN Description LONGTEXT;

CREATE TABLE order_scan_status (
  Id int NOT NULL AUTO_INCREMENT,
  OrderId int NOT NULL,
  Status int NOT NULL,
  Timestamp timestamp NOT NULL,
  UserId int NOT NULL,
  CONSTRAINT `pk_order_scan_status_id` PRIMARY KEY (Id),
  INDEX (OrderId ASC),
  CONSTRAINT `order_scan_status` UNIQUE `unique_idx_order_scan_status`(`OrderId`, `Status`),
  CONSTRAINT `fk_order_scan_status_order_id` FOREIGN KEY (OrderId) REFERENCES transport_order(Id),
  CONSTRAINT `fk_order_scan_status_user_id` FOREIGN KEY (UserId) REFERENCES user(Id)
);

CREATE TABLE sms_log (
  Id int NOT NULL AUTO_INCREMENT,
  BatchId int NULL,
  UserId int NULL,
  Message Text NOT NULL,
  Content Text NOT NULL,
  phonenumber varchar(16) NULL,
  Timestamp timestamp NOT NULL,
  CONSTRAINT `pk_sms_log_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_sms_log_batch_id` FOREIGN KEY (BatchId) REFERENCES batch(Id),
  CONSTRAINT `fk_sms_log_user_id` FOREIGN KEY (UserId) REFERENCES user(Id)
);

CREATE TABLE support_user (
  UserId int NOT NULL,
  WeChat varchar(64) NOT NULL,
  Warehouse varchar(128) NOT NULL,
  CONSTRAINT `pk_support_user_user_id` PRIMARY KEY (UserId),
  CONSTRAINT `fk_support_user_user_id` FOREIGN KEY (UserId) REFERENCES user(Id)
);

INSERT INTO support_user (UserId, WeChat, Warehouse)
VALUES 
(1, '', '壹嘉国际'),
(1612, '', '壹嘉国际'),
(7608, '', '壹嘉国际广州仓库'),
(1613, '', '壹嘉国际义乌仓库'),
(2689, '', '壹嘉国际多伦多仓库')


CREATE TABLE ringcentral_credential (
  UserId int NOT NULL,
  ApplicationName varchar(64) NOT NULL,
  ClientID varchar(32) NOT NULL,
  ClientSecret varchar(64) NOT NULL,
  UserName varchar(16) NOT NULL,
  Extension varchar(8) NOT NULL,
  Password varchar(32) NOT NULL,
  FromNumber varchar(16) NOT NULL,
  CONSTRAINT `pk_ringcentral_credential_user_id` PRIMARY KEY (UserId)
)

INSERT INTO ringcentral_credential (UserId, ApplicationName, ClientID, ClientSecret, UserName, Extension, Password, FromNumber) VALUES
(1, 'EplusWebsite', 'IsGrtWw3Rsi73jlZz75egA', 'kJldtFksTu-xjv7n1mfqcAN9XAR6DiQ2--s1liqih6eg', '16476702288', '1001', 'WangBaichun@0625', '16476702458'),
(1612, 'EplusWebsite', 'IsGrtWw3Rsi73jlZz75egA', 'kJldtFksTu-xjv7n1mfqcAN9XAR6DiQ2--s1liqih6eg', '16476702288', '1001', 'WangBaichun@0625', '16476702458'),
(1613, 'EPlusWebsite3', 'UrDTNJioSKaWscVWXbvQkA', 'VVvqLQJWRv6kAcmJUuZJUAzvdiG6maQvWf7IYRBEH12w', '16476702288', '1002', 'Yijia@1122', '16476702263'),
(7608, 'EplusWebsite4', 'kiKmzrlpTR2Cc51WdWWhaA', 'mKMLXAuuRyqfBJEvfuIOfwWbURUZSMS_eEMdmOycDiQw', '16476702288', '1004', 'Yijia@1122', '16476700941'),
(2689, 'EplusWebsite5', 'rSxZT3xQTzSWp8L3KVnUTQ', 'QTizESxxTNeGd8nZox9rGgx_FOlyqFQ4y44X7pW4fjXA', '16476702288', '1003', 'Yijia@1122', '16476701941'),
(13, 'EplusWebsite5', 'rSxZT3xQTzSWp8L3KVnUTQ', 'QTizESxxTNeGd8nZox9rGgx_FOlyqFQ4y44X7pW4fjXA', '16476702288', '1003', 'Yijia@1122', '16476701941'),
(10414, 'EplusWebsite5', 'rSxZT3xQTzSWp8L3KVnUTQ', 'QTizESxxTNeGd8nZox9rGgx_FOlyqFQ4y44X7pW4fjXA', '16476702288', '1003', 'Yijia@1122', '16476701941'),
(10435, 'EplusWebsite5', 'rSxZT3xQTzSWp8L3KVnUTQ', 'QTizESxxTNeGd8nZox9rGgx_FOlyqFQ4y44X7pW4fjXA', '16476702288', '1003', 'Yijia@1122', '16476701941')

CREATE TABLE order_status_internal (
  Id int NOT NULL AUTO_INCREMENT,
  OrderId int NOT NULL,
  Status int NOT NULL,
  DateCreated datetime NOT NULL,
  UserId int NOT NULL,
  CONSTRAINT `pk_order_status_internal_id` PRIMARY KEY (Id),
  INDEX (OrderId ASC),
  CONSTRAINT `fk_order_status_internal_order_id` FOREIGN KEY (OrderId) REFERENCES transport_order(Id) ON DELETE CASCADE,
  CONSTRAINT `fk_order_status_internal_user_id` FOREIGN KEY (UserId) REFERENCES user(Id)
)

ALTER TABLE balance_history ADD TransactionGuid varchar(36)

ALTER TABLE transport_order ADD LoadDeliveryBatchId int

ALTER TABLE sms_log ADD Content Text NOT NULL 

ALTER TABLE sms_log ADD phonenumber varchar(16)

CREATE TABLE load_delivery_batch (
  Id int NOT NULL,
  FlightInfo varchar(32) NULL,
  CargoNumber varchar(32) NULL,
  ArrivalTime timestamp NULL,
  CONSTRAINT `pk_load_delivery_batch_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_load_delivery_batch_id` FOREIGN KEY (Id) REFERENCES batch(Id)
)

CREATE TABLE coupon (
  Id int NOT NULL AUTO_INCREMENT,
  ShippingCost DECIMAL(19, 4) NOT NULL,
  CouponNumber varchar(50) NOT NULL,
  DomesticNumber varchar(50) NOT NULL,
  CreatedById int NOT NULL,
  CreateTime timestamp NOT NULL,
  ValidFrom timestamp NULL,
  ValidUntil timestamp NULL,
  CouponBatchId int NOT NULL,
  AssignedUserId int NULL,
  ConsumedUserId int NULL,
  Active bit NOT NULL Default 1,
  CONSTRAINT `pk_coupon_id` PRIMARY KEY (Id),
  CONSTRAINT `order_scan_status` UNIQUE `unique_idx_coupon_number`(`CouponNumber`),
  CONSTRAINT `fk_coupon_created_by_id` FOREIGN KEY (CreatedById) REFERENCES user(Id),
  CONSTRAINT `fk_coupon_assigned_user_id` FOREIGN KEY (AssignedUserId) REFERENCES user(Id),
  CONSTRAINT `fk_coupon_coupon_batch_id` FOREIGN KEY (CouponBatchId) REFERENCES coupon_batch(Id) ON DELETE CASCADE
)

CREATE TABLE coupon_batch (
  Id int NOT NULL AUTO_INCREMENT,
  Name varchar(50) NOT NULL Default "",
  CreatedById int NOT NULL,
  CreateTime timestamp NOT NULL,
  Anonymous bit NULL,
  PhotoUrl longtext NULL, 
  EmailContent longtext NULL, 
  SmsContent longtext NULL, 
  CONSTRAINT `fk_coupon_batch_created_by_id` FOREIGN KEY (CreatedById) REFERENCES user(Id),
  CONSTRAINT `pk_coupon_batch_id` PRIMARY KEY (Id)
)

CREATE TABLE coupon_status (
  Id int NOT NULL AUTO_INCREMENT,
  CouponId int NOT NULL,
  Status int NOT NULL,
  DateCreated timestamp NOT NULL,
  UserId int NOT NULL,

  INDEX (CouponId ASC),
  CONSTRAINT `pk_coupon_status_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_coupon_status_user_id` FOREIGN KEY (UserId) REFERENCES user(Id),
  CONSTRAINT `fk_coupon_status_coupon_id` FOREIGN KEY (CouponId) REFERENCES coupon(Id) ON DELETE CASCADE
)

ALTER TABLE pick_up_location ADD COLUMN AreaId INT NULL
ALTER TABLE pick_up_location ADD COLUMN Visible BIT NOT NULL DEFAULT 1

create table Area (
  Id int NOT NULL AUTO_INCREMENT,
  ShortName varchar(8),
  FullName varchar(50),
  CountryCode varchar(4),
  CONSTRAINT `pk_area_id` PRIMARY KEY (Id)
)

INSERT INTO Area(ShortName, FullName, CountryCode) VALUES
('BC', 'British Columbia', 'CA'),
('MB', 'Manitoba', 'CA'),
('ON', 'Ontario', 'CA'),
('QC', 'Quebec', 'CA'),
('SK', 'Saskatchewan', 'CA'),
('AB', 'Alberta', 'CA'),
('NL', 'Newfoundland', 'CA'),
('NB', 'New Brunswick', 'CA'),
('NS', 'Nova Scotia', 'CA'),
('PEI', 'PEI', 'CA'),
('NV', 'Nunavut', 'CA'),
('NT', 'Northwest Territories', 'CA'),
('YK', 'Yukon', 'CA')

CREATE TABLE CouponStatus(
  Id int not null,
  Name varchar(20) not null,
  CONSTRAINT `pk_couponstatus_id` PRIMARY KEY (Id)
)

INSERT INTO CouponStatus (Id, Name) VALUES
(1, '已创建'),
(11, '已打印'),
(12, '已寄送'),
(21, '已指定用户'),
(31, '已生效'),
(32, '已失效'),
(41, '已使用')

ALTER TABLE user ADD COLUMN AreaId INT NULL
ALTER TABLE user ADD COLUMN Visible BIT NOT NULL DEFAULT 1
ALTER TABLE coupon ADD COLUMN MinimumPrice DECIMAL(19, 4) NOT NULL Default 0

CREATE TABLE email_data(
  Id int not null AUTO_INCREMENT,
  OrderId int(11) null,
  SenderUserId int(11) not null,
  RecipientUserId int(11) not null,
  BatchId int(11) null,
  DateCreated datetime NOT NULL,
  DateSent datetime NULL,
  CONSTRAINT `pk_email_data_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_email_data_order_id_transport_order_id` FOREIGN KEY (OrderId) REFERENCES transport_order(Id) ON DELETE NO ACTION,
  CONSTRAINT `fk_email_data_sender_user_id_user_id` FOREIGN KEY (SenderUserId) REFERENCES user(Id),
  CONSTRAINT `fk_email_data_recipient_user_id_user_id` FOREIGN KEY (RecipientUserId) REFERENCES user(Id),
  CONSTRAINT `fk_email_data_batch_id_batch_id` FOREIGN KEY (BatchId) REFERENCES batch(Id)
)

CREATE VIEW vw_order_item(OrderId, Category)
AS
SELECT OrderId, MAX(Category) Category FROM china_item GROUP BY OrderId

ALTER TABLE batch_box ADD COLUMN Length DECIMAL(18,0) NULL
ALTER TABLE batch_box ADD COLUMN Width DECIMAL(18,0) NULL
ALTER TABLE batch_box ADD COLUMN Height DECIMAL(18,0) NULL
ALTER TABLE batch_box ADD COLUMN ActualWeightKg DECIMAL(18,0) NULL

ALTER TABLE route ADD COLUMN IsRegular bit NOT NULL DEFAULT TRUE

ALTER TABLE pick_up_location ADD COLUMN version TINYINT NOT NULL DEFAULT 1

ALTER TABLE pick_up_location ADD COLUMN note TEXT NOT NULL

CREATE TABLE todo_item(
  Id INT NOT NULL AUTO_INCREMENT,
  Message TEXT NOT NULL,
  Comment TEXT NULL,
  CreatedByUserId int(11) NOT NULL,
  DateResolved DATETIME NULL,
  Resolution TEXT NULL,
  CustomerInfo TEXT NULL,
  OrderInfo TEXT NULL,
  DateCreated DATETIME NOT NULL,
  NotifyCustomer BIT,
  Status TINYINT NOT NULL,

  CONSTRAINT `pk_todo_item_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_todo_item_created_by_user_id_user_id` FOREIGN KEY (CreatedByUserId) REFERENCES user(Id),
)

CREATE TABLE todo_item_assignee(
  Id INT NOT NULL AUTO_INCREMENT,
  ItemId int(11) NOT NULL,
  UserId int(11) NOT NULL,

  INDEX (ItemId ASC),
  INDEX (UserId ASC),

  CONSTRAINT `pk_todo_item_assignee_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_todo_item_assignee_item_id_item_id` FOREIGN KEY (ItemId) REFERENCES todo_item(Id) ON DELETE CASCADE,
  CONSTRAINT `fk_todo_item_assignee_user_id_user_id` FOREIGN KEY (UserId) REFERENCES user(Id) ON DELETE NO ACTION
)

CREATE TABLE todo_item_customer(
  Id INT NOT NULL AUTO_INCREMENT,
  ItemId int(11) NOT NULL,
  UserId int(11) NOT NULL,

  INDEX (ItemId ASC),
  INDEX (UserId ASC),

  CONSTRAINT `pk_todo_item_customer_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_todo_item_customer_item_id_item_id` FOREIGN KEY (ItemId) REFERENCES todo_item(Id) ON DELETE CASCADE,
  CONSTRAINT `fk_todo_item_customer_user_id_user_id` FOREIGN KEY (UserId) REFERENCES user(Id) ON DELETE NO ACTION
)

CREATE TABLE todo_item_order(
  Id INT NOT NULL AUTO_INCREMENT,
  ItemId int(11) NOT NULL,
  OrderId int(11) NOT NULL,

  INDEX (ItemId ASC),
  INDEX (OrderId ASC),

  CONSTRAINT `pk_todo_item_order_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_todo_item_order_item_id_item_id` FOREIGN KEY (ItemId) REFERENCES todo_item(Id) ON DELETE CASCADE,
  CONSTRAINT `fk_todo_item_order_order_id_order_id` FOREIGN KEY (OrderId) REFERENCES transport_order(Id) ON DELETE NO ACTION
)

CREATE TABLE session(
  Id INT NOT NULL AUTO_INCREMENT,
  ItemId int(11) NOT NULL,
  OrderId int(11) NOT NULL,

  INDEX (ItemId ASC),
  INDEX (OrderId ASC),

  CONSTRAINT `pk_todo_item_order_id` PRIMARY KEY (Id),
  CONSTRAINT `fk_todo_item_order_item_id_item_id` FOREIGN KEY (ItemId) REFERENCES todo_item(Id) ON DELETE CASCADE,
  CONSTRAINT `fk_todo_item_order_order_id_order_id` FOREIGN KEY (OrderId) REFERENCES transport_order(Id) ON DELETE NO ACTION
)