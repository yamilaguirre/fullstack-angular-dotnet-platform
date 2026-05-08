-- Sample data for local development and demos.

DELETE FROM dbo.Clientes;

DELETE FROM dbo.Paises;

INSERT INTO dbo.Paises (Nombre)
VALUES (N'Chile'),
       (N'Argentina'),
       (N'Perú'),
       (N'México');

-- IdPais is 1..4 in insertion order above.
INSERT INTO dbo.Clientes (NombreCompleto, Telefono, IdPais)
VALUES (N'Ana Pérez', N'+56912345678', 1),
       (N'Lucas Silva', N'56988776655', 2),
       (N'Carla Ramos', N'519998887766', 3),
       (N'Diego Torres', N'+5215544332211', 4),
       (N'Valentina Rojas', N'56900001111', 1),
       (N'Martín Acosta', N'+541199887766', 2),
       (N'Julieta Morales', N'51911112222', 3),
       (N'Pablo Vargas', N'+526612345678', 4);
