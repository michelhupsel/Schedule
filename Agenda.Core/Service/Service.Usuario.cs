using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Agenda.Entity;

namespace Agenda.Core.Service
{
    public class Service : Interface.ContactService
    {
        public Entity.Usuario LoginUsuario(string login, string senha)
        {
            try
            {
                return new Repository.Repository().LoginUsuario(login, senha);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }

            return new Entity.Usuario();
        }

        public bool CadastrarUsuario(Entity.Usuario objUsuario)
        {
            try
            {
                return new Repository.Repository().CadastrarUsuario(objUsuario);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }
            return false;
        }

        public bool ChecarUsuario(string login)
        {
            try
            {
                var ret = new Repository.Repository();
                var obj = new Entity.Usuario() { nome = login };

                if (ret.ChecarUsuario(login).idusuario > 0)
                    return true;

                //return ret2;

                //return new Repository.Repository().ChecarUsuario(login);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }
            // return new Entity.Usuario();

            return false;
        }

    }
}
