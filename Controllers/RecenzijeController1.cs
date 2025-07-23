using Microsoft.AspNetCore.Mvc;
using Flor.Data;
using Flor.Models;
using System;
using System.Linq;

namespace Flor.Controllers
{
    public class RecenzijeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecenzijeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Kreiraj()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Kreiraj(Recenzija recenzija)
        {
            if (ModelState.IsValid)
            {
                recenzija.Odobrena = false;
                recenzija.Datum = DateTime.Now;

                _context.Recenzije.Add(recenzija);
                _context.SaveChanges();

                ViewBag.Poruka = "Hvala na recenziji! Bit će prikazana nakon odobrenja.";
                ModelState.Clear();
                return View();
            }

            return View(recenzija);
        }

        public IActionResult Index()
        {
            var odobrene = _context.Recenzije
                .Where(r => r.Odobrena)
                .OrderByDescending(r => r.Datum)
                .ToList();

            return View(odobrene);
        }

        public IActionResult Admin()
        {
            var sveRecenzije = _context.Recenzije
                .OrderByDescending(r => r.Datum)
                .ToList();

            return View(sveRecenzije);
        }

        [HttpPost]
        public IActionResult Odobri(int id)
        {
            var recenzija = _context.Recenzije.FirstOrDefault(r => r.Id == id);
            if (recenzija != null)
            {
                recenzija.Odobrena = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Admin");
        }
    }
}
