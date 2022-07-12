using Infinite.Core.Business.CQRS.Funcionario.Commands;
using Infinite.Core.Business.CQRS.Funcionario.Queries;
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
    [Authorize(Roles = "Master")]
    public class FuncionarioController : ControllerBase
    {
        private IMediator _mediator;

        public FuncionarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllFuncionarioQuery()));
        }

        [HttpGet("{funcionarioId}")]
        public async Task<IActionResult> Get([FromRoute] int funcionarioId)
        {
            return Ok(await _mediator.Send(new GetByIdFuncionarioQuerry { FuncionarioId = funcionarioId }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFuncionarioCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{funcionarioId}")]
        public async Task<IActionResult> Update([FromBody] UpdateFuncionarioCommand command, [FromRoute] int funcionarioId)
        {
            if (funcionarioId != command.FuncionarioId) throw new Exception("O id deve ser o mesmo do objeto enviado");
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{funcionarioId}")]
        public async Task<IActionResult> Delete([FromRoute] int funcionarioId)
        {
            return Ok(await _mediator.Send(new DeleteFuncionarioCommand { FuncionarioId = funcionarioId}));
        }
    }
}
