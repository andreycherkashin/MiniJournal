drop table if exists comments;
drop table if exists articles;
drop table if exists users;

create table users
(
	id bigint not null primary key generated always as identity, 
	name varchar(500) not null
);

create table articles
(
	id bigint not null primary key generated always as identity, 
	user_id bigint not null references users (id) on delete cascade,
	image_id varchar(1000),
	text varchar(4000)	
);

create table comments
(
	id bigint not null primary key generated always as identity, 
	article_id bigint not null references articles(id) on delete cascade,
	user_id bigint not null references users(id) on delete cascade,
	text varchar(1000)	
);