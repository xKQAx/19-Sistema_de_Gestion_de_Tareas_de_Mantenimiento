using System;
using System.Collections.Generic;

namespace _19_Sistema_de_Gestión_de_Tareas_de_Mantenimiento.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Correo { get; set; } = null!;

    public string Clave { get; set; } = null!;
}
