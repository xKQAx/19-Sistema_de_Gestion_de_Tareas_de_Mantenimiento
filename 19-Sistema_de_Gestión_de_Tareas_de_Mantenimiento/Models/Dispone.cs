using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Dispone
{
    public int TareaId { get; set; }

    public int ProgramacionId { get; set; }

    public virtual Programacione Programacion { get; set; } = null!;

    public virtual Tarea Tarea { get; set; } = null!;
}
