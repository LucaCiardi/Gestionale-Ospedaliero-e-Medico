using Microsoft.EntityFrameworkCore;
using Gestionale_Ospedaliero_e_Medico.Data;
using Gestionale_Ospedaliero_e_Medico.Models;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;

namespace Gestionale_Ospedaliero_e_Medico.Services.Implementations
{
    public class MedicoService : IMedicoService
    {
        private readonly OspedaleDbContext _context;

        public MedicoService(OspedaleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medico>> GetAllMedici()
        {
            return await _context.Medici
                .Include(m => m.Ospedale)
                .Include(m => m.Pazienti)
                .ToListAsync();
        }

        public async Task<Medico?> GetMedicoById(int id)
        {
            return await _context.Medici
                .Include(m => m.Ospedale)
                .Include(m => m.Pazienti)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Medico> CreateMedico(Medico medico)
        {
            _context.Medici.Add(medico);
            await _context.SaveChangesAsync();
            return medico;
        }

        public async Task UpdateMedico(Medico medico)
        {
            _context.Entry(medico).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedico(int id)
        {
            var medico = await _context.Medici.FindAsync(id);
            if (medico != null)
            {
                _context.Medici.Remove(medico);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Medico>> GetMediciByOspedale(int ospedaleId)
        {
            return await _context.Medici
                .Include(m => m.Pazienti)
                .Where(m => m.OspedaleId == ospedaleId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Medico>> GetMediciByReparto(string reparto)
        {
            return await _context.Medici
                .Include(m => m.Ospedale)
                .Include(m => m.Pazienti)
                .Where(m => m.Reparto == reparto)
                .ToListAsync();
        }
    }
}
