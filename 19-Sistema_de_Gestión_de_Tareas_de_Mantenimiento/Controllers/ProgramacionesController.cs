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
    [Authorize(Roles = "Admin, Jefe Mantenimiento")]
    public class ProgramacionesController : Controller
    {
        private readonly SistemaGestionContext _context;

        public ProgramacionesController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: Programaciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.Programaciones.ToListAsync());
        }

        // GET: Programaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programacione = await _context.Programaciones
                .FirstOrDefaultAsync(m => m.ProgramacionId == id);
            if (programacione == null)
            {
                return NotFound();
            }

            return View(programacione);
        }

        // GET: Programaciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Programaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramacionId,FechaProgramada,Estado")] Programacione programacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(programacione);
        }

        // GET: Programaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programacione = await _context.Programaciones.FindAsync(id);
            if (programacione == null)
            {
                return NotFound();
            }
            return View(programacione);
        }

        // POST: Programaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramacionId,FechaProgramada,Estado")] Programacione programacione)
        {
            if (id != programacione.ProgramacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramacioneExists(programacione.ProgramacionId))
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
            return View(programacione);
        }

        // GET: Programaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programacione = await _context.Programaciones
                .FirstOrDefaultAsync(m => m.ProgramacionId == id);
            if (programacione == null)
            {
                return NotFound();
            }

            return View(programacione);
        }

        // POST: Programaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programacione = await _context.Programaciones.FindAsync(id);
            if (programacione != null)
            {
                _context.Programaciones.Remove(programacione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramacioneExists(int id)
        {
            return _context.Programaciones.Any(e => e.ProgramacionId == id);
        }
    }
}
