CREATE TABLE customer(
	customer_id int identity(1, 1) not null primary key,
	username varchar(20) not null,
	customer_info nvarchar(MAX) not null,
	pwd_hash char(64) not null
);

CREATE TABLE auth_token(
	token_id int identity(1, 1) not null primary key,
	username varchar(20) not null,
	token char(64) not null,
	relevance_limit int not null
);