using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class EquiposRepuesto
{
    public int EquipoId { get; set; }

    public int RepuestoId { get; set; }

    public int Cantidad { get; set; }

    public virtual Equipo Equipo { get; set; } = null!;

    public virtual Repuesto Repuesto { get; set; } = null!;
}
