using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agenda.Entity;
using MySql.Data.MySqlClient;

namespace Agenda.Core.Repository
{
    public class Repository : Interface.ContactRepository
    {
        private Entity.Usuario objUsuario;
        private StringBuilder sqlquery;
        private List<MySqlParameter> paramentos;

        // Consulta
        public Entity.Usuario LoginUsuario(string login, string senha)
        {
            try
            {
                sqlquery = new StringBuilder();
                objUsuario = new Entity.Usuario();
                paramentos = new List<MySqlParameter>();

                sqlquery.Append("SELECT IDUSUARIO, NOME,LOGIN, SENHA, STATUS, NIVEL FROM USUARIO  WHERE LOGIN = @login AND SENHA = @senha");

                paramentos.Add(new MySqlParameter() { ParameterName = "@login", Value = login.ToUpper().Trim() });
                paramentos.Add(new MySqlParameter() { ParameterName = "@senha", Value = senha });

                var dataSet = new DataContext.Banco().Read(sqlquery.ToString(), paramentos.ToArray());

                if (dataSet.Tables.Count > 0)
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        objUsuario.idusuario = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                        objUsuario.nome = Convert.ToString(dataSet.Tables[0].Rows[0][1]);
                        objUsuario.login = Convert.ToString(dataSet.Tables[0].Rows[0][2]);
                        objUsuario.senha = Convert.ToString(dataSet.Tables[0].Rows[0][3]);
                        objUsuario.status = Convert.ToInt32(dataSet.Tables[0].Rows[0][4]);
                        objUsuario.nivelAcesso = Convert.ToInt32(dataSet.Tables[0].Rows[0][5]);
                    }

                return objUsuario;

            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }

            return new Entity.Usuario();
        }

        // Checar usuario antes de cadastrar
        public Entity.Usuario ChecarUsuario(string login)
        {
            try
            {
                sqlquery = new StringBuilder();
                objUsuario = new Entity.Usuario();
                paramentos = new List<MySqlParameter>();

                sqlquery.Append("SELECT IDUSUARIO, NOME,LOGIN, SENHA, STATUS, NIVEL FROM USUARIO  WHERE LOGIN = @login ");

                paramentos.Add(new MySqlParameter() { ParameterName = "@login", Value = login.ToUpper().Trim() });
              
                var dataSet = new DataContext.Banco().Read(sqlquery.ToString(), paramentos.ToArray());

                if (dataSet.Tables.Count > 0)
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        objUsuario.idusuario = Convert.ToInt32(dataSet.Tables[0].Rows[0][0]);
                        objUsuario.nome = Convert.ToString(dataSet.Tables[0].Rows[0][1]);
                        objUsuario.login = Convert.ToString(dataSet.Tables[0].Rows[0][2]);
                        objUsuario.senha = Convert.ToString(dataSet.Tables[0].Rows[0][3]);
                        objUsuario.status = Convert.ToInt32(dataSet.Tables[0].Rows[0][4]);
                        objUsuario.nivelAcesso = Convert.ToInt32(dataSet.Tables[0].Rows[0][5]);
                    }

                return objUsuario;

            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }

            return new Entity.Usuario();
        }


        // Insert
        public bool CadastrarUsuario(Entity.Usuario objUsuario)
        {
            try
            {
                sqlquery = new StringBuilder();
                //objUsuario = new Entity.Usuario();
                paramentos = new List<MySqlParameter>();

                sqlquery.Append(@" INSERT INTO usuario (NOME, SOBRENOME, LOGIN, SENHA, SEXO, STATUS, DTCADASTRO, NIVEL, EMAIL, FOTO)  ");
                sqlquery.Append(@"  VALUES  ");
                sqlquery.Append(@" (@nome,@sobrenome, @login, @senha, @sexo, @status, CURRENT_TIMESTAMP, @nivel, @email, @foto) ");

                paramentos.Add(new MySqlParameter() { ParameterName = "@nome", Value = objUsuario.nome.ToUpper().Trim() });
                paramentos.Add(new MySqlParameter() { ParameterName = "@sobrenome", Value = objUsuario.sobrenome.ToUpper() });
                paramentos.Add(new MySqlParameter() { ParameterName = "@login", Value = objUsuario.login.ToUpper() });

                paramentos.Add(new MySqlParameter() { ParameterName = "@senha", Value = objUsuario.senha });
                paramentos.Add(new MySqlParameter() { ParameterName = "@sexo", Value = objUsuario.sexo.ToUpper() });
                paramentos.Add(new MySqlParameter() { ParameterName = "@status", Value = objUsuario.status });
                paramentos.Add(new MySqlParameter() { ParameterName = "@nivel", Value = objUsuario.nivelAcesso });


                paramentos.Add(new MySqlParameter() { ParameterName = "@email", Value = objUsuario.email });
                paramentos.Add(new MySqlParameter() { ParameterName = "@foto", Value = objUsuario.foto  });

              

                var retorno = new DataContext.Banco().Execute(sqlquery.ToString(), paramentos.ToArray());

                return retorno;

            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }

            return false;
        }
    }
}
