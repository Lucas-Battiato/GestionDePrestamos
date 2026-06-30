USE GestionDePrestamos
GO

-- =================================== --
-- CLIENTES DUMMY                      --
-- =================================== --
INSERT INTO Cliente (username, password, email, telefono, direccion) VALUES
('mfernandez', 'test123', 'mfernandez@gmail.com', '11-2233-4455', 'Av. Rivadavia 1234, CABA'),
('jperez', 'test123', 'jperez@hotmail.com', '11-5544-3322', 'San Martin 567, Villa Ballester'),
('lgomez', 'test123', 'lgomez@gmail.com', '11-6677-8899', 'Belgrano 890, San Andres'),
('rsanchez', 'test123', 'rsanchez@outlook.com', '11-9988-7766', 'Mitre 234, Boulogne'),
('cvazquez', 'test123', 'cvazquez@gmail.com', '11-1122-3344', 'Independencia 456, San Martin'),
('atorres', 'test123', 'atorres@gmail.com', '11-3344-5566', 'Las Heras 789, Munro'),
('jrodriguez', 'test123', 'jrodriguez@gmail.com', '11-7788-9900', 'Maipu 321, Olivos');
GO

-- =================================== --
-- USUARIOS DUMMY                      --
-- 1 = Administrador, 2 = Operador     --
-- =================================== --
INSERT INTO Usuario (username, password, idRol, activo) VALUES
('mlopez', 'test123', 2, 1),
('dmartinez', 'test123', 2, 1),
('snavarro', 'test123', 2, 0);
GO

-- =================================== --
-- PRESTAMOS DUMMY                     --
-- idEstadoPrestamo:                   --
-- 1 Solicitado, 2 Aprobado,           --
-- 3 Rechazado, 4 En Curso,            --
-- 5 Finalizado, 6 Cancelado           --
-- =================================== --

-- mfernandez (idCliente 2): prestamo personal, en curso
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 2, 1, 300000.00, 19500.00, 6, 4, '2026-04-10', GETDATE(), 4);

-- jperez (idCliente 3): prestamo personal, finalizado (sin deuda activa)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 3, 1, 150000.00, 8250.00, 3, 0, '2026-01-05', GETDATE(), 5);

-- lgomez (idCliente 4): prestamo prendario, en curso
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 4, 2, 2000000.00, 80000.00, 24, 18, '2026-02-15', GETDATE(), 4);

-- rsanchez (idCliente 5): prestamo personal, cancelado (mala historia crediticia)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 5, 1, 100000.00, 5750.00, 3, 0, '2025-11-01', GETDATE(), 6);

-- cvazquez (idCliente 6): prestamo personal, finalizado (buena historia)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 6, 2, 200000.00, 13000.00, 6, 0, '2025-09-10', GETDATE(), 5);

-- cvazquez (idCliente 6): segundo prestamo, en curso (ya con historial finalizado)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 6, 1, 800000.00, 28000.00, 12, 8, '2026-03-20', GETDATE(), 4);

-- atorres (idCliente 7): prestamo hipotecario, solicitado (pendiente de evaluar)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (3, 7, NULL, 8000000.00, 280000.00, 60, 60, NULL, GETDATE(), 1);

-- jrodriguez (idCliente 8): prestamo personal, solicitado (pendiente de evaluar)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 8, NULL, 250000.00, 14375.00, 6, 6, NULL, GETDATE(), 1);

-- jrodriguez (idCliente 8): prestamo rechazado anterior
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 8, 2, 4500000.00, 258750.00, 36, 0, NULL, GETDATE(), 3);
GO

-- =================================== --
-- CUOTAS DUMMY                        --
-- Solo para prestamos En Curso y      --
-- Finalizado (los que generan cuotas) --
-- idEstadoCuota: 1 Pendiente,         --
-- 2 Pagada, 3 Vencida                 --
-- =================================== --

-- Cuotas del prestamo de mfernandez (idPrestamo se infiere por orden de insercion, asumiendo 1)
-- 6 cuotas, 2 ya pagadas, 4 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(1, 2, '2026-05-10', '2026-05-09', 53250.00, 2),
(1, 2, '2026-06-10', '2026-06-10', 53250.00, 2),
(1, 1, '2026-07-10', NULL, 53250.00, NULL),
(1, 1, '2026-08-10', NULL, 53250.00, NULL),
(1, 1, '2026-09-10', NULL, 53250.00, NULL),
(1, 1, '2026-10-10', NULL, 53250.00, NULL);

-- Cuotas del prestamo finalizado de jperez (idPrestamo 2), las 3 pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(2, 2, '2026-02-05', '2026-02-04', 52750.00, 1),
(2, 2, '2026-03-05', '2026-03-05', 52750.00, 3),
(2, 2, '2026-04-05', '2026-04-03', 52750.00, 1);

-- Cuotas del prestamo de lgomez (idPrestamo 3), 24 cuotas, 6 pagadas, 18 pendientes (muestro solo algunas representativas)
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(3, 2, '2026-03-15', '2026-03-14', 86666.66, 2),
(3, 2, '2026-04-15', '2026-04-15', 86666.66, 2),
(3, 2, '2026-05-15', '2026-05-15', 86666.66, 2),
(3, 1, '2026-06-15', NULL, 86666.66, NULL),
(3, 1, '2026-07-15', NULL, 86666.66, NULL),
(3, 1, '2026-08-15', NULL, 86666.66, NULL);

-- Cuotas del prestamo finalizado de cvazquez (idPrestamo 5), las 6 pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(5, 2, '2025-10-10', '2025-10-08', 35500.00, 1),
(5, 2, '2025-11-10', '2025-11-10', 35500.00, 4),
(5, 2, '2025-12-10', '2025-12-09', 35500.00, 1),
(5, 2, '2026-01-10', '2026-01-10', 35500.00, 2),
(5, 2, '2026-02-10', '2026-02-08', 35500.00, 1),
(5, 2, '2026-03-10', '2026-03-10', 35500.00, 4);

-- Cuotas del segundo prestamo de cvazquez (idPrestamo 6), en curso, 12 cuotas, 4 pagadas, 1 vencida sin pagar, resto pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(6, 2, '2026-04-20', '2026-04-19', 69000.00, 2),
(6, 2, '2026-05-20', '2026-05-20', 69000.00, 2),
(6, 3, '2026-06-20', NULL, 69000.00, NULL),
(6, 1, '2026-07-20', NULL, 69000.00, NULL),
(6, 1, '2026-08-20', NULL, 69000.00, NULL),
(6, 1, '2026-09-20', NULL, 69000.00, NULL),
(6, 1, '2026-10-20', NULL, 69000.00, NULL),
(6, 1, '2026-11-20', NULL, 69000.00, NULL),
(6, 1, '2026-12-20', NULL, 69000.00, NULL),
(6, 1, '2027-01-20', NULL, 69000.00, NULL),
(6, 1, '2027-02-20', NULL, 69000.00, NULL),
(6, 1, '2027-03-20', NULL, 69000.00, NULL);
GO