using Infinite.Core.Business.CQRS.Cliente.Commands;
using Infinite.Core.Business.CQRS.Cliente.Queries;
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
    public class ClienteController : ControllerBase
    {

        private IMediator _mediator;
        public ClienteController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllClienteQuerry()));
        }

        [HttpGet("{idCliente}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetById([FromRoute] int idCliente)
        {

            return Ok(await _mediator.Send(new GetByIdClienteQuerry { ClienteId = idCliente}));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClienteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{clienteId}")]
        public async Task<IActionResult> Update([FromRoute] int clienteId, UpdateClienteCommand command)
        {
            if (clienteId != command.ClienteId) throw new Exception("O ID informado deve ser o mesmo do Objeto");
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{clienteId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Delete([FromRoute] int clienteId)
        {
            return Ok(await _mediator.Send(new DeleteClienteCommand { ClienteId = clienteId }));
        }

        [HttpGet("Endereco")]
        public async Task<IActionResult> GetAllEnderecos()
        {
            return Ok(await _mediator.Send(new GetAllEnderecosClienteQuery { UserId = HttpContext.User.GetUserId() }));
        }

        [HttpPost("Endereco")]
        public async Task<IActionResult> CreateEndereco([FromBody] AdicionarEnderecoCommand command)
        {
            command.UserId = HttpContext.User.GetUserId();
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("Cartao")]
        public async Task<IActionResult> GetAllCartoes()
        {
            return Ok(await _mediator.Send(new GetAllCartoesClienteQuery { UserId = HttpContext.User.GetUserId() }));
        }

        [HttpPost("Cartao")]
        public async Task<IActionResult> CreateCartao([FromBody] AdicionarCartaoCommand command)
        {
            command.UserId = HttpContext.User.GetUserId();
            return Ok(await _mediator.Send(command));
        }
    }
}
