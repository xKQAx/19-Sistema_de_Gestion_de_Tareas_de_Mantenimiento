﻿using System;
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
    public class AspNetRolesController : Controller
    {
        private readonly SistemaGestionContext _context;

        public AspNetRolesController(SistemaGestionContext context)
        {
            _context = context;
        }

        // GET: AspNetRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.AspNetRoles.ToListAsync());
        }

        // GET: AspNetRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRole = await _context.AspNetRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRole == null)
            {
                return NotFound();
            }

            return View(aspNetRole);
        }

        // GET: AspNetRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AspNetRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Seccion,FechaAlta,Name,NormalizedName,ConcurrencyStamp")] AspNetRole aspNetRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aspNetRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aspNetRole);
        }

        // GET: AspNetRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRole = await _context.AspNetRoles.FindAsync(id);
            if (aspNetRole == null)
            {
                return NotFound();
            }
            return View(aspNetRole);
        }

        // POST: AspNetRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Seccion,FechaAlta,Name,NormalizedName,ConcurrencyStamp")] AspNetRole aspNetRole)
        {
            if (id != aspNetRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aspNetRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetRoleExists(aspNetRole.Id))
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
            return View(aspNetRole);
        }

        // GET: AspNetRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetRole = await _context.AspNetRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRole == null)
            {
                return NotFound();
            }

            return View(aspNetRole);
        }

        // POST: AspNetRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetRole = await _context.AspNetRoles.FindAsync(id);
            if (aspNetRole != null)
            {
                _context.AspNetRoles.Remove(aspNetRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetRoleExists(string id)
        {
            return _context.AspNetRoles.Any(e => e.Id == id);
        }

        // GET: AspNetUsers/AssignRole/5
        public async Task<IActionResult> AssignRole(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.AspNetUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Obtener todos los roles disponibles para seleccionar
            ViewBag.Roles = new SelectList(await _context.AspNetRoles.ToListAsync(), "Id", "Name");

            return View(user);
        }

        

    }
}



