using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class HistorialMantenimiento
{
    public int HistorialId { get; set; }

    public int EquipoId { get; set; }

    public DateOnly FechaMantenimiento { get; set; }

    public string? Descripcion { get; set; }

    public int? PersonalId { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;

    public virtual Personal? Personal { get; set; }
}
