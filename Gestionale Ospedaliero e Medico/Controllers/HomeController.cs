using Microsoft.AspNetCore.Mvc;
using Gestionale_Ospedaliero_e_Medico.Models;
using System.Diagnostics;

namespace Gestionale_Ospedaliero_e_Medico.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
