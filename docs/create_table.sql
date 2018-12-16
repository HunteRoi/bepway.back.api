DROP TABLE [dbo].[Company];
DROP TABLE [dbo].[User];
DROP TABLE [dbo].[ActivitySector];

CREATE TABLE [dbo].[ActivitySector]
(
	[id] [numeric] IDENTITY(0,1),
	[name] nvarchar(200) NOT NULL,

	CONSTRAINT activitySector_pk PRIMARY KEY ([id])
);

CREATE TABLE [dbo].[User] (
	[id] [numeric] IDENTITY(0,1),
	[login] nvarchar(50) NOT NULL,
	[password] nvarchar(200) NOT NULL,
	[email] nvarchar(200) NOT NULL,
	[birthdate] [date] NOT NULL,
	[isAdmin] [bit] NOT NULL,
	[isEnabled] [bit] NOT NULL,
	[todoList] nvarchar(500),
	[creator] nvarchar(50),

	CONSTRAINT user_pk PRIMARY KEY ([id]),
	CONSTRAINT user_uk UNIQUE ([login]),
	CONSTRAINT userCreator_fk FOREIGN KEY ([creator]) REFERENCES [dbo].[User] ([login])
);

CREATE TABLE [dbo].[Company]
(
	[id] [numeric] IDENTITY(0,1),
	[idOpenData] nvarchar(100),
	[name] nvarchar(50) NOT NULL,
	[imageURL] nvarchar(100),
	[siteURL] nvarchar(200),
	[description] nvarchar(500),
	[status] nvarchar(50) NOT NULL, -- "Draft", "Existing", "Expired"
	[address] nvarchar(200) NOT NULL,
	[latitude] [numeric] NOT NULL,
	[longitude] [numeric] NOT NULL,
	[creationDate] [date],
	[activitySector_id] [numeric],
	[creator_id] nvarchar(50),
	[isPremium] [bit] NOT NULL,
	[rowVersion] [timestamp],

	CONSTRAINT company_pk PRIMARY KEY ([id]),
	CONSTRAINT activitySector_fk 
		FOREIGN KEY ([activitySector_id]) 
		REFERENCES [dbo].[ActivitySector] ([id]) 
		ON DELETE SET NULL,
	CONSTRAINT companyCreator_fk
		FOREIGN KEY ([creator_id])
		REFERENCES [dbo].[User] ([login])
		ON DELETE SET NULL
);
