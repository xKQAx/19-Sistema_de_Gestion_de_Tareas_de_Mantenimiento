using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Personal
{
    /// <summary>
    /// Identificador de personal.
    /// </summary>
    public int PersonalId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int DepartamentoId { get; set; }

    public virtual Departamento Departamento { get; set; } = null!;

    public virtual ICollection<HistorialMantenimiento> HistorialMantenimientos { get; set; } = new List<HistorialMantenimiento>();

    public virtual PuedeSer? PuedeSer { get; set; }

    public virtual ICollection<Tiene> Tienes { get; set; } = new List<Tiene>();

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
