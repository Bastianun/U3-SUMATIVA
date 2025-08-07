using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace U1_evaluacion_sumativa.Models;

public partial class Trabajadore
{
    public int Id { get; set; }

    public string? Cargo { get; set; }

    public int InicioproyectoId { get; set; }

    public int UsuarioId { get; set; }
    [ValidateNever]
    public virtual InicioProyecto Inicioproyecto { get; set; } = null!;
    [ValidateNever]
    public virtual Usuario Usuario { get; set; } = null!;
}
