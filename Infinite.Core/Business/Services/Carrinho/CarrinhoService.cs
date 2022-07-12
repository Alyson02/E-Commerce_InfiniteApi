using Infinite.Core.Business.Services.Base;
using Infinite.Core.Domain.Entities;
using Infinite.Core.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.Services.Carrinho
{
    public class CarrinhoService : ServiceBase<CarrinhoEntity>, ICarrinhoService
    {
        public CarrinhoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
