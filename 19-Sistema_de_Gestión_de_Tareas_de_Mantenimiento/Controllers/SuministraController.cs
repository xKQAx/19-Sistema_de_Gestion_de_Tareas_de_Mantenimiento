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
    [Authorize(Roles = "Admin, JefeRH")]
    public class SuministraController : Controller
    {
        private readonly SistemaGestionContext _context;

        public SuministraController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: Suministra
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.Suministras.Include(s => s.Proveedor).Include(s => s.Repuesto);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: Suministra/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministra = await _context.Suministras
                .Include(s => s.Proveedor)
                .Include(s => s.Repuesto)
                .FirstOrDefaultAsync(m => m.ProveedorId == id);
            if (suministra == null)
            {
                return NotFound();
            }

            return View(suministra);
        }

        // GET: Suministra/Create
        public IActionResult Create()
        {
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "ProveedorId");
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId");
            return View();
        }

        // POST: Suministra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProveedorId,RepuestoId")] Suministra suministra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suministra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "ProveedorId", suministra.ProveedorId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId", suministra.RepuestoId);
            return View(suministra);
        }

        // GET: Suministra/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministra = await _context.Suministras.FindAsync(id);
            if (suministra == null)
            {
                return NotFound();
            }
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "ProveedorId", suministra.ProveedorId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId", suministra.RepuestoId);
            return View(suministra);
        }

        // POST: Suministra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProveedorId,RepuestoId")] Suministra suministra)
        {
            if (id != suministra.ProveedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suministra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuministraExists(suministra.ProveedorId))
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
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "ProveedorId", "ProveedorId", suministra.ProveedorId);
            ViewData["RepuestoId"] = new SelectList(_context.Repuestos, "RepuestoId", "RepuestoId", suministra.RepuestoId);
            return View(suministra);
        }

        // GET: Suministra/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suministra = await _context.Suministras
                .Include(s => s.Proveedor)
                .Include(s => s.Repuesto)
                .FirstOrDefaultAsync(m => m.ProveedorId == id);
            if (suministra == null)
            {
                return NotFound();
            }

            return View(suministra);
        }

        // POST: Suministra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suministra = await _context.Suministras.FindAsync(id);
            if (suministra != null)
            {
                _context.Suministras.Remove(suministra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuministraExists(int id)
        {
            return _context.Suministras.Any(e => e.ProveedorId == id);
        }
    }
}
