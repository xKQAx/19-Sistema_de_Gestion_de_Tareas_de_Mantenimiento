using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class TieneAsignado
{
    public int EquipoId { get; set; }

    public int TareaId { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;

    public virtual Tarea Tarea { get; set; } = null!;
}
