IF OBJECT_ID('dbo.Rental', 'U') IS NOT NULL 
  DROP TABLE dbo.Rental; 
IF OBJECT_ID('dbo.Film', 'U') IS NOT NULL 
  DROP TABLE dbo.Film;
IF OBJECT_ID('dbo.Director', 'U') IS NOT NULL 
  DROP TABLE dbo.Director;
IF OBJECT_ID('dbo.FilmGenre', 'U') IS NOT NULL 
  DROP TABLE dbo.FilmGenre;
IF OBJECT_ID('dbo.Client', 'U') IS NOT NULL 
  DROP TABLE dbo.Client;

  
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Client' AND xtype='U')
  CREATE TABLE Client(
  clientId int NOT NULL IDENTITY(1, 1),
  name varchar(255) DEFAULT NULL,
  address varchar(255) DEFAULT NULL,
  city varchar(255) DEFAULT NULL,
  zip varchar(5) DEFAULT NULL,
  PRIMARY KEY (clientId)
);
GO

INSERT INTO Client (name,address,city,zip) VALUES 
	('Martin Guerre','7 rue de Gaule','Grenoble','38800'),
	('Gérard de la marre','54 Avenue Foch','Grenoble','38800');

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='FilmGenre' AND xtype='U')
  CREATE TABLE FilmGenre(
  genreId int NOT NULL IDENTITY(1, 1),
  name varchar(255) DEFAULT NULL,
  PRIMARY KEY (genreId)
);
GO

INSERT INTO FilmGenre (name) VALUES
	('Horror'),
	('Sci-Fi'),
	('Comedie'),
	('Drame');
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Director' AND xtype='U')
	CREATE TABLE Director(
	  directorId int NOT NULL IDENTITY(1, 1),
	  name varchar(255) DEFAULT NULL,
	  PRIMARY KEY (directorId)
);
GO

INSERT INTO Director (name) VALUES
	('Wes Craven'),
	('George Lucas'),
	('Alain Chabat');

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Film' AND xtype='U')
	CREATE TABLE Film(
	  filmId int NOT NULL IDENTITY(1, 1),
	  title varchar(255) DEFAULT NULL,
	  director int,
	  releaseDate date DEFAULT NULL,
	  genre int,
	  dailyPrice float,
	  PRIMARY KEY (filmId),
	  constraint fk_genre foreign key (genre) references FilmGenre(genreId) ON DELETE CASCADE,
	  constraint fk_director foreign key (director) references Director(directorId) ON DELETE CASCADE,
	);
GO

INSERT INTO Film (title,director,releaseDate,genre,dailyPrice) VALUES
	('La colline a des yeux',1,'1979-06-20',1,0.5),
	('un nouvel espoir',2,'1977-09-11',2,0.2),
	('Asterix Mission Cléopatre',3,'2002-01-30',3,1),
	('L Empire contre attaque',2,'1979-09-11',2,0.4);
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Rental' AND xtype='U')
	CREATE TABLE Rental(
	  rentalId int NOT NULL IDENTITY(1, 1),
	  client int,
	  film int,
	  dateLocation date,
	  dateLimiteRendu date,	
	  dateRendu date DEFAULT NULL,
  
	  PRIMARY KEY (rentalId),
	  FOREIGN KEY (client) REFERENCES Client(clientId) ON DELETE CASCADE,
	  FOREIGN KEY (film) REFERENCES Film(filmId) ON DELETE CASCADE
	);
GO

INSERT INTO Rental (client,film,dateLocation,dateLimiteRendu) VALUES
	/*(1,1,'2017-12-05','2018-12-20'),*/
	(1,2,'2017-12-05','2017-12-21'),
	(2,4,'2017-12-05','2017-12-22');

	
INSERT INTO Rental (client,film,dateLocation,dateLimiteRendu,dateRendu) VALUES
	(2,2,'2017-11-05','2017-12-05','2017-12-04');
