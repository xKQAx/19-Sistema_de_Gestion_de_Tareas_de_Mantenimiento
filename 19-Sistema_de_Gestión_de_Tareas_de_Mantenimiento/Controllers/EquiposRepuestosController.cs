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
    public class EquiposRepuestosController : Controller
    {
        private readonly SistemaGestionContext _context;

        public EquiposRepuestosController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: EquiposRepuestos
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.EquiposRepuestos.Include(e => e.Equipo).Include(e => e.Repuesto);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: EquiposRepuestos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equiposRepuesto = await _context.EquiposRepuestos
                .Include(e => e.Equipo)
                .Include(e => e.Repuesto)
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (equiposRepuesto == null)
            {
                return NotFound();
            }

            return View(equiposRepuesto);
        }

        // GET: EquiposRepuestos/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId");
            return View();
        }

        // POST: EquiposRepuestos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipoId,RepuestoId,Cantidad")] EquiposRepuesto equiposRepuesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equiposRepuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", equiposRepuesto.EquipoId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId", equiposRepuesto.RepuestoId);
            return View(equiposRepuesto);
        }

        // GET: EquiposRepuestos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equiposRepuesto = await _context.EquiposRepuestos.FindAsync(id);
            if (equiposRepuesto == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", equiposRepuesto.EquipoId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId", equiposRepuesto.RepuestoId);
            return View(equiposRepuesto);
        }

        // POST: EquiposRepuestos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipoId,RepuestoId,Cantidad")] EquiposRepuesto equiposRepuesto)
        {
            if (id != equiposRepuesto.EquipoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equiposRepuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquiposRepuestoExists(equiposRepuesto.EquipoId))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", equiposRepuesto.EquipoId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId", equiposRepuesto.RepuestoId);
            return View(equiposRepuesto);
        }

        // GET: EquiposRepuestos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equiposRepuesto = await _context.EquiposRepuestos
                .Include(e => e.Equipo)
                .Include(e => e.Repuesto)
                .FirstOrDefaultAsync(m => m.EquipoId == id);
            if (equiposRepuesto == null)
            {
                return NotFound();
            }

            return View(equiposRepuesto);
        }

        // POST: EquiposRepuestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equiposRepuesto = await _context.EquiposRepuestos.FindAsync(id);
            if (equiposRepuesto != null)
            {
                _context.EquiposRepuestos.Remove(equiposRepuesto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquiposRepuestoExists(int id)
        {
            return _context.EquiposRepuestos.Any(e => e.EquipoId == id);
        }
    }
}
