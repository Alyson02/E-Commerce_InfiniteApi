using Infinite.Core.Business.CQRS.Cupom.Commands;
using Infinite.Core.Business.CQRS.Cupom.Queries;
using Infinite.Core.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Infinite.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CupomController : ControllerBase
    {
        private IMediator _mediator;

        public CupomController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCupomQuery() { UsuarioId = HttpContext.User.GetUserId()}));
        }

        [HttpGet("{cupomId}")]
        public async Task<IActionResult> Get([FromRoute] string cupomId)
        {
            return Ok(await _mediator.Send(new GetCupomByIdQuery {CupomId = cupomId}));
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Create([FromBody] CreateCupomCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{cupomId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Update([FromRoute] string cupomId ,[FromBody] UpdateCupomCommand command)
        {
            if (cupomId != command.CupomId) throw new Exception("Id do cupom deve ser o mesmo do objeto enviado");
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Delete([FromRoute] string cupomId)
        {
            return Ok(await _mediator.Send(new DeleteCupomCommand { CupomId = cupomId}));
        }

    }
}
