-- Indici per ricerche frequenti sui medici
CREATE NONCLUSTERED INDEX IX_Medici_Reparto 
ON Medici(Reparto, OspedaleId)
INCLUDE (Nome, Cognome);

CREATE NONCLUSTERED INDEX IX_Medici_Cognome 
ON Medici(Cognome, Nome)
INCLUDE (Reparto, OspedaleId);

-- Indici per ricerche frequenti sui pazienti
CREATE NONCLUSTERED INDEX IX_Pazienti_Cognome 
ON Pazienti(Cognome, Nome)
INCLUDE (DataRicovero, MedicoId);

CREATE NONCLUSTERED INDEX IX_Pazienti_DataRicovero 
ON Pazienti(DataRicovero)
INCLUDE (Nome, Cognome, MedicoId);
