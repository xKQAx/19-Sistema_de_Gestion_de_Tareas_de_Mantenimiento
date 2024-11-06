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
    public class TieneAsignadoController : Controller
    {
        private readonly SistemaGestionContext _context;

        public TieneAsignadoController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: TieneAsignado
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.TieneAsignados.Include(t => t.Equipo).Include(t => t.Tarea);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: TieneAsignado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tieneAsignado = await _context.TieneAsignados
                .Include(t => t.Equipo)
                .Include(t => t.Tarea)
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (tieneAsignado == null)
            {
                return NotFound();
            }

            return View(tieneAsignado);
        }

        // GET: TieneAsignado/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId");
            return View();
        }

        // POST: TieneAsignado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipoId,TareaId")] TieneAsignado tieneAsignado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tieneAsignado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", tieneAsignado.EquipoId);
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId", tieneAsignado.TareaId);
            return View(tieneAsignado);
        }

        // GET: TieneAsignado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tieneAsignado = await _context.TieneAsignados.FindAsync(id);
            if (tieneAsignado == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", tieneAsignado.EquipoId);
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId", tieneAsignado.TareaId);
            return View(tieneAsignado);
        }

        // POST: TieneAsignado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipoId,TareaId")] TieneAsignado tieneAsignado)
        {
            if (id != tieneAsignado.EquipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tieneAsignado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TieneAsignadoExists(tieneAsignado.EquipoId))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", tieneAsignado.EquipoId);
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId", tieneAsignado.TareaId);
            return View(tieneAsignado);
        }

        // GET: TieneAsignado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tieneAsignado = await _context.TieneAsignados
                .Include(t => t.Equipo)
                .Include(t => t.Tarea)
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (tieneAsignado == null)
            {
                return NotFound();
            }

            return View(tieneAsignado);
        }

        // POST: TieneAsignado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tieneAsignado = await _context.TieneAsignados.FindAsync(id);
            if (tieneAsignado != null)
            {
                _context.TieneAsignados.Remove(tieneAsignado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TieneAsignadoExists(int id)
        {
            return _context.TieneAsignados.Any(e => e.EquipoId == id);
        }
    }
}
