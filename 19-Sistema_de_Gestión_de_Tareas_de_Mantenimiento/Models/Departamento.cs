using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Departamento
{
    public int DepartamentoId { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public int JefeId { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();

    public virtual Tiene? Tiene { get; set; }
}
