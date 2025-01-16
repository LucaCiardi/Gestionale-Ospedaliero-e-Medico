-- Trigger per aggiornare le statistiche del medico
CREATE TRIGGER trg_UpdateMedicoStats
ON Pazienti
AFTER DELETE
AS
BEGIN
    UPDATE m
    SET m.PazientiGuariti = (
        SELECT COUNT(*) 
        FROM Pazienti 
        WHERE MedicoId = m.Id
    )
    FROM Medici m
    INNER JOIN deleted d ON m.Id = d.MedicoId;
END;
