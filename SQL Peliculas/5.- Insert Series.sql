INSERT INTO [dbo].[Series]
([Titulo]
,[Temporadas]
,[Episodios]
,[FechaEstreno]
,[Sinopsis]
,[GeneroID]
,[DirectorID]
,[ImageURL]
,[Trailer])
VALUES
('The Haunting of Hill House', 1, 10, '2018-10-12', 
'Un grupo de hermanos se enfrenta a los fantasmas de su pasado tras volver a la casa donde crecieron y que ha sido una de las casas embrujadas más conocidas del país.',
 1, 9, 
'https://es.web.img2.acsta.net/pictures/18/09/20/08/44/5720467.jpg',
'https://youtu.be/mTvNeafShH0'),
('Stranger Things', 4, 34, '2016-07-15', 
'Cuando un niño desaparece, su madre, un jefe de policía y sus amigos deben enfrentarse a fuerzas terroríficas para recuperarlo.',
 2, 10, 
'https://sm.ign.com/t/ign_es/tv/s/stranger-t/stranger-things_tqfy.1200.jpg',
'https://youtu.be/mnd7sFt5c3A'),
('The Mandalorian', 3, 24, '2019-11-12', 
'Un cazador de recompensas solitario explora las regiones exteriores de la galaxia, lejos de la autoridad de la Nueva República.',
 2, 11, 
'https://i.imgur.com/iaUN5EA.jpeg',
'https://youtu.be/aOC8E8z_ifw'),
('Sherlock', 4, 13, '2010-07-25', 
'Una versión moderna del detective más famoso del mundo, Sherlock Holmes, quien usa la lógica y la tecnología moderna para resolver crímenes.',
 7, 12, 
'https://i.pinimg.com/originals/83/3a/da/833adadb73431f86b1642897ea871456.jpg',
'https://youtu.be/xK7S9mrFWL4'),
('Attack on Titan', 4, 87, '2013-04-06', 
'La humanidad se enfrenta a los Titanes, seres gigantes que devoran humanos, desde dentro de unas murallas protectoras.',
 3, 13, 
'https://es.web.img2.acsta.net/pictures/14/03/25/09/51/067342.jpg',
'https://youtu.be/MGRm4IzK1SQ'),
('The Witcher', 3, 24, '2019-12-20', 
'Geralt de Rivia, un cazador de monstruos solitario, lucha por encontrar su lugar en un mundo donde las personas a menudo son más crueles que las bestias.',
 8, 14, 
'https://i.3djuegos.com/juegos/17680/the_witcher__la_serie__temporada_2/fotos/ficha/the_witcher__la_serie__temporada_2-5550842.webp',
'https://youtu.be/ndl1W4ltcmg'),
('Brooklyn Nine-Nine', 8, 153, '2013-09-17', 
'Un grupo diverso de detectives de una comisaría de Nueva York enfrenta casos de todo tipo, a menudo con un toque de humor absurdo.',
 5, 15, 
'https://image.tmdb.org/t/p/original/eTw0aeKQp32u0440WYU5qH2DPoU.jpg',
'https://youtu.be/sEOuJ4z5aTc'),
('Bridgerton', 2, 16, '2020-12-25', 
'En el Londres de la era Regencia, las familias de la alta sociedad buscan el amor y la aventura mientras lidian con escándalos y secretos.',
 4, 16, 
'https://es.web.img3.acsta.net/pictures/20/11/04/12/03/4756219.jpg?coixp=50&coiyp=32',
'https://youtu.be/gpv7ayf_tyE')
GO

SELECT * FROM Series