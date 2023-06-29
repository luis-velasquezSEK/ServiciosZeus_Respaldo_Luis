//using Apis_Zeus.ConexionBases;

using AspNetCoreRateLimit;
using Core.Dtos.Public;
using Core.Entidades.Core;
using Core.Entidades.Public;
using Core.Interfaces.Core;
using Core.Interfaces.Generico;
using Core.Interfaces.Public;
using Infraestructure.Configuration.Zeus.Core;
using Infraestructure.Configuration.Zeus.Public;
using Infraestructure.Repository.Core;
using Infraestructure.Repository.Generico;
using Infraestructure.Repository.Public;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Servicios_Zeus.Helpers.Errors;
using System.Text;
using System.Text.Json.Serialization;


namespace Servicios_Zeus.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",builder=>
            builder.WithOrigins("http://localhost:4200")
            //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "http://localhost")
            .AllowAnyHeader()
            .AllowAnyMethod());
        });

        public static void AddAplicationService(this IServiceCollection services)
        {
            services.AddTransient<Infraestructure.Configuration.Zeus.Public.ZeusPublicContext, Infraestructure.Configuration.Zeus.Public.ZeusPublicContext>();
            services.AddTransient<Infraestructure.Configuration.Zeus.Core.ZeusCoreContext, Infraestructure.Configuration.Zeus.Core.ZeusCoreContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericCoreRepository<>));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericPublicRepository<>));
            services.AddTransient<IPaisRepository, PaisRepository>();
            services.AddTransient<IUserLoginRepository,UserLoginRepository>();
            services.AddTransient<IUserRepository, UsuarioRepository>();
            services.AddTransient<IUsuarioPerfilRepository, UsuarioPerfilRepository>();
            services.AddTransient<IRolesUserRepository,RolesRepository>();
            services.AddTransient<IMenuRepository, MenuReposiory>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IMateriaprincipalRepository, MateriaPrincipalRepository>();
            services.AddTransient<ITipoMateriaCatalogoRepository, TipoMateriaCatalogoRepository>();
            services.AddTransient<ITipoComponenteRepository, TipoComponenteRepository>();
            services.AddTransient<IUnidadOrganizacionalCurricularRepository, UnidadOrganizacionalCurricularRepository>();
            services.AddTransient<IComponenteRepository, ComponenteRepository>();
            services.AddTransient<IPeriodicidadRepository,PeriodicidadRepository>();
            services.AddTransient<IEstadoPeRepository, EstadoPeRepository>();
            services.AddTransient<INivelEstudioRepository, NivelEstudioRepository>();
            services.AddTransient<IFacultadRepository, FacultadRepository>();
            services.AddTransient<ICarreraRepository, CarreraRepository>();
            services.AddTransient<ISubtipoComponenteRepository, SubtipoComponenteRepository>();
            services.AddTransient<IPlanEstudioRepository, PlanEstudioRepository>();
            services.AddTransient<IMallaRepository,MallaRepository>();
            services.AddTransient<IModalidadPE, ModalidadPERepository>();
            services.AddTransient<IMateriaComponenteRepository, MateriaComponenteRepository>();
            services.AddTransient<IHorasFranjaHoraria, HorasFranjaHorariaRepository>();
            services.AddTransient<IPlanificacionRepository, PlanificacionRepository>();
            services.AddTransient<ITipoInfraestructuraRepository, TipoInfraestructuraRepository>();
            services.AddTransient<IInfraestructuraRepository, InfraestructuraRepository>();
            services.AddTransient<INivelInfraestructuraRepository, NivelInfraestructuraRepository>();
            services.AddTransient<IEspaciosFisicosRepository, EspaciosFisicosRepository>();
            services.AddTransient<IPeriodoRepository, PeriodoRepository>();
            services.AddTransient<IEmpleadoRepository, EmpleadoRepository>();
            services.AddTransient<IPlanificacionRepository, PlanificacionRepository>();
            services.AddTransient<IComponentePlanificacionRepository, ComponentePlanificacionRepository>();
            services.AddTransient<IHorarioRepository, HorarioFechaRepository>();
            services.AddTransient<IHorario, HorarioRepository>();
            services.AddTransient<IProvinciaRepository, ProvinciaRepository>();
            services.AddTransient<ICantonRepository, CantonRepository>();
            services.AddTransient<IParroquiaRepository, ParroquiaRepository>();
            services.AddTransient<IEstadoEmpleadoRepository, EstadoEmpleadoRepository>();
            services.AddTransient<ITipoEmpleadoRepository, TipoEmpleadoRepository>();
            services.AddTransient<IUnidadOrganizativaRepository, UnidadOrganizativaRepository>();
            services.AddTransient<ITipoDocumentoRepository, TipoDocumentoRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddJsonOptions(x =>
             x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        }


        public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<JWT>(configuration.GetSection("JWT"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;//en producion cambiar a true
                o.SaveToken = false;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                };
            });
        }
        public static void ConfigureApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new HeaderApiVersionReader("X-Version"));
                options.ReportApiVersions = true;
            });
        }

        public static void ConfigureRateLimitiong(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint ="*",
                        Period = "10s",
                        Limit = 20    

                    }
                };
            });


        }

        public static void AddValidationErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {

                    var errors = actionContext.ModelState.Where(u => u.Value.Errors.Count > 0)
                                                    .SelectMany(u => u.Value.Errors)
                                                    .Select(u => u.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidation()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });
        }
    }
}
