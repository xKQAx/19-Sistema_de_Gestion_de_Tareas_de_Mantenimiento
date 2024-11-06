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
    public class CategoriaEquiposController : Controller
    {
        private readonly SistemaGestionContext _context;

        public CategoriaEquiposController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: CategoriaEquipos
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.CategoriaEquipos.Include(c => c.Equipo);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: CategoriaEquipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEquipo = await _context.CategoriaEquipos
                .Include(c => c.Equipo)
                .FirstOrDefaultAsync(m => m.CategoriaEquiposId == id);
            if (categoriaEquipo == null)
            {
                return NotFound();
            }

            return View(categoriaEquipo);
        }

        // GET: CategoriaEquipos/Create
        public IActionResult Create()
        {
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId");
            return View();
        }

        // POST: CategoriaEquipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaEquiposId,NombreCategoria,EquipoId")] CategoriaEquipo categoriaEquipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaEquipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", categoriaEquipo.EquipoId);
            return View(categoriaEquipo);
        }

        // GET: CategoriaEquipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEquipo = await _context.CategoriaEquipos.FindAsync(id);
            if (categoriaEquipo == null)
            {
                return NotFound();
            }
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", categoriaEquipo.EquipoId);
            return View(categoriaEquipo);
        }

        // POST: CategoriaEquipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaEquiposId,NombreCategoria,EquipoId")] CategoriaEquipo categoriaEquipo)
        {
            if (id != categoriaEquipo.CategoriaEquiposId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaEquipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaEquipoExists(categoriaEquipo.CategoriaEquiposId))
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
            ViewData["EquipoId"] = new SelectList(_context.Equipos, "EquipoId", "EquipoId", categoriaEquipo.EquipoId);
            return View(categoriaEquipo);
        }

        // GET: CategoriaEquipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEquipo = await _context.CategoriaEquipos
                .Include(c => c.Equipo)
                .FirstOrDefaultAsync(m => m.CategoriaEquiposId == id);
            if (categoriaEquipo == null)
            {
                return NotFound();
            }

            return View(categoriaEquipo);
        }

        // POST: CategoriaEquipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaEquipo = await _context.CategoriaEquipos.FindAsync(id);
            if (categoriaEquipo != null)
            {
                _context.CategoriaEquipos.Remove(categoriaEquipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaEquipoExists(int id)
        {
            return _context.CategoriaEquipos.Any(e => e.CategoriaEquiposId == id);
        }
    }
}
