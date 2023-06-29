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
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/empleado")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoRepository _iempleado;
        private readonly IMapper _mapper;

        public EmpleadoController(IEmpleadoRepository empleadorepo, IMapper mapper)
        {
            _iempleado = empleadorepo;
            _mapper = mapper;
        }

        [Route("listarPage")]
        [HttpGet]
        public async Task<ActionResult<Pager<Empleado>>> GetAllEmpleados([FromQuery] Params empParams)
        {
            var emp = await _iempleado.GetAllAsync(empParams.PageIndex, empParams.PageSize,
                                    empParams.Search);
            return new Pager<Empleado>(emp.registros, emp.totalRegistros,
            empParams.PageIndex, empParams.PageSize, empParams.Search);
        }

        [Route("listar")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetAllEmpleado()
        {
            var emp = await _iempleado.GetAllAsync();
            if (emp == null)
                return NotFound(new ApiResponse(404, "La lista no contiene ningún elemento."));
            return Ok(emp);
        }

        [Route("ver/{id}")]
        [HttpGet]
        public async Task<ActionResult<Empleado>> GetEmpleadobyId(int id)
        {
            var emp = await _iempleado.GetByIdAsync(id);
            if (emp == null)
                return NotFound(new ApiResponse(404));
            return Ok(emp);
        }

        [Route("crear")]
        [HttpPost]
        public async Task<ActionResult<Empleado>> SaveEmpleado([FromBody] EmpleadoDto empdto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmpleadoDto, Empleado>();
            });
            var _mapper = new Mapper(config);
            var _emp = _mapper.Map<Empleado>(empdto);
            _iempleado.Add(_emp);
            await _iempleado.SaveAsync();
            if (empdto == null)
            {
                return BadRequest(new ApiResponse(400));
            }
            empdto.IdEmp = _emp.IdEmp;
            return CreatedAtAction(nameof(SaveEmpleado), new { IdEmp = empdto.IdEmp });
        }

        [Route("editar/{id}")]
        [HttpPut]
        public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody] EmpleadoDto empdto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmpleadoDto, Empleado>();
            });
            var _mapper = new Mapper(config);

            if (empdto == null)
                return NotFound(new ApiResponse(404, "EL Empleado solicitado no existe."));

            var _emp = await _iempleado.GetByIdAsync(id);
            if (_emp == null)
                return NotFound(new ApiResponse(404, "EL Empleado solicitado no existe."));

            var _empdto = _mapper.Map<Empleado>(empdto);
            _iempleado.Update(_empdto);
            await _iempleado.SaveAsync();
            return empdto;
        }

        [Route("eliminar/{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var _emp = await _iempleado.GetByIdAsync(id);
            if (_emp == null)
                return NotFound(new ApiResponse(404, "El Empleado que solicita no existe."));

            _iempleado.Remove(_emp);
            await _iempleado.SaveAsync();

            return NoContent();
        }
    }

}
