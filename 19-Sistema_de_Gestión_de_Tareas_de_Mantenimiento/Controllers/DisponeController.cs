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
    public class DisponeController : Controller
    {
        private readonly SistemaGestionContext _context;

        public DisponeController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: Dispone
        public async Task<IActionResult> Index()
        {
            var sistemaGestionContext = _context.Dispones.Include(d => d.Programacion).Include(d => d.Tarea);
            return View(await sistemaGestionContext.ToListAsync());
        }

        // GET: Dispone/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispone = await _context.Dispones
                .Include(d => d.Programacion)
                .Include(d => d.Tarea)
                .FirstOrDefaultAsync(m => m.TareaId == id);
            if (dispone == null)
            {
                return NotFound();
            }

            return View(dispone);
        }

        // GET: Dispone/Create
        public IActionResult Create()
        {
            ViewData["ProgramacionId"] = new SelectList(_context.Programaciones, "ProgramacionId", "ProgramacionId");
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId");
            return View();
        }

        // POST: Dispone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TareaId,ProgramacionId")] Dispone dispone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dispone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramacionId"] = new SelectList(_context.Programaciones, "ProgramacionId", "ProgramacionId", dispone.ProgramacionId);
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId", dispone.TareaId);
            return View(dispone);
        }

        // GET: Dispone/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispone = await _context.Dispones.FindAsync(id);
            if (dispone == null)
            {
                return NotFound();
            }
            ViewData["ProgramacionId"] = new SelectList(_context.Programaciones, "ProgramacionId", "ProgramacionId", dispone.ProgramacionId);
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId", dispone.TareaId);
            return View(dispone);
        }

        // POST: Dispone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TareaId,ProgramacionId")] Dispone dispone)
        {
            if (id != dispone.TareaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DisponeExists(dispone.TareaId))
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
            ViewData["ProgramacionId"] = new SelectList(_context.Programaciones, "ProgramacionId", "ProgramacionId", dispone.ProgramacionId);
            ViewData["TareaId"] = new SelectList(_context.Tareas, "TareaId", "TareaId", dispone.TareaId);
            return View(dispone);
        }

        // GET: Dispone/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispone = await _context.Dispones
                .Include(d => d.Programacion)
                .Include(d => d.Tarea)
                .FirstOrDefaultAsync(m => m.TareaId == id);
            if (dispone == null)
            {
                return NotFound();
            }

            return View(dispone);
        }

        // POST: Dispone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dispone = await _context.Dispones.FindAsync(id);
            if (dispone != null)
            {
                _context.Dispones.Remove(dispone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DisponeExists(int id)
        {
            return _context.Dispones.Any(e => e.TareaId == id);
        }
    }
}
