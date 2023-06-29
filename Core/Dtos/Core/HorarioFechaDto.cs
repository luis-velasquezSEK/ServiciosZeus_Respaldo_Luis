using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos.Core
{
    public class HorarioFechaDto
    {
        public int? IdPlanificacion { get; set; }

        public DateTime? Fecha { get; set; }

        public DateTime? HoraIni { get; set; }

        public DateTime? HoraFin { get; set; }
    }
}
