using System;
using System.Collections.Generic;

namespace U1_evaluacion_sumativa.Models;

public partial class DetalleProyecto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int ProyectoId { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;
}
