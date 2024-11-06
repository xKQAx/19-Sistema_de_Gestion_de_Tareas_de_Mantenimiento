using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Tarea
{
    public int TareaId { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateOnly FechaCreacion { get; set; }

    public virtual Dispone? Dispone { get; set; }

    public virtual ICollection<TieneAsignado> TieneAsignados { get; set; } = new List<TieneAsignado>();

    public virtual ICollection<CategoriaTarea> CategoriaTareas { get; set; } = new List<CategoriaTarea>();

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
