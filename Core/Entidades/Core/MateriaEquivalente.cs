using System;
using System.Collections.Generic;

namespace Core.Entidades.Core;

public partial class MateriaEquivalente
{
    public int IdMateriaEquivalente { get; set; }

    public int? IdMalla { get; set; }

    public int? IdMateria { get; set; }

    public int? IdNivelEstudio { get; set; }

    public int? IdPlanEstudio { get; set; }

    public int? MalIdMalla { get; set; }

    public int? MalIdMateria { get; set; }

    public int? MalIdNivelEstudio { get; set; }

    public int? MalIdPlanEstudio { get; set; }

    public string? ObservacionesMateriaEquivalente { get; set; }

    public string? PathautorizacionMateriaEquivalente { get; set; }

    public bool? AutorizacionMateriaEquivalente { get; set; }

    public bool? ActivoMateriaEquivalente { get; set; }

    public virtual Malla? Malla { get; set; }

    public virtual Malla? MallaNavigation { get; set; }
}
