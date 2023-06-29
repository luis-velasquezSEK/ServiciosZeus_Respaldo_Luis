using System.Linq.Expressions;
using Core.Entidades.Core;
using Core.Interfaces.Core;
using Infraestructure.Configuration.Zeus.Core;
using Infraestructure.Repository.Generico;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository.Core
{
    public class EmpleadoRepository : GenericCoreRepository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(ZeusCoreContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Empleado>> GetAllAsync(bool noseguimineto = true)
        {
            var query = noseguimineto ? _context.Empleados.AsNoTracking()
                    : _context.Empleados;

            return await query
                                .Include(x => x.IdCantonNavigation)
                                .Include(y => y.IdEstadoEmpNavigation)
                                .Include(z => z.IdPaisNacNavigation)
                                .Include(a => a.IdParroquiaNavigation)
                                .Include(b => b.IdProvinciaNavigation)
                                .Include(c => c.IdTipoDocumentoNavigation)
                                .Include(d => d.IdTipoEmpNavigation)
                                .Include(e => e.IdUnidadNavigation)
                                .ToListAsync();
        }

        public override async Task<Empleado> GetByIdAsync(int idemp, bool noseguimiento = true)
        {
            var query = noseguimiento ? _context.Empleados.AsNoTracking()
                                   : _context.Empleados;

            return await query
                                .Include(x => x.IdCantonNavigation)
                                .Include(y => y.IdEstadoEmpNavigation)
                                .Include(z => z.IdPaisNacNavigation)
                                .Include(a => a.IdParroquiaNavigation)
                                .Include(b => b.IdProvinciaNavigation)
                                .Include(c => c.IdTipoDocumentoNavigation)
                                .Include(d => d.IdTipoEmpNavigation)
                                .Include(e => e.IdUnidadNavigation)
                                .FirstOrDefaultAsync(x => x.IdEmp == idemp);
        }

        public override async Task<(int totalRegistros, IEnumerable<Empleado> registros)> GetAllAsync(
            int pageIndex, int pageSize, string search, bool noseguimiento = true)
        {

            var query = noseguimiento ? _context.Empleados.AsNoTracking()
                                  : _context.Empleados;

            if (!String.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.IdentificacionEmp.ToLower().Contains(search));
            }


            var totalRegistros = await query
                                        .CountAsync();

            var registros = await query

                                .Include(x => x.IdCantonNavigation)
                                .Include(y => y.IdEstadoEmpNavigation)
                                .Include(z => z.IdPaisNacNavigation)
                                .Include(a => a.IdParroquiaNavigation)
                                .Include(b => b.IdProvinciaNavigation)
                                .Include(c => c.IdTipoDocumentoNavigation)
                                .Include(d => d.IdTipoEmpNavigation)
                                .Include(e => e.IdUnidadNavigation)
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (totalRegistros, registros);
        }
    }
}
