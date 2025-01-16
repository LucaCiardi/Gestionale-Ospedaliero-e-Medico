-- Funzione per calcolare l'età
CREATE FUNCTION fn_CalcolaEta
(
    @DataNascita datetime
)
RETURNS int
AS
BEGIN
    RETURN DATEDIFF(YEAR, @DataNascita, GETDATE()) -
        CASE
            WHEN (MONTH(@DataNascita) > MONTH(GETDATE())) OR
                 (MONTH(@DataNascita) = MONTH(GETDATE()) AND DAY(@DataNascita) > DAY(GETDATE()))
            THEN 1
            ELSE 0
        END
END;
