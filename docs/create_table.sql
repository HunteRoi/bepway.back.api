--ALTER TABLE [dbo].[Company] DROP CONSTRAINT company_pk, activitySector_fk, companyCreator_fk;
--ALTER TABLE [db].[User] DROP CONSTRAINT user_pl, user_uk, userCreator_fk;

DROP TABLE [dbo].[Company];
DROP TABLE [dbo].[User];
DROP TABLE [dbo].[ActivitySector];

CREATE TABLE [dbo].[ActivitySector]
(
	[id] NUMERIC IDENTITY(0,1),
	[name] NVARCHAR(200) NOT NULL,

	CONSTRAINT activitySector_pk PRIMARY KEY ([id])
);

CREATE TABLE [dbo].[User] (
	[id] [NUMERIC] IDENTITY(0,1),
	[login] NVARCHAR(50) NOT NULL,
	[password] NVARCHAR(200) NOT NULL,
	[email] NVARCHAR(200) NOT NULL,
	[birthDate] DATE NOT NULL,
	[roles] NVARCHAR(500) NOT NULL,
	[isEnabled] BIT NOT NULL,
	[todoList] NVARCHAR(500),
	[creator_id] NVARCHAR(50),
	[rowVersion] TIMESTAMP,

	CONSTRAINT user_pk PRIMARY KEY ([id]),
	CONSTRAINT user_uk UNIQUE ([login]),
	CONSTRAINT userCreator_fk FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([login])
);

CREATE TABLE [dbo].[Company]
(
	[id] [NUMERIC] IDENTITY(0,1),
	[idOpenData] NVARCHAR(100) NOT NULL,
	[name] NVARCHAR(50) NOT NULL,
	[imageURL] NVARCHAR(100),
	[siteURL] NVARCHAR(200),
	[description] NVARCHAR(500),
	[status] NVARCHAR(50) NOT NULL, -- "Draft", "Existing", "Expired"
	[address] NVARCHAR(200) NOT NULL,
	[latitude] NUMERIC NOT NULL,
	[longitude] NUMERIC NOT NULL,
	[creationDate] DATE NOT NULL,
	[activitySector_id] [NUMERIC],
	[creator_id] NVARCHAR(50),
	[isPremium] BIT NOT NULL,
	[rowVersion] TIMESTAMP,

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
