CREATE TABLE [Rol] (
	[idRol] INTEGER NOT NULL IDENTITY UNIQUE,
	[Descripcion] TEXT NOT NULL UNIQUE,
	PRIMARY KEY([idRol])
);
GO

CREATE TABLE [Prestamo] (
	[idPrestamo] INTEGER NOT NULL IDENTITY UNIQUE,
	[idProducto] INTEGER NOT NULL,
	[idCliente] INTEGER NOT NULL,
	[idUsuarioAprobador] INTEGER,
	[monto] DOUBLE NOT NULL,
	[interesTotal] DOUBLE NOT NULL,
	[cantidadCuotas] INTEGER NOT NULL,
	[cuotasRestantes] INTEGER NOT NULL,
	[fechaAprobacion] DATETIME,
	[fechaUltimaActualizacion] DATETIME NOT NULL,
	[idEstadoPrestamo] INTEGER NOT NULL,
	PRIMARY KEY([idPrestamo])
);
GO

CREATE TABLE [EstadoPrestamo] (
	[idEstadoPrestamo] INTEGER NOT NULL IDENTITY UNIQUE,
	[Descripcion] TEXT NOT NULL,
	PRIMARY KEY([idEstadoPrestamo])
);
GO

CREATE TABLE [Cuota] (
	[idCuota] INTEGER NOT NULL IDENTITY UNIQUE,
	[idPrestamo] INTEGER NOT NULL,
	[idEstadoCuota] INTEGER NOT NULL,
	[fechaVencimiento] DATETIME NOT NULL,
	[fechaPago] DATETIME,
	[monto] DOUBLE NOT NULL,
	[idMetodoPago] INTEGER,
	PRIMARY KEY([idCuota])
);
GO

CREATE TABLE [EstadoCuota] (
	[idEstadoCuota] INTEGER NOT NULL IDENTITY UNIQUE,
	[Descripcion] TEXT NOT NULL,
	PRIMARY KEY([idEstadoCuota])
);
GO

CREATE TABLE [Cliente] (
	[idCliente] INTEGER NOT NULL IDENTITY UNIQUE,
	[username] TEXT NOT NULL,
	[password] TEXT NOT NULL,
	[email] TEXT NOT NULL,
	[telefono] TEXT NOT NULL,
	[direccion] TEXT NOT NULL,
	PRIMARY KEY([idCliente])
);
GO

CREATE TABLE [Usuario] (
	[idUsuario] INTEGER NOT NULL IDENTITY UNIQUE,
	[idRol] INTEGER NOT NULL,
	[username] TEXT NOT NULL UNIQUE,
	[password] TEXT NOT NULL,
	[activo] BIT NOT NULL,
	PRIMARY KEY([idUsuario])
);
GO

CREATE TABLE [ProductoPrestamo] (
	[idProducto] INTEGER NOT NULL IDENTITY UNIQUE,
	[nombre] TEXT NOT NULL,
	[descripcion] TEXT NOT NULL,
	[montoMinimo] DOUBLE NOT NULL,
	[montoMaximo] DOUBLE NOT NULL,
	[cuotasMinimas] INTEGER NOT NULL,
	[cuotasMaximas] INTEGER NOT NULL,
	PRIMARY KEY([idProducto])
);
GO

CREATE TABLE [HistorialEstadoPrestamo] (
	[idHistorial] INTEGER NOT NULL IDENTITY,
	[idPrestamo] INTEGER NOT NULL,
	[idEstadoPrestamo] INTEGER NOT NULL,
	[fechaCambio] DATETIME NOT NULL,
	[idUsuario] INTEGER,
	[Observaciones] TEXT,
	PRIMARY KEY([idHistorial])
);
GO

CREATE TABLE [TasaInteres] (
	[idTasaInteres] INTEGER NOT NULL IDENTITY UNIQUE,
	[idProducto] INTEGER NOT NULL,
	[cuotasDesde] INTEGER NOT NULL,
	[cuotasHasta] INTEGER NOT NULL,
	[tasaMensual] DOUBLE NOT NULL,
	PRIMARY KEY([idTasaInteres])
);
GO

CREATE TABLE [MetodoPago] (
	[idMetodoPago] INTEGER NOT NULL IDENTITY UNIQUE,
	[Descripcion] TEXT NOT NULL,
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