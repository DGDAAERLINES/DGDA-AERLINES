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

