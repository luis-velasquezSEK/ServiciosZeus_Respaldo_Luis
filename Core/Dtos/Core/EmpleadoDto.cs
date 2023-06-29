using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entidades.Core;

namespace Core.Dtos.Core
{
    public class EmpleadoDto
    {
        public int IdEmp { get; set; }

        public int? IdTipoEmp { get; set; }

        public string? NombresEmp { get; set; }

        public string? ApellidoEmp { get; set; }

        public string? IdentificacionEmp { get; set; }

        public DateTime? FnacEmp { get; set; }

        public int? EdadEmp { get; set; }

        public string? SexoEmp { get; set; }

        public int? IdTipoDocumento { get; set; }

        public int? IdPaisNac { get; set; }

        public int? IdPais { get; set; }

        public int? IdProvincia { get; set; }

        public int? IdCanton { get; set; }

        public int? IdParroquia { get; set; }

        public string? CallePrincipal { get; set; }

        public string? CalleSecundaria { get; set; }

        public string? Numeracion { get; set; }

        public string? CodPostal { get; set; }

        public string? Referencia { get; set; }

        public string? TelefonoEmp { get; set; }

        public string? CelularEmp { get; set; }

        public string? CorreoEmp { get; set; }

        public DateTime? FechaRegistroEmp { get; set; }

        public DateTime? FechaActualizaEmp { get; set; }

        public string? PathfirmadigitalEmp { get; set; }

        public string? FotoEmp { get; set; }

        public int? IdUnidad { get; set; }

        public int? IdEstadoEmp { get; set; }

        public bool? ActivoEmp { get; set; }

        public virtual ICollection<Contrato> Contratos { get; } = new List<Contrato>();

        public virtual ICollection<DistributivoDocente> DistributivoDocentes { get; } = new List<DistributivoDocente>();

        public virtual Canton? IdCantonNavigation { get; set; }

        public virtual EstadoEmpleado? IdEstadoEmpNavigation { get; set; }

        public virtual Pai? IdPaisNacNavigation { get; set; }

        public virtual Parroquium? IdParroquiaNavigation { get; set; }

        public virtual Provincium? IdProvinciaNavigation { get; set; }

        public virtual TipoDocumento? IdTipoDocumentoNavigation { get; set; }

        public virtual TipoEmpleado? IdTipoEmpNavigation { get; set; }

        public virtual UnidadOrganizativa? IdUnidadNavigation { get; set; }

        public virtual ICollection<InfoAcademica> InfoAcademicas { get; } = new List<InfoAcademica>();

        public virtual ICollection<InfoExperiencium> InfoExperiencia { get; } = new List<InfoExperiencium>();

        public virtual ICollection<InfoPersonal> InfoPersonals { get; } = new List<InfoPersonal>();
    }
}
