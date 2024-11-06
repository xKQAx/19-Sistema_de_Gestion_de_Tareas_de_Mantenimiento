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
    public class HistorialMantenimientoController : Controller
    {
        private readonly SistemaGestionContext _context;

        public HistorialMantenimientoController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: HistorialMantenimiento
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.HistorialMantenimientos.Include(h => h.Equipo).Include(h => h.Personal);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: HistorialMantenimiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMantenimiento = await _context.HistorialMantenimientos
                .Include(h => h.Equipo)
                .Include(h => h.Personal)
                .FirstOrDefaultAsync(m => m.HistorialId == id);
            if (historialMantenimiento == null)
            {
                return NotFound();
            }

            return View(historialMantenimiento);
        }

        // GET: HistorialMantenimiento/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId");
            return View();
        }

        // POST: HistorialMantenimiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistorialId,EquipoId,FechaMantenimiento,Descripcion,PersonalId")] HistorialMantenimiento historialMantenimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialMantenimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", historialMantenimiento.EquipoId);
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", historialMantenimiento.PersonalId);
            return View(historialMantenimiento);
        }

        // GET: HistorialMantenimiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMantenimiento = await _context.HistorialMantenimientos.FindAsync(id);
            if (historialMantenimiento == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", historialMantenimiento.EquipoId);
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", historialMantenimiento.PersonalId);
            return View(historialMantenimiento);
        }

        // POST: HistorialMantenimiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistorialId,EquipoId,FechaMantenimiento,Descripcion,PersonalId")] HistorialMantenimiento historialMantenimiento)
        {
            if (id != historialMantenimiento.HistorialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialMantenimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialMantenimientoExists(historialMantenimiento.HistorialId))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", historialMantenimiento.EquipoId);
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", historialMantenimiento.PersonalId);
            return View(historialMantenimiento);
        }

        // GET: HistorialMantenimiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialMantenimiento = await _context.HistorialMantenimientos
                .Include(h => h.Equipo)
                .Include(h => h.Personal)
                .FirstOrDefaultAsync(m => m.HistorialId == id);
            if (historialMantenimiento == null)
            {
                return NotFound();
            }

            return View(historialMantenimiento);
        }

        // POST: HistorialMantenimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialMantenimiento = await _context.HistorialMantenimientos.FindAsync(id);
            if (historialMantenimiento != null)
            {
                _context.HistorialMantenimientos.Remove(historialMantenimiento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialMantenimientoExists(int id)
        {
            return _context.HistorialMantenimientos.Any(e => e.HistorialId == id);
        }
    }
}
