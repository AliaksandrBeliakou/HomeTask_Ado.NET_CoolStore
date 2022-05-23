INSERT INTO [dbo].[Products] VALUES
--Id, Name,					Description,				weight,	height,	width,	length
('Super computer',		'Calculate really fast',	100000,	2000,	400,	700),
('Developer station',	'For apps creation',		10000,	700,	20,		500),
('Play station 5',		'Gate to entertainment',	5000,	400,	20,		30),
('Notebook',				'Compact but useful',		2500,	35,		40,		25),
('Laptop',				'Modern type machine',		1250,	25,		30,		20),
('Tablet',				'Youtube is our all!',		700,	15,		20,		17),
('Smartphone',			'Be in touch',				1200,	10,		6,		10)


INSERT INTO [dbo].[Orders] VALUES
--Status,					CreateDate,				UpdateDate,	ProductId
('NotStarted',	'2021-05-21',	'2021-01-28',	1),
('Loading',		'2021-05-22',	'2021-02-28',	2),
('InProgress',	'2021-05-23',	'2021-03-28',	3),
('Arrived',		'2021-05-24',	'2021-04-28',	4),
('Unloading',	'2021-05-25',	'2021-05-28',	5),
('Cancelled',	'2021-05-26',	'2021-06-28',	6),
('Done',		'2021-05-27',	'2021-07-28',	7),
('Done',		'2021-05-28',	'2021-08-28',	1),
('Done',		'2021-05-29',	'2021-09-28',	2),
('Done',		'2021-05-20',	'2021-01-28',	3)
