using Infinite.Core.Business.CQRS.Produto.Commands;
using Infinite.Core.Business.CQRS.Produto.Queries;
using Infinite.Core.Domain.Filter;
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
    public class ProdutoController : ControllerBase
    {
        private IMediator _mediator;

        public ProdutoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] ProdutoFilter filter)
        {
            var query = new GetAllProdutoQuery
            {
                filter = filter
            };
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("top4")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTop4()
        {
            return Ok(await _mediator.Send(new GetTop4Query()));
        }

        [HttpGet("{produtoId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] int produtoId)
        {
            return Ok(await _mediator.Send(new GetByIdProdutoQuerry { ProdutoId = produtoId }));
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Create([FromBody] CreateProdutoCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{produtoId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Update([FromBody] UpdateProdutoCommand command, [FromRoute] int produtoId)
        {
            if (produtoId != command.ProdutoId) throw new Exception("O id deve ser o mesmo do objeto enviado");
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{produtoId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> Delete([FromRoute] int produtoId)
        {
            return Ok(await _mediator.Send(new DeleteProdutoCommand { ProdutoId = produtoId}));
        }
    }
}
