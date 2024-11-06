using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Programacione
{
    public int ProgramacionId { get; set; }

    public DateOnly FechaProgramada { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Dispone> Dispones { get; set; } = new List<Dispone>();
}
