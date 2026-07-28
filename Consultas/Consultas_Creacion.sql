CREATE TABLE Paciente (
    PacienteID INT IDENTITY(1,1) PRIMARY KEY,   -- ID único autoincremental
    TipoDocumento NVARCHAR(20) NOT NULL,        -- CC, TI, Pasaporte, etc.
    NumeroDocumento NVARCHAR(50) NOT NULL UNIQUE, -- Evita duplicados
    Nombre NVARCHAR(100) NOT NULL,              -- Nombre completo
    FechaNacimiento DATE NOT NULL,              -- Fecha de nacimiento
    CorreoElectronico NVARCHAR(100),            -- Email, puede ser NULL
    Genero NVARCHAR(10),                        -- Masculino, Femenino, Otro
    Direccion NVARCHAR(200),                    -- Dirección física
    NumeroTelefono NVARCHAR(20),                -- Teléfono de contacto
    Activo BIT
);

--------------------------------------- Crear paciente ---------------------------------------

CREATE PROCEDURE sp_InsertPaciente
    @TipoDocumento NVARCHAR(20),
    @NumeroDocumento NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @FechaNacimiento DATE,
    @CorreoElectronico NVARCHAR(100),
    @Genero NVARCHAR(10),
    @Direccion NVARCHAR(200),
    @NumeroTelefono NVARCHAR(20),
    @Activo BIT
AS
BEGIN
    INSERT INTO Paciente (TipoDocumento, NumeroDocumento, Nombre, FechaNacimiento, CorreoElectronico, Genero, Direccion, NumeroTelefono, Activo)
    VALUES (@TipoDocumento, @NumeroDocumento, @Nombre, @FechaNacimiento, @CorreoElectronico, @Genero, @Direccion, @NumeroTelefono, @Activo);
END;


-------------------------- Traer paciente -------------------------------------

CREATE PROCEDURE sp_GetPacienteByID
    @PacienteID INT
AS
BEGIN
    SELECT * 
    FROM Paciente
    WHERE PacienteID = @PacienteID;
END;

-------------------------- Traer todos los pacientes ----------------------------------

CREATE PROCEDURE sp_GetAllPacientes
AS
BEGIN
    SELECT * 
    FROM Paciente;
END;


----------------------- Actualizar un paciente ---------------------------------------------

CREATE PROCEDURE sp_UpdatePaciente
    @PacienteID INT,
    @TipoDocumento NVARCHAR(20),
    @NumeroDocumento NVARCHAR(50),
    @Nombre NVARCHAR(100),
    @FechaNacimiento DATE,
    @CorreoElectronico NVARCHAR(100),
    @Genero NVARCHAR(10),
    @Direccion NVARCHAR(200),
    @NumeroTelefono NVARCHAR(20),
    @Activo BIT
AS
BEGIN
    UPDATE Paciente
    SET TipoDocumento = @TipoDocumento,
        NumeroDocumento = @NumeroDocumento,
        Nombre = @Nombre,
        FechaNacimiento = @FechaNacimiento,
        CorreoElectronico = @CorreoElectronico,
        Genero = @Genero,
        Direccion = @Direccion,
        NumeroTelefono = @NumeroTelefono,
        Activo = @Activo
    WHERE PacienteID = @PacienteID;
END;


-------------------------- Elimienar un paciente ----------------


CREATE PROCEDURE sp_DeletePaciente
    @PacienteID INT
AS
BEGIN
    DELETE FROM Paciente
    WHERE PacienteID = @PacienteID;
END;


------------------------- Insertar usuarios ---------------------------

-- Insertar 5 pacientes de prueba usando el SP
EXEC sp_InsertPaciente 
    'CC', '1001', 'Ana Gómez', '1985-03-12', 'ana.gomez@mail.com', 'Femenino', 'Calle 45 #12-34', '3101234567', 1;

EXEC sp_InsertPaciente 
    'TI', '2002', 'Luis Martínez', '2000-07-25', 'luis.martinez@mail.com', 'Masculino', 'Carrera 10 #56-78', '3209876543', 1;

EXEC sp_InsertPaciente 
    'Pasaporte', 'X12345', 'María Rodríguez', '1992-11-05', 'maria.rodriguez@mail.com', 'Femenino', 'Av. Siempre Viva 742', '3004567890', 1;

EXEC sp_InsertPaciente 
    'CC', '3003', 'Carlos Pérez', '1990-05-10', 'carlos.perez@mail.com', 'Masculino', 'Calle 123 #45-67', '3012345678', 1;

EXEC sp_InsertPaciente 
    'CC', '4004', 'Sofía Hernández', '1998-01-20', 'sofia.hernandez@mail.com', 'Femenino', 'Carrera 20 #89-10', '3156789012', 1;

