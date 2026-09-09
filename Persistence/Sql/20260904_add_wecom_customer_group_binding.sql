CREATE TABLE `wecom_customer_group_binding` (
  `id` BIGINT NOT NULL AUTO_INCREMENT,
  `company_id` INT NOT NULL,
  `user_id` INT NOT NULL,
  `chat_id` VARCHAR(128) NOT NULL,
  `group_owner_userid` VARCHAR(128) NOT NULL,
  `group_name` VARCHAR(255) NULL,
  `binding_source` VARCHAR(32) NOT NULL DEFAULT 'manual',
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL,
  `updated_at` DATETIME NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `ux_wecom_group_company_user` (`company_id`, `user_id`),
  UNIQUE KEY `ux_wecom_group_company_chat` (`company_id`, `chat_id`),
  KEY `ix_wecom_group_owner` (`group_owner_userid`),
  CONSTRAINT `fk_wecom_group_company` FOREIGN KEY (`company_id`) REFERENCES `company` (`Id`),
  CONSTRAINT `fk_wecom_group_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
