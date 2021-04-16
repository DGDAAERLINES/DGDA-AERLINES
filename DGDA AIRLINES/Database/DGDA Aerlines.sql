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
	id int not null identity (1,1),
	nombre varchar (100),
	constraint Pk_Pais_id
	primary key  CLUSTERED  (id)
 )
go



--tabla pasajero--
create TABLE  Aerlines.Pasajero (
  idPasajero INT NOT NULL identity(1,1),
  nombre VARCHAR(45) NOT NULL,
  apellido VARCHAR(45) NOT NULL,
  idPais INT NOT NULL,
  sexo CHAR(1) NOT NULL,
  edad CHAR(2) NOT NULL,
  telefono VARCHAR(20) NOT NULL,
   CONSTRAINT Pk_Pasajero_idPasajero
  PRIMARY KEY  CLUSTERED  (idPasajero)
  )
  go

 


	--Tabla Vuelo--

create table Aerlines.Vuelo(
  idVuelo INT NOT NULL identity(1,1) ,
  fechaSalida DATE NOT NULL,
  horaSalida TIME NOT NULL,
  fechaLlegada DATE NOT NULL,
  horaLlegada TIME NOT NULL,
  id int not null,
  idAeropuertoHN int not null,
  PRIMARY KEY  CLUSTERED  (idVuelo),
)
  
go



--Tabla Clase--
Create table Aerlines.clase(
	idclase int not null identity(1,1),
	NombreClase varchar (15),
	constraint Pk_Clase_idclase
  PRIMARY KEY  CLUSTERED  (idclase)
)
go


--Tabla Facturacion --
create TABLE Aerlines.DetallePrecio (
  idPrecio INT NOT NULL identity(1,1),
  idClase INT NOT NULL,
  Precio DECIMAL(8,2) NOT NULL,
  PRIMARY KEY CLUSTERED   (idPrecio),
  CONSTRAINT pk_DetallePrecio_idClase
    FOREIGN KEY (idClase)
    REFERENCES Aerlines.Clase (idClase)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
go

--Tabla Boleto falta --
create TABLE Aerlines.Boleto (
  idBoleto INT NOT NULL identity(1,1),
  Boleto varchar(50),
  idVuelo INT NOT NULL,
  idPasajero INT NOT NULL,
  idDetallePrecio INT NOT NULL,
  idClase INT NOT NULL,
  PRIMARY KEY   CLUSTERED  (idBoleto),
  --Restricciones para llaves foraneas--
   CONSTRAINT fk_Boleto_idVuelos
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

--Tabla de los aeropuertos de Honduras--
CREATE table Aerlines.AeropuertoHN(
idAeropuertoHN int not null identity(1,1),
Nombre varchar(80)
 PRIMARY KEY   CLUSTERED  (idAeropuertoHN),
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

-- La fecha de salida no puede ser menor o igual a la fecha de retorno
ALTER TABLE Aerlines.Reserva WITH CHECK
	ADD CONSTRAINT CHK_Aerlines_Vuelo$VerificarFechaSalida
	CHECK (fechaSalida > fechaLlegada)
GO

-- Llave foránea para el vuelo
ALTER TABLE Aerlines.Vuelo
	ADD CONSTRAINT FK_Aerlines_Vuelo$TieneUn$Aerlines_Aerlines
	FOREIGN KEY (id) REFERENCES Aerlines.Pais(id)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO

ALTER TABLE Aerlines.Vuelo
	ADD CONSTRAINT FK_Aerlines_AeropuertoHN$TieneUn$Aerlines_Aerlines
	FOREIGN KEY (idAeropuertoHN) REFERENCES Aerlines.AeropuertoHN(idAeropuertoHN)
	ON UPDATE NO ACTION
	ON DELETE NO ACTION
GO



------------------INSERT--------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------
-- Inserts AeropuertoHN --


INSERT INTO Aerlines.AeropuertoHN VALUES
('Honduras, TGS:Aeropuerto Internacional Toncontín'),
('Honduras, SPS:Aeropuerto Internacional Ramón Villeda Morales'),
('Honduras,Ceiba:Aeropuerto Internacional Goloson')
 
INSERT INTO Aerlines.AeropuertoHN VALUES ('Honduras,Ceiba:Aeropuerto Internacional Goloson',13)

select * from Aerlines.AeropuertoHN
-- Inserts Vuelo --

INSERT INTO Aerlines.Vuelo VALUES('2021-01-03', '11:00:00', '2021-01-01', '16:00:00', 1,14)
INSERT INTO Aerlines.Vuelo VALUES('2021-01-03', '12:00:00', '2021-01-01', '17:00:00', 1,5)

select * from Aerlines.Vuelo

---insert Aeropuerto--
select * from Aerlines.Pais

insert into Aerlines.Pais
values
('Honduras, TGS:Aeropuerto Internacional Toncontín'),
('Honduras, SPS:Aeropuerto Internacional Ramón Villeda Morales'),
('Honduras,Ceiba:Aeropuerto Internacional Goloson'),
('Estados Unidos, Houston:Aeropuerto Intercontinental George Bush'),
('Estados Unidos, New Jersey:Aeropuerto Internacional Libertad de Newark'),
('Colombia, Medellin:Aeropuerto Olaya Herrera'),
('Colombia, Cartajena:Aeropuerto Internacional Rafael Núñez'),
('Mexico,Veracruz:Aeropuerto Internacional de Veracruz'),
('Mexico,Tijuana:Aeropuerto Internacional de Tijuana'),
('Panama, Tocumen:Aeropuerto Internacional de Tocumen'),
('Panama, Arraijan:Aeropuerto Internacional Panamá Pacífico'),
('Corea del sur,Incheon:Aeropuerto Internacional de Incheon'),
('Corea del sur,Seul:Aeropuerto Internacional de Gimpo'),
('Bolivia,Santa cruz:Aeropuerto Internacional Viru Viru'),
('Bolivia,Cochabamba:Aeropuerto Internacional Jorge Wilsterman'),
('El salvador,San miguel:Aeropuerto Regional de San Miguel'),
('El salvador:El Salvador Aeropuerto internacional'),
('Canada,Toronto:Aeropuerto Toronto City Centre'),
('Canada,Montreal:Aeropuerto Internacional Pierre Elliott Trudeau'),
('Argentina, Buenos aires:Aeropuerto Internacional Ezeiza'),
('Argentina,Mendoza:Aeropuerto Internacional El Plumerillo'),
('Espana,Madrid:Aeropuerto de Madrid-Barajas Adolfo Suárez'),
('Espana,Barcelona:Aeropuerto Josep Tarradellas Barcelona-El Prat')




----procedimiento almacenado para insertar un vuelo---
create procedure Aerlines.InsertarVuelo
(

 @fechaSalida DATE,
 @horaSalida TIME ,
 @fechaLlegada DATE ,
 @horaLlegada TIME ,
 @id int,
 @idAeropuertoHN int
)
as
begin
insert into Aerlines.Vuelo ( fechaSalida, horaSalida, fechaLlegada, horaLlegada, id, idAeropuertoHN)
values( @fechaSalida, @horaSalida, @fechaLlegada, @horaLlegada, @id, @idAeropuertoHN) 
end

exec Aerlines.InsertarVuelo '2021-03-04', '11:00:00', '2021-04-01', '17:00:00',12,1
exec Aerlines.InsertarVuelo '2021-01-05', '10:00:00', '2021-03-01', '17:00:00',4,1
exec Aerlines.InsertarVuelo '2021-01-06', '9:00:00', '2021-01-02', '17:00:00', 5,1

select * from Aerlines.Vuelo



create procedure MostrarVuelos
as
select AV.idVuelo,AV.fechaLlegada,AV.fechaSalida,AV.horaLlegada,AV.horaSalida, AA.nombre as 'Origen', AP.Nombre as 'Destino' from Aerlines.Vuelo AV
INNER JOIN Aerlines.Pais AP
ON AV.id = AP.id
INNER JOIN Aerlines.AeropuertoHN AA
ON AV.idAeropuertoHN = AA.idAeropuertoHN
go







--insert de boleto--

insert into Aerlines.Boleto values ( '2020-08-01', 1, 1, 1,1)

--Insert Pasajero
insert into Aerlines.Pasajero values ('Josefina','Hernandez','F','30',99999898,1,1,1)

--insert Detalle Precio
insert into Aerlines.DetallePrecio values (1,1800)

select * from Aerlines.Boleto
