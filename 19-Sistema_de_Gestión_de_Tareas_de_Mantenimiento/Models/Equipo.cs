using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Equipo
{
    public int EquipoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public DateOnly FechaAdquisicion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual CategoriaEquipo? CategoriaEquipo { get; set; }

    public virtual ICollection<EquiposRepuesto> EquiposRepuestos { get; set; } = new List<EquiposRepuesto>();

    public virtual ICollection<HistorialMantenimiento> HistorialMantenimientos { get; set; } = new List<HistorialMantenimiento>();

    public virtual TieneAsignado? TieneAsignado { get; set; }
}
