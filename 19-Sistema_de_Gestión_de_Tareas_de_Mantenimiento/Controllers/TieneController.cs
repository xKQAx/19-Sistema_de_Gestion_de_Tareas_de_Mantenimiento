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
    public class TieneController : Controller
    {
        private readonly SistemaGestionContext _context;

        public TieneController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: Tiene
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.Tienes.Include(t => t.Departamento).Include(t => t.Personal);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: Tiene/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiene = await _context.Tienes
                .Include(t => t.Departamento)
                .Include(t => t.Personal)
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);
            if (tiene == null)
            {
                return NotFound();
            }

            return View(tiene);
        }

        // GET: Tiene/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "DepartamentoId");
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId");
            return View();
        }

        // POST: Tiene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoId,PersonalId")] Tiene tiene)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiene);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "DepartamentoId", tiene.DepartamentoId);
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", tiene.PersonalId);
            return View(tiene);
        }

        // GET: Tiene/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiene = await _context.Tienes.FindAsync(id);
            if (tiene == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "DepartamentoId", tiene.DepartamentoId);
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", tiene.PersonalId);
            return View(tiene);
        }

        // POST: Tiene/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartamentoId,PersonalId")] Tiene tiene)
        {
            if (id != tiene.DepartamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TieneExists(tiene.DepartamentoId))
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
            ViewData["DepartamentoId"] = new SelectList(_context.Departamentos, "DepartamentoId", "DepartamentoId", tiene.DepartamentoId);
            ViewData["PersonalId"] = new SelectList(_context.Personals, "PersonalId", "PersonalId", tiene.PersonalId);
            return View(tiene);
        }

        // GET: Tiene/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiene = await _context.Tienes
                .Include(t => t.Departamento)
                .Include(t => t.Personal)
                .FirstOrDefaultAsync(m => m.DepartamentoId == id);
            if (tiene == null)
            {
                return NotFound();
            }

            return View(tiene);
        }

        // POST: Tiene/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiene = await _context.Tienes.FindAsync(id);
            if (tiene != null)
            {
                _context.Tienes.Remove(tiene);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TieneExists(int id)
        {
            return _context.Tienes.Any(e => e.DepartamentoId == id);
        }
    }
}
