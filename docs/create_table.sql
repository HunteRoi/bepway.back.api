--ALTER TABLE [dbo].[Company] DROP CONSTRAINT company_pk, activitySector_fk, companyCreator_fk;
--ALTER TABLE [db].[User] DROP CONSTRAINT user_pl, user_uk, userCreator_fk;

DROP TABLE [dbo].[Company];
--DROP TABLE [dbo].[Intersection];
--DROP TABLE [dbo].[RoadCoordinates];
--DROP TABLE [dbo].[Road];
DROP TABLE [dbo].[Zoning];
DROP TABLE [dbo].[Coordinates];
DROP TABLE [dbo].[User];
DROP TABLE [dbo].[ActivitySector];

CREATE TABLE [dbo].[ActivitySector]
(
	[id] INT IDENTITY(0,1),
	[name] NVARCHAR(200) NOT NULL,

	CONSTRAINT activitySector_pk PRIMARY KEY ([id])
);

CREATE TABLE [dbo].[User] (
	[id] INT IDENTITY(0,1),
	[login] NVARCHAR(50) NOT NULL,
	[password] NVARCHAR(200) NOT NULL,
	[email] NVARCHAR(200) NOT NULL,
	[birthDate] DATE NOT NULL,
	[roles] NVARCHAR(500) NOT NULL,
	[isEnabled] BIT NOT NULL,
	[todoList] NVARCHAR(500),
	[creator_id] INT,
	[rowVersion] TIMESTAMP,

	CONSTRAINT user_pk PRIMARY KEY ([id]),
	CONSTRAINT user_uk UNIQUE ([login]),
	CONSTRAINT userCreator_fk FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([id])
);


CREATE TABLE [dbo].[Coordinates]
(
	[id] INT IDENTITY(0,1),
	[latitude] NVARCHAR(50) NOT NULL,
	[longitude] NVARCHAR(50) NOT NULL,
	
	CONSTRAINT coordinates_pk PRIMARY KEY ([id])
);

CREATE TABLE [dbo].[Zoning]
(
	[id] INT IDENTITY(0,1),
	[idOpenData] NVARCHAR(100) NOT NULL,
	[ntisid] INT NOT NULL,
	[name] NVARCHAR(200) NOT NULL,
	[coordinates_id] INT NOT NULL,
	
	CONSTRAINT zoning_pk PRIMARY KEY ([id]),
	CONSTRAINT zoning_uk UNIQUE ([name]),
	CONSTRAINT zoning_openData_uk UNIQUE ([idOpenData]),
	constraint zoningCoordinates_fk
		FOREIGN KEY ([coordinates_id])
		REFERENCES [dbo].[Coordinates] ([id])
		ON DELETE CASCADE
);

--CREATE TABLE [dbo].[Road]
--(
--	[id] INT IDENTITY(0,1),
--	[zoning_id] INT NOT NULL,
	
--	CONSTRAINT road_pk PRIMARY KEY ([id]),
--	constraint roadZoning_fk
--		FOREIGN KEY ([zoning_id])
--		REFERENCES [dbo].[Zoning] ([id])
--);

--CREATE TABLE [dbo].[RoadCoordinates]
--(
--	[id] INT IDENTITY(0,1),
--	[zoning_id] INT NOT NULL,
--	[coordinates_id] INT NOT NULL,
	
--	CONSTRAINT roadCoordinates_pk PRIMARY KEY ([id]),
--	constraint roadCoordinatesZoning_fk
--		FOREIGN KEY ([zoning_id])
--		REFERENCES [dbo].[Zoning] ([id]),
--	constraint roadCoordinatesCoords_fk
--		FOREIGN KEY ([coordinates_id])
--		REFERENCES [dbo].[Coordinates] ([id])
--		ON DELETE CASCADE
--);

--CREATE TABLE [dbo].[Intersection]
--(
--	[id] INT IDENTITY(0,1),
	
--	CONSTRAINT interseciton_pk PRIMARY KEY ([id])
--);

CREATE TABLE [dbo].[Company]
(
	[id] INT IDENTITY(0,1),
	[idOpenData] NVARCHAR(100) NOT NULL,
	[name] NVARCHAR(50) NOT NULL,
	[imageURL] NVARCHAR(100),
	[siteURL] NVARCHAR(200),
	[description] NVARCHAR(500),
	[status] NVARCHAR(50) NOT NULL, -- "Draft", "Existing", "Expired"
	[address] NVARCHAR(200) NOT NULL,
	[creationDate] DATE NOT NULL,
	[activitySector_id] INT,
	[creator_id] NVARCHAR(50),
	[coordinates_id] INT NOT NULL,
	[zoning_id] INT NOT NULL,
	[isPremium] BIT NOT NULL,
	[rowVersion] TIMESTAMP,

	CONSTRAINT company_pk PRIMARY KEY ([id]),
	CONSTRAINT company_uk UNIQUE ([idOpenData]),
	CONSTRAINT activitySector_fk 
		FOREIGN KEY ([activitySector_id]) 
		REFERENCES [dbo].[ActivitySector] ([id]) 
		ON DELETE SET NULL,
	CONSTRAINT companyCreator_fk
		FOREIGN KEY ([creator_id])
		REFERENCES [dbo].[User] ([login])
		ON DELETE SET NULL,
	CONSTRAINT companyCoordinates_fk
		FOREIGN KEY ([coordinates_id])
		REFERENCES [dbo].[Coordinates] ([id])
		ON DELETE CASCADE,
	CONSTRAINT zoning_fk
		FOREIGN KEY ([zoning_id])
		REFERENCES [dbo].[Zoning] ([id])
		ON DELETE CASCADE;
);