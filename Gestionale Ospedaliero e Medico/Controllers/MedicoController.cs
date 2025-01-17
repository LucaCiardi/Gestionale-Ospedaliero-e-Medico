using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;
using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoService _medicoService;
        private readonly IOspedaleService _ospedaleService;

        public MedicoController(IMedicoService medicoService, IOspedaleService ospedaleService)
        {
            _medicoService = medicoService;
            _ospedaleService = ospedaleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] MedicoFilterModel? filter)
        {
            filter ??= new MedicoFilterModel();

            var ospedali = await _ospedaleService.GetAllOspedali();
            ViewBag.Ospedali = new SelectList(
                ospedali.Select(o => new
                {
                    Id = o.Id,
                    Nome = $"{o.Nome} ({o.Sede})"
                }),
                "Id",
                "Nome",
                filter.OspedaleId
            );

            var medici = await _medicoService.GetAllMedici();
            ViewBag.Reparti = new SelectList(
                medici.Select(m => m.Reparto).Distinct().OrderBy(r => r)
            );

            var filteredMedici = await _medicoService.GetFilteredMedici(filter);
            return View(filteredMedici);
        }

        public async Task<IActionResult> Details(int id)
        {
            var medico = await _medicoService.GetMedicoById(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        public async Task<IActionResult> Create()
        {
            var ospedali = await _ospedaleService.GetAllOspedali();
            ViewBag.Ospedali = new SelectList(ospedali, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cognome,Dob,Residenza,Reparto,Primario,PazientiGuariti,TotaleDecessi,OspedaleId")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                await _medicoService.CreateMedico(medico);
                return RedirectToAction(nameof(Index));
            }
            var ospedali = await _ospedaleService.GetAllOspedali();
            ViewBag.Ospedali = new SelectList(ospedali, "Id", "Nome", medico.OspedaleId);
            return View(medico);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medico = await _medicoService.GetMedicoById(id);
            if (medico == null)
            {
                return NotFound();
            }
            var ospedali = await _ospedaleService.GetAllOspedali();
            ViewBag.Ospedali = new SelectList(ospedali, "Id", "Nome", medico.OspedaleId);
            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cognome,Dob,Residenza,Reparto,Primario,PazientiGuariti,TotaleDecessi,OspedaleId")] Medico medico)
        {
            if (id != medico.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _medicoService.UpdateMedico(medico);
                return RedirectToAction(nameof(Index));
            }
            var ospedali = await _ospedaleService.GetAllOspedali();
            ViewBag.Ospedali = new SelectList(ospedali, "Id", "Nome", medico.OspedaleId);
            return View(medico);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var medico = await _medicoService.GetMedicoById(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _medicoService.DeleteMedico(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
