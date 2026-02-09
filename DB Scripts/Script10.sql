IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'company')
BEGIN
	CREATE TABLE company (
		Id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
		Name varchar(256) NOT NULL,
		Code varchar(32) NOT NULL
	);
END

ALTER TABLE route
ADD CONSTRAINT FK_Route_CompanyId FOREIGN KEY (CompanyId) REFERENCES company(Id);

IF COL_LENGTH('dbo.batch_other_order', 'UserId') IS NULL
BEGIN
	ALTER TABLE `batch_other_order` ADD UserId INT(11) NULL
	ALTER TABLE `batch_other_order` ADD CONSTRAINT `FK_dbo.BatchOtherOrder_dbo.User_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
END

IF COL_LENGTH('dbo.batch_other_order', 'DateCreated') IS NULL
BEGIN
    ALTER TABLE `batch_other_order` ADD DateCreated DATETIME NULL
END