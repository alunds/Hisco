CREATE TABLE IF NOT EXISTS `entries` (
	`id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
	`level` smallint(5) unsigned NOT NULL,
	`name` varchar(50) NOT NULL,
	`score` smallint(5) unsigned NOT NULL,
	`created` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
	PRIMARY KEY (`id`),
	UNIQUE KEY `id` (`id`),
	KEY `score` (`score`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=1;