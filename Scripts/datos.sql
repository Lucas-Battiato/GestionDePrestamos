USE GestionDePrestamos
GO


-- =================== --
-- ESTADOS DE PRESTAMO --
-- =================== --
INSERT INTO [EstadoPrestamo] ([descripcion]) VALUES ('Solicitado');
INSERT INTO [EstadoPrestamo] ([descripcion]) VALUES ('Aprobado');
INSERT INTO [EstadoPrestamo] ([descripcion]) VALUES ('Rechazado');
INSERT INTO [EstadoPrestamo] ([descripcion]) VALUES ('En Curso');
INSERT INTO [EstadoPrestamo] ([descripcion]) VALUES ('Finalizado');
INSERT INTO [EstadoPrestamo] ([descripcion]) VALUES ('Cancelado');
GO


-- ================ --
-- ESTADOS DE CUOTA --
-- ================ --
INSERT INTO [EstadoCuota] ([descripcion]) VALUES ('Pendiente');
INSERT INTO [EstadoCuota] ([descripcion]) VALUES ('Pagada');
INSERT INTO [EstadoCuota] ([descripcion]) VALUES ('Vencida');
INSERT INTO [EstadoCuota] ([descripcion]) VALUES ('Cancelada');
GO


-- ===== --
-- ROLES --
-- ===== --
INSERT INTO [Rol] ([descripcion]) VALUES ('Administrador');
INSERT INTO [Rol] ([descripcion]) VALUES ('Operador');
GO


-- =============== --
-- METODOS DE PAGO --
-- =============== --
INSERT INTO [MetodoPago] ([descripcion]) VALUES ('Efectivo');
INSERT INTO [MetodoPago] ([descripcion]) VALUES ('Transferencia');
INSERT INTO [MetodoPago] ([descripcion]) VALUES ('Tarjeta de Credito');
INSERT INTO [MetodoPago] ([descripcion]) VALUES ('Tarjeta de Debito');
INSERT INTO [MetodoPago] ([descripcion]) VALUES ('Criptomoneda');
GO


-- ===================== --
-- PRODUCTOS DE PRESTAMO --
-- ===================== --
INSERT INTO [ProductoPrestamo] ([nombre], [descripcion], [montoMinimo], [montoMaximo], [cuotasMinimas], [cuotasMaximas])
VALUES (
    'Prestamo Personal',
    'Credito de libre destino a sola firma, sin garantia real requerida.',
    50000.00,
    5000000.00,
    3,
    36
);

INSERT INTO [ProductoPrestamo] ([nombre], [descripcion], [montoMinimo], [montoMaximo], [cuotasMinimas], [cuotasMaximas])
VALUES (
    'Prestamo Prendario',
    'Credito para financiacion de vehiculos. El bien adquirido queda como garantia hasta la cancelacion total.',
    500000.00,
    20000000.00,
    12,
    60
);

INSERT INTO [ProductoPrestamo] ([nombre], [descripcion], [montoMinimo], [montoMaximo], [cuotasMinimas], [cuotasMaximas])
VALUES (
    'Prestamo Hipotecario',
    'Credito para adquisicion de inmuebles. La propiedad queda como garantia hasta la cancelacion total.',
    5000000.00,
    150000000.00,
    24,
    240
);
GO


-- =================================== --
-- TASAS DE INTERES                    --
-- idProducto 1 = Prestamo Personal    --
-- idProducto 2 = Prestamo Prendario   --
-- idProducto 3 = Prestamo Hipotecario --
-- =================================== --

-- Prestamo Personal --
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (1, 3,  6,  0.0450);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (1, 7,  12, 0.0500);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (1, 13, 24, 0.0575);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (1, 25, 36, 0.0650);

-- Prestamo Prendario --
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (2, 12, 24, 0.0350);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (2, 25, 36, 0.0400);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (2, 37, 48, 0.0475);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (2, 49, 60, 0.0525);

-- Prestamo Hipotecario --
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (3, 24,  60,  0.0250);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (3, 61,  120, 0.0300);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (3, 121, 180, 0.0350);
INSERT INTO [TasaInteres] ([idProducto], [cuotasDesde], [cuotasHasta], [tasaMensual]) VALUES (3, 181, 240, 0.0400);
GO


-- ===================== --
-- CLIENTE TEST --
-- ===================== --
INSERT INTO Cliente VALUES('testCliente', 'test123', 'testcliente.utn@gmail.com', '1111-1111', 'Calle Falsa 123')


-- ===================== --
-- USUARIOS TEST --
-- ===================== --
INSERT INTO Usuario VALUES (1, 'testAdmin', 'test123', 1);
INSERT INTO Usuario VALUES (2, 'testOperador', 'test123', 1);