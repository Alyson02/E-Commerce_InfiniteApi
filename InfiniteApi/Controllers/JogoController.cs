using Infinite.Core.Business.CQRS.Jogo.Commands;
using Infinite.Core.Business.CQRS.Jogo.Queries;
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
    public class JogoController : ControllerBase
    {

        private IMediator _mediator;
        public JogoController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // Bloco de consulta
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllJogoQuerry()));
        }
        [HttpGet("{idJogo}")]
        public async Task<IActionResult> GetById([FromRoute] int idJogo)
        {
            return Ok(await _mediator.Send(new GetByIdJogoQuerry { JogoId = idJogo }));
        }


        // Bloco de consulta

        // Bloco de inserção
        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Create([FromBody] CreateJogoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        // Bloco de inserção

        // Bloco de Atualização
        [HttpPut("{JogoId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Update([FromRoute] int JogoId, UpdateJogoCommand command)
        {
            if (JogoId != command.JogoId) throw new Exception("O ID informado deve ser o mesmo do Objeto");
            return Ok(await _mediator.Send(command));
        }
        // Bloco de Atualização

        // Bloco de Deletação
        [HttpDelete("{JogoId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Delete([FromRoute] int JogoId)
        {

            return Ok(await _mediator.Send(new DeleteJogoCommand { JogoId = JogoId }));
        }
        // Bloco de Deletação

    }
}