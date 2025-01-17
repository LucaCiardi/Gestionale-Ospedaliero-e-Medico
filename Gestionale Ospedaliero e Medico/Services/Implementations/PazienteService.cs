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
        public async Task<IEnumerable<Paziente>> GetFilteredPazienti(PazienteFilterModel filter)
        {
            var query = _context.Pazienti
                .Include(p => p.Medico)
                .ThenInclude(m => m.Ospedale)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(p => p.Nome.Contains(filter.SearchTerm) ||
                                       p.Cognome.Contains(filter.SearchTerm) ||
                                       p.CodiceFiscale.Contains(filter.SearchTerm));
            }

            if (filter.MedicoId.HasValue)
            {
                query = query.Where(p => p.MedicoId == filter.MedicoId);
            }

            if (!string.IsNullOrEmpty(filter.Reparto))
            {
                query = query.Where(p => p.Medico.Reparto == filter.Reparto);
            }

            if (filter.DataRicoveroInizio.HasValue)
            {
                query = query.Where(p => p.DataRicovero >= filter.DataRicoveroInizio);
            }

            if (filter.DataRicoveroFine.HasValue)
            {
                query = query.Where(p => p.DataRicovero <= filter.DataRicoveroFine);
            }

            // Apply sorting
            query = filter.SortBy?.ToLower() switch
            {
                "nome" => filter.SortAscending ?
                    query.OrderBy(p => p.Nome) :
                    query.OrderByDescending(p => p.Nome),
                "cognome" => filter.SortAscending ?
                    query.OrderBy(p => p.Cognome) :
                    query.OrderByDescending(p => p.Cognome),
                "dataricovero" => filter.SortAscending ?
                    query.OrderBy(p => p.DataRicovero) :
                    query.OrderByDescending(p => p.DataRicovero),
                "medico" => filter.SortAscending ?
                    query.OrderBy(p => p.Medico.Cognome) :
                    query.OrderByDescending(p => p.Medico.Cognome),
                _ => query.OrderBy(p => p.Cognome)
            };

            return await query.ToListAsync();
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
