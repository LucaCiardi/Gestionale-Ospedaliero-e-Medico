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
        public async Task<IEnumerable<Medico>> GetFilteredMedici(MedicoFilterModel filter)
        {
            var query = _context.Medici
                .Include(m => m.Ospedale)
                .Include(m => m.Pazienti)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(m => m.Nome.Contains(filter.SearchTerm) ||
                                       m.Cognome.Contains(filter.SearchTerm));
            }

            if (!string.IsNullOrEmpty(filter.Reparto))
            {
                query = query.Where(m => m.Reparto == filter.Reparto);
            }

            if (filter.Primario.HasValue)
            {
                query = query.Where(m => m.Primario == filter.Primario);
            }

            if (filter.OspedaleId.HasValue)
            {
                query = query.Where(m => m.OspedaleId == filter.OspedaleId);
            }

            // Apply sorting
            query = filter.SortBy?.ToLower() switch
            {
                "nome" => filter.SortAscending ?
                    query.OrderBy(m => m.Nome) :
                    query.OrderByDescending(m => m.Nome),
                "cognome" => filter.SortAscending ?
                    query.OrderBy(m => m.Cognome) :
                    query.OrderByDescending(m => m.Cognome),
                "reparto" => filter.SortAscending ?
                    query.OrderBy(m => m.Reparto) :
                    query.OrderByDescending(m => m.Reparto),
                "pazienti" => filter.SortAscending ?
                    query.OrderBy(m => m.Pazienti.Count) :
                    query.OrderByDescending(m => m.Pazienti.Count),
                _ => query.OrderBy(m => m.Cognome)
            };

            return await query.ToListAsync();
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
