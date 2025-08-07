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
    public class DetalleProyectoesController : Controller
    {
        private readonly DbProyectoRedesContext _context;

        public DetalleProyectoesController(DbProyectoRedesContext context)
        {
            _context = context;
        }

        // GET: DetalleProyectoes
        public async Task<IActionResult> Index()
        {
            var dbProyectoRedesContext = _context.DetalleProyectos.Include(d => d.Proyecto);
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        public async Task<IActionResult> DetallesPorProyecto(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
                
            var proyecto = await _context.Proyectos.FindAsync(Id);
            if (proyecto == null)
            {
                return NotFound();
            }
                
            var dbProyectoRedesContext = _context.DetalleProyectos
                .Where(p => p.ProyectoId == Id);
            //Include(d => d.Proyecto);
            ViewBag.Proyecto = proyecto;
            ViewBag.servicio_id = Id;
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        // GET: DetalleProyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyectos
                .Include(d => d.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }

            return View(detalleProyecto);
        }

        // GET: DetalleProyectoes/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Id_Servicio = id;
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Nombre");
            return View();
        }

        // POST: DetalleProyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,ProyectoId")] DetalleProyecto detalleProyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleProyecto);
                await _context.SaveChangesAsync();

                //Para redireccionar al DetallesPorProyecto recientemente creado.
                return RedirectToAction("DetallesPorProyecto", new { Id = detalleProyecto.ProyectoId });
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Nombre", detalleProyecto.ProyectoId);
            return View(detalleProyecto);
        }

        // GET: DetalleProyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyectos.FindAsync(id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", detalleProyecto.ProyectoId);
            return View(detalleProyecto);
        }

        // POST: DetalleProyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Descripcion,ProyectoId")] DetalleProyecto detalleProyecto)
        {
            if (id != detalleProyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleProyectoExists(detalleProyecto.Id))
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
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", detalleProyecto.ProyectoId);
            return View(detalleProyecto);
        }

        // GET: DetalleProyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleProyecto = await _context.DetalleProyectos
                .Include(d => d.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detalleProyecto == null)
            {
                return NotFound();
            }

            return View(detalleProyecto);
        }

        // POST: DetalleProyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleProyecto = await _context.DetalleProyectos.FindAsync(id);
            if (detalleProyecto != null)
            {
                _context.DetalleProyectos.Remove(detalleProyecto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleProyectoExists(int id)
        {
            return _context.DetalleProyectos.Any(e => e.Id == id);
        }
    }
}
