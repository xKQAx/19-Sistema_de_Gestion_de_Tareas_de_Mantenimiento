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
    public class TipoPersonalController : Controller
    {
        private readonly SistemaGestionContext _context;

        public TipoPersonalController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: TipoPersonal
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoPersonals.ToListAsync());
        }

        // GET: TipoPersonal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPersonal = await _context.TipoPersonals
                .FirstOrDefaultAsync(m => m.TipoPersonalId == id);
            if (tipoPersonal == null)
            {
                return NotFound();
            }

            return View(tipoPersonal);
        }

        // GET: TipoPersonal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoPersonal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoPersonalId,TipoPersonal1")] TipoPersonal tipoPersonal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPersonal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPersonal);
        }

        // GET: TipoPersonal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPersonal = await _context.TipoPersonals.FindAsync(id);
            if (tipoPersonal == null)
            {
                return NotFound();
            }
            return View(tipoPersonal);
        }

        // POST: TipoPersonal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoPersonalId,TipoPersonal1")] TipoPersonal tipoPersonal)
        {
            if (id != tipoPersonal.TipoPersonalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPersonal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPersonalExists(tipoPersonal.TipoPersonalId))
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
            return View(tipoPersonal);
        }

        // GET: TipoPersonal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoPersonal = await _context.TipoPersonals
                .FirstOrDefaultAsync(m => m.TipoPersonalId == id);
            if (tipoPersonal == null)
            {
                return NotFound();
            }

            return View(tipoPersonal);
        }

        // POST: TipoPersonal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoPersonal = await _context.TipoPersonals.FindAsync(id);
            if (tipoPersonal != null)
            {
                _context.TipoPersonals.Remove(tipoPersonal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPersonalExists(int id)
        {
            return _context.TipoPersonals.Any(e => e.TipoPersonalId == id);
        }
    }
}
