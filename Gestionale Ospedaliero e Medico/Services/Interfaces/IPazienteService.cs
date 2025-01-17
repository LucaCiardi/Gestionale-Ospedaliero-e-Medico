using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Services.Interfaces
{
    public interface IPazienteService
    {
        Task<IEnumerable<Paziente>> GetFilteredPazienti(PazienteFilterModel filter);
        Task<IEnumerable<Paziente>> GetAllPazienti();
        Task<Paziente?> GetPazienteById(int id);
        Task<Paziente> CreatePaziente(Paziente paziente);
        Task UpdatePaziente(Paziente paziente);
        Task DeletePaziente(int id);
        Task<IEnumerable<Paziente>> GetPazientiByMedico(int medicoId);
        Task<Paziente?> GetPazienteByCodiceFiscale(string codiceFiscale);
    }
}
