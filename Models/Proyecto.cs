using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace U1_evaluacion_sumativa.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// 0: Finalizado ; 1: Proceso; 2: Cancelado
    /// </summary>
    public int Estado { get; set; }

    /// <summary>
    /// 0: Cableado UTP; 1: Configuración Equipos; 2: Ambos
    /// </summary>
    public int Tipo { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<DetalleProyecto> DetalleProyectos { get; set; } = new List<DetalleProyecto>();

    public virtual ICollection<InicioProyecto> InicioProyectos { get; set; } = new List<InicioProyecto>();

    [ValidateNever]
    public virtual Usuario Usuario { get; set; } = null!;
}
