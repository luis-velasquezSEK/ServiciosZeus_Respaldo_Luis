﻿using System;
using System.Collections.Generic;

namespace Core.Entidades.Core;

public partial class Prerrequisito
{
    public int IdPrerequisito { get; set; }

    public int? IdMalla { get; set; }

    public int? IdMateria { get; set; }

    public int? IdNivelEstudio { get; set; }

    public int? IdPlanEstudio { get; set; }

    public int? IdTipoPrecorrequisito { get; set; }

    public int? IdTipoAprobacion { get; set; }

    public string? DescripcionPpestudios { get; set; }

    public bool? ActivoMateriaPpestudios { get; set; }

    public string? PathPrerrequsito { get; set; }

    public virtual TipoAprobacion? IdTipoAprobacionNavigation { get; set; }

    public virtual TipoPrecorequisito? IdTipoPrecorrequisitoNavigation { get; set; }

    public virtual Malla? Malla { get; set; }
}
