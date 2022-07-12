using Infinite.Core.Domain.Entities;
using Infinite.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinite.Core.Business.Services.Token
{
    public interface ITokenService
    {
        UsuarioToken GerarToken(ClienteEntity clienteEntity, UsuarioEntity userInfo);
    }
}
