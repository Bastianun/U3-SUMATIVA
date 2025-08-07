using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using U1_evaluacion_sumativa.Models;

namespace U1_evaluacion_sumativa.Controllers
{
    public class ProyectoesController : Controller
    {
        private readonly DbProyectoRedesContext _context;


        public ProyectoesController(DbProyectoRedesContext context)
        {
            _context = context;
        }

        // GET: Proyectoes
        public async Task<IActionResult> Index()
        {
            var dbProyectoRedesContext = _context.Proyectos.Include(p => p.Usuario);
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        public async Task<IActionResult> ProyectosPorUsuario(int? Id)
        {
            var dbProyectoRedesContext = _context.Proyectos
                .Where(p => p.UsuarioId == Id);
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        // GET: Proyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectoes/Create
        public IActionResult Create(int? id)
        {

            Debug.WriteLine("=========================");
            Debug.WriteLine(id);
            Debug.WriteLine("=========================");
            ViewBag.Id_Usuario = id;
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Proyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estado,Tipo,UsuarioId")] Proyecto proyecto)
        {
            proyecto.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", proyecto.UsuarioId);
            return View(proyecto);
        }

        // GET: Proyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
            return View(proyecto);
        }



        // POST: Proyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estado,Tipo,UsuarioId")] Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
            return View(proyecto);
        }

        [HttpPost]
        public async Task<IActionResult> Cancelar(int? id)
        {
            Console.WriteLine($"ID recibido: {id}");

            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            // Cambiar el estado directamente
            proyecto.Estado = 2; // Asumiendo que 2 significa "Cancelado"

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    Console.WriteLine("ACTUALIZADO");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar: {ex.Message}");
                    // Manejo de errores si lo deseas
                }
            }
            return RedirectToAction(nameof(Index));
            
            //ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
        }

        [HttpPost]
        public async Task<IActionResult> Finalizar(int? id)
        {
            Console.WriteLine($"ID recibido: {id}");

            // Verificar la id que llega
            if (id == null)
            {
                return NotFound();
            }


            // Para tener el proyecto a editar
            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == id);

            if (proyecto == null)
            {
                return NotFound();
            }

            // Cambiar el estado directamente
            proyecto.Estado = 0; // Asumiendo que 2 significa "Cancelado"

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    Console.WriteLine("ACTUALIZADO");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al actualizar: {ex.Message}");
                    // Manejo de errores si lo deseas
                }
            }
            return RedirectToAction(nameof(Index));

            //ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
        }
        // GET: Proyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
