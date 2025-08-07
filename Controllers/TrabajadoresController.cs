using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using U1_evaluacion_sumativa.Models;

namespace U1_evaluacion_sumativa.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly DbProyectoRedesContext _context;

        public TrabajadoresController(DbProyectoRedesContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index()
        {
            var dbProyectoRedesContext = _context.Trabajadores.Include(t => t.Inicioproyecto).Include(t => t.Usuario);
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.Inicioproyecto)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // GET: Trabajadores/Create
        public IActionResult Create(int? id)
        {
            
            ViewBag.Id_Inicio_proyecto = id; //id es de "inicio_proyecto"
            ViewData["InicioproyectoId"] = new SelectList(_context.InicioProyectos, "Id", "EntidadInvolucrada");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cargo,InicioproyectoId,UsuarioId")] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajadore);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "InicioProyectoes", new { id = trabajadore.InicioproyectoId });
            }
            ViewData["InicioproyectoId"] = new SelectList(_context.InicioProyectos, "Id", "Id", trabajadore.InicioproyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", trabajadore.UsuarioId);
            return View(trabajadore);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore == null)
            {
                return NotFound();
            }
            ViewData["InicioproyectoId"] = new SelectList(_context.InicioProyectos, "Id", "Id", trabajadore.InicioproyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", trabajadore.UsuarioId);
            return View(trabajadore);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cargo,InicioproyectoId,UsuarioId")] Trabajadore trabajadore)
        {
            if (id != trabajadore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajadore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadoreExists(trabajadore.Id))
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
            ViewData["InicioproyectoId"] = new SelectList(_context.InicioProyectos, "Id", "Id", trabajadore.InicioproyectoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", trabajadore.UsuarioId);
            return View(trabajadore);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.Inicioproyecto)
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore != null)
            {
                _context.Trabajadores.Remove(trabajadore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadoreExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }
    }
}
