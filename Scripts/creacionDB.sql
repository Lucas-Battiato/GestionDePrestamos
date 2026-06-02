USE master
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'SistemaPrestamosPersonales')
    DROP DATABASE SistemaPrestamosPersonales
GO

CREATE DATABASE SistemaPrestamosPersonales
GO

USE SistemaPrestamosPersonales
GO

CREATE TABLE [Rol] (
	[idRol] INTEGER NOT NULL IDENTITY UNIQUE,
	[descripcion] VARCHAR(20) NOT NULL UNIQUE,
	PRIMARY KEY([idRol])
);
GO

CREATE TABLE [Prestamo] (
	[idPrestamo] INTEGER NOT NULL IDENTITY UNIQUE,
	[idProducto] INTEGER NOT NULL,
	[idCliente] INTEGER NOT NULL,
	[idUsuarioAprobador] INTEGER,
	[monto] DECIMAL(12,2) NOT NULL,
	[interesTotal] DECIMAL(12,2) NOT NULL,
	[cantidadCuotas] SMALLINT NOT NULL,
	[cuotasRestantes] SMALLINT NOT NULL,
	[fechaAprobacion] DATE,
	[fechaUltimaActualizacion] DATE NOT NULL,
	[idEstadoPrestamo] INTEGER NOT NULL,
	PRIMARY KEY([idPrestamo])
);
GO

CREATE TABLE [EstadoPrestamo] (
	[idEstadoPrestamo] INTEGER NOT NULL IDENTITY UNIQUE,
	[descripcion] VARCHAR(20) NOT NULL,
	PRIMARY KEY([idEstadoPrestamo])
);
GO

CREATE TABLE [Cuota] (
	[idCuota] INTEGER NOT NULL IDENTITY UNIQUE,
	[idPrestamo] INTEGER NOT NULL,
	[idEstadoCuota] INTEGER NOT NULL,
	[fechaVencimiento] DATE NOT NULL,
	[fechaPago] DATE,
	[monto] DECIMAL(12,2) NOT NULL,
	[idMetodoPago] INTEGER,
	PRIMARY KEY([idCuota])
);
GO

CREATE TABLE [EstadoCuota] (
	[idEstadoCuota] INTEGER NOT NULL IDENTITY UNIQUE,
	[descripcion] VARCHAR(20) NOT NULL,
	PRIMARY KEY([idEstadoCuota])
);
GO

CREATE TABLE [Cliente] (
	[idCliente] INTEGER NOT NULL IDENTITY UNIQUE,
	[username] VARCHAR(25) NOT NULL,
	[password] VARCHAR(255) NOT NULL,
	[email] VARCHAR(50) NOT NULL,
	[telefono] VARCHAR(20) NOT NULL,
	[direccion] VARCHAR(255) NOT NULL,
	PRIMARY KEY([idCliente])
);
GO

CREATE TABLE [Usuario] (
	[idUsuario] INTEGER NOT NULL IDENTITY UNIQUE,
	[idRol] INTEGER NOT NULL,
	[username] VARCHAR(25) NOT NULL UNIQUE,
	[password] VARCHAR(255) NOT NULL,
	[activo] BIT NOT NULL,
	PRIMARY KEY([idUsuario])
);
GO

CREATE TABLE [ProductoPrestamo] (
	[idProducto] INTEGER NOT NULL IDENTITY UNIQUE,
	[nombre] VARCHAR(50) NOT NULL,
	[descripcion] VARCHAR(255) NOT NULL,
	[montoMinimo] DECIMAL(12,2) NOT NULL,
	[montoMaximo] DECIMAL(12,2) NOT NULL,
	[cuotasMinimas] INTEGER NOT NULL,
	[cuotasMaximas] INTEGER NOT NULL,
	PRIMARY KEY([idProducto])
);
GO

CREATE TABLE [HistorialEstadoPrestamo] (
	[idHistorial] INTEGER NOT NULL IDENTITY,
	[idPrestamo] INTEGER NOT NULL,
	[idEstadoPrestamo] INTEGER NOT NULL,
	[fechaCambio] DATE NOT NULL,
	[idUsuario] INTEGER,
	[observaciones] VARCHAR(255),
	PRIMARY KEY([idHistorial])
);
GO

CREATE TABLE [TasaInteres] (
	[idTasaInteres] INTEGER NOT NULL IDENTITY UNIQUE,
	[idProducto] INTEGER NOT NULL,
	[cuotasDesde] INTEGER NOT NULL,
	[cuotasHasta] INTEGER NOT NULL,
	[tasaMensual] DECIMAL(5,4) NOT NULL,
	PRIMARY KEY([idTasaInteres])
);
GO

CREATE TABLE [MetodoPago] (
	[idMetodoPago] INTEGER NOT NULL IDENTITY UNIQUE,
	[descripcion] VARCHAR(25) NOT NULL,
	PRIMARY KEY([idMetodoPago])
);
GO


ALTER TABLE [HistorialEstadoPrestamo]
ADD FOREIGN KEY([idUsuario])
REFERENCES [Usuario]([idUsuario])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [HistorialEstadoPrestamo]
ADD FOREIGN KEY([idEstadoPrestamo])
REFERENCES [EstadoPrestamo]([idEstadoPrestamo])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [HistorialEstadoPrestamo]
ADD FOREIGN KEY([idPrestamo])
REFERENCES [Prestamo]([idPrestamo])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Usuario]
ADD FOREIGN KEY([idRol])
REFERENCES [Rol]([idRol])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Cuota]
ADD FOREIGN KEY([idEstadoCuota])
REFERENCES [EstadoCuota]([idEstadoCuota])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Prestamo]
ADD FOREIGN KEY([idEstadoPrestamo])
REFERENCES [EstadoPrestamo]([idEstadoPrestamo])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Prestamo]
ADD FOREIGN KEY([idUsuarioAprobador])
REFERENCES [Usuario]([idUsuario])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Prestamo]
ADD FOREIGN KEY([idCliente])
REFERENCES [Cliente]([idCliente])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Cuota]
ADD FOREIGN KEY([idPrestamo])
REFERENCES [Prestamo]([idPrestamo])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Prestamo]
ADD FOREIGN KEY([idProducto])
REFERENCES [ProductoPrestamo]([idProducto])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [TasaInteres]
ADD FOREIGN KEY([idProducto])
REFERENCES [ProductoPrestamo]([idProducto])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [Cuota]
ADD FOREIGN KEY([idMetodoPago])
REFERENCES [MetodoPago]([idMetodoPago])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO