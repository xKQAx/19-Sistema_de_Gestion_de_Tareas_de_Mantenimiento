using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Data;
using _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;
using Microsoft.AspNetCore.Authorization;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PuedeSerController : Controller
    {
        private readonly SistemaGestionContext _context;

        public PuedeSerController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: PuedeSer
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.PuedeSers.Include(p => p.Personal).Include(p => p.TipoPersonal);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: PuedeSer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puedeSer = await _context.PuedeSers
                .Include(p => p.Personal)
                .Include(p => p.TipoPersonal)
                .FirstOrDefaultAsync(m => m.PersonalId == id);
            if (puedeSer == null)
            {
                return NotFound();
            }

            return View(puedeSer);
        }

        // GET: PuedeSer/Create
        public IActionResult Create()
        {
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId");
            ViewData["TipoPersonalId"] = new SelectList(_context.TipoPersonals, "TipoPersonalId", "TipoPersonalId");
            return View();
        }

        // POST: PuedeSer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalId,TipoPersonalId")] PuedeSer puedeSer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puedeSer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", puedeSer.PersonalId);
            ViewData["TipoPersonalId"] = new SelectList(_context.TipoPersonals, "TipoPersonalId", "TipoPersonalId", puedeSer.TipoPersonalId);
            return View(puedeSer);
        }

        // GET: PuedeSer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puedeSer = await _context.PuedeSers.FindAsync(id);
            if (puedeSer == null)
            {
                return NotFound();
            }
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", puedeSer.PersonalId);
            ViewData["TipoPersonalId"] = new SelectList(_context.TipoPersonals, "TipoPersonalId", "TipoPersonalId", puedeSer.TipoPersonalId);
            return View(puedeSer);
        }

        // POST: PuedeSer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonalId,TipoPersonalId")] PuedeSer puedeSer)
        {
            if (id != puedeSer.PersonalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puedeSer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PuedeSerExists(puedeSer.PersonalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", puedeSer.PersonalId);
            ViewData["TipoPersonalId"] = new SelectList(_context.TipoPersonals, "TipoPersonalId", "TipoPersonalId", puedeSer.TipoPersonalId);
            return View(puedeSer);
        }

        // GET: PuedeSer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var puedeSer = await _context.PuedeSers
                .Include(p => p.Personal)
                .Include(p => p.TipoPersonal)
                .FirstOrDefaultAsync(m => m.PersonalId == id);
            if (puedeSer == null)
            {
                return NotFound();
            }

            return View(puedeSer);
        }

        // POST: PuedeSer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var puedeSer = await _context.PuedeSers.FindAsync(id);
            if (puedeSer != null)
            {
                _context.PuedeSers.Remove(puedeSer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PuedeSerExists(int id)
        {
            return _context.PuedeSers.Any(e => e.PersonalId == id);
        }
    }
}
