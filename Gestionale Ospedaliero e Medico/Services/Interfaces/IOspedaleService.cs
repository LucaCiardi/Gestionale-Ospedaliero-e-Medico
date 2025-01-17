using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Services.Interfaces
{
    public interface IOspedaleService
    {
        Task<IEnumerable<Ospedale>> GetFilteredOspedali(OspedaleFilterModel filter);
        Task<IEnumerable<Ospedale>> GetAllOspedali();
        Task<Ospedale?> GetOspedaleById(int id);
        Task<Ospedale> CreateOspedale(Ospedale ospedale);
        Task UpdateOspedale(Ospedale ospedale);
        Task DeleteOspedale(int id);
        Task<IEnumerable<Ospedale>> GetOspedaliWithMedici();
    }
}
