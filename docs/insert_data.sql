-- missing hashed passwords

DELETE FROM [dbo].[User] WHERE [id] = 0;
DELETE FROM [dbo].[User] WHERE [id] = 1;
DELETE FROM [dbo].[User] WHERE [id] = 2;

INSERT 
	INTO [dbo].[User] 
	([login], [password], [email], [birthDate], [roles], [isEnabled])
	VALUES (
		'hunteroi',
		'hdGMt7sL2z+OcavhqsXoQ1l50BJLmVqXjUC8no1fXqY=',
		'hunteroi@bep.be',
		'19990321',
		'Admin',
		1
	);
--root

INSERT
	INTO [dbo].[User]
	([login], [password], [email], [birthDate], [roles], [isEnabled])
	VALUES (
		'imnoot',
		'NDZMQ70MmY+Oy1doM0GMiaC0NF4i1+KhQbmA8mNarTE=',
		'imnoot@bep.be',
		'19980917',
		'Gestionnary',
		1
	);
--NootNoot

INSERT
	INTO [dbo].[User]
	([login], [password], [email], [birthDate], [roles], [isEnabled])
	VALUES (
		'schsa',
		'Az6REuYtQtEpu68eqys5X6vjMBvCN7NaqjULl2mO5fM=',
		'samuel.scholtes@bep.be',
		'19840101',
		'User',
		0
	);
--schsa