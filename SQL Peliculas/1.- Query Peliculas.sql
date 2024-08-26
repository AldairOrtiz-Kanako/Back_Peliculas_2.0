CREATE DATABASE hub_megaPeliculas
GO

USE hub_megaPeliculas
GO

---Crear Tabla Usarios
Create TABLE Usuarios ( 
   UsuarioID INT IDENTITY(1,1)  PRIMARY KEY,
   Nombre NVARCHAR(200)NOT NULL ,
   Apellido NVARCHAR(200) NOT NULL,
   NombreUsuario NVARCHAR(50) NOT NULL,
   Correo NVARCHAR(100) NOT NULL,
   Contrasena NVARCHAR(200) NOT NULL,
   ContrasenaHash VARBINARY(70) NOT NULL,
   Sal VARBINARY(32) NOT NULL,
   FechaRegistro DATE 
)
GO

---Crear Tabla Genero
CREATE TABLE  Genero(
     GeneroID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	 GeneroNombre NVARCHAR(140) NOT NULL
)
GO

---Crear Tabla Director
CREATE TABLE Director (
    DirectorID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	DirectorNombre NVARCHAR(25) NOT NULL
)
GO

--Crear Tabla Peliculas
CREATE TABLE Peliculas(
   PeliculasID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
   Titulo NVARCHAR(100) NOT NULL,
   FechaEstreno DATE NULL,
   Duracion NVARCHAR(10) NULL,
   Sinopsis NVARCHAR(MAX) NULL,
   GeneroID INT,
   DirectorID INT,
   ImageURL NVARCHAR(MAX) NULL,
   Trailer NVARCHAR(MAX) NULL,
   FOREIGN KEY (GeneroID) REFERENCES Genero(GeneroID),
   FOREIGN KEY (DirectorID) REFERENCES Director(DirectorID)
)
GO

--Crear Tabla Series
CREATE TABLE Series(
     SeriesID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	 Titulo NVARCHAR(100) NOT NULL,
	 Temporadas INT NOT NULL,
	 Episodios INT NOT NULL,
	 FechaEstreno DATE NULL,
	 Sinopsis NVARCHAR(MAX) NULL,
	 GeneroID INT,
	 DirectorID INT,
	 ImageURL NVARCHAR(255) NULL,
	 Trailer NVARCHAR(MAX) NULL,
	 FOREIGN KEY (GeneroID) REFERENCES Genero(GeneroID),
     FOREIGN KEY (DirectorID) REFERENCES Director(DirectorID)
)
GO

--Crear Tabla Favoritos
CREATE TABLE Favoritos(
     FavoritosID INT IDENTITY(1,1) NOT NULL,
	 UsuarioID INT,
	 PeliculasID INT,
	 SeriesID INT,
	 FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
	 FOREIGN KEY (PeliculasID) REFERENCES Peliculas(PeliculasID),
     FOREIGN KEY (SeriesID) REFERENCES Series(SeriesID)
)
Go


---SP Para crear usuarios
CREATE PROCEDURE [dbo].[CrearUsuarios]
@Nombre NVARCHAR(100),
@Apellido NVARCHAR(100),
@NombreUsuario NVARCHAR(50),
@Correo NVARCHAR(100),
@Contrasena NVARCHAR(255) -- Asegúrate de que el tamaño sea suficiente para la contraseña en texto plano
AS
BEGIN 
-- Declarar variables para la sal y el hash
DECLARE @Sal VARBINARY(32) = NEWID();
DECLARE @ContrasenaHash VARBINARY(64); -- SHA-512 genera un hash de 64 bytes

-- Crear el hash de la contraseña combinada con la sal
-- Convertir la contraseña a VARBINARY antes de concatenar
SET @ContrasenaHash = HASHBYTES('SHA2_512', CONVERT(VARBINARY(MAX), @Contrasena) + @Sal);

-- Insertar el nuevo usuario en la base de datos
INSERT INTO Usuarios (Nombre, Apellido, NombreUsuario, Correo, Contrasena, ContrasenaHash, Sal)
VALUES (@Nombre, @Apellido, @NombreUsuario, @Correo, @Contrasena, @ContrasenaHash, @Sal);
END
GO

---SP para añadir a favoritos
CREATE PROCEDURE [dbo].[AgregarAFavoritos]
@UsuarioID INT,
@PeliculasID INT = NULL,
@SeriesID INT = NULL
AS
BEGIN
-- Verificar que solo uno de los IDs (PeliculasID o SeriesID) esté presente
IF (@PeliculasID IS NOT NULL AND @SeriesID IS NOT NULL)
BEGIN
	RAISERROR('Debe proporcionar solo un ID de película o serie, no ambos.', 16, 1);
	RETURN;
END

-- Verificar que al menos uno de los IDs esté presente
IF (@PeliculasID IS NULL AND @SeriesID IS NULL)
BEGIN
	RAISERROR('Debe proporcionar un ID de película o serie.', 16, 1);
	RETURN;
END

-- Verificar si el usuario ya tiene este favorito registrado
IF (@PeliculasID IS NOT NULL)
BEGIN
	IF EXISTS (SELECT 1 FROM Favoritos WHERE UsuarioID = @UsuarioID AND PeliculasID = @PeliculasID)
		BEGIN
			RAISERROR('La película ya está en los favoritos.', 16, 1);
			RETURN;
		END
	END
    ELSE
		BEGIN
			IF EXISTS (SELECT 1 FROM Favoritos WHERE UsuarioID = @UsuarioID AND SeriesID = @SeriesID)
			BEGIN
				RAISERROR('La serie ya está en los favoritos.', 16, 1);
				RETURN;
			END
    END

-- Insertar el nuevo favorito
INSERT INTO Favoritos (UsuarioID, PeliculasID, SeriesID)
VALUES (@UsuarioID, @PeliculasID, @SeriesID);
END
GO

--SP para eliminar de favoritos
CREATE PROCEDURE [dbo].[EliminarDeFavoritos]
@UsuarioID INT,
@PeliculasID INT = NULL,
@SeriesID INT = NULL
AS
BEGIN
-- Verificar que se haya proporcionado al menos uno de los IDs
IF (@PeliculasID IS NULL AND @SeriesID IS NULL)
BEGIN
	RAISERROR('Debe proporcionar un ID de película o serie para eliminar.', 16, 1);
	RETURN;
END

-- Verificar que solo uno de los IDs (PeliculasID o SeriesID) esté presente
IF (@PeliculasID IS NOT NULL AND @SeriesID IS NOT NULL)
	BEGIN
		RAISERROR('Debe proporcionar solo un ID de película o serie, no ambos.', 16, 1);
        RETURN;
    END

-- Eliminar el registro de favoritos correspondiente
DELETE FROM Favoritos
WHERE UsuarioID = @UsuarioID
AND (PeliculasID = @PeliculasID OR SeriesID = @SeriesID);

-- Verificar si se eliminó algún registro
IF @@ROWCOUNT = 0
BEGIN
	RAISERROR('No se encontró el favorito especificado para el usuario.', 16, 1);
END
END
GO

--SP para Obtener lista de peliculas
CREATE PROCEDURE ObtenerPeliculas
AS
BEGIN
SET NOCOUNT ON;

SELECT
p.Titulo AS Titulo,
p.FechaEstreno AS FechaEstreno,
p.Duracion AS Duracion,
p.Sinopsis AS Sinopsis,
g.GeneroNombre AS Genero,
d.DirectorNombre AS Director,
p.ImageURL AS Poster,
p.Trailer As Trailer
FROM Peliculas p
JOIN Genero g ON p.GeneroID = g.GeneroID
JOIN Director d ON p.DirectorID = d.DirectorID
END
GO

--SP para obtener lista de series
CREATE PROCEDURE ObtenerSeries
AS
BEGIN
SET NOCOUNT ON;

SELECT
s.Titulo AS Titulo,
s.Temporadas AS Temporadas,
s.Episodios AS Episodios,
s.FechaEstreno AS FechaEstreno,
s.Sinopsis AS Sinopsis,
g.GeneroNombre AS Genero,
d.DirectorNombre AS Director,
s.ImageURL AS Poster,
s.Trailer As Trailer
FROM Series s
JOIN Genero g ON s.GeneroID = g.GeneroID
JOIN Director d ON s.DirectorID = d.DirectorID
END
GO

--SP para obtener Catalogo completo
CREATE PROCEDURE ObtenerCatalogoCompleto
AS
BEGIN
SET NOCOUNT ON;

SELECT
'Serie' AS Tipo,
s.Titulo AS Titulo,
s.Temporadas AS Temporadas,
s.Episodios AS Episodios,
s.FechaEstreno AS FechaEstreno,
s.Sinopsis AS Sinopsis,
g1.GeneroNombre AS Genero,
d1.DirectorNombre AS Director,
s.ImageURL AS Poster,
s.Trailer AS Trailer,
NULL AS Duracion
FROM Series s
JOIN Genero g1 ON s.GeneroID = g1.GeneroID
JOIN Director d1 ON s.DirectorID = d1.DirectorID
UNION ALL
SELECT
'Pelicula' AS Tipo,
p.Titulo AS Titulo,
NULL AS Temporadas,
NULL AS Episodios,
p.FechaEstreno AS FechaEstreno,
p.Sinopsis AS Sinopsis,
g2.GeneroNombre AS Genero,
d2.DirectorNombre AS Director,
p.ImageURL AS Poster,
p.Trailer AS Trailer,
p.Duracion AS Duracion
FROM Peliculas p
JOIN Genero g2 ON p.GeneroID = g2.GeneroID
JOIN Director d2 ON p.DirectorID = d2.DirectorID;
END
GO

--Llave para JWT
SELECT NEWID()

