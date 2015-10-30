CREATE TABLE Application(
	Id int IDENTITY(1,1) NOT NULL primary key,
	AppKey varchar(100) NOT NULL,
	AppSecret varchar(100) NOT NULL,
	CallbackUrl varchar(100) NOT NULL,
	Name nvarchar(200) NULL)


