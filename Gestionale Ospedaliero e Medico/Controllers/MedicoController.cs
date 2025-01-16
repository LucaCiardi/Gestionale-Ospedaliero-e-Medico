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

        // GET: Medico
        public async Task<IActionResult> Index()
        {
            var medici = await _medicoService.GetAllMedici();
            return View(medici);
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var medico = await _medicoService.GetMedicoById(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // GET: Medico/Create
        public async Task<IActionResult> Create()
        {
            var ospedali = await _ospedaleService.GetAllOspedali();
            ViewBag.Ospedali = new SelectList(ospedali, "Id", "Nome");
            return View();
        }

        // POST: Medico/Create
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

        // GET: Medico/Edit/5
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

        // POST: Medico/Edit/5
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

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var medico = await _medicoService.GetMedicoById(id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _medicoService.DeleteMedico(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Medico/ByReparto
        public async Task<IActionResult> ByReparto(string reparto)
        {
            var medici = await _medicoService.GetMediciByReparto(reparto);
            return View("Index", medici);
        }
    }
}
