using AutoMapper;
using Core.Dtos.Core;
using Core.Entidades.Core;
using Core.Interfaces.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios_Zeus.Helpers;
using Servicios_Zeus.Helpers.Errors;

namespace Servicios_Zeus.Controllers.Core
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/materia-principal")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MateriaPrincipalController : ControllerBase
    {
        private readonly IMateriaprincipalRepository _imateriaprincipal;
        private readonly IMapper _mapper;
        public MateriaPrincipalController(IMateriaprincipalRepository materiarepo, IMapper mapper)
        {
            _imateriaprincipal = materiarepo;
            _mapper = mapper;
        }
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materium>>> GetAllMaterias()
        {
            var materia = await _imateriaprincipal.GetAllAsync();
            if (materia == null)
                return NotFound(new ApiResponse(404, "La lista no contiene ningún elemento."));
            return Ok(materia);
        }
        [Route("GetAllPaging")]
        [HttpGet]
        public async Task<ActionResult<Pager<Materium>>> GetAllMaterias([FromQuery] Params paisParams)
        {
            var materia = await _imateriaprincipal.GetAllAsync(paisParams.PageIndex, paisParams.PageSize,
                                    paisParams.Search);
            return new Pager<Materium>(materia.registros, materia.totalRegistros,
            paisParams.PageIndex, paisParams.PageSize, paisParams.Search);
        }



        [Route("Save")]
        [HttpPost]
        public async Task<ActionResult<Materium>> SaveMateriaPrincipal([FromBody] MateriaPrincipalDto materiaprincpaldto)
        {
            //cambiar esta quemado//
            //materiaprincpaldto.IdUoc = 1;
            materiaprincpaldto.IdTipoAprobacion = 1;
            //materiaprincpaldto.IdTipoMateriaCatalogo = 1;



           var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MateriaPrincipalDto, Materium>()
                .ForMember(x => x.CodigoMateria, conf => conf.MapFrom(o => o.CodigoMateriap))
                .ForMember(x => x.NombreMateria, conf => conf.MapFrom(o => o.NombreMateriap))
                .ForMember(x => x.CreditosMateria, conf => conf.MapFrom(o => o.CreditosMateriap))
                .ForMember(x => x.HorasSemestralesMateria, conf => conf.MapFrom(o => o.HorasSemestralesMateriap))
                .ForMember(x => x.CuposMatriculaMateria, conf => conf.MapFrom(o => o.CuposMatriculaMateriap))
                .ForMember(x => x.CampoUnescoMateria, conf => conf.MapFrom(o => o.CampoUnescoMateriap))
                .ForMember(x => x.ActivoMateria, conf => conf.MapFrom(o => o.ActivoMateriap));
                
            });
            var _mapper = new Mapper(config);
            var _materia = _mapper.Map<Materium>(materiaprincpaldto);
            _imateriaprincipal.Add(_materia);
            await _imateriaprincipal.SaveAsync();
            if (materiaprincpaldto == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            materiaprincpaldto.IdMateria = _materia.IdMateria;
            return CreatedAtAction(nameof(SaveMateriaPrincipal), new { IdMateria = materiaprincpaldto.IdMateria });

        }
        [Route("Update/{id}")]
        [HttpPut]
        public async Task<ActionResult<MateriaPrincipalDto>> Put(int id, [FromBody] MateriaPrincipalDto materiaprincipalDto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MateriaPrincipalDto, Materium>()
                .ForMember(x => x.CodigoMateria, conf => conf.MapFrom(o => o.CodigoMateriap))
                .ForMember(x => x.NombreMateria, conf => conf.MapFrom(o => o.NombreMateriap))
                .ForMember(x => x.CreditosMateria, conf => conf.MapFrom(o => o.CreditosMateriap))
                .ForMember(x => x.HorasSemestralesMateria, conf => conf.MapFrom(o => o.HorasSemestralesMateriap))
                .ForMember(x => x.CuposMatriculaMateria, conf => conf.MapFrom(o => o.CuposMatriculaMateriap))
                .ForMember(x => x.CampoUnescoMateria, conf => conf.MapFrom(o => o.CampoUnescoMateriap))
                .ForMember(x => x.ActivoMateria, conf => conf.MapFrom(o => o.ActivoMateriap));
            });
            var _mapper = new Mapper(config);

            if (materiaprincipalDto == null)
                return NotFound(new ApiResponse(404, "La materia solicitada no existe."));

            var _materia = await _imateriaprincipal.GetByIdAsync(id);
            if (_materia == null)
                return NotFound(new ApiResponse(404, "La materia solicitada no existe."));

            var _materiadto = _mapper.Map<Materium>(materiaprincipalDto);
            _imateriaprincipal.Update(_materiadto);
            await _imateriaprincipal.SaveAsync();
            return materiaprincipalDto;
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var _materia = await _imateriaprincipal.GetByIdAsync(id);
            if (_materia == null)
                return NotFound(new ApiResponse(404, "La materia que solicita no existe."));

            _imateriaprincipal.Remove(_materia);
            await _imateriaprincipal.SaveAsync();

            return NoContent();
        }

    }
}
