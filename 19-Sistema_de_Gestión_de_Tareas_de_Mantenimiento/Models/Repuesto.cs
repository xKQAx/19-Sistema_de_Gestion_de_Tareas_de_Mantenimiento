using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Repuesto
{
    public int RepuestoId { get; set; }

    public string NombreRepuesto { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<EquiposRepuesto> EquiposRepuestos { get; set; } = new List<EquiposRepuesto>();

    public virtual ICollection<Suministra> Suministras { get; set; } = new List<Suministra>();
}
