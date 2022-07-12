using Infinite.Core.Business.CQRS.Agendamento.Commands;
using Infinite.Core.Business.CQRS.Agendamento.Queries;
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
    [Authorize]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {

        private IMediator _mediator;
        public AgendamentoController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        // Bloco de consulta
        [HttpGet]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllAgendamentoQuerry()));
        }

        [HttpGet("Usuario")]
        public async Task<IActionResult> GetAgendaAtual()
        {
            var usuarioId = HttpContext.User.GetUserId();
            return Ok(await _mediator.Send(new GetAgendaAtualQuery { UserId = usuarioId }));
        }

        [HttpGet("{idAgendamento}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetById([FromRoute] int idAgendamento)
        {
            return Ok(await _mediator.Send(new GetByIdAgendamentoQuerry { AgendamentoId = idAgendamento }));
        }


        // Bloco de consulta

        // Bloco de inserção
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAgendamentoCommand command)
        {
            command.UsuarioId = HttpContext.User.GetUserId();
            return Ok(await _mediator.Send(command));
        }
        // Bloco de inserção

        // Bloco de Atualização
        [HttpPut("{AgendamentoId}")]
        public async Task<IActionResult> Update([FromRoute] int AgendamentoId, UpdateAgendamentoCommand command)
        {
            if (AgendamentoId != command.AgendamentoId) throw new Exception("O ID informado deve ser o mesmo do Objeto");
            return Ok(await _mediator.Send(command));
        }
        // Bloco de Atualização

        // Bloco de Deletação
        [HttpDelete("{AgendamentoId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Delete([FromRoute] int AgendamentoId)
        {

            return Ok(await _mediator.Send(new DeleteAgendamentoCommand { AgendamentoId = AgendamentoId }));
        }
        // Bloco de Deletação

    }
}
