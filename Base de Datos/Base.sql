USE master
GO

IF EXISTS(SELECT * FROM SysDataBases WHERE name='Terminal')
BEGIN
	DROP DATABASE Terminal
END
GO

CREATE DATABASE Terminal
ON(
	NAME=Terminal,
	FILENAME='C:\Terminal.mdf'
)
GO

USE Terminal
GO


CREATE TABLE Compania(
nombre varchar (50) NOT NULL PRIMARY KEY,
direccion varchar (50) NOT NULL,
telefono int NOT NULL,
activo BIT NOT NULL DEFAULT 1
)
go

CREATE TABLE Destinos(
codigo varchar (3) CHECK (codigo LIKE '[a-Z][a-Z][a-Z]') NOT NULL PRIMARY KEY ,
ciudad varchar (50) NOT NULL,
pais varchar (50)  check (pais in ('argentina','brasil','paraguay','uruguay')) NOT NULL,
activo BIT NOT NULL DEFAULT 1
)
go

CREATE TABLE Facilidades(
codigo varchar(3) FOREIGN KEY REFERENCES Destinos(codigo),
facilidad varchar(50), 
PRIMARY KEY (codigo, facilidad)
)
go

CREATE TABLE Empleados(
ci varchar (8) CHECK (ci LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]') NOT NULL PRIMARY KEY,
contrasena varchar (6) NOT NULL CHECK (len(contrasena) = 6),
nomCompleto varchar (50) NOT NULL,
activo BIT NOT NULL DEFAULT 1
)
GO  


CREATE TABLE Viajes(
numViaje int CHECK (numViaje > 0) NOT NULL primary key,
nomCompania varchar (50) NOT NULL FOREIGN KEY REFERENCES Compania(nombre),
destino varchar (3) NOT NULL FOREIGN KEY REFERENCES Destinos(codigo),
fSalida DATETIME NOT NULL,
fArribo DATETIME NOT NULL,
CantAsientos int CHECK (CantAsientos >= 0) NOT NULL,
ultEmpleado varchar (8) NOT NULL FOREIGN KEY REFERENCES Empleados(ci),
CHECK (fSalida <= fArribo), 
CHECK (fsalida >= getdate())
)
GO


CREATE TABLE ViajeNacional(
numViaje int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Viajes(numViaje),
paradas int CHECK (paradas >= 0) NOT NULL
)
GO

CREATE TABLE ViajeInter(
numViaje int NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Viajes(numViaje),
servAbordo BIT NOT NULL,
documentos varchar (50)  NOT NULL
)
GO


--*******************LOGIN*********************

CREATE PROCEDURE Login
--ALTER PROC LOGIN
@ci varchar (8),
@contrasena varchar (6) 
AS 
BEGIN
	
	SELECT * FROM EMPLEADOS WHERE CI = @ci AND CONTRASENA = @CONTRASENA and Activo = 1 
 
END
GO


--*********************************************
--***************COMPAÑIAS*********************

Create Procedure BuscarCompaniaActiva @nombre varchar (50) As
Begin
	Select * From Compania where nombre  = @nombre and Activo = 1 
End
go

Create Procedure BuscarCompaniaTodas @nombre varchar (50) As
Begin
	Select * From Compania where nombre  = @nombre
End
go

Create Procedure ListadoCompanias As 
Begin
	Select * From Compania where Activo = 1
End
go

CREATE PROCEDURE AltaCompania
@nombre varchar (50),
@direccion varchar (50),
@telefono INT
AS
BEGIN
	IF EXISTS (SELECT nombre FROM Compania WHERE nombre = @nombre AND activo = 1)
		RETURN -1 --La Compañia que intenta ingresar ya existe.
		
	IF EXISTS (SELECT nombre FROM Compania WHERE nombre = @nombre AND activo = 0)
	BEGIN
		UPDATE Compania SET direccion = @direccion, telefono = @telefono, activo = 1
		WHERE nombre = @nombre
		
		IF @@ERROR > 0
			RETURN -6 --Error en la Base de datos.
		
		RETURN 1 --Compañia activada correctamente.
	END
	
	INSERT INTO Compania(nombre,direccion,telefono) VALUES (@nombre,@direccion,@telefono)
	
	IF @@ERROR > 0
		RETURN -6 --Error en la Base de datos.
	
	RETURN 1 --Compañia ingresada correctamente.
END 
GO

CREATE PROCEDURE ModificarCompania
@nombre varchar (50),
@direccion varchar (50),
@telefono INT
AS
BEGIN
	IF NOT EXISTS (SELECT nombre FROM Compania WHERE nombre = @nombre AND activo = 1)
		RETURN -1 --La Compañia que intenta modificar no existe.
	
	UPDATE Compania SET direccion = @direccion, telefono = @telefono
	WHERE nombre = @nombre
	
	IF @@ERROR > 0
		RETURN -6 --Error en la Base de datos.
	
	RETURN 1 --Compañia modificada correctamente.
END 
GO

CREATE PROCEDURE EliminarCompania
@nombre varchar (50)
AS
BEGIN
	IF NOT EXISTS (SELECT nombre FROM Compania WHERE nombre = @nombre)
		RETURN -1 --La Compañia que intenta eliminar no existe.
		
	IF EXISTS (SELECT numViaje FROM Viajes WHERE nomCompania = @nombre)
	BEGIN
		UPDATE Compania SET activo = 0
		WHERE nombre = @nombre
		
		IF @@ERROR > 0
			RETURN -6 --Error en la Base de datos.
		
		RETURN 1 --Compañia dada de baja correctamente.
	END
	
	DELETE Compania WHERE nombre = @nombre
	
	IF @@ERROR > 0
		RETURN -6 --Error en la Base de datos.
	
	RETURN 1 --Compañia eliminada correctamente.
END 
GO 

--*********************************************
--*****************EMPLEADOS*******************

Create Procedure BuscarEmpleadoActivo @ci varchar (8) As
Begin
	Select * From Empleados where ci  = @ci and Activo = 1 
End
go

Create Procedure BuscarEmpleadosTodos @ci varchar (8) As
Begin
	Select * From Empleados where ci  = @ci
End
go


Create Procedure AltaEmpleado 
--ALTER PROC AltaEmpleado 
@ci varchar (8), 
@contrasena varchar(6), 
@nomCompleto varchar(50) 
As
Begin
		--existe el empleado y no esta activo.
		IF EXISTS (SELECT * FROM Empleados WHERE ci = @ci AND activo = 1)
			RETURN -1 --Ya existe y esta activo
		
		IF EXISTS(SELECT * FROM Empleados WHERE ci = @ci and Activo = 0)
			BEGIN
				UPDATE Empleados SET Activo = 1,contrasena = @contrasena, nomCompleto = @nomCompleto 
				WHERE ci = @ci
				
				IF (@@ERROR > 0)
					RETURN -6 --ERROR SQL
					
				RETURN 1 --Dado de alta
			END
        
		--no existe el empleado
	    INSERT Empleados (ci, contrasena, nomCompleto) VALUES (@ci, @contrasena, @nomCompleto)
	    	IF (@@ERROR > 0)
					RETURN -6 --ERROR SQL
	    RETURN 1 --Dado de alta
End
go

Create Procedure BajaEmpleado 
--ALTER PROC BajaEmpleado 
@ci varchar (8) 
As
Begin
        --si no existe el empleado
		if (Not Exists(Select * From Empleados Where ci = @ci))
			return -1
			
        --si existe y esta registrado en un viaje
		if (Exists(Select * From Viajes Where ultEmpleado = @ci))
			Begin
				update Empleados set Activo = 0 where ci = @ci
				
				IF (@@ERROR = 0)
					RETURN 1--Baja logica, Viajes me necesita
				
				RETURN -6 --Error SQL
			end
			
		--si existe y no esta en un viaje
		Delete From Empleados where ci = @ci
				
		If (@@ERROR = 0)
			return 1	--Baja fisica
			
	   	return -6 --Error SQL
End
go

Create Procedure ModificarEmpleado 
--ALTER PROC ModificarEmpleado 
@ci varchar (8), 
@contrasena varchar(6), 
@nomCompleto varchar(50) 
As
Begin
        --si no existe y no esta activo
		if (Not Exists(Select * From Empleados Where ci = @ci and Activo = 1))
			return -1
			
		--si exise y esta activo
		Update Empleados Set contrasena = @contrasena, nomCompleto = @nomCompleto Where ci = @ci

		IF (@@ERROR = 0)
			return 1
		return -6 --Error SQL
End
go


--********************************************************
--**********************VIAJES NACIONALES******************


CREATE PROC BuscarViajeNacional
--ALTER PROC BuscarViajeNacional
@numViaje int
as
BEGIN
SELECT *  FROM ViajeNacional vn join Viajes v on vn.numViaje = v.numViaje
where vn.numViaje = @numViaje
END
GO

CREATE PROC EliminarNacional
--ALTER PROC EliminarNacional
@numViaje int
as
BEGIN

IF NOT EXISTS (SELECT numViaje FROM ViajeNacional WHERE numViaje = @numViaje)
RETURN -1 --no existe el viaje

BEGIN TRAN
DELETE FROM ViajeNacional WHERE numViaje = @numViaje

IF @@ERROR <> 0 
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

DELETE FROM Viajes WHERE numViaje = @numViaje

IF @@ERROR <> 0 
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

COMMIT TRAN
RETURN 1

END
GO

CREATE PROC ModificarNacional
--ALTER PROC ModificarNacional 
	@numViaje int,
	@nomCompania varchar (50),
	@destino varchar (3),
	@fSalida DATETIME,
	@fArribo DATETIME,
	@CantAsientos int,
	@ultEmpleado varchar (8),
	@paradas int
as
BEGIN

IF NOT EXISTS (SELECT numViaje FROM ViajeNacional WHERE numViaje = @numViaje)
	RETURN -1 --no existe

IF EXISTS (SELECT * FROM Viajes 
WHERE destino = @destino AND (120 > DATEDIFF(MINUTE, fSalida , @fSalida) AND (-120 < DATEDIFF(MINUTE, fSalida , @fSalida))) 
AND numViaje <> @numViaje)
	RETURN -2 --RNE 2 horas de diferencia al mismo destino

IF NOT EXISTS (SELECT * FROM Compania WHERE nombre = @nomCompania AND activo = 1)
	RETURN -3 --No existe la Compania
	
IF NOT EXISTS (SELECT * FROM Destinos WHERE codigo = @destino AND activo = 1)
	return -4 --No existe el destino
	
IF NOT EXISTS (SELECT * FROM Empleados WHERE ci = @ultEmpleado AND activo = 1)
	return -5 --No existe el empleado

BEGIN TRAN
UPDATE ViajeNacional SET paradas = @paradas WHERE numViaje = @numViaje

IF @@ERROR <> 0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

UPDATE Viajes SET nomCompania = @nomCompania, destino = @destino,
fSalida = @fSalida, fArribo=@fArribo, CantAsientos = @CantAsientos,
ultEmpleado = @ultEmpleado    
WHERE numViaje = @numViaje

IF @@ERROR <> 0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

COMMIT TRAN
RETURN 1						

END	
GO

CREATE PROC AgregarNacional
--ALTER PROC AgregarNacional 
	@numViaje int,
	@nomCompania varchar (50),
	@destino varchar (3),
	@fSalida DATETIME,
	@fArribo DATETIME,
	@CantAsientos int,
	@ultEmpleado varchar (8),
	@paradas int

as
BEGIN

IF EXISTS (SELECT * FROM viajes WHERE numViaje = @numViaje)
	RETURN -1 --si ESTA no lo puedo agregar

IF EXISTS (SELECT * FROM Viajes WHERE destino = @destino AND (120 > DATEDIFF(MINUTE, fSalida , @fSalida) AND (-120 < DATEDIFF(MINUTE, fSalida , @fSalida))))
	RETURN -2 --RNE 2 horas de diferencia al mismo destino
	
IF NOT EXISTS (SELECT * FROM Compania WHERE nombre = @nomCompania AND activo = 1)
	RETURN -3 --No existe la Compania
	
IF NOT EXISTS (SELECT * FROM Destinos WHERE codigo = @destino AND activo = 1)
	return -4 --No existe el destino
	
IF NOT EXISTS (SELECT * FROM Empleados WHERE ci = @ultEmpleado AND activo = 1)
	return -5 --No existe el empleado

BEGIN TRAN
INSERT Viajes(numViaje,nomCompania,destino, fSalida, fArribo, CantAsientos, ultEmpleado) VALUES 
(@numViaje,@nomCompania,@destino, @fSalida, @fArribo, @CantAsientos, @ultEmpleado)

IF @@ERROR<>0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

INSERT ViajeNacional(numViaje,paradas) VALUES (@numViaje,@paradas)

IF @@ERROR<>0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

COMMIT TRAN
RETURN 1						

END	
GO

CREATE Procedure ListadoNacionales 
As 
Begin
	Select * FROM Viajes v INNER JOIN ViajeNacional vn ON v.numViaje = vn.numViaje 
End
go	

Create Proc ListadoSinPartirNac
As
Begin
	Select * From Viajes v Inner Join ViajeNacional vn ON v.numViaje = vn.numViaje
	Where GETDATE() < v.fSalida
End
go

--**********************************************************
--*****************VIAJES INERNACIONALES********************


CREATE PROC BuscarViajeInter
--ALTER PROC BuscarViajeInter
@numViaje int
as
BEGIN
SELECT *  FROM ViajeInter vi join Viajes v on vi.numViaje = v.numViaje
WHERE vi.numViaje = @numViaje
END
GO

CREATE PROC EliminarInter
--ALTER PROC EliminarInter
@numViaje int
as
BEGIN

IF NOT EXISTS (SELECT numViaje FROM ViajeInter WHERE numViaje = @numViaje)
RETURN -1 --no existe o es un viaje nacional

BEGIN TRAN
DELETE FROM ViajeInter WHERE numViaje = @numViaje

IF @@ERROR <> 0 
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

DELETE FROM Viajes WHERE numViaje = @numViaje

IF @@ERROR <> 0 
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

COMMIT TRAN
RETURN 1

END
GO

CREATE PROC ModificarInter
--ALTER PROC ModificarInter 
	@numViaje int,
	@nomCompania varchar (50),
	@destino varchar (3),
	@fSalida DATETIME,
	@fArribo DATETIME,
	@CantAsientos int,
	@ultEmpleado varchar (8),
	@servAbordo BIT,
	@documentos varchar (50)
	
as
BEGIN

IF NOT EXISTS (SELECT numViaje FROM ViajeInter WHERE numViaje = @numViaje)
	RETURN -1 --no existe o es un viaje nacional
	
IF EXISTS (SELECT * FROM Viajes 
WHERE destino = @destino AND (120 > DATEDIFF(MINUTE, fSalida , @fSalida) AND (-120 < DATEDIFF(MINUTE, fSalida , @fSalida))) 
AND numViaje <> @numViaje)
	RETURN -2 --RNE 2 horas de diferencia al mismo destino
	
IF NOT EXISTS (SELECT * FROM Compania WHERE nombre = @nomCompania AND activo = 1)
	RETURN -3 --No existe la Compania
	
IF NOT EXISTS (SELECT * FROM Destinos WHERE codigo = @destino AND activo = 1)
	return -4 --No existe el destino
	
IF NOT EXISTS (SELECT * FROM Empleados WHERE ci = @ultEmpleado AND activo = 1)
	return -5 --No existe el empleado

BEGIN TRAN
UPDATE ViajeInter SET servAbordo = @servAbordo, documentos = @documentos WHERE numViaje = @numViaje

IF @@ERROR <> 0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

UPDATE Viajes SET nomCompania = @nomCompania, destino = @destino,
fSalida = @fSalida, fArribo=@fArribo, CantAsientos = @CantAsientos,
ultEmpleado = @ultEmpleado    
WHERE numViaje = @numViaje

IF @@ERROR <> 0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

COMMIT TRAN
RETURN 1						

END	
GO

CREATE PROC AgregarInter
--ALTER PROC AgregarInter 
	@numViaje int,
	@nomCompania varchar (50),
	@destino varchar (3),
	@fSalida DATETIME,
	@fArribo DATETIME,
	@CantAsientos int,
	@ultEmpleado varchar (8),
	@servAbordo BIT,
	@documentos varchar (50)

as
BEGIN

IF EXISTS (SELECT * FROM viajes WHERE numViaje = @numViaje)
	RETURN -1 --si ESTA no lo puedo agregar

IF EXISTS (SELECT * FROM Viajes WHERE destino = @destino AND (120 > DATEDIFF(MINUTE, fSalida , @fSalida) AND (-120 < DATEDIFF(MINUTE, fSalida , @fSalida))))
	RETURN -2 --RNE 2 horas de diferencia al mismo destino

IF NOT EXISTS (SELECT * FROM Compania WHERE nombre = @nomCompania AND activo = 1)
	RETURN -3 --No existe la Compania
	
IF NOT EXISTS (SELECT * FROM Destinos WHERE codigo = @destino AND activo = 1)
	return -4 --No existe el destino
	
IF NOT EXISTS (SELECT * FROM Empleados WHERE ci = @ultEmpleado AND activo = 1)
	return -5 --No existe el empleado


DECLARE @ERROR int
BEGIN TRAN
INSERT Viajes(numViaje,nomCompania,destino, fSalida, fArribo, CantAsientos, ultEmpleado) VALUES 
(@numViaje,@nomCompania,@destino, @fSalida, @fArribo, @CantAsientos, @ultEmpleado)

IF @@ERROR<>0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

INSERT ViajeInter(numViaje,servAbordo,documentos) VALUES (@numViaje,@servAbordo,@documentos)

IF @@ERROR<>0
BEGIN
ROLLBACK TRAN
RETURN -6 --ERROR SQL
END

COMMIT TRAN
RETURN 1						

END	
GO

CREATE Procedure ListadoInter 
As 
Begin
	Select * From Viajes v INNER JOIN ViajeInter vi ON v.numViaje = vi.numViaje
End
go	

CREATE Procedure ListadoSinPartirInter
As 
Begin
	Select * From Viajes v INNER JOIN ViajeInter vi ON v.numViaje = vi.numViaje
	Where GETDATE() < v.fSalida
End	
go

--****************************************************
--*********************Destinos********************


CREATE PROC BuscarDestinoActivo
--ALTER PROC BuscarDestinoActivo
@codigo varchar(3)
as
BEGIN
	SELECT * FROM Destinos 
	WHERE codigo = @codigo AND activo = 1
END
go

CREATE PROC BuscarDestinoTodos
--ALTER PROC BuscarDestinoTodos
@codigo varchar(3)
as
BEGIN
	SELECT * FROM Destinos 
	WHERE codigo = @codigo
END
go


CREATE PROC AltaDestino 
--ALTER PROC AltaDestino
@codigo varchar(3),
@ciudad varchar(50),
@pais varchar(50)
as
BEGIN 
IF EXISTS (SELECT * FROM Destinos WHERE codigo = @codigo AND activo = 1)
RETURN -1 --Ya existe y esta activo

IF EXISTS (SELECT * FROM Destinos WHERE codigo = @codigo AND activo = 0)
BEGIN
	UPDATE Destinos 
	SET ciudad = @ciudad, pais = @pais, activo = 1
	WHERE codigo = @codigo 

	IF @@ERROR <> 0
		RETURN -6 --Error de SQL
		 
	RETURN 1 --Ya existe pero no estaba activo
END

INSERT INTO Destinos (codigo ,ciudad, pais) VALUES (@codigo, @ciudad, @pais)

IF (@@ERROR <> 0)
BEGIN
	RETURN -6 --Error de SQL
END

RETURN 1 -- Agregado correctamente

END 
go

CREATE PROC BajaDestino
--ALTER PROC BajaDestino
@codigo varchar(3)
AS
BEGIN

DECLARE @ERROR INT

IF EXISTS (SELECT * FROM Destinos WHERE codigo = @codigo AND activo = 1)
BEGIN
	IF EXISTS (SELECT * FROM Viajes WHERE destino = @codigo)
	BEGIN
		BEGIN TRAN 
			UPDATE Destinos SET activo = 0 WHERE codigo = @codigo
			IF (@@ERROR<>0)
			BEGIN
				ROLLBACK TRAN
				RETURN -6 --Error SQL 
			END
				
			--Borro Facilidades para que cuando lo vuelva a activar queden solo los datos nuevos.
			DELETE FROM Facilidades WHERE codigo = @codigo
			IF (@@ERROR<>0)
			BEGIN
				ROLLBACK TRAN
				RETURN -6 --Error SQL 
			END
			
		COMMIT TRAN
		RETURN 1 --Baja Logica
	END

	BEGIN TRAN 
	
	DELETE FROM Facilidades WHERE codigo = @codigo
	
	IF @@ERROR<>0
	BEGIN
		ROLLBACK TRAN
		RETURN -6 --Error SQL
	END
	
	DELETE FROM Destinos WHERE codigo = @codigo 
	
	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRAN
		RETURN -6 --Error SQL 
	END
	COMMIT TRAN
	RETURN 1 --	Baja Fisica
 
END
	RETURN -1 --No esta activo o no existe el destino que quiero borrar 

END

go

CREATE PROC ModificarDestino 
--ALTER PROC ModificarDestino
@codigo varchar(30),
@ciudad varchar(50),
@pais varchar(50)
as
BEGIN

IF NOT EXISTS (SELECT * FROM Destinos WHERE codigo = @codigo AND activo = 1)
	RETURN -1 --No existe o no esta activo el destino

	UPDATE Destinos 
	SET ciudad = @ciudad, pais = @pais 
	WHERE codigo = @codigo

  	IF @@ERROR <> 0
	BEGIN
		RETURN -6 --Error SQL
	END
	
	RETURN 1 --Modificacion exitosa

END
go

CREATE PROC ListarDestinosActivos
--ALTER PROC ListarDestinosActivos
as
BEGIN

SELECT * FROM Destinos WHERE activo = 1

END
go

--****************************************************
--*********************FACILIDADES********************

CREATE PROC AgregarFacilidades
--ALTER PROC AgregarFacilidades
@codigo varchar(3),
@facilidad varchar(50)
AS
BEGIN

IF NOT EXISTS (SELECT * FROM Destinos WHERE codigo = @codigo AND activo = 1)
RETURN -2 --no existe el destino no se puede agregar facilidad

IF EXISTS (SELECT * FROM Facilidades WHERE codigo = @codigo AND facilidad = @facilidad)
RETURN -1 --Ya existe la facilidad

INSERT INTO Facilidades (codigo, facilidad) VALUES (@codigo, @facilidad)

IF @@ERROR <> 0
	RETURN -6 --Error SQL
	
RETURN 1 --Exito
 
END
go

CREATE PROC EliminarFacilidades
--ALTER PROC EliminarFacilidades
@codigo varchar(3)
AS
BEGIN

DELETE FROM Facilidades WHERE codigo = @codigo 
IF @@ERROR <>0 
	RETURN -6 --Error SQL
RETURN 1--Exito
END
go


Create Procedure FacilidadesDeUnDestino 
@Cod varchar (3) 
As
Begin
	Select * From Facilidades Where  codigo = @Cod
End
go


---DATOS DE PRUEBA

--Empleados
EXEC AltaEmpleado '41231231','123456','Pepe Trueno'
EXEC AltaEmpleado '47777278','123456','Bruno Bardesio'
EXEC AltaEmpleado '54025652','123456','Damian Boz'
EXEC AltaEmpleado '48524325','123456','Damian Hernandez'
EXEC AltaEmpleado '00000000', 'Admin1', 'Admin'

--Companias
EXEC AltaCompania 'Copsa','Uruguay y Yaguaron', 1715
EXEC AltaCompania 'Nuñez','Tres Cruces', 24025363
EXEC AltaCompania 'BUQUEBUS', 'Tres Cruces',24018998
EXEC AltaCompania 'EGA', 'Tres Cruces agencia 30',24025164
EXEC AltaCompania 'Cutcsa', 'me canse de buscar una direccion real',321654
EXEC AltaCompania 'Coect', 'es muy tarde para poner algo real',789654
EXEC AltaCompania 'Turismar', 'en algun lugar de la mancha',47586532
EXEC AltaCompania 'Raincop', 'por ahi anda',23045252
EXEC AltaCompania 'JC', 'por ahi por el centro',23051468
EXEC AltaCompania 'Jorge Martinez', 'cruzando la calle',23058868
EXEC AltaCompania 'COT', 'no se',23458868

--Destinos
EXEC AltaDestino 'MLO', 'Melo', 'Uruguay'
EXEC AgregarFacilidades 'MLO', 'baño'
EXEC AgregarFacilidades 'MLO', 'wifi'
EXEC AgregarFacilidades 'MLO', 'enfermeria'
EXEC AgregarFacilidades 'MLO', 'plaza comidas'

EXEC AltaDestino 'MVD','Montevideo','Uruguay'
EXEC AgregarFacilidades 'MVD', 'plaza comida'
EXEC AgregarFacilidades 'MVD', 'shopping'
EXEC AgregarFacilidades 'MVD', 'cafeteria'

EXEC AltaDestino 'ATD', 'Atlantida', 'Uruguay'
EXEC AgregarFacilidades 'ATD', 'baño'
EXEC AgregarFacilidades 'ATD', 'wifi'
EXEC AgregarFacilidades 'ATD', 'enfermeria'
EXEC AgregarFacilidades 'ATD', 'plaza comidas'
EXEC AgregarFacilidades 'ATD', 'otro'

EXEC AltaDestino 'RIO', 'Rio de Janeiro', 'Brasil'
EXEC AgregarFacilidades 'RIO', 'turismo'
EXEC AgregarFacilidades 'RIO', 'wifi'

EXEC AltaDestino 'BAS', 'Buenos Aires', 'Argentina'

EXEC AltaDestino 'FLO', 'Florianopolis', 'Brasil'
EXEC AgregarFacilidades 'FLO', 'turismo'
EXEC AgregarFacilidades 'FLO', 'wifi'
EXEC AgregarFacilidades 'FLO', 'baño'

EXEC AltaDestino 'CHU', 'Chuy', 'Uruguay'
EXEC AgregarFacilidades 'CHU', 'turismo'
EXEC AgregarFacilidades 'CHU', 'wifi'
EXEC AgregarFacilidades 'CHU', 'bagayo'

/* 
EXEC AltaDestino 'LPZ', 'La Paz', 'Bolivia'
EXEC AgregarFacilidades 'LPZ', 'altura'
EXEC AgregarFacilidades 'LPZ', 'mas altura'
EXEC AgregarFacilidades 'LPZ', 'mas altura, me falta el aire'

EXEC AltaDestino 'CAR', 'Caracas', 'Venezuela'
EXEC AgregarFacilidades 'CAR', 'Maduro'

*/
EXEC AgregarInter 1,'Buquebus','BAS','20190120 08:00:00','20190121 12:00:00',3,41231231,1,'ninguno'
EXEC AgregarNacional 2,'Nuñez','MLO','20190125 00:00:00','20190126 02:00:00',2,'41231231',2
EXEC AgregarInter 3,'EGA','RIO','20190122 08:00:00','20190123 12:00:00',3,41231231,1,'ninguno'
EXEC AgregarNacional 4,'Copsa','ATD','20190126 16:30:00','20190126 17:41:00',2,'41231231',2
EXEC AgregarNacional 5,'COT','CHU','20181224 11:42:00','20181224 17:41:00',40,'48524325',4
EXEC AgregarNacional 6,'COPSA','CHU','20181224 08:15:00','20181224 12:33:00',45,'54025652',33
EXEC AgregarNacional 7,'Nuñez','MLO','20190124 08:15:00','20190124 12:33:00',39,'47777278',0
EXEC AgregarInter 8,'JC','FLO','20190401 05:35:00','20190402 07:22:00',45,48524325,1,'muchos documentos para hacer tramites'
--EXEC AgregarInter 9,'Jorge Martinez','LPZ','20190521 03:40:00','20190523 22:59:00',40,47777278,0,'varios tipos de documentos'
--EXEC AgregarInter 10,'EGA','CAR','20190615 11:34:00','20190623 18:03:00',20,54025652,1,'papelitos de colores'
--EXEC AgregarInter 11,'EGA','CAR','20180105 14:44:00','20180107 16:32:00',20,54025652,1,'viaje viejo'
--EXEC AgregarInter 12,'Jorge Martinez','LPZ','20190112 19:35:00','20190116 15:21:00',40,47777278,0,'viaje viejo'
EXEC AgregarNacional 13,'Copsa','ATD','20190115 13:41:00','20190115 14:55:00',40,'48524325',30
EXEC AgregarInter 14,'Buquebus','BAS','20200906 08:00:00','20200906 12:00:00',3,41231231,1,'idem a los primeros'
EXEC AgregarNacional 15,'Nuñez','MLO','20200925 00:00:00','20200926 02:00:00',2,'41231231',2
EXEC AgregarInter 16,'EGA','RIO','20200906 08:00:00','20200906 12:00:00',3,41231231,1,'idem a los primeros'
EXEC AgregarNacional 17,'Copsa','ATD','20201026 16:30:00','20201026 17:41:00',2,'41231231',2

/*
SELECT * FROM Empleados
SELECT * FROM Viajes
SELECT * FROM Destinos
SELECT * FROM Compania
SELECT * FROM Facilidades
*/
--*********************************************

--creacion de usuario IIS para que el webservice pueda acceder a la bd------------------------
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

USE Terminal
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]
GO

Grant Execute to [IIS APPPOOL\DefaultAppPool]
go