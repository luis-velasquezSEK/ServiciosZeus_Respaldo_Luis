﻿using System;
using System.Collections.Generic;

namespace Core.Entidades.Core;

public partial class Pai
{
    public int IdPais { get; set; }

    public string? CodigoPais { get; set; }

    public string? NombrePais { get; set; }

    public bool? ActivoPais { get; set; }

    public string? CodsniesePais { get; set; }

    public virtual ICollection<Empleado> Empleados { get; } = new List<Empleado>();

    public virtual ICollection<Provincium> Provincia { get; } = new List<Provincium>();

    public virtual ICollection<Region> Regions { get; } = new List<Region>();
}
