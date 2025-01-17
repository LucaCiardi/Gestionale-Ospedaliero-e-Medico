using Microsoft.EntityFrameworkCore;
using Gestionale_Ospedaliero_e_Medico.Data;
using Gestionale_Ospedaliero_e_Medico.Models;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;

namespace Gestionale_Ospedaliero_e_Medico.Services.Implementations
{
    public class OspedaleService : IOspedaleService
    {
        private readonly OspedaleDbContext _context;

        public OspedaleService(OspedaleDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ospedale>> GetFilteredOspedali(OspedaleFilterModel filter)
        {
            var query = _context.Ospedali
                .Include(o => o.Medici)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(o => o.Nome.Contains(filter.SearchTerm));
            }

            if (!string.IsNullOrEmpty(filter.Sede))
            {
                query = query.Where(o => o.Sede == filter.Sede);
            }

            if (filter.Pubblico.HasValue)
            {
                query = query.Where(o => o.Pubblico == filter.Pubblico);
            }

            // Apply sorting
            query = filter.SortBy?.ToLower() switch
            {
                "nome" => filter.SortAscending ?
                    query.OrderBy(o => o.Nome) :
                    query.OrderByDescending(o => o.Nome),
                "sede" => filter.SortAscending ?
                    query.OrderBy(o => o.Sede) :
                    query.OrderByDescending(o => o.Sede),
                "tipo" => filter.SortAscending ?
                    query.OrderBy(o => o.Pubblico) :
                    query.OrderByDescending(o => o.Pubblico),
                "medici" => filter.SortAscending ?
                    query.OrderBy(o => o.Medici.Count) :
                    query.OrderByDescending(o => o.Medici.Count),
                _ => query.OrderBy(o => o.Nome)
            };

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Ospedale>> GetAllOspedali()
        {
            return await _context.Ospedali
                .Include(o => o.Medici)
                .ToListAsync();
        }

        public async Task<Ospedale?> GetOspedaleById(int id)
        {
            return await _context.Ospedali
                .Include(o => o.Medici)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Ospedale> CreateOspedale(Ospedale ospedale)
        {
            _context.Ospedali.Add(ospedale);
            await _context.SaveChangesAsync();
            return ospedale;
        }

        public async Task UpdateOspedale(Ospedale ospedale)
        {
            _context.Entry(ospedale).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOspedale(int id)
        {
            var ospedale = await _context.Ospedali.FindAsync(id);
            if (ospedale != null)
            {
                _context.Ospedali.Remove(ospedale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ospedale>> GetOspedaliWithMedici()
        {
            return await _context.Ospedali
                .Include(o => o.Medici)
                .ThenInclude(m => m.Pazienti)
                .ToListAsync();
        }
    }
}
