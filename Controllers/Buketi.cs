using Flor.Data;
using Flor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Flor.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BuketiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuketiController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [AllowAnonymous] 
        public IActionResult Index()
        {
            var buketi = _context.Buketi.ToList();
            return View(buketi);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Buket buket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buket);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var buket = await _context.Buketi.FindAsync(id);
            if (buket == null)
                return NotFound();

            return View(buket);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Buket buket)
        {
            if (id != buket.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Buketi.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(buket);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var buket = await _context.Buketi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buket == null)
                return NotFound();

            return View(buket);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buket = await _context.Buketi.FindAsync(id);
            _context.Buketi.Remove(buket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
