using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Core.Service.Interface
{
    public interface ContactService
    {
        Entity.Usuario LoginUsuario(string login, string senha);
        bool CadastrarUsuario(Entity.Usuario objUsuario);
    }
}
