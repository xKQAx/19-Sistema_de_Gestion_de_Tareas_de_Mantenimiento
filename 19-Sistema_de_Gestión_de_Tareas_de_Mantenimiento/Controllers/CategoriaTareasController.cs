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
    public class CategoriaTareasController : Controller
    {
        private readonly SistemaGestionContext _context;

        public CategoriaTareasController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: CategoriaTareas
        public async Task<IActionResult> Index()
        {
            return View(await _context.CategoriaTareas.ToListAsync());
        }

        // GET: CategoriaTareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaTarea = await _context.CategoriaTareas
                .FirstOrDefaultAsync(m => m.CategoriaTareasId == id);
            if (categoriaTarea == null)
            {
                return NotFound();
            }

            return View(categoriaTarea);
        }

        // GET: CategoriaTareas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriaTareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaTareasId,NombreCategoria")] CategoriaTarea categoriaTarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaTarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaTarea);
        }

        // GET: CategoriaTareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaTarea = await _context.CategoriaTareas.FindAsync(id);
            if (categoriaTarea == null)
            {
                return NotFound();
            }
            return View(categoriaTarea);
        }

        // POST: CategoriaTareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaTareasId,NombreCategoria")] CategoriaTarea categoriaTarea)
        {
            if (id != categoriaTarea.CategoriaTareasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaTarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaTareaExists(categoriaTarea.CategoriaTareasId))
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
            return View(categoriaTarea);
        }

        // GET: CategoriaTareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaTarea = await _context.CategoriaTareas
                .FirstOrDefaultAsync(m => m.CategoriaTareasId == id);
            if (categoriaTarea == null)
            {
                return NotFound();
            }

            return View(categoriaTarea);
        }

        // POST: CategoriaTareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoriaTarea = await _context.CategoriaTareas.FindAsync(id);
            if (categoriaTarea != null)
            {
                _context.CategoriaTareas.Remove(categoriaTarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaTareaExists(int id)
        {
            return _context.CategoriaTareas.Any(e => e.CategoriaTareasId == id);
        }
    }
}
