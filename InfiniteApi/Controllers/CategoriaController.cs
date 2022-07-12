using Infinite.Core.Business.CQRS.Categoria.Commands;
using Infinite.Core.Business.CQRS.Categoria.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Infinite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetlAllCategoriaQuery()));
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Create([FromBody] CreateCategoriaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Create([FromBody] UpdateCategoriaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
