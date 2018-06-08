using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Core.Repository.Interface
{
    public interface ContactRepository
    {
        Entity.Usuario LoginUsuario(string login, string senha);

        bool CadastrarUsuario(Entity.Usuario objUsuario);

        Entity.Usuario ChecarUsuario(string login);
    }
}
