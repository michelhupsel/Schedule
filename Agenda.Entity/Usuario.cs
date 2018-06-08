using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda.Entity
{
    public class Usuario
    {
        //atalho para criar pode ser o "pro" TAB TAB
        public int idusuario { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string sexo { get; set; }
        public int status { get; set; }
        public DateTime dataCadastro { get; set; }
        public int nivelAcesso { get; set; }
        public string email { get; set; }
        public byte[] foto { get; set; }
        public int idUsuarioLogado { get; set; }
        public string loginLogado { get; set; }
    }

    public enum Perfil
    {
        Administrador = 1,
        Operador = 2,
        Suporte = 3
    }
}
