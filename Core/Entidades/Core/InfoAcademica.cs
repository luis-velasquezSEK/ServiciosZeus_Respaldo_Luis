using System;
using System.Collections.Generic;

namespace Core.Entidades.Core;

public partial class InfoAcademica
{
    public int IdInfoAcademica { get; set; }

    public int? IdEmp { get; set; }

    public virtual Empleado? IdEmpNavigation { get; set; }

    public virtual ICollection<InfoTitulo> InfoTitulos { get; } = new List<InfoTitulo>();
}
