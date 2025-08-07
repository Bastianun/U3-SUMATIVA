using System;
using System.Collections.Generic;

namespace U1_evaluacion_sumativa.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public string? Password { get; set; }

    /// <summary>
    /// 0: Desactivado ; 1: Activado
    /// </summary>
    public int Estado { get; set; }

    /// <summary>
    /// 0: Administador ; 1: Trabajador
    /// </summary>
    public int Rol { get; set; }

    public virtual ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();

    public virtual ICollection<Trabajadore> Trabajadores { get; set; } = new List<Trabajadore>();
}
