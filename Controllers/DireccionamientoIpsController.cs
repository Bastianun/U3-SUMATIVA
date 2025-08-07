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
    public class DireccionamientoIpsController : Controller
    {
        private readonly DbProyectoRedesContext _context;

        public DireccionamientoIpsController(DbProyectoRedesContext context)
        {
            _context = context;
        }

        // GET: DireccionamientoIps
        public async Task<IActionResult> Index()
        {
            var dbProyectoRedesContext = _context.DireccionamientoIps.Include(d => d.InicioProyecto);
            return View(await dbProyectoRedesContext.ToListAsync());
        }

        // GET: DireccionamientoIps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionamientoIp = await _context.DireccionamientoIps
                .Include(d => d.InicioProyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccionamientoIp == null)
            {
                return NotFound();
            }

            return View(direccionamientoIp);
        }

        // GET: DireccionamientoIps/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Id_InicioProyecto = id;
            ViewData["InicioProyectoId"] = new SelectList(_context.InicioProyectos, "Id", "EntidadInvolucrada");
            return View();
        }

        // POST: DireccionamientoIps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubRed,IpNetwork,Prefijo, InicioProyectoId")] DireccionamientoIp direccionamientoIp)
        {
            if (ModelState.IsValid)
            {
                int prefijo = int.Parse(direccionamientoIp.Prefijo.Replace("/", ""));

                // Recalcular todo lo demás
                var datosCalculados = CalcularDireccionamiento(direccionamientoIp.IpNetwork, prefijo);

                // Copiar los valores calculados al modelo original
                direccionamientoIp.Mask = datosCalculados.Mask;
                direccionamientoIp.CantIptotales = datosCalculados.CantIptotales;
                direccionamientoIp.CantIpHost = datosCalculados.CantIpHost;
                direccionamientoIp.RangoInicial = datosCalculados.RangoInicial;
                direccionamientoIp.RangoFinal = datosCalculados.RangoFinal;
                direccionamientoIp.IpBroadcast = datosCalculados.IpBroadcast;

                _context.Add(direccionamientoIp);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "InicioProyectoes", new { id = direccionamientoIp.InicioProyectoId });
            }
            ViewData["InicioProyectoId"] = new SelectList(_context.InicioProyectos, "Id", "EntidadInvolucrada", direccionamientoIp.InicioProyectoId);
            return View(direccionamientoIp);
        }

        // GET: DireccionamientoIps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionamientoIp = await _context.DireccionamientoIps.FindAsync(id);
            if (direccionamientoIp == null)
            {
                return NotFound();
            }
            ViewData["InicioProyectoId"] = new SelectList(_context.InicioProyectos, "Id", "Id", direccionamientoIp.InicioProyectoId);
            return View(direccionamientoIp);
        }

        // POST: DireccionamientoIps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IpNetwork,Prefijo,Mask,CantIptotales,CantIpHost,IpBroadcast,RangoInicial,RangoFinal,InicioProyectoId")] DireccionamientoIp direccionamientoIp)
        {
            if (id != direccionamientoIp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccionamientoIp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionamientoIpExists(direccionamientoIp.Id))
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
            ViewData["InicioProyectoId"] = new SelectList(_context.InicioProyectos, "Id", "Id", direccionamientoIp.InicioProyectoId);
            return View(direccionamientoIp);
        }

        // GET: DireccionamientoIps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionamientoIp = await _context.DireccionamientoIps
                .Include(d => d.InicioProyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccionamientoIp == null)
            {
                return NotFound();
            }

            return View(direccionamientoIp);
        }

        // POST: DireccionamientoIps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var id_inicioProyecto = 0;
            var direccionamientoIp = await _context.DireccionamientoIps.FindAsync(id);
            
            if (direccionamientoIp != null)
            {
                id_inicioProyecto = direccionamientoIp.InicioProyectoId;
                _context.DireccionamientoIps.Remove(direccionamientoIp);
            }

            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Details", "InicioProyectoes", new { id = id_inicioProyecto });
        }

        private bool DireccionamientoIpExists(int id)
        {
            return _context.DireccionamientoIps.Any(e => e.Id == id);
        }


        //para calcular direccionamiento IP ========================================================================
        private DireccionamientoIp CalcularDireccionamiento(string ipNetwork, int prefijo)
        {
            var direccionamiento = new DireccionamientoIp
            {
                IpNetwork = ipNetwork,
                Prefijo = prefijo.ToString(),
                Mask = ObtenerMascaraDecimal(prefijo)
            };

            // Cálculo total IPs y hosts
            int totalIps = (int)Math.Pow(2, 32 - prefijo);
            int totalHosts = totalIps - 2;

            direccionamiento.CantIptotales = totalIps;
            direccionamiento.CantIpHost = totalHosts;

            // Calcular rango y broadcast
            var partes = ipNetwork.Split('.');
            int ipBase = (int.Parse(partes[0]) << 24) | (int.Parse(partes[1]) << 16) |
                         (int.Parse(partes[2]) << 8) | int.Parse(partes[3]);

            int bloque = totalIps;
            int broadcast = ipBase + bloque - 1;

            // Dirección de primer y último host
            direccionamiento.RangoInicial = ConvertirDecimalAIP(ipBase + 1);
            direccionamiento.RangoFinal = ConvertirDecimalAIP(broadcast - 1);
            direccionamiento.IpBroadcast = ConvertirDecimalAIP(broadcast);

            return direccionamiento;
        }

        private string ConvertirDecimalAIP(int ip)
        {
            return string.Join(".", new[]
            {
                (ip >> 24) & 0xFF,
                (ip >> 16) & 0xFF,
                (ip >> 8) & 0xFF,
                ip & 0xFF
            });
        }

        private string ObtenerMascaraDecimal(int prefijo)
        {
            uint mask = 0xffffffff << (32 - prefijo);
            return string.Join(".", new[]
            {
                (mask >> 24) & 0xFF,
                (mask >> 16) & 0xFF,
                (mask >> 8) & 0xFF,
                mask & 0xFF
            });
        }
    }
}
