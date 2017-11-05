-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'login_info'
--
-- ---

DROP TABLE IF EXISTS `login_info`;

CREATE TABLE `login_info` (
  `player_id` INTEGER NULL DEFAULT NULL,
  `login_name` VARCHAR(255) NULL DEFAULT NULL,
  `password_hash` VARCHAR(255) NULL DEFAULT NULL,
  `salt` VARCHAR(255) NULL DEFAULT NULL
);

-- ---
-- Table 'scores'
--
-- ---

DROP TABLE IF EXISTS `scores`;

CREATE TABLE `scores` (
  `player_id` INTEGER NULL DEFAULT NULL,
  `score` INTEGER NULL DEFAULT NULL
);

-- ---
-- Table 'friends'
--
-- ---

DROP TABLE IF EXISTS `friends`;

CREATE TABLE `friends` (
  `player_1_id` INTEGER NULL DEFAULT NULL,
  `player_2_id` INTEGER NULL DEFAULT NULL
);

-- ---
-- Table 'player'
--
-- ---

DROP TABLE IF EXISTS `player`;

CREATE TABLE `player` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `screen_name` VARCHAR NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
);

-- ---
-- Foreign Keys
-- ---

ALTER TABLE `login_info` ADD FOREIGN KEY (player_id) REFERENCES `player` (`id`);
ALTER TABLE `scores` ADD FOREIGN KEY (player_id) REFERENCES `player` (`id`);
ALTER TABLE `friends` ADD FOREIGN KEY (player_1_id) REFERENCES `player` (`id`);
ALTER TABLE `friends` ADD FOREIGN KEY (player_2_id) REFERENCES `player` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `login_info` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `scores` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `friends` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `player` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `login_info` (`player_id`,`login_name`,`password_hash`,`salt`) VALUES
-- ('','','','');
-- INSERT INTO `scores` (`player_id`,`score`) VALUES
-- ('','');
-- INSERT INTO `friends` (`player_1_id`,`player_2_id`) VALUES
-- ('','');
-- INSERT INTO `player` (`id`,`screen_name`) VALUES
-- ('','');
