-- Procedura per ottenere statistiche complete di un ospedale
CREATE PROCEDURE sp_GetStatisticheOspedale
    @OspedaleId INT
AS
BEGIN
    SELECT 
        o.*,
        COUNT(DISTINCT m.Id) AS NumeroMedici,
        COUNT(DISTINCT p.Id) AS NumeroPazienti,
        SUM(m.PazientiGuariti) AS TotalePazientiGuariti,
        SUM(m.TotaleDecessi) AS TotaleDecessi,
        COUNT(CASE WHEN m.Primario = 1 THEN 1 END) AS NumeroPrimari
    FROM Ospedali o
    LEFT JOIN Medici m ON o.Id = m.OspedaleId
    LEFT JOIN Pazienti p ON m.Id = p.MedicoId
    WHERE o.Id = @OspedaleId
    GROUP BY o.Id, o.Nome, o.Sede, o.Pubblico;
END;
GO
-- Procedura per ricerca pazienti avanzata
CREATE PROCEDURE sp_RicercaPazienti
    @Cognome NVARCHAR(255) = NULL,
    @DataRicoveroInizio DATETIME = NULL,
    @DataRicoveroFine DATETIME = NULL,
    @Reparto NVARCHAR(255) = NULL
AS
BEGIN
    SELECT 
        p.*,
        m.Nome AS NomeMedico,
        m.Cognome AS CognomeMedico,
        m.Reparto,
        o.Nome AS NomeOspedale
    FROM Pazienti p
    INNER JOIN Medici m ON p.MedicoId = m.Id
    INNER JOIN Ospedali o ON m.OspedaleId = o.Id
    WHERE 
        (@Cognome IS NULL OR p.Cognome LIKE @Cognome + '%')
        AND (@DataRicoveroInizio IS NULL OR p.DataRicovero >= @DataRicoveroInizio)
        AND (@DataRicoveroFine IS NULL OR p.DataRicovero <= @DataRicoveroFine)
        AND (@Reparto IS NULL OR m.Reparto = @Reparto);
END;
GO