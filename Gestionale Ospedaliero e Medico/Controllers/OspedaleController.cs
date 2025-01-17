using Microsoft.AspNetCore.Mvc;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;
using Gestionale_Ospedaliero_e_Medico.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gestionale_Ospedaliero_e_Medico.Controllers
{
    public class OspedaleController : Controller
    {
        private readonly IOspedaleService _ospedaleService;

        public OspedaleController(IOspedaleService ospedaleService)
        {
            _ospedaleService = ospedaleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] OspedaleFilterModel? filter)
        {
            filter ??= new OspedaleFilterModel();

            var ospedali = await _ospedaleService.GetAllOspedali();
            ViewBag.Sedi = new SelectList(
                ospedali.Select(o => o.Sede).Distinct().OrderBy(s => s)
            );

            var filteredOspedali = await _ospedaleService.GetFilteredOspedali(filter);
            return View(filteredOspedali);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ospedale = await _ospedaleService.GetOspedaleById(id);
            if (ospedale == null)
            {
                return NotFound();
            }
            return View(ospedale);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(int id)
        {
            var ospedale = await _ospedaleService.GetOspedaleById(id);
            if (ospedale == null)
            {
                return NotFound();
            }
            return View(ospedale);
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            var ospedale = await _ospedaleService.GetOspedaleById(id);
            if (ospedale == null)
            {
                return NotFound();
            }
            return View(ospedale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ospedaleService.DeleteOspedale(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
