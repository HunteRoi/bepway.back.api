-- missing hashed passwords

--TRUNCATE TABLE [dbo].[User];

INSERT 
	INTO [dbo].[User] 
	([login], [password], [email], [birthDate], [roles], [isEnabled])
	VALUES (
		'hunteroi',
		'JfSNVtDAMFSUtYxHBVbAPb/CoTUUMveRrXP1umpjyqo=.NBkSxy9rUR1kiq2pucANXA==',
		'hunteroi@bep.be',
		'19990321',
		'Admin',
		1
	);
--root

INSERT
	INTO [dbo].[User]
	([login], [password], [email], [birthDate], [roles], [isEnabled], [creator_id])
	VALUES (
		'imnoot',
		'cA2rOJvY6XgviX1tzAoz0hQz+l49Png9Zc8iT6kqGIU=.p0dxYVxs7mz89AhuSlFxgg==',
		'imnoot@bep.be',
		'19980917',
		'Gestionnary',
		1,
		(SELECT [id] FROM [dbo].[User] WHERE [login] = 'hunteroi')
	);
--NootNoot

INSERT
	INTO [dbo].[User]
	([login], [password], [email], [birthDate], [roles], [isEnabled], [creator_id])
	VALUES (
		'mdpschsa',
		'7fhnBBKfPcZjFUNgQTFsSCpwJt5+y5MkdFt5lV1IOXA=.lpsYHvPscFWqh+/YD/ATRg==',
		'samuel.scholtes@bep.be',
		'19840101',
		'User',
		0,
		(SELECT [id] FROM [dbo].[User] WHERE [login] = 'hunteroi')
	);
--schsa