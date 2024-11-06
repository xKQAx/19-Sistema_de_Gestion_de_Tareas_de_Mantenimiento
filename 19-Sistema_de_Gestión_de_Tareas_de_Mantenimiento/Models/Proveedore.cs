using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Proveedore
{
    public int ProveedorId { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Email { get; set; }

    public virtual Suministra? Suministra { get; set; }
}
