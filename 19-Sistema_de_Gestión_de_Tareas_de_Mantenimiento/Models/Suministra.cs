using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Suministra
{
    public int ProveedorId { get; set; }

    public int RepuestoId { get; set; }

    public virtual Proveedore Proveedor { get; set; } = null!;

    public virtual Repuesto Repuesto { get; set; } = null!;
}
