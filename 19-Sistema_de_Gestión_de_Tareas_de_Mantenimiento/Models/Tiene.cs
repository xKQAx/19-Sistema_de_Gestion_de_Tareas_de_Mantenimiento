using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Tiene
{
    public int DepartamentoId { get; set; }

    /// <summary>
    /// Identificador de personal.
    /// </summary>
    public int PersonalId { get; set; }

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual Personal Personal { get; set; } = null!;
}
