using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Models;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Infrastructure.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infinite.Core.Business.CQRS.Carrinho.Validations;
using FluentValidation;
using Infinite.Core.Business.Services.Carrinho;

namespace Infinite.Core.Business.CQRS.Carrinho.Commands
{

    public class CreateCarrinhoCommand : IRequest<Response>
    {
        public int UserId { get; set; }
        public class CreateCarrinhoCommandHandler : IRequestHandler<CreateCarrinhoCommand, Response>
        {
            private readonly ICarrinhoService _service;
            private readonly IServiceBase<ClienteEntity> _clienteService;
            public CreateCarrinhoCommandHandler(ICarrinhoService service, IServiceBase<ClienteEntity> clienteService)
            {
                _service = service;
                _clienteService = clienteService;
            }

            public async Task<Response> Handle(CreateCarrinhoCommand command, CancellationToken cancellationToken)
            {

                try
                {
                    var carrinhoValidator = new CreateCarrinhoValidator(_service);
                    var validateCarrinhoResult = await carrinhoValidator.ValidateAsync(command);

                    if (!validateCarrinhoResult.IsValid)
                    {
                        throw new ValidationException(validateCarrinhoResult.Errors);
                    }

                    var specCliente = _clienteService.CreateSpec(x => x.UsuarioId == command.UserId);
                    var cliente = await _clienteService.FindAsync(specCliente);

                    //if (cliente is null) throw new Exception("Não conseguimos identificar você");

                    //var spec = _service.CreateSpec(x => x.ClienteId == cliente.ClienteId);
                    //var carrinhos = await _service.ListAsync(spec);

                    //var lastCarrinho = carrinhos.FirstOrDefault();


                    //if (lastCarrinho.Status == false) throw new Exception("Ultimo carrinho ainda está aberto");

                    var carrinho = new CarrinhoEntity
                    {
                        ClienteId = cliente.ClienteId,
                        Status = false,
                    };

                    await _service.InsertAsync(carrinho);

                    return new Response("Carrinho adicionado com sucesso");
                }
                catch (ValidationException ve)
                {
                    throw new ValidationException(ve.Errors);
                }
                catch (Exception e)
                {
                    throw new AppException("Erro ao fazer o cadastro do novo Carrinho", e);
                }
            }
        }
    }
}
