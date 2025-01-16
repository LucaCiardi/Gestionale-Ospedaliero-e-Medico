-- Vista per dashboard generale
CREATE VIEW vw_DashboardGenerale AS
SELECT 
    o.Id AS OspedaleId,
    o.Nome AS NomeOspedale,
    o.Sede,
    COUNT(DISTINCT m.Id) AS NumeroMedici,
    COUNT(DISTINCT p.Id) AS NumeroPazienti,
    SUM(m.PazientiGuariti) AS TotalePazientiGuariti,
    SUM(m.TotaleDecessi) AS TotaleDecessi
FROM Ospedali o
LEFT JOIN Medici m ON o.Id = m.OspedaleId
LEFT JOIN Pazienti p ON m.Id = p.MedicoId
GROUP BY o.Id, o.Nome, o.Sede;
GO

-- Vista per statistiche medici
CREATE VIEW vw_StatisticheMedici AS
SELECT 
    m.Id AS MedicoId,
    m.Nome + ' ' + m.Cognome AS NomeMedico,
    m.Reparto,
    o.Nome AS NomeOspedale,
    COUNT(p.Id) AS PazientiAttuali,
    m.PazientiGuariti,
    m.TotaleDecessi,
    CAST((CAST(m.PazientiGuariti AS FLOAT) / 
          NULLIF((m.PazientiGuariti + m.TotaleDecessi), 0) * 100) AS DECIMAL(5,2)) AS PercentualeSuccesso
FROM Medici m
INNER JOIN Ospedali o ON m.OspedaleId = o.Id
LEFT JOIN Pazienti p ON p.MedicoId = m.Id
GROUP BY m.Id, m.Nome, m.Cognome, m.Reparto, o.Nome, m.PazientiGuariti, m.TotaleDecessi;
GO
