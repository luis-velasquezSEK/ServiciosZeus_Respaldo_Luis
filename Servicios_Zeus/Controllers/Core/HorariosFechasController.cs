using AutoMapper;
using Core.Dtos.Core;
using Core.Entidades.Core;
using Core.Interfaces.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios_Zeus.Helpers.Errors;

namespace Servicios_Zeus.Controllers.Core
{
    [ApiVersion("1.0")]
    [Route("api/HorarioFechas")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class HorariosFechasController : ControllerBase
    {

        private readonly IHorarioRepository _ihorario;
        private readonly IMapper _mapper;
        public HorariosFechasController(IHorarioRepository horariorepo, IMapper mapper)
        {
            _ihorario = horariorepo;
            _mapper = mapper;
        }

        [Route("Save")]
        [HttpPost]
        public async Task<ActionResult<List<HorarioFechaDto>>> SaveHorarios([FromBody] List<HorarioFechaDto> horarios)
        {

            List<HorarioFechaDto> ID_PLANIFICACION = new List<HorarioFechaDto>();

            foreach (var horario in horarios)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<HorarioFechaDto, HorarioFecha>();
                });

                var _mapper = new Mapper(config);
                var _horario = _mapper.Map<HorarioFecha>(horario);
                _ihorario.Add(_horario);
                await _ihorario.SaveAsync();
                if (horario == null)
                {
                    return BadRequest(new ApiResponse(400));
                }
                horario.IdPlanificacion = _horario.IdPlanificacion;
                ID_PLANIFICACION.Add(new HorarioFechaDto { IdPlanificacion = horario.IdPlanificacion });
            }

            return CreatedAtAction(nameof(SaveHorarios), ID_PLANIFICACION);
        }
    }
}
