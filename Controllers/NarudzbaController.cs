using Flor.Data;
using Flor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flor.Controllers
{
    public class NarudzbaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private const string CartSessionKey = "Cart";

        public NarudzbaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private List<CartItem> GetCart()
        {
            var sessionCart = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(sessionCart))
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(sessionCart);
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
        }

        [HttpPost]
        public IActionResult AddToCart(int buketId, int kolicina, string velicina)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Poruka"] = "Za naručivanje buketa potrebno je biti prijavljen.";
                return RedirectToAction("Index", "Buketi");
            }

            var cart = GetCart();
            var existingItem = cart.Find(x => x.BuketId == buketId && x.Velicina == velicina);
            if (existingItem != null)
            {
                existingItem.Kolicina += kolicina;
            }
            else
            {
                var buket = _context.Buketi.FirstOrDefault(b => b.Id == buketId);
                if (buket != null)
                {
                    decimal cijena = velicina switch
                    {
                        "S" => buket.CijenaMali,
                        "M" => buket.CijenaSrednji,
                        "L" => buket.CijenaVeliki,
                        _ => 0
                    };

                    cart.Add(new CartItem
                    {
                        BuketId = buketId,
                        Naziv = buket.Naziv,
                        Velicina = velicina,
                        Kolicina = kolicina,
                        Cijena = cijena
                    });
                }
            }

            SaveCart(cart);
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int buketId, string velicina)
        {
            var cart = GetCart();
            var itemToRemove = cart.FirstOrDefault(x => x.BuketId == buketId && x.Velicina == velicina);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                SaveCart(cart);
            }
            return RedirectToAction("Cart");
        }

        [Authorize]
        public IActionResult Cart()
        {
            var cart = GetCart();
            return View(cart);
        }

        [Authorize]
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (cart == null || !cart.Any())
                return RedirectToAction("Index", "Buketi");

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Narudzba narudzba)
        {
            var cart = GetCart();
            narudzba.UserId = _userManager.GetUserId(User);

            if (cart == null || !cart.Any())
            {
                ModelState.AddModelError("", "Košarica je prazna.");
                return View(narudzba);
            }

            if (narudzba.DatumIsporuke < DateTime.Today)
                ModelState.AddModelError("DatumIsporuke", "Datum isporuke ne može biti u prošlosti.");

            if (narudzba.NacinDostave == "Adresa" && string.IsNullOrWhiteSpace(narudzba.Adresa))
                ModelState.AddModelError("Adresa", "Molimo unesite adresu dostave.");

            if (!ModelState.IsValid)
                return View(narudzba);

            decimal ukupno = 0;
            var stavke = new List<NarudzbaStavka>();

            foreach (var item in cart)
            {
                var buket = _context.Buketi.FirstOrDefault(b => b.Id == item.BuketId);
                if (buket == null) continue;

                decimal cijena = item.Velicina switch
                {
                    "S" => buket.CijenaMali,
                    "M" => buket.CijenaSrednji,
                    "L" => buket.CijenaVeliki,
                    _ => 0
                };

                stavke.Add(new NarudzbaStavka
                {
                    BuketId = item.BuketId,
                    Velicina = item.Velicina,
                    Kolicina = item.Kolicina,
                    Cijena = cijena,
                    Narudzba = narudzba
                });

                ukupno += cijena * item.Kolicina;
            }

            narudzba.UkupnaCijena = ukupno;
            narudzba.Stavke = stavke;

            _context.Narudzbe.Add(narudzba);
            _context.SaveChanges();

            SaveCart(new List<CartItem>());
            return RedirectToAction("Potvrda");
        }

        public IActionResult Potvrda()
        {
            var narudzba = _context.Narudzbe
                .OrderByDescending(n => n.Id)
                .FirstOrDefault();

            if (narudzba == null)
                return RedirectToAction("Index", "Buketi");

            narudzba.Stavke = _context.NarudzbaStavke
                .Where(s => s.NarudzbaId == narudzba.Id)
                .ToList();

            return View(narudzba);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SveNarudzbe()
        {
            var narudzbe = _context.Narudzbe
                .OrderBy(n => n.DatumIsporuke)
                .ToList();

            foreach (var narudzba in narudzbe)
            {
                narudzba.Stavke = _context.NarudzbaStavke
                    .Where(s => s.NarudzbaId == narudzba.Id)
                    .ToList();
            }

            return View(narudzbe);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Obrisi(int id)
        {
            var narudzba = _context.Narudzbe.FirstOrDefault(n => n.Id == id);
            if (narudzba != null)
            {
                var stavke = _context.NarudzbaStavke.Where(s => s.NarudzbaId == id).ToList();
                _context.NarudzbaStavke.RemoveRange(stavke);
                _context.Narudzbe.Remove(narudzba);
                _context.SaveChanges();
            }

            return RedirectToAction("SveNarudzbe");
        }

        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult OznaciKaoObavljeno(int id)
        {
            var narudzba = _context.Narudzbe.FirstOrDefault(n => n.Id == id);
            if (narudzba != null)
            {
                narudzba.Obavljeno = true;
                _context.SaveChanges();
            }

            return RedirectToAction("SveNarudzbe");
        }
    }
}
