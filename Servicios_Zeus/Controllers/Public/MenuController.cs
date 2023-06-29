using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core.Interfaces.Public;
using Core.Dtos.Public;

namespace Servicios_Zeus.Controllers.Public
{
    [Authorize]
    [Route("api/menu")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MenuController : ControllerBase
    {
        IMenuRepository _aplicacionRepository;
        public MenuController(IMenuRepository aplicacionRepository)
        {
            _aplicacionRepository = aplicacionRepository;
        }
        [Route("GetByUser/{username}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenuByUser(string username)
        {
            var result =await _aplicacionRepository.menuUsername(username);
            return Ok(result);
        }

        [Route("findMenuItems/{username}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> findMenuItems(string username)
        {
            var result =await _aplicacionRepository.findMenuItems(username);
            return Ok(result);
        }

    }
}
