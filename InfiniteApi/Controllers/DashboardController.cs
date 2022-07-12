using Infinite.Core.Business.CQRS.Dashboard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Infinite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Master")]
    public class DashboardController : ControllerBase
    {
        private IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("contadores")]
        public async Task<IActionResult> GetContadores()
        {
            return Ok(await _mediator.Send(new GetContadoresQuery()));
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            return Ok(await _mediator.Send(new GetCategoriasQuery()));
        }

        [HttpGet("top10")]
        public async Task<IActionResult> GetTop10()
        {
            return Ok(await _mediator.Send(new GetTop10Query()));
        }
    }
}
