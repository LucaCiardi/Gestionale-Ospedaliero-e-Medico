-- Create Ospedali table
CREATE TABLE Ospedali (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Sede NVARCHAR(255) NOT NULL,
    Nome NVARCHAR(255) NOT NULL,
    Pubblico BIT NOT NULL,
    CONSTRAINT UQ_Ospedali_Nome_Sede UNIQUE (Nome, Sede)
);

-- Create Medici table
CREATE TABLE Medici (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(255) NOT NULL,
    Cognome NVARCHAR(255) NOT NULL,
    Dob DATETIME NOT NULL,
    Residenza NVARCHAR(255) NOT NULL,
    Reparto NVARCHAR(255) NOT NULL,
    Primario BIT NOT NULL DEFAULT 0,
    PazientiGuariti INT NOT NULL DEFAULT 0,
    TotaleDecessi INT NOT NULL DEFAULT 0,
    OspedaleId INT NOT NULL,
    CONSTRAINT FK_Medici_Ospedali FOREIGN KEY (OspedaleId) 
        REFERENCES Ospedali(Id) ON DELETE CASCADE,
    CONSTRAINT CHK_Medici_PazientiGuariti CHECK (PazientiGuariti >= 0),
    CONSTRAINT CHK_Medici_TotaleDecessi CHECK (TotaleDecessi >= 0)
);

-- Create Pazienti table
CREATE TABLE Pazienti (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(255) NOT NULL,
    Cognome NVARCHAR(255) NOT NULL,
    DataNascita DATETIME NOT NULL,
    Residenza NVARCHAR(255) NOT NULL,
    CodiceFiscale NVARCHAR(16) NOT NULL,
    DataRicovero DATETIME NOT NULL DEFAULT GETDATE(),
    MedicoId INT NOT NULL,
    CONSTRAINT FK_Pazienti_Medici FOREIGN KEY (MedicoId) 
        REFERENCES Medici(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_Pazienti_CodiceFiscale UNIQUE (CodiceFiscale),
    CONSTRAINT CHK_Pazienti_DataNascita CHECK (DataNascita <= GETDATE()),
    CONSTRAINT CHK_Pazienti_DataRicovero CHECK (DataRicovero >= DataNascita)
);
