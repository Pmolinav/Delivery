SET IDENTITY_INSERT Vehiculo ON;

INSERT INTO Vehiculo (Id, Direccion, Conductor, Latitud, Longitud, CreationDate, RevisionDate)
VALUES (1, 'Madrid, Spain', 'Conductor 1', 40.4167047, -3.7035825, GETDATE(), null);
INSERT INTO Vehiculo (Id, Direccion, Conductor, Latitud, Longitud, CreationDate, RevisionDate)
VALUES (2, 'Barcelona, Cataluña, España', 'Conductor 2', 41.3828939, 2.1774322, GETDATE(), null);
INSERT INTO Vehiculo (Id, Direccion, Conductor, Latitud, Longitud, CreationDate, RevisionDate)
VALUES (3, 'Murcia, Región de Murcia, España', 'Conductor 5', 37.9923795, -1.1305431, GETDATE(), null);
INSERT INTO Vehiculo (Id, Direccion, Conductor, Latitud, Longitud, CreationDate, RevisionDate)
VALUES (4, 'Iglesia San Pablo, Calle Greco, 30008 Murcia, España', 'Conductor 4', 37.99078650137848, -1.1274231591891066, GETDATE(), null);
INSERT INTO Vehiculo (Id, Direccion, Conductor, Latitud, Longitud, CreationDate, RevisionDate)
VALUES (5, 'Calle San Vicente, 13, 28901 Getafe, España', 'Conductor 5', 40.30795816154705, -3.7259077033913, GETDATE(), null);

SET IDENTITY_INSERT Vehiculo OFF;

SET IDENTITY_INSERT Pedido ON;

INSERT INTO Pedido (Id, Titulo, Urgencia, VehiculoId, CreationDate, RevisionDate)
VALUES (1, 'Pedido Prueba 1', 1, 5, GETDATE(), null);
INSERT INTO Pedido (Id, Titulo, Urgencia, VehiculoId, CreationDate, RevisionDate)
VALUES (2, 'Pedido Ejemplo 2', 3, 3, GETDATE(), null);
INSERT INTO Pedido (Id, Titulo, Urgencia, VehiculoId, CreationDate, RevisionDate)
VALUES (3, 'Entrega Madrid Ejemplo', 2, 1, GETDATE(), null);
INSERT INTO Pedido (Id, Titulo, Urgencia, VehiculoId, CreationDate, RevisionDate)
VALUES (4, 'Ejemplo Barcelona', 3, 2, GETDATE(), null);
INSERT INTO Pedido (Id, Titulo, Urgencia, VehiculoId, CreationDate, RevisionDate)
VALUES (5, 'Otra entrega Madrid', 1, 1, GETDATE(), null);

SET IDENTITY_INSERT Pedido OFF;