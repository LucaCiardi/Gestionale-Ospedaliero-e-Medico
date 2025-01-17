using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Gestionale_Ospedaliero_e_Medico.Services.Interfaces;
using Gestionale_Ospedaliero_e_Medico.Models;

namespace Gestionale_Ospedaliero_e_Medico.Controllers
{
    public class PazienteController : Controller
    {
        private readonly IPazienteService _pazienteService;
        private readonly IMedicoService _medicoService;

        public PazienteController(IPazienteService pazienteService, IMedicoService medicoService)
        {
            _pazienteService = pazienteService;
            _medicoService = medicoService;
        }

        // GET: Paziente
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] PazienteFilterModel? filter)
        {
            filter ??= new PazienteFilterModel();

            var medici = await _medicoService.GetAllMedici();
            ViewBag.Medici = new SelectList(medici
                .Select(m => new
                {
                    m.Id,
                    NomeCompleto = $"{m.Cognome} {m.Nome} - {m.Reparto}"
                }), "Id", "NomeCompleto", filter.MedicoId);

            var reparti = medici
                .Select(m => m.Reparto)
                .Distinct()
                .OrderBy(r => r)
                .ToList();
            ViewBag.Reparti = new SelectList(reparti, filter.Reparto);

            var pazienti = await _pazienteService.GetFilteredPazienti(filter);
            return View(pazienti);
        }


        // GET: Paziente/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var paziente = await _pazienteService.GetPazienteById(id);
            if (paziente == null)
            {
                return NotFound();
            }
            return View(paziente);
        }

        // GET: Paziente/Create
        public async Task<IActionResult> Create()
        {
            var medici = await _medicoService.GetAllMedici();
            ViewBag.Medici = new SelectList(medici, "Id", "Cognome");
            return View();
        }

        // POST: Paziente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Cognome,DataNascita,Residenza,CodiceFiscale,DataRicovero,MedicoId")] Paziente paziente)
        {
            if (ModelState.IsValid)
            {
                await _pazienteService.CreatePaziente(paziente);
                return RedirectToAction(nameof(Index));
            }
            var medici = await _medicoService.GetAllMedici();
            ViewBag.Medici = new SelectList(medici, "Id", "Cognome", paziente.MedicoId);
            return View(paziente);
        }

        // GET: Paziente/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var paziente = await _pazienteService.GetPazienteById(id);
            if (paziente == null)
            {
                return NotFound();
            }
            var medici = await _medicoService.GetAllMedici();
            ViewBag.Medici = new SelectList(medici, "Id", "Cognome", paziente.MedicoId);
            return View(paziente);
        }

        // POST: Paziente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cognome,DataNascita,Residenza,CodiceFiscale,DataRicovero,MedicoId")] Paziente paziente)
        {
            if (id != paziente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _pazienteService.UpdatePaziente(paziente);
                return RedirectToAction(nameof(Index));
            }
            var medici = await _medicoService.GetAllMedici();
            ViewBag.Medici = new SelectList(medici, "Id", "Cognome", paziente.MedicoId);
            return View(paziente);
        }

        // GET: Paziente/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var paziente = await _pazienteService.GetPazienteById(id);
            if (paziente == null)
            {
                return NotFound();
            }
            return View(paziente);
        }

        // POST: Paziente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pazienteService.DeletePaziente(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Paziente/ByMedico/5
        public async Task<IActionResult> ByMedico(int medicoId)
        {
            var filter = new PazienteFilterModel { MedicoId = medicoId };
            return RedirectToAction(nameof(Index), filter);
        }
    }
}
