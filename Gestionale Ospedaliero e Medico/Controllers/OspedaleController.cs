using Microsoft.AspNetCore.Mvc;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;
using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Controllers
{
    public class OspedaleController : Controller
    {
        private readonly IOspedaleService _ospedaleService;

        public OspedaleController(IOspedaleService ospedaleService)
        {
            _ospedaleService = ospedaleService;
        }

        // GET: Ospedale
        public async Task<IActionResult> Index()
        {
            var ospedali = await _ospedaleService.GetAllOspedali();
            return View(ospedali);
        }

        // GET: Ospedale/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ospedale = await _ospedaleService.GetOspedaleById(id);
            if (ospedale == null)
            {
                return NotFound();
            }
            return View(ospedale);
        }

        // GET: Ospedale/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ospedale/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sede,Pubblico")] Ospedale ospedale)
        {
            if (ModelState.IsValid)
            {
                await _ospedaleService.CreateOspedale(ospedale);
                return RedirectToAction(nameof(Index));
            }
            return View(ospedale);
        }

        // GET: Ospedale/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ospedale = await _ospedaleService.GetOspedaleById(id);
            if (ospedale == null)
            {
                return NotFound();
            }
            return View(ospedale);
        }

        // POST: Ospedale/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sede,Pubblico")] Ospedale ospedale)
        {
            if (id != ospedale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _ospedaleService.UpdateOspedale(ospedale);
                return RedirectToAction(nameof(Index));
            }
            return View(ospedale);
        }

        // GET: Ospedale/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ospedale = await _ospedaleService.GetOspedaleById(id);
            if (ospedale == null)
            {
                return NotFound();
            }
            return View(ospedale);
        }

        // POST: Ospedale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ospedaleService.DeleteOspedale(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
