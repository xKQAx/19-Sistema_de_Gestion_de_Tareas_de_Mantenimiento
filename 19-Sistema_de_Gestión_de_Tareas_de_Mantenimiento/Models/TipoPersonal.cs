using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class TipoPersonal
{
    public int TipoPersonalId { get; set; }

    public string TipoPersonal1 { get; set; } = null!;

    public virtual ICollection<PuedeSer> PuedeSers { get; set; } = new List<PuedeSer>();
}
