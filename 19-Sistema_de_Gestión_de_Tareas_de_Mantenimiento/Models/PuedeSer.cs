using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class PuedeSer
{
    /// <summary>
    /// Identificador de personal.
    /// </summary>
    public int PersonalId { get; set; }

    public int TipoPersonalId { get; set; }

    public virtual Personal Personal { get; set; } = null!;

    public virtual TipoPersonal TipoPersonal { get; set; } = null!;
}
