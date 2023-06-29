using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Core.Entidades.Core;
using Core.Interfaces.Core;
using Servicios_Zeus.Helpers.Errors;
using Servicios_Zeus.Helpers;
using Core.Dtos.Core;
using AutoMapper;
using Infraestructure.Mappers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace Servicios_Zeus.Controllers.Core
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/componente")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ComponenteController : ControllerBase
    {
        private readonly IComponenteRepository _icomponente;
        private readonly IMapper _mapper;
        public ComponenteController(IComponenteRepository componenterepo, IMapper mapper)
        {
            _icomponente = componenterepo;
            _mapper = mapper;
        }
        [Route("Save")]
        [HttpPost]
        public async Task<ActionResult<Componente>> SaveComponente([FromBody] ComponenteDto componenteDto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ComponenteDto, Componente>();
            });
            var _mapper = new Mapper(config);
            var _componente = _mapper.Map<Componente>(componenteDto);
            _icomponente.Add(_componente);
            await _icomponente.SaveAsync();
            if (componenteDto == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            componenteDto.IdComponente = _componente.IdComponente;
            return CreatedAtAction(nameof(SaveComponente), new { IdComponente = componenteDto.IdComponente });

        }
    }
}
