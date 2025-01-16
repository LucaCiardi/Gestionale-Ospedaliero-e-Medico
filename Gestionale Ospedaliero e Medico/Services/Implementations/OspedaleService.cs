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
