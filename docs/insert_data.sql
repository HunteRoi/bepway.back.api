-- missing hashed passwords

DELETE FROM [dbo].[User] WHERE [id] = 0;
DELETE FROM [dbo].[User] WHERE [id] = 1;
DELETE FROM [dbo].[User] WHERE [id] = 2;

INSERT 
	INTO [dbo].[User] 
	([login], [password], [email], [birthDate], [roles], [isEnabled])
	VALUES (
		'hunteroi',
		'',
		'hunteroi@bep.be',
		'19990321',
		'Admin',
		1
	);

INSERT
	INTO [dbo].[User]
	([login], [password], [email], [birthDate], [roles], [isEnabled])
	VALUES (
		'imnoot',
		'',
		'imnoot@bep.be',
		'19980917',
		'Gestionnary',
		1
	);
	
INSERT
	INTO [dbo].[User]
	([login], [password], [email], [birthDate], [roles], [isEnabled])
	VALUES (
		'schsa',
		'',
		'samuel.scholtes@bep.be',
		'19840101',
		'User',
		0
	);