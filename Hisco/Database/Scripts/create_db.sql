﻿CREATE TABLE 'entries'
(
	'id' BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
	'level' SMALLINT UNSIGNED NOT NULL,
	'name' VARCHAR(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
	'score' DECIMAL(6,3) NOT NULL,
	'created' TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY ('id'),
	INDEX ('score'),
	UNIQUE ('id')
)
ENGINE = INNODB CHARACTER SET utf8 COLLATE utf8_general_ci;