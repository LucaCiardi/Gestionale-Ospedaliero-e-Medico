using Microsoft.EntityFrameworkCore;
using Gestionale_Ospedaliero_e_Medico.Data;
using Gestionale_Ospedaliero_e_Medico.Models;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;

namespace Gestionale_Ospedaliero_e_Medico.Services.Implementations
{
    public class PazienteService : IPazienteService
    {
        private readonly OspedaleDbContext _context;

        public PazienteService(OspedaleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paziente>> GetAllPazienti()
        {
            return await _context.Pazienti
                .Include(p => p.Medico)
                .ThenInclude(m => m.Ospedale)
                .ToListAsync();
        }

        public async Task<Paziente?> GetPazienteById(int id)
        {
            return await _context.Pazienti
                .Include(p => p.Medico)
                .ThenInclude(m => m.Ospedale)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Paziente> CreatePaziente(Paziente paziente)
        {
            _context.Pazienti.Add(paziente);
            await _context.SaveChangesAsync();
            return paziente;
        }

        public async Task UpdatePaziente(Paziente paziente)
        {
            _context.Entry(paziente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaziente(int id)
        {
            var paziente = await _context.Pazienti.FindAsync(id);
            if (paziente != null)
            {
                _context.Pazienti.Remove(paziente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Paziente>> GetPazientiByMedico(int medicoId)
        {
            return await _context.Pazienti
                .Include(p => p.Medico)
                .ThenInclude(m => m.Ospedale)
                .Where(p => p.MedicoId == medicoId)
                .ToListAsync();
        }

        public async Task<Paziente?> GetPazienteByCodiceFiscale(string codiceFiscale)
        {
            return await _context.Pazienti
                .Include(p => p.Medico)
                .ThenInclude(m => m.Ospedale)
                .FirstOrDefaultAsync(p => p.CodiceFiscale == codiceFiscale);
        }
    }
}
