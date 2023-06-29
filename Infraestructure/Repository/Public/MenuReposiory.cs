using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Dtos.Public;
using Core.Entidades.Public;
using Core.Interfaces.Public;
using Infraestructure.Repository.Generico;
using Microsoft.EntityFrameworkCore;
namespace Infraestructure.Repository.Public
{
    public class MenuReposiory : GenericPublicRepository<MenuDto>, IMenuRepository
    {
        public MenuReposiory(Configuration.Zeus.Public.ZeusPublicContext context) : base(context)
        {

        }


        public async Task<ICollection<MenuDto>> menuUsername(string username)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<Autorizacion, AutorizacionDto>();
                cfg.CreateMap<UsuarioPerfil, UsuarioPerfilDto2>();
            });


            var mapper = new Mapper(config);

            var query = from c in _context.Menus.AsQueryable()
                        join d in _context.Autorizacions.AsQueryable() on c.IdMenu equals d.IdMenu
                        join e in _context.UsuarioPerfils.AsQueryable() on d.IdPerfil equals e.IdPerfil
                        where e.NombreUsuario == username
                        select mapper.Map<MenuDto>(c);


            //var configmenu = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<query.ToList<Menu>(), MenuDto>();

            //});


            //where c.(c.IdMenu


            //                )
            //.Select(y => new
            //{
            //    MenuDto = y,
            //}).Where(x => x.MenuDto.IdMenu.);




            //var configmenu = new MapperConfiguration(cfg =>
            //{
            //cfg.CreateMap<Autorizacion, AutorizacionDto>();
            //cfg.CreateMap<Menu, MenuDto>().IncludeAllDerived();

            //cfg.CreateMap<Autorizacion, AutorizacionDto>().IncludeMembers(s => s.PermisosAutorizacion);
            //.ForMember(x => x.IdAutorizacion, conf => conf.MapFrom(o => o.IdAutorizacion))
            //.ForMember(x => x.IdMenus, conf => conf.MapFrom(o => o.IdMenus))               
            //.ForMember(x => x.IdPerfiles, conf => conf.MapFrom(o => o.IdPerfiles))
            //.ForMember(x => x.PermisosAutorizacion, conf => conf.MapFrom(o => o.PermisosAutorizacion))
            //.ForMember(x => x.ActivoAutorizacion, conf => conf.MapFrom(o => o.ActivoAutorizacion));

            //});

            return await query.ToListAsync();
            //return  _context.Menus.
            //    ProjectTo<MenuDto>(query).
            //    ToList();
        }

        public async Task<ICollection<MenuDto>> menuPadreByUser(string username)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<Autorizacion, AutorizacionDto>();
                cfg.CreateMap<UsuarioPerfil, UsuarioPerfilDto2>();
            });


            var mapper = new Mapper(config);

            var query = from c in _context.Menus.AsQueryable()
                        join d in _context.Autorizacions.AsQueryable() on c.IdMenu equals d.IdMenu
                        join e in _context.UsuarioPerfils.AsQueryable() on d.IdPerfil equals e.IdPerfil
                        where e.NombreUsuario == username && c.IdMenuPadre==null && c.ActivoMenu==true
                        orderby c.OrdenMenu ascending

                        select mapper.Map<MenuDto>(c);


            return await query.ToListAsync();
        }

        public async Task<ICollection<MenuDto>> menuHijoByUser(int? idmenupadre,string username)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Menu, MenuDto>();
                cfg.CreateMap<Autorizacion, AutorizacionDto>();
                cfg.CreateMap<UsuarioPerfil, UsuarioPerfilDto2>();
            });


            var mapper = new Mapper(config);

            var query = from c in _context.Menus.AsQueryable()
                        join d in _context.Autorizacions.AsQueryable() on c.IdMenu equals d.IdMenu
                        join e in _context.UsuarioPerfils.AsQueryable() on d.IdPerfil equals e.IdPerfil
                        where e.NombreUsuario == username && c.IdMenuPadre == idmenupadre && c.ActivoMenu == true
                        orderby c.OrdenMenu ascending

                        select mapper.Map<MenuDto>(c);


            return await query.ToListAsync();
        }




        public async Task<ICollection<ItemMenuDto>> findMenuItems(string username)
        {

            ICollection<MenuDto> lspadre=await menuPadreByUser(username);
            ICollection<ItemMenuDto> lst = new List<ItemMenuDto>();
            foreach (MenuDto obj in lspadre)
            {
                ItemMenuDto menu = new ItemMenuDto();
                ItemDto item = new ItemDto();
                item.label=obj.NombreMenu;
                item.icon=obj.IconoMenu;
                item.url=obj.UrlMenu;

                ICollection<MenuDto> lstHijo =await menuHijoByUser(obj.IdMenu, username);
                List<ItemDto> listItemHijo = new List<ItemDto>();
                foreach (MenuDto objHijo in lstHijo)
                {
                    ItemDto itemHijo = new ItemDto();
                    itemHijo.label=objHijo.NombreMenu;
                    itemHijo.icon=objHijo.IconoMenu;
                    itemHijo.routerLink=objHijo.UrlMenu;
                    listItemHijo.Add(itemHijo);
                }

                menu.ItemDTO=item;
                if (lstHijo.Count > 0)
                    menu.ItemsDTO=listItemHijo;
                lst.Add(menu);
            }
            return lst;
        }

    }
}
