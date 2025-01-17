using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Services.Interfaces
{
    public interface IMedicoService
    {
        Task<IEnumerable<Medico>> GetFilteredMedici(MedicoFilterModel filter);
        Task<IEnumerable<Medico>> GetAllMedici();
        Task<Medico?> GetMedicoById(int id);
        Task<Medico> CreateMedico(Medico medico);
        Task UpdateMedico(Medico medico);
        Task DeleteMedico(int id);
        Task<IEnumerable<Medico>> GetMediciByOspedale(int ospedaleId);
        Task<IEnumerable<Medico>> GetMediciByReparto(string reparto);
    }
}
