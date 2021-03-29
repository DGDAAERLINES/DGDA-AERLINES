use [tempdb]

--crear la base de datos--
create database DGDAaerlines
go

--usar la base de datos
use [DGDAaerlines];

-- Crear los schema de la base de datos
CREATE SCHEMA Usuarios
GO

CREATE SCHEMA Aerlines
GO


--tabla usuario--
create table Usuarios.usuario(
	id INT NOT NULL IDENTITY (500, 1),
	nombreCompleto VARCHAR(255) NOT NULL,
	username VARCHAR(100) NOT NULL,
	password VARCHAR(100) NOT NULL,
	estado BIT NOT NULL,
	CONSTRAINT PK_Usuario_id
		PRIMARY KEY CLUSTERED (id)
)

--Tabla pais--

create table Aerlines.Pais (
	id int not null,
	nombre varchar (80),
	constraint Pk_Pais_id
	primary key  CLUSTERED  (id)
 )
go



--tabla pasajero--
CREATE TABLE  Aerlines.Pasajero (
  idPasajero INT NOT NULL,
  nombre VARCHAR(45) NOT NULL,
  apellido VARCHAR(45) NOT NULL,
  idPais INT NOT NULL,
  sexo CHAR(1) NOT NULL,
  edad CHAR(2) NOT NULL,
  telefono VARCHAR(20) NOT NULL,
  idTipoVisa INT NULL,
  idPasaporte CHAR(1) NOT NULL,
   CONSTRAINT Pk_Pasajero_idPasajero
  PRIMARY KEY  CLUSTERED  (idPasajero)
  )
  go

 


	--Tabla Vuelo--
CREATE TABLE  Aerlines.Vuelo (
  idVuelo INT NOT NULL,
  fechaSalida DATE NOT NULL,
  horaSalida TIME NOT NULL,
  fechaLlegada DATE NOT NULL,
  horaLlegada TIME NOT NULL,
  origen varchar(30),
  destino varchar (30),
  constraint PK_Vuelo_idvuelo
  PRIMARY KEY  CLUSTERED  (idVuelo)
)
go


--Tabla Clase--
create table Aerlines.clase(
	idclase int not null,
	NombreClase varchar (15),
	constraint Pk_Clase_idclase
  PRIMARY KEY  CLUSTERED  (idclase)
)
go

--Tabla Facturacion--
CREATE TABLE Aerlines.DetallePrecio (
  idPrecio INT NOT NULL,
  idClase INT NOT NULL,
  Precio DECIMAL(8,2) NOT NULL,
  PRIMARY KEY CLUSTERED   (idPrecio),
  CONSTRAINT pk_DetallePrecio_idClase
    FOREIGN KEY (idClase)
    REFERENCES Aerlines.Clase (idClase)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
go

--Tabla Boleto--
create TABLE Aerlines.Boleto (
  idBoleto INT NOT NULL,
  idVuelo INT NOT NULL,
  fecha DATETIME NOT NULL,
  idPasajero INT NOT NULL,
  idDetallePrecio INT NOT NULL,
  idClase INT NOT NULL,
  PRIMARY KEY   CLUSTERED  (idBoleto),
  --Restricciones para llaves foraneas--
   CONSTRAINT fk_Boleto_idVuelo
    FOREIGN KEY (idVuelo)
    REFERENCES Aerlines.Vuelo (idVuelo)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_Boleto_idPasajero
    FOREIGN KEY (idPasajero)
    REFERENCES Aerlines.Pasajero (idPasajero)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_Boleto_idDetallePrecio
    FOREIGN KEY (idDetallePrecio)
    REFERENCES Aerlines.DetallePrecio (idPrecio)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_Boleto_idClase
    FOREIGN KEY (idClase)
    REFERENCES Aerlines.Clase (idClase)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
  
go

--Tabla emision de pasaporte--
create table aerlines.emisionPasaporte(
	id int not null,
	Nacionalidad varchar (20) not null,
	Nombres varchar(15) not null,
	Apellidos varchar(15),
	Edad char(2) not null,
	sexo varchar(1) not null,
	NumeroIdentidad char(13) not null,
	FechaDeNacimiento char(10) not null,
	constraint PK_emisionPasaporte_id
	primary key  CLUSTERED  (id)
)
go


  --Restrinciones--
  -- No puede existir nombres de usuarios repetidos
ALTER TABLE Usuarios.Usuario
	ADD CONSTRAINT AK_Usuarios_Usuario_username
	UNIQUE NONCLUSTERED (username)
GO

-- La contraseña debe contener al menos 6 caracteres
ALTER TABLE Usuarios.Usuario WITH CHECK
	ADD CONSTRAINT CHK_Usuarios_Usuario$VerificarLongitudContraseña
	CHECK (LEN(password) >= 6)
GO

-- La fecha de salida no puede ser menor o igual a la fecha de llegada
ALTER TABLE Aerlines.vuelo WITH CHECK
	ADD CONSTRAINT CHK_Aerlines_Vuelo$VerificarFechaSalida
	CHECK (fechaSalida > fechaLlegada)
GO

----------------------------------------------------------------PROCEDIMIENTOS-------------------------------------------------------------------------

create procedure CONSULTAR_PAIS
@id int
AS
BEGIN
	select nombre as 'Nombre' from Aerlines.Pais where id like '%' +@id+ '%'
END
GO
create procedure INSERTAR_PAIS
@id int,
@nombre varchar(80)
AS
BEGIN
	if exists (select nombre from Aerlines.Pais where id = @id)
	raiserror ('Ya existe ese pais, porfavor ingrese uno nuevo',16,1)
	else
	insert into Aerlines.Pais(id, nombre)
	values(@id, @nombre)
END
GO

create procedure MODIFICAR_NOMBRE_PAIS
@id int,
@nombre varchar(80)
AS
BEGIN
	update Aerlines.Pais
	set nombre = @nombre
	where id = @id
END
GO

create procedure MODIFICAR_CODIGO_PAIS
@id int,
@nombre varchar(80)
AS
BEGIN
	update Aerlines.Pais
	set id = @id
	where nombre = @nombre
END
GO

create procedure ELIMINAR_PAIS
@id int
AS
BEGIN
	delete from Aerlines.Pais 
	where id = @id
END
GO

create procedure CONSULTAR_PASAJERO
@id int
AS
BEGIN
	select nombre as 'Nombre', apellido as 'Apellido', idPais as 'ID Pais', sexo as 'Sexo', edad as 'Edad', telefono as 'Telefono', idTipoVisa as 'ID Tipo Visa',
	idPasaporte as 'ID Pasaporte' 
	from Aerlines.Pasajero where idPasajero like '%' +@id+ '%'
END
GO
create procedure INSERTAR_PASAJERO
@id int,
@nombre varchar(80),
@apellido varchar(45),
@idPais int,
@sexo char(1),
@edad char(2),
@telefono varchar(20),
@idTipoVisa int,
@idPasaporte char(1)
AS
BEGIN
	if exists (select idPasaporte from Aerlines.Pasajero where idPasajero = @id)
	raiserror ('Ya existe ese pasajero, porfavor ingrese uno nuevo',16,1)
	else
	insert into Aerlines.Pasajero(idPasajero, nombre, apellido, idPais, sexo, edad, telefono, idTipoVisa, idPasaporte)
	values(@id, @nombre, @apellido, @idPais, @sexo, @edad, @telefono, @idTipoVisa, @idPasaporte)
END
GO

create procedure MODIFICAR_PASAJERO
@id int,
@nombre varchar(80),
@apellido varchar(45),
@idPais int,
@sexo char(1),
@edad char(2),
@telefono varchar(20),
@idTipoVisa int,
@idPasaporte char(1)
AS
BEGIN
	update Aerlines.Pasajero
	set nombre = @nombre, apellido = @apellido, idPais = @idPais, sexo = @sexo, edad = @edad, telefono = @telefono, idTipoVisa = @idTipoVisa, idPasaporte = @idPasaporte
	where idPasajero = @id
END
GO

create procedure ELIMINAR_PASAJERO
@id int
AS
BEGIN
	delete from Aerlines.Pasajero 
	where idPasajero = @id
END
GO

-------------------------------------------------------------Inserciones---------------------------------------------------------------------------------
use DGDAaerlines
go

insert into Pais(id, nombre)
values(004, 'Afganistán'),
(008, 'Albania'),
(276, 'Alemania'),
(020, 'Andorra'),
(024, 'Angola'),
(028, 'Antigua y Barbuda'),
(682, 'Arabia Saudita'),
(012, 'Argelia'),
(032, 'Argentina'),
(051, 'Armenia'),
(036, 'Australia'),
(040, 'Austria'),
(031, 'Azerbaiyán'),
(044, 'Bahamas'),
(050, 'Bangladés'),
(052, 'Barbados'),
(048, 'Baréin'),
(056, 'Bélgica'),
(084, 'Belice'),
(204, 'Benín'),
(112, 'Bielorrusia'),
(104, 'Birmania/Myanmar'),
(068, 'Bolivia'),
(070, 'Bosnia y Herzegovina'),
(072, 'Botsuana'),
(076, 'Brasil'),
(096, 'Brunéi'),
(100, 'Bulgaria'),
(854, 'Burkina Faso'),
(108, 'Burundi'),
(064, 'Bután'),
(132, 'Cabo Verde'),
(116, 'Camboya'),
(120, 'Camerún'),
(124, 'Canadá'),
(634, 'Catar'),
(148, 'Chad'),
(152, 'Chile'),
(156, 'China'),
(196, 'Chipre'),
(170, 'Colombia'),
(174, 'Comoras'),
(408, 'Corea del Norte'),
(410, 'Corea del Sur'),
(384, 'Costa de Marfil'),
(188, 'Costa Rica'),
(191, 'Croacia'),
(192, 'Cuba'),
(208, 'Dinamarca'),
(212, 'Dominica'),
(218, 'Ecuador'),
(818, 'Egipto'),
(222, 'El Salvador'),
(784, 'Emiratos Árabes Unidos'),
(232, 'Eritrea'),
(703, 'Eslovaquia'),
(705, 'Eslovenia'),
(724, 'España'),
(840, 'Estados Unidos'),
(233, 'Estonia'),
(231, 'Etiopía'),
(608, 'Filipinas'),
(246, 'Finlandia'),
(242, 'Fiyi'),
(250, 'Francia'),
(266, 'Gabón'),
(270, 'Gambia'),
(268, 'Georgia'),
(288, 'Ghana'),
(308, 'Granada'),
(300, 'Grecia'),
(320, 'Guatemala'),
(328, 'Guyana'),
(324, 'Guinea'),
(226, 'Guinea Ecuatorial'),
(624, 'Guinea-Bisáu'),
(332, 'Haití'),
(340, 'Honduras'),
(348, 'Hungría'),
(356, 'India'),
(360, 'Indonesia'),
(368, 'Irak'),
(364, 'Irán'),
(372, 'Irlanda'),
(352, 'Islandia'),
(584, 'Islas Marshall'),
(090, 'Islas Salomón'),
(376, 'Israel'),
(380, 'Italia'),
(388, 'Jamaica'),
(392, 'Japón'),
(400, 'Jordania'),
(398, 'Kazajistán'),
(404, 'Kenia'),
(417, 'Kirguistán'),
(296, 'Kiribati'),
(414, 'Kuwait'),
(418, 'Laos'),
(426, 'Lesoto'),
(428, 'Letonia'),
(422, 'Líbano'),
(430, 'Liberia'),
(434, 'Libia'),
(438, 'Liechtenstein'),
(440, 'Lituania'),
(442, 'Luxemburgo'),
(807, 'Macedonia del Norte'),
(450, 'Madagascar'),
(458, 'Malasia'),
(454, 'Malaui'),
(462, 'Maldivas'),
(466, 'Malí'),
(470, 'Malta'),
(504, 'Marruecos'),
(480, 'Mauricio'),
(478, 'Mauritania'),
(484, 'México'),
(583, 'Micronesia'),
(498, 'Moldova'),
(492, 'Mónaco'),
(496, 'Mongolia'),
(499, 'Montenegro'),
(508, 'Mozambique'),
(516, 'Namibia'),
(520, 'Nauru'),
(524, 'Nepal'),
(558, 'Nicaragua'),
(562, 'Níger'),
(566, 'Nigeria'),
(578, 'Noruega'),
(554, 'Nueva Zelanda'),
(512, 'Omán'),
(528, 'Países Bajos'),
(586, 'Pakistán'),
(585, 'Palaos'),
(591, 'Panamá'),
(598, 'Papúa Nueva Guinea'),
(600, 'Paraguay'),
(604, 'Perú'),
(616, 'Polonia'),
(620, 'Portugal'),
(630, 'Puerto Rico'),
(826, 'Reino Unido'),
(140, 'República Centroafricana'),
(203, 'República Checa'),
(178, 'República del Congo'),
(180, 'República Democrática del Congo'),
(214, 'República Dominicana'),
(710, 'República Sudafricana'),
(646, 'Ruanda'),
(642, 'Rumanía'),
(643, 'Rusia'),
(882, 'Samoa'),
(659, 'San Cristóbal y Nieves'),
(674, 'San Marino'),
(670, 'San Vicente y las Granadinas'),
(662, 'Santa Lucía'),
(678, 'Santo Tomé y Príncipe'),
(686, 'Senegal'),
(891, 'Serbia'),
(690, 'Seychelles'),
(694, 'Sierra Leona'),
(702, 'Singapur'),
(760, 'Siria'),
(706, 'Somalia'),
(144, 'Sri Lanka'),
(748, 'Suazilandia'),
(729, 'Sudán'),
(728, 'Sudán del Sur'),
(752, 'Suecia'),
(756, 'Suiza'),
(740, 'Surinam'),
(764, 'Tailandia'),
(834, 'Tanzania'),
(762, 'Tayikistán'),
(626, 'Timor Oriental'),
(768, 'Togo'),
(776, 'Tonga'),
(780, 'Trinidad y Tobago'),
(788, 'Túnez'),
(795, 'Turkmenistán'),
(792, 'Turquía'),
(798, 'Tuvalu'),
(804, 'Ucrania'),
(800, 'Uganda'),
(858, 'Uruguay'),
(860, 'Uzbekistán'),
(548, 'Vanuatu'),
(862, 'Venezuela'),
(704, 'Vietnam'),
(887, 'Yemen'),
(262, 'Yibuti'),
(894, 'Zambia'),
(716, 'Zimbabue')
;
