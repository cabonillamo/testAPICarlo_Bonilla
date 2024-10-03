CREATE DATABASE DBTest;
GO

USE DBTest;
GO

CREATE TABLE Gestores (
    Id INT PRIMARY KEY,
    Nombre NVARCHAR(50)
);
GO

CREATE TABLE Saldos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Monto DECIMAL(10, 2)
);
GO

-- Inserta los gestores (10 gestores en total)
INSERT INTO Gestores (Id, Nombre) VALUES 
(1, 'Gestor 1'), (2, 'Gestor 2'), (3, 'Gestor 3'), 
(4, 'Gestor 4'), (5, 'Gestor 5'), (6, 'Gestor 6'), 
(7, 'Gestor 7'), (8, 'Gestor 8'), (9, 'Gestor 9'), 
(10, 'Gestor 10');
GO

-- Inserta los saldos
INSERT INTO Saldos (Monto) VALUES 
(2277), (3953), (4726), (1414), (627), (1784), (1634), (3958), (2156), (1347), 
(2166), (820), (2325), (3613), (2389), (4130), (2007), (3027), (2591), (3940), 
(3888), (2975), (4470), (2291), (3393), (3588), (3286), (2293), (4353), (3315), 
(4900), (794), (4424), (4505), (2643), (2217), (4193), (2893), (4120), (3352), 
(2355), (3219), (3064), (4893), (272), (1299), (4725), (1900), (4927), (4011);
GO

CREATE PROCEDURE AsignarMontos
AS
BEGIN
    -- Declara variables
    DECLARE @ID INT;
    DECLARE @GestorId INT;
    DECLARE @SaldoMonto DECIMAL(10, 2);
    DECLARE @TotalGestores INT = (SELECT COUNT(*) FROM Gestores);
    DECLARE @TotalSaldos INT = (SELECT COUNT(*) FROM Saldos);
    DECLARE @Iteraciones INT = CEILING((1.0 * @TotalSaldos) / @TotalGestores);

    -- Crea una tabla temporal para los resultados
    CREATE TABLE #SaldosAsignados (
        ID INT PRIMARY KEY IDENTITY(1,1),
        GestorId INT,
        SaldoMonto DECIMAL(10, 2)
    );

    -- Declarar un cursor para obtener los saldos ordenados en orden descendente
    DECLARE SaldoCursor CURSOR FOR 
    SELECT Monto FROM Saldos ORDER BY Monto DESC;

    -- Abrir el cursor para comenzar a leer los saldos
    OPEN SaldoCursor;
    -- Leer el primer saldo desde el cursor
    FETCH NEXT FROM SaldoCursor INTO @SaldoMonto;
    -- Inicializar contador para realizar un seguimiento de las iteraciones
    DECLARE @Contador INT = 1;
    -- Iterar mientras haya saldos disponibles
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Determinar el gestor correspondiente para asignar el saldo
        SET @GestorId = ((@Contador - 1) % @TotalGestores) + 1;
        -- Asignar el saldo al gestor correspondiente
        INSERT INTO #SaldosAsignados (GestorId, SaldoMonto) VALUES (@GestorId, @SaldoMonto);
        -- Leer el siguiente saldo desde el cursor
        FETCH NEXT FROM SaldoCursor INTO @SaldoMonto;
        -- Incrementar el contador
        SET @Contador = @Contador + 1;
    END;

    CLOSE SaldoCursor;
    -- Liberar el cursor de la memoria
    DEALLOCATE SaldoCursor;

    -- Devuelve la tabla de saldos asignados
    SELECT * FROM #SaldosAsignados;

    -- Limpia la tabla temporal
    DROP TABLE #SaldosAsignados;
END;
GO

-- Corremos el procedimiento almacenado
EXEC AsignarMontos;
