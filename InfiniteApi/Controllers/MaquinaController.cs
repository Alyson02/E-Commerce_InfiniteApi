using Infinite.Core.Business.CQRS.Maquina.Commands;
using Infinite.Core.Business.CQRS.Maquina.Queries;
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
    public class MaquinaController : ControllerBase
    {

        private IMediator _mediator;
        public MaquinaController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        
        // Bloco de consulta
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllMaquinaQuerry()));
        }

        [HttpGet("{idMaquina}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetById([FromRoute] int idMaquina)
        {

            return Ok(await _mediator.Send(new GetByIdMaquinaQuerry { MaquinaId = idMaquina }));
        }


        // Bloco de consulta

        // Bloco de inserção
        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Create([FromBody] CreateMaquinaCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        // Bloco de inserção

        // Bloco de Atualização
        [Authorize(Roles = "Master")]
        [HttpPut("{MaquinaId}")]
        public async Task<IActionResult> Update([FromRoute] int MaquinaId, UpdateMaquinaCommand command)
        {
            if (MaquinaId != command.MaquinaId) throw new Exception("O ID informado deve ser o mesmo do Objeto");
            return Ok(await _mediator.Send(command));
        }
        // Bloco de Atualização

        // Bloco de Deletação
        [Authorize(Roles = "Master")]
        [HttpDelete("{MaquinaId}")]
        public async Task<IActionResult> Delete([FromRoute] int MaquinaId)
        {

            return Ok(await _mediator.Send(new DeleteMaquinaCommand { MaquinaId = MaquinaId }));
        }
        // Bloco de Deletação

    }
}


// Queries

