USE GestionDePrestamos
GO

-- ============================================================ --
-- CLIENTES DUMMY (15 clientes, idCliente 2-16)                --
-- ============================================================ --
INSERT INTO Cliente (username, password, email, telefono, direccion) VALUES
('mfernandez',  'test123', 'mfernandez@gmail.com',   '11-2233-4455', 'Av. Rivadavia 1234, CABA'),
('jperez',      'test123', 'jperez@hotmail.com',      '11-5544-3322', 'San Martin 567, Villa Ballester'),
('lgomez',      'test123', 'lgomez@gmail.com',        '11-6677-8899', 'Belgrano 890, San Andres'),
('rsanchez',    'test123', 'rsanchez@outlook.com',    '11-9988-7766', 'Mitre 234, Boulogne'),
('cvazquez',    'test123', 'cvazquez@gmail.com',      '11-1122-3344', 'Independencia 456, San Martin'),
('atorres',     'test123', 'atorres@gmail.com',       '11-3344-5566', 'Las Heras 789, Munro'),
('jrodriguez',  'test123', 'jrodriguez@gmail.com',    '11-7788-9900', 'Maipu 321, Olivos'),
('pmoreno',     'test123', 'pmoreno@gmail.com',       '11-4455-6677', 'Corrientes 2345, CABA'),
('ldiaz',       'test123', 'ldiaz@hotmail.com',       '11-8899-0011', 'Sarmiento 890, Lomas de Zamora'),
('sruiz',       'test123', 'sruiz@gmail.com',         '11-2244-6688', 'Lavalle 456, Quilmes'),
('dlopez',      'test123', 'dlopez@outlook.com',      '11-1357-2468', 'Tucuman 123, Mar del Plata'),
('mmartinez',   'test123', 'mmartinez@gmail.com',     '11-9753-1357', 'Florida 789, CABA'),
('cgarcia',     'test123', 'cgarcia@gmail.com',       '11-3691-2580', 'Pueyrredon 321, Rosario'),
('nherrera',    'test123', 'nherrera@hotmail.com',    '11-7412-8520', 'Dorrego 654, Cordoba'),
('ajimenez',    'test123', 'ajimenez@gmail.com',      '11-5678-9012', 'Mendoza 987, Tucuman');
GO

-- ============================================================ --
-- USUARIOS DUMMY (3 operadores, idUsuario 3-5)                --
-- 1 = Administrador, 2 = Operador                             --
-- ============================================================ --
INSERT INTO Usuario (idRol, username, password, activo) VALUES
(2, 'mlopez',    'test123', 1),
(2, 'dmartinez', 'test123', 1),
(2, 'snavarro',  'test123', 0);
GO

-- ============================================================ --
-- PRESTAMOS DUMMY                                              --
-- idEstadoPrestamo:                                           --
-- 1 Solicitado, 2 Aprobado, 3 Rechazado                      --
-- 4 En Curso, 5 Finalizado, 6 Cancelado                      --
-- idCliente: 2=mfernandez, 3=jperez, 4=lgomez               --
--            5=rsanchez, 6=cvazquez, 7=atorres               --
--            8=jrodriguez, 9=pmoreno, 10=ldiaz               --
--            11=sruiz, 12=dlopez, 13=mmartinez               --
--            14=cgarcia, 15=nherrera, 16=ajimenez            --
-- ============================================================ --

-- PERSONALES FINALIZADOS (historial limpio)
-- mfernandez: personal finalizado 2024
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 2, 1, 500000.00, 82050.00, 6, 0, '2024-06-10', '2024-12-10', 5); -- idPrestamo 1

-- jperez: personal finalizado 2024
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 3, 1, 300000.00, 49230.00, 6, 0, '2024-08-05', '2025-02-05', 5); -- idPrestamo 2

-- cvazquez: personal finalizado 2025
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 6, 2, 200000.00, 32820.00, 6, 0, '2025-01-10', '2025-07-10', 5); -- idPrestamo 3

-- pmoreno: personal finalizado 2025
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 9, 3, 400000.00, 65640.00, 6, 0, '2025-03-15', '2025-09-15', 5); -- idPrestamo 4

-- mmartinez: personal finalizado 2025
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 13, 3, 350000.00, 57435.00, 6, 0, '2025-05-20', '2025-11-20', 5); -- idPrestamo 5

-- PERSONALES CANCELADOS (mala historia)
-- rsanchez: personal cancelado 2025
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 5, 1, 150000.00, 24615.00, 6, 3, '2025-02-01', '2025-08-01', 6); -- idPrestamo 6

-- sruiz: personal cancelado 2025
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 11, 3, 250000.00, 41025.00, 6, 2, '2025-04-10', '2025-10-10', 6); -- idPrestamo 7

-- PERSONALES EN CURSO
-- mfernandez: segundo personal en curso (tiene historial)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 2, 1, 800000.00, 131280.00, 12, 9, '2026-01-10', GETDATE(), 4); -- idPrestamo 8

-- jperez: segundo personal en curso
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 3, 1, 600000.00, 98460.00, 12, 8, '2026-02-05', GETDATE(), 4); -- idPrestamo 9

-- dlopez: personal en curso
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 12, 3, 1000000.00, 164100.00, 12, 10, '2025-12-15', GETDATE(), 4); -- idPrestamo 10

-- PRENDARIOS FINALIZADOS
-- lgomez: prendario finalizado 2025
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 4, 2, 3000000.00, 315360.00, 24, 0, '2024-07-15', '2026-07-15', 5); -- idPrestamo 11

-- cgarcia: prendario finalizado 2026
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 14, 3, 5000000.00, 525600.00, 24, 0, '2024-09-20', '2026-09-20', 5); -- idPrestamo 12

-- PRENDARIOS CANCELADOS
-- nherrera: prendario cancelado
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 15, 1, 2000000.00, 210240.00, 24, 10, '2025-06-01', '2026-01-01', 6); -- idPrestamo 13

-- PRENDARIOS EN CURSO
-- cvazquez: prendario en curso (tiene historial personal)
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 6, 2, 8000000.00, 756000.00, 36, 30, '2026-01-20', GETDATE(), 4); -- idPrestamo 14

-- ldiaz: prendario en curso
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 10, 3, 6000000.00, 567000.00, 36, 32, '2025-12-10', GETDATE(), 4); -- idPrestamo 15

-- HIPOTECARIOS EN CURSO
-- atorres: hipotecario en curso
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (3, 7, 1, 50000000.00, 19500000.00, 120, 115, '2025-11-01', GETDATE(), 4); -- idPrestamo 16

-- ajimenez: hipotecario en curso
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (3, 16, 2, 80000000.00, 43200000.00, 180, 176, '2025-10-15', GETDATE(), 4); -- idPrestamo 17

-- RECHAZADOS
-- jrodriguez: personal rechazado
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 8, 1, 4500000.00, 0.00, 36, 0, NULL, '2025-09-15', 3); -- idPrestamo 18

-- rsanchez: hipotecario rechazado
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (3, 5, 2, 30000000.00, 0.00, 60, 0, NULL, '2025-12-01', 3); -- idPrestamo 19

-- SOLICITADOS (pendientes de evaluacion esta semana)
-- jrodriguez: personal solicitado
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 8, NULL, 750000.00, 123075.00, 12, 12, NULL, GETDATE(), 1); -- idPrestamo 20

-- pmoreno: prendario solicitado
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (2, 9, NULL, 4000000.00, 420480.00, 24, 24, NULL, GETDATE(), 1); -- idPrestamo 21

-- nherrera: hipotecario solicitado
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (3, 15, NULL, 60000000.00, 23400000.00, 120, 120, NULL, GETDATE(), 1); -- idPrestamo 22
GO

-- ============================================================ --
-- CUOTAS DUMMY                                                 --
-- Solo para prestamos En Curso y Finalizados                   --
-- idEstadoCuota: 1=Pendiente, 2=Pagada, 3=Vencida, 4=Cancelada--
-- ============================================================ --

-- idPrestamo 1: mfernandez personal finalizado, 6 cuotas todas pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(1, 2, '2024-08-10', '2024-08-09', 97008.33, 2),
(1, 2, '2024-09-10', '2024-09-10', 97008.33, 2),
(1, 2, '2024-10-10', '2024-10-08', 97008.33, 1),
(1, 2, '2024-11-12', '2024-11-11', 97008.33, 4),
(1, 2, '2024-12-10', '2024-12-09', 97008.33, 2),
(1, 2, '2025-01-10', '2025-01-10', 97008.33, 1);

-- idPrestamo 2: jperez personal finalizado, 6 cuotas todas pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(2, 2, '2024-10-07', '2024-10-05', 58205.00, 1),
(2, 2, '2024-11-07', '2024-11-07', 58205.00, 3),
(2, 2, '2024-12-09', '2024-12-08', 58205.00, 2),
(2, 2, '2025-01-07', '2025-01-06', 58205.00, 1),
(2, 2, '2025-02-07', '2025-02-07', 58205.00, 4),
(2, 2, '2025-03-07', '2025-03-05', 58205.00, 2);

-- idPrestamo 3: cvazquez personal finalizado, 6 cuotas todas pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(3, 2, '2025-03-10', '2025-03-09', 38803.33, 1),
(3, 2, '2025-04-10', '2025-04-10', 38803.33, 2),
(3, 2, '2025-05-12', '2025-05-11', 38803.33, 1),
(3, 2, '2025-06-10', '2025-06-09', 38803.33, 4),
(3, 2, '2025-07-10', '2025-07-10', 38803.33, 2),
(3, 2, '2025-08-11', '2025-08-10', 38803.33, 1);

-- idPrestamo 4: pmoreno personal finalizado, 6 cuotas todas pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(4, 2, '2025-05-15', '2025-05-14', 77606.67, 3),
(4, 2, '2025-06-13', '2025-06-12', 77606.67, 1),
(4, 2, '2025-07-15', '2025-07-14', 77606.67, 2),
(4, 2, '2025-08-15', '2025-08-14', 77606.67, 3),
(4, 2, '2025-09-15', '2025-09-15', 77606.67, 1),
(4, 2, '2025-10-15', '2025-10-14', 77606.67, 4);

-- idPrestamo 5: mmartinez personal finalizado, 6 cuotas todas pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(5, 2, '2025-07-21', '2025-07-20', 67905.83, 2),
(5, 2, '2025-08-20', '2025-08-19', 67905.83, 1),
(5, 2, '2025-09-22', '2025-09-21', 67905.83, 2),
(5, 2, '2025-10-20', '2025-10-19', 67905.83, 4),
(5, 2, '2025-11-20', '2025-11-20', 67905.83, 1),
(5, 2, '2025-12-22', '2025-12-21', 67905.83, 2);

-- idPrestamo 6: rsanchez personal cancelado, 3 pagadas, 3 canceladas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(6, 2, '2025-04-01', '2025-03-31', 29102.50, 1),
(6, 2, '2025-05-01', '2025-04-30', 29102.50, 1),
(6, 2, '2025-06-02', '2025-06-01', 29102.50, 2),
(6, 4, '2025-07-01', NULL, 29102.50, NULL),
(6, 4, '2025-08-01', NULL, 29102.50, NULL),
(6, 4, '2025-09-01', NULL, 29102.50, NULL);

-- idPrestamo 7: sruiz personal cancelado, 4 pagadas, 2 canceladas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(7, 2, '2025-06-10', '2025-06-09', 48337.50, 3),
(7, 2, '2025-07-10', '2025-07-09', 48337.50, 2),
(7, 2, '2025-08-11', '2025-08-10', 48337.50, 1),
(7, 2, '2025-09-10', '2025-09-09', 48337.50, 3),
(7, 4, '2025-10-10', NULL, 48337.50, NULL),
(7, 4, '2025-11-10', NULL, 48337.50, NULL);

-- idPrestamo 8: mfernandez segundo personal en curso, 12 cuotas, 3 pagadas, 9 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(8, 2, '2026-03-10', '2026-03-09', 77615.00, 2),
(8, 2, '2026-04-10', '2026-04-09', 77615.00, 2),
(8, 2, '2026-05-12', '2026-05-11', 77615.00, 1),
(8, 1, '2026-06-10', NULL, 77615.00, NULL),
(8, 1, '2026-07-10', NULL, 77615.00, NULL),
(8, 1, '2026-08-10', NULL, 77615.00, NULL),
(8, 1, '2026-09-10', NULL, 77615.00, NULL),
(8, 1, '2026-10-12', NULL, 77615.00, NULL),
(8, 1, '2026-11-10', NULL, 77615.00, NULL),
(8, 1, '2026-12-10', NULL, 77615.00, NULL),
(8, 1, '2027-01-11', NULL, 77615.00, NULL),
(8, 1, '2027-02-10', NULL, 77615.00, NULL);

-- idPrestamo 9: jperez segundo personal en curso, 12 cuotas, 4 pagadas, 8 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(9, 2, '2026-04-07', '2026-04-06', 58221.25, 1),
(9, 2, '2026-05-07', '2026-05-06', 58221.25, 4),
(9, 2, '2026-06-09', '2026-06-08', 58221.25, 2),
(9, 3, '2026-07-07', NULL, 58221.25, NULL),
(9, 1, '2026-08-07', NULL, 58221.25, NULL),
(9, 1, '2026-09-07', NULL, 58221.25, NULL),
(9, 1, '2026-10-07', NULL, 58221.25, NULL),
(9, 1, '2026-11-09', NULL, 58221.25, NULL),
(9, 1, '2026-12-07', NULL, 58221.25, NULL),
(9, 1, '2027-01-07', NULL, 58221.25, NULL),
(9, 1, '2027-02-08', NULL, 58221.25, NULL),
(9, 1, '2027-03-08', NULL, 58221.25, NULL);

-- idPrestamo 10: dlopez personal en curso, 12 cuotas, 2 pagadas, 1 vencida, 9 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(10, 2, '2026-02-17', '2026-02-16', 97008.33, 3),
(10, 2, '2026-03-17', '2026-03-16', 97008.33, 1),
(10, 3, '2026-04-17', NULL, 97008.33, NULL),
(10, 3, '2026-05-19', NULL, 97008.33, NULL),
(10, 1, '2026-06-17', NULL, 97008.33, NULL),
(10, 1, '2026-07-17', NULL, 97008.33, NULL),
(10, 1, '2026-08-17', NULL, 97008.33, NULL),
(10, 1, '2026-09-17', NULL, 97008.33, NULL),
(10, 1, '2026-10-19', NULL, 97008.33, NULL),
(10, 1, '2026-11-17', NULL, 97008.33, NULL),
(10, 1, '2026-12-17', NULL, 97008.33, NULL),
(10, 1, '2027-01-18', NULL, 97008.33, NULL);

-- idPrestamo 11: lgomez prendario finalizado, 24 cuotas todas pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(11, 2, '2024-09-15', '2024-09-14', 63472.00, 2),
(11, 2, '2024-10-15', '2024-10-14', 63472.00, 2),
(11, 2, '2024-11-15', '2024-11-14', 63472.00, 1),
(11, 2, '2024-12-16', '2024-12-15', 63472.00, 3),
(11, 2, '2025-01-15', '2025-01-14', 63472.00, 2),
(11, 2, '2025-02-17', '2025-02-16', 63472.00, 4),
(11, 2, '2025-03-17', '2025-03-16', 63472.00, 1),
(11, 2, '2025-04-15', '2025-04-14', 63472.00, 2),
(11, 2, '2025-05-15', '2025-05-14', 63472.00, 3),
(11, 2, '2025-06-16', '2025-06-15', 63472.00, 1),
(11, 2, '2025-07-15', '2025-07-14', 63472.00, 2),
(11, 2, '2025-08-15', '2025-08-14', 63472.00, 4),
(11, 2, '2025-09-15', '2025-09-14', 63472.00, 2),
(11, 2, '2025-10-15', '2025-10-14', 63472.00, 1),
(11, 2, '2025-11-17', '2025-11-16', 63472.00, 3),
(11, 2, '2025-12-15', '2025-12-14', 63472.00, 2),
(11, 2, '2026-01-15', '2026-01-14', 63472.00, 1),
(11, 2, '2026-02-16', '2026-02-15', 63472.00, 4),
(11, 2, '2026-03-16', '2026-03-15', 63472.00, 2),
(11, 2, '2026-04-15', '2026-04-14', 63472.00, 1),
(11, 2, '2026-05-15', '2026-05-14', 63472.00, 3),
(11, 2, '2026-06-15', '2026-06-14', 63472.00, 2),
(11, 2, '2026-07-15', '2026-07-14', 63472.00, 1),
(11, 2, '2026-08-15', '2026-08-14', 63472.00, 2);

-- idPrestamo 12: cgarcia prendario finalizado, 24 cuotas todas pagadas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(12, 2, '2024-11-20', '2024-11-19', 105900.00, 1),
(12, 2, '2024-12-20', '2024-12-19', 105900.00, 2),
(12, 2, '2025-01-20', '2025-01-19', 105900.00, 1),
(12, 2, '2025-02-20', '2025-02-19', 105900.00, 3),
(12, 2, '2025-03-20', '2025-03-19', 105900.00, 2),
(12, 2, '2025-04-22', '2025-04-21', 105900.00, 4),
(12, 2, '2025-05-20', '2025-05-19', 105900.00, 1),
(12, 2, '2025-06-20', '2025-06-19', 105900.00, 2),
(12, 2, '2025-07-21', '2025-07-20', 105900.00, 3),
(12, 2, '2025-08-20', '2025-08-19', 105900.00, 1),
(12, 2, '2025-09-22', '2025-09-21', 105900.00, 2),
(12, 2, '2025-10-20', '2025-10-19', 105900.00, 4),
(12, 2, '2025-11-20', '2025-11-19', 105900.00, 1),
(12, 2, '2025-12-22', '2025-12-21', 105900.00, 2),
(12, 2, '2026-01-20', '2026-01-19', 105900.00, 3),
(12, 2, '2026-02-20', '2026-02-19', 105900.00, 1),
(12, 2, '2026-03-20', '2026-03-19', 105900.00, 2),
(12, 2, '2026-04-20', '2026-04-19', 105900.00, 4),
(12, 2, '2026-05-20', '2026-05-19', 105900.00, 1),
(12, 2, '2026-06-22', '2026-06-21', 105900.00, 2),
(12, 2, '2026-07-20', '2026-07-19', 105900.00, 3),
(12, 2, '2026-08-20', '2026-08-19', 105900.00, 1),
(12, 2, '2026-09-22', '2026-09-21', 105900.00, 2),
(12, 2, '2026-10-20', '2026-10-19', 105900.00, 4);

-- idPrestamo 13: nherrera prendario cancelado, 14 pagadas, 10 canceladas
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(13, 2, '2025-08-01', '2025-07-31', 42760.00, 1),
(13, 2, '2025-09-01', '2025-08-31', 42760.00, 2),
(13, 2, '2025-10-01', '2025-09-30', 42760.00, 1),
(13, 2, '2025-11-03', '2025-11-02', 42760.00, 3),
(13, 2, '2025-12-01', '2025-11-30', 42760.00, 2),
(13, 2, '2026-01-01', '2025-12-31', 42760.00, 1),
(13, 2, '2026-02-02', '2026-02-01', 42760.00, 4),
(13, 2, '2026-03-02', '2026-03-01', 42760.00, 2),
(13, 2, '2026-04-01', '2026-03-31', 42760.00, 1),
(13, 2, '2026-05-01', '2026-04-30', 42760.00, 3),
(13, 2, '2026-06-01', '2026-05-31', 42760.00, 2),
(13, 2, '2026-07-01', '2026-06-30', 42760.00, 1),
(13, 2, '2026-08-03', '2026-08-02', 42760.00, 4),
(13, 2, '2026-09-01', '2026-08-31', 42760.00, 2),
(13, 4, '2026-10-01', NULL, 42760.00, NULL),
(13, 4, '2026-11-02', NULL, 42760.00, NULL),
(13, 4, '2026-12-01', NULL, 42760.00, NULL),
(13, 4, '2027-01-01', NULL, 42760.00, NULL),
(13, 4, '2027-02-01', NULL, 42760.00, NULL),
(13, 4, '2027-03-03', NULL, 42760.00, NULL),
(13, 4, '2027-04-01', NULL, 42760.00, NULL),
(13, 4, '2027-05-01', NULL, 42760.00, NULL),
(13, 4, '2027-06-01', NULL, 42760.00, NULL),
(13, 4, '2027-07-01', NULL, 42760.00, NULL);

-- idPrestamo 14: cvazquez prendario en curso, 36 cuotas, 6 pagadas, 30 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(14, 2, '2026-03-20', '2026-03-19', 235416.67, 2),
(14, 2, '2026-04-22', '2026-04-21', 235416.67, 1),
(14, 2, '2026-05-20', '2026-05-19', 235416.67, 4),
(14, 2, '2026-06-20', '2026-06-19', 235416.67, 2),
(14, 2, '2026-07-21', '2026-07-20', 235416.67, 1),
(14, 2, '2026-08-20', '2026-08-19', 235416.67, 3),
(14, 1, '2026-09-22', NULL, 235416.67, NULL),
(14, 1, '2026-10-20', NULL, 235416.67, NULL),
(14, 1, '2026-11-20', NULL, 235416.67, NULL),
(14, 1, '2026-12-22', NULL, 235416.67, NULL),
(14, 1, '2027-01-20', NULL, 235416.67, NULL),
(14, 1, '2027-02-22', NULL, 235416.67, NULL),
(14, 1, '2027-03-22', NULL, 235416.67, NULL),
(14, 1, '2027-04-20', NULL, 235416.67, NULL),
(14, 1, '2027-05-20', NULL, 235416.67, NULL),
(14, 1, '2027-06-22', NULL, 235416.67, NULL),
(14, 1, '2027-07-20', NULL, 235416.67, NULL),
(14, 1, '2027-08-20', NULL, 235416.67, NULL),
(14, 1, '2027-09-20', NULL, 235416.67, NULL),
(14, 1, '2027-10-20', NULL, 235416.67, NULL),
(14, 1, '2027-11-20', NULL, 235416.67, NULL),
(14, 1, '2027-12-22', NULL, 235416.67, NULL),
(14, 1, '2028-01-20', NULL, 235416.67, NULL),
(14, 1, '2028-02-21', NULL, 235416.67, NULL),
(14, 1, '2028-03-20', NULL, 235416.67, NULL),
(14, 1, '2028-04-20', NULL, 235416.67, NULL),
(14, 1, '2028-05-22', NULL, 235416.67, NULL),
(14, 1, '2028-06-20', NULL, 235416.67, NULL),
(14, 1, '2028-07-20', NULL, 235416.67, NULL),
(14, 1, '2028-08-21', NULL, 235416.67, NULL),
(14, 1, '2028-09-20', NULL, 235416.67, NULL),
(14, 1, '2028-10-20', NULL, 235416.67, NULL),
(14, 1, '2028-11-20', NULL, 235416.67, NULL),
(14, 1, '2028-12-20', NULL, 235416.67, NULL),
(14, 1, '2029-01-22', NULL, 235416.67, NULL),
(14, 1, '2029-02-20', NULL, 235416.67, NULL);

-- idPrestamo 15: ldiaz prendario en curso, 36 cuotas, 4 pagadas, 32 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(15, 2, '2026-02-10', '2026-02-09', 176250.00, 3),
(15, 2, '2026-03-10', '2026-03-09', 176250.00, 1),
(15, 2, '2026-04-13', '2026-04-12', 176250.00, 2),
(15, 3, '2026-05-12', NULL, 176250.00, NULL),
(15, 1, '2026-06-10', NULL, 176250.00, NULL),
(15, 1, '2026-07-10', NULL, 176250.00, NULL),
(15, 1, '2026-08-10', NULL, 176250.00, NULL),
(15, 1, '2026-09-10', NULL, 176250.00, NULL),
(15, 1, '2026-10-12', NULL, 176250.00, NULL),
(15, 1, '2026-11-10', NULL, 176250.00, NULL),
(15, 1, '2026-12-10', NULL, 176250.00, NULL),
(15, 1, '2027-01-11', NULL, 176250.00, NULL),
(15, 1, '2027-02-10', NULL, 176250.00, NULL),
(15, 1, '2027-03-10', NULL, 176250.00, NULL),
(15, 1, '2027-04-12', NULL, 176250.00, NULL),
(15, 1, '2027-05-10', NULL, 176250.00, NULL),
(15, 1, '2027-06-10', NULL, 176250.00, NULL),
(15, 1, '2027-07-12', NULL, 176250.00, NULL),
(15, 1, '2027-08-10', NULL, 176250.00, NULL),
(15, 1, '2027-09-10', NULL, 176250.00, NULL),
(15, 1, '2027-10-11', NULL, 176250.00, NULL),
(15, 1, '2027-11-10', NULL, 176250.00, NULL),
(15, 1, '2027-12-10', NULL, 176250.00, NULL),
(15, 1, '2028-01-10', NULL, 176250.00, NULL),
(15, 1, '2028-02-10', NULL, 176250.00, NULL),
(15, 1, '2028-03-13', NULL, 176250.00, NULL),
(15, 1, '2028-04-10', NULL, 176250.00, NULL),
(15, 1, '2028-05-10', NULL, 176250.00, NULL),
(15, 1, '2028-06-12', NULL, 176250.00, NULL),
(15, 1, '2028-07-10', NULL, 176250.00, NULL),
(15, 1, '2028-08-10', NULL, 176250.00, NULL),
(15, 1, '2028-09-11', NULL, 176250.00, NULL),
(15, 1, '2028-10-10', NULL, 176250.00, NULL),
(15, 1, '2028-11-10', NULL, 176250.00, NULL),
(15, 1, '2028-12-11', NULL, 176250.00, NULL),
(15, 1, '2029-01-10', NULL, 176250.00, NULL);

-- idPrestamo 16: atorres hipotecario en curso, 120 cuotas, 5 pagadas, 115 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(16, 2, '2026-01-01', '2025-12-31', 496250.00, 2),
(16, 2, '2026-02-02', '2026-02-01', 496250.00, 1),
(16, 2, '2026-03-02', '2026-03-01', 496250.00, 4),
(16, 2, '2026-04-01', '2026-03-31', 496250.00, 2),
(16, 2, '2026-05-01', '2026-04-30', 496250.00, 1),
(16, 1, '2026-06-01', NULL, 496250.00, NULL),
(16, 1, '2026-07-01', NULL, 496250.00, NULL),
(16, 1, '2026-08-03', NULL, 496250.00, NULL),
(16, 1, '2026-09-01', NULL, 496250.00, NULL),
(16, 1, '2026-10-01', NULL, 496250.00, NULL);

-- idPrestamo 17: ajimenez hipotecario en curso, 180 cuotas, 4 pagadas, 176 pendientes
INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(17, 2, '2025-12-15', '2025-12-14', 683333.33, 3),
(17, 2, '2026-01-15', '2026-01-14', 683333.33, 1),
(17, 2, '2026-02-16', '2026-02-15', 683333.33, 2),
(17, 2, '2026-03-16', '2026-03-15', 683333.33, 4),
(17, 1, '2026-04-15', NULL, 683333.33, NULL),
(17, 1, '2026-05-15', NULL, 683333.33, NULL),
(17, 1, '2026-06-16', NULL, 683333.33, NULL),
(17, 1, '2026-07-15', NULL, 683333.33, NULL),
(17, 1, '2026-08-17', NULL, 683333.33, NULL),
(17, 1, '2026-09-15', NULL, 683333.33, NULL);
GO

-- testCliente (idCliente 1): personal en curso con cuota vencida
INSERT INTO Prestamo (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
VALUES (1, 1, 1, 500000.00, 82050.00, 6, 5, '2026-03-10', GETDATE(), 4); -- idPrestamo 23

INSERT INTO Cuota (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago) VALUES
(23, 2, '2026-05-10', '2026-05-09', 97008.33, 1),
(23, 3, '2026-06-10', NULL,         97008.33, NULL),
(23, 1, '2026-07-10', NULL,         97008.33, NULL),
(23, 1, '2026-08-11', NULL,         97008.33, NULL),
(23, 1, '2026-09-10', NULL,         97008.33, NULL),
(23, 1, '2026-10-12', NULL,         97008.33, NULL);
GO