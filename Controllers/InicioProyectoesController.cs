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
    public class InicioProyectoesController : Controller
    {
        private readonly DbProyectoRedesContext _context;

        public InicioProyectoesController(DbProyectoRedesContext context)
        {
            _context = context;
        }

        // GET: InicioProyectoes
        public async Task<IActionResult> Index()
        {
            var dbProyectoRedesContext = _context.InicioProyectos.Include(i => i.Proyecto);
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        public async Task<IActionResult> MisIniciosProyectos(int? id)
        {
            var dbProyectoRedesContext = _context.Trabajadores.Include(i => i.Inicioproyecto).Where(p => p.UsuarioId == id);
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        // GET: InicioProyectoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inicioProyecto = await _context.InicioProyectos
                .Include(i => i.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);

            

            if (inicioProyecto == null)
            {
                return NotFound();
            }
            var trabajadores = await _context.Trabajadores
                .Include(u => u.Usuario)
                .Where(p => p.InicioproyectoId == id)
                .ToListAsync();
            if (trabajadores == null)
            {
                return NotFound();
            }
            var direccionamientos = await _context.DireccionamientoIps
                .Where(p => p.InicioProyectoId == id)
                .ToListAsync();


            ViewBag.Trabajadores = trabajadores;
            ViewBag.DireccionamientoIp = direccionamientos;
            return View(inicioProyecto);
        }

        // GET: InicioProyectoes/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Id_Proyecto = id;
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Cancelar(int? id)
        {
            Console.WriteLine($"ID recibido: {id}");

            if (id == null)
            {
                return NotFound();
            }

            var inicio_proyecto = await _context.InicioProyectos.FirstOrDefaultAsync(p => p.Id == id);

            if (inicio_proyecto == null)
            {
                return NotFound();
            }

            // Cambiar el estado directamente
            inicio_proyecto.Estado = 2; // Asumiendo que 2 significa "Cancelado"

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
            return RedirectToAction("Details","InicioProyectoes", new { id = inicio_proyecto.Id });

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
            var inicio_proyecto = await _context.InicioProyectos.FirstOrDefaultAsync(p => p.Id == id);

            if (inicio_proyecto == null)
            {
                return NotFound();
            }

            // Cambiar el estado directamente
            inicio_proyecto.Estado = 0; // Asumiendo que 2 significa "Cancelado"

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
            return RedirectToAction("Details", "InicioProyectoes", new { id = inicio_proyecto.Id });

            //ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.UsuarioId);
        }
        // POST: InicioProyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntidadInvolucrada,FechaInicio,FechaFinalizacion,Estado,Observaciones,ProyectoId")] InicioProyecto inicioProyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inicioProyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Nombre", inicioProyecto.ProyectoId);
            return View(inicioProyecto);
        }

        // GET: InicioProyectoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inicioProyecto = await _context.InicioProyectos.FindAsync(id);
            if (inicioProyecto == null)
            {
                return NotFound();
            }
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", inicioProyecto.ProyectoId);
            return View(inicioProyecto);
        }

        // POST: InicioProyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntidadInvolucrada,FechaInicio,FechaFinalizacion,Estado,Observaciones,ProyectoId")] InicioProyecto inicioProyecto)
        {
            if (id != inicioProyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inicioProyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InicioProyectoExists(inicioProyecto.Id))
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
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", inicioProyecto.ProyectoId);
            return View(inicioProyecto);
        }

        // GET: InicioProyectoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inicioProyecto = await _context.InicioProyectos
                .Include(i => i.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inicioProyecto == null)
            {
                return NotFound();
            }

            return View(inicioProyecto);
        }

        // POST: InicioProyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inicioProyecto = await _context.InicioProyectos.FindAsync(id);
            if (inicioProyecto != null)
            {
                _context.InicioProyectos.Remove(inicioProyecto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InicioProyectoExists(int id)
        {
            return _context.InicioProyectos.Any(e => e.Id == id);
        }
    }
}
