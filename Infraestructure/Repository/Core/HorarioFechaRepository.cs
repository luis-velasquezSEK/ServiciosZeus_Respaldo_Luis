using Core.Entidades.Core;
using Core.Interfaces.Core;
using Infraestructure.Repository.Generico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository.Core
{


    public class HorarioFechaRepository : GenericCoreRepository<HorarioFecha>, IHorarioRepository
    {
        public HorarioFechaRepository(Configuration.Zeus.Core.ZeusCoreContext context) : base(context)
        {

        }


    }

}
