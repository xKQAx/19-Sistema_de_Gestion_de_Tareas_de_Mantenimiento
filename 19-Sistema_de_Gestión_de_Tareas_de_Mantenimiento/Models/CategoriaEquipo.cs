using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class CategoriaEquipo
{
    public int CategoriaEquiposId { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public int EquipoId { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;
}
