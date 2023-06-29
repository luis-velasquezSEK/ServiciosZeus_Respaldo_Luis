using System.Linq.Expressions;
using Core.Entidades.Core;
using Core.Interfaces.Core;
using Infraestructure.Configuration.Zeus.Core;
using Infraestructure.Repository.Generico;
using Microsoft.EntityFrameworkCore;


namespace Infraestructure.Repository.Core
{
    public class ComponenteRepository : GenericCoreRepository<Componente>, IComponenteRepository
    {
        public ComponenteRepository(Configuration.Zeus.Core.ZeusCoreContext context) : base(context)
        {

        }


    }
}
