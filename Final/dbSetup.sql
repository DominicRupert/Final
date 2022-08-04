CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        name varchar(255) NOT Null COMMENT 'Keep Name',
        description varchar(255) NOT NULL COMMENT 'Keep Description',
        img varchar(255) NOT NULL COMMENT 'Keep Image',
        views int COMMENT 'Keep Views',
        kept INT,
        creatorId VARCHAR(255) NOT Null,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) DEFAULT CHARSET utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        name varchar(255) NOT Null COMMENT 'Vault Name',
        description varchar(255) NOT NULL COMMENT 'Vault Description',
        isPrivate TINYINT NOT NULL DEFAULT 0,
        creatorId VARCHAR(255) NOT Null,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    )DEFAULT CHARSET utf8 COMMENT '';


CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        keepId INT NOT NULL,
        vaultId INT NOT NULL,
        creatorId VARCHAR(255) NOT Null,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    )DEFAULT CHARSET utf8 COMMENT '';



    SELECT 
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = '625ef350daa8efd3f6cce827'