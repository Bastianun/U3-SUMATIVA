using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace U1_evaluacion_sumativa.Models;

public partial class DireccionamientoIp
{
    public int Id { get; set; }

    public string SubRed { get; set; } = null!;

    public string IpNetwork { get; set; } = null!;

    public string Prefijo { get; set; } = null!;

    public string? Mask { get; set; }

    public int? CantIptotales { get; set; }

    public int? CantIpHost { get; set; }

    public string? IpBroadcast { get; set; }

    public string? RangoInicial { get; set; }

    public string? RangoFinal { get; set; }

    public int InicioProyectoId { get; set; }
    [ValidateNever]
    public virtual InicioProyecto InicioProyecto { get; set; } = null!;
}
