using Infinite.Core.Business.CQRS.Carrinho.Commands;
using Infinite.Core.Business.CQRS.Carrinho.Queries;
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
    public class CarrinhoController : ControllerBase
    {
        private IMediator _mediator;

        public CarrinhoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = HttpContext.User.GetUserId();
            return Ok(await _mediator.Send(new GetAllProdutoQuery { UserId = userId }));
        }


        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var userId = HttpContext.User.GetUserId();
            return Ok(await _mediator.Send(new CreateCarrinhoCommand { UserId = userId }));
        }

        [HttpPost("AddProduto")]
        public async Task<IActionResult> AdicionarProduto([FromBody] AdicionaProdutoCommand command)
        {
            command.UserId = HttpContext.User.GetUserId(); ;
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("FecharCarrinho")]
        public async Task<IActionResult> FecharCarrinho([FromBody] FecharCarrinhoCommand command)
        {
            command.UserId = HttpContext.User.GetUserId();
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("RemoveProduto/{produtoId}")]
        public async Task<IActionResult> RemoverProduto([FromRoute] int produtoId)
        {
            return Ok(await _mediator.Send( new RemoverProdutoCommand { ProdutoId = produtoId, UserId = HttpContext.User.GetUserId() } ));
        }

        //Apenas ADM
        [HttpGet("Carrinhos")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetAllCarrinho()
        {
            //if (User.GetUserRole() != "Master") return Forbid();
            return Ok(await _mediator.Send(new GetAllCarrinhoQuery()));
        }

        [HttpGet("Carrinhos/{carrinhoId}")]
        [Authorize(Roles = "Master")]
        public async Task<IActionResult> GetAllCarrinho([FromRoute] int carrinhoId)
        {
            return Ok(await _mediator.Send(new GetByIdCarrinhoQuerry { CarrinhoId = carrinhoId }));
        }
    }
}
