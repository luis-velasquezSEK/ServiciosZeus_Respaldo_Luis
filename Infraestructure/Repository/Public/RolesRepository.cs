using Servicios_Zeus.Dtos;
using Core.Interfaces;
using Infraestructure.Configuration.Zeus.Public;

using Microsoft.EntityFrameworkCore;
using Core.Interfaces.Public;
using Infraestructure.Mappers;

namespace Infraestructure.Repository.Public
{
    public class RolesRepository : IRolesUserRepository
    {
        private readonly ZeusPublicContext _context;
        public RolesRepository(ZeusPublicContext context)
        {
            _context = context;

        }

        public async Task<ICollection<UsuarioPerfilDto>> GetRolesUsuario(string nombreperfil)
        {

            var collection = await _context.Perfils
                .Join(_context.UsuarioPerfils,
                a => a.IdPerfil,
                b => b.IdPerfil,
                (a, b) => new { a, b })
                .Select(y => new
                {
                    y.b.NombreUsuario,
                    y.a.IdPerfil,
                    y.b.ActivoPerfilUsuario,
                    y.a.NombrePerfil,
                    y.a.DescripcionPerfil
                })
                .Where(x => x.NombreUsuario == nombreperfil).OrderByDescending(x => x.IdPerfil).ToListAsync();

            return collection.MapTo<ICollection<UsuarioPerfilDto>>();

        }

    }
}
