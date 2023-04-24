CREATE DATABASE QSestimateDB

CREATE TABLE login
(
user_id varchar(50) not null ,
user_type varchar(20) not null,
user_name varchar(50) not null,
password varchar(50) not null,
group_id int 
);

CREATE TABLE quantity_deta
(
Type varchar(50) not null ,
Unit varchar(20) not null ,
Quantity float not null,
Rate int not null,
Amount float not null
 );

CREATE TABLE final_view
(
Description varchar(50) not null ,
Amount float not null
 );
