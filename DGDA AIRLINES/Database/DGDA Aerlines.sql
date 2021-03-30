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
	id int not null IDENTITY (500, 1),
	nombre varchar (50),
	constraint Pk_Pais_id
	primary key  CLUSTERED  (id)
 )
go



--/ tabla pasajero--
create TABLE  Aerlines.Pasajero (
  idPasajero INT NOT NULL IDENTITY (1, 1),
  nombres VARCHAR(45) NOT NULL,
  apellidos VARCHAR(45) NOT NULL,
  sexo CHAR(1) NOT NULL,
  edad CHAR(2) NOT NULL,
  telefono VARCHAR(8) NOT NULL,
  idTipoVisa INT NULL,
  idPasaporte int NOT NULL,
  idPais INT NOT NULL,
   CONSTRAINT Pk_Pasajero_idPasajero
  PRIMARY KEY  CLUSTERED  (idPasajero)
  )
  go



 --Tabla de pasaporte--
 create table Aerlines.Pasaporte(
 idPasaporte int not null identity (1,1),
 DescripcionPasaporte varchar(15)
  CONSTRAINT Pk_Pasaporte_idPasaporte
  PRIMARY KEY  CLUSTERED  (idPasaporte)
 )
 go

 --Tabla de visa--
 create table Aerlines.TipoVisa(
 idTipoVisa int not null identity (1,1),
 Nombre varchar(25),
   CONSTRAINT Pk_TipoVisa_idTipoVisa
  PRIMARY KEY  CLUSTERED  ( idTipoVisa)
 )
 go

	-- / Tabla Vuelo--
create TABLE  Aerlines.Vuelo (
  idVuelo INT NOT NULL IDENTITY (1, 1),
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


-- / Tabla Clase--
create table Aerlines.clase(
	idclase int not null identity (1,1) ,
	NombreClase varchar (15),
	constraint Pk_Clase_idclase
  PRIMARY KEY  CLUSTERED  (idclase)
)
go


--/Tabla Facturacion--
create TABLE Aerlines.DetallePrecio(
  idPrecio INT identity (1,1),
  idClase INT NOT NULL,
  Precio DECIMAL(8,2) NOT NULL,
  PRIMARY KEY CLUSTERED   (idPrecio),
  CONSTRAINT pk_DetallePrecio_idClase
    FOREIGN KEY (idClase)
    REFERENCES Aerlines.Clase (idClase)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
go

--/Tabla Boleto--
create TABLE Aerlines.Boleto (
  idBoleto INT NOT NULL identity (1,1),
  fecha DATETIME NOT NULL,
  idVuelo INT NOT NULL,
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


--/Tabla emision de pasaporte--
create table aerlines.emisionPasaporte(
	id int not null IDENTITY (2, 1),
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

/*Para eliminar un constrain*/
/*ALTER TABLE Aerlines.Boleto DROP CONSTRAINT [fk_Boleto_idDetallePrecio]
ALTER TABLE Aerlines.detalleprecio DROP CONSTRAINT [pk_DetallePrecio_idClase]
GO*/





--insert de boleto--

insert into Aerlines.Boleto values ( '2020-08-01', 1, 1, 1,1)

--Insert Pasajero
insert into Aerlines.Pasajero values ('Josefina','Hernandez','F','30',99999898,1,1,1)

--insert Detalle Precio
insert into Aerlines.DetallePrecio values (1,1800)

select * from Aerlines.Boleto