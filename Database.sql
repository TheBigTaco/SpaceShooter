-- ---
-- Globals
-- ---

-- SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
-- SET FOREIGN_KEY_CHECKS=0;

-- ---
-- Table 'players'
--
-- ---

DROP TABLE IF EXISTS `players`;

CREATE TABLE `players` (
  `id` INTEGER NULL AUTO_INCREMENT DEFAULT NULL,
  `username` VARCHAR(255) NULL DEFAULT NULL,
  `password` VARCHAR(255) NULL DEFAULT NULL,
  `salt` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
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
-- Foreign Keys
-- ---

ALTER TABLE `scores` ADD FOREIGN KEY (player_id) REFERENCES `players` (`id`);
ALTER TABLE `friends` ADD FOREIGN KEY (player_1_id) REFERENCES `players` (`id`);
ALTER TABLE `friends` ADD FOREIGN KEY (player_2_id) REFERENCES `players` (`id`);

-- ---
-- Table Properties
-- ---

-- ALTER TABLE `players` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `scores` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
-- ALTER TABLE `friends` ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- ---
-- Test Data
-- ---

-- INSERT INTO `players` (`id`,`username`,`password`,`salt`) VALUES
-- ('','','','');
-- INSERT INTO `scores` (`player_id`,`score`) VALUES
-- ('','');
-- INSERT INTO `friends` (`player_1_id`,`player_2_id`) VALUES
-- ('','');
