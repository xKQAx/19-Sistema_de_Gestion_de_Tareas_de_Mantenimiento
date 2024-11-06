using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string Seccion { get; set; } = null!;

    public DateTime FechaAlta { get; set; }

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
