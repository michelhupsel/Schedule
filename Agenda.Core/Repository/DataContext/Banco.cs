using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Agenda.Core.Repository.DataContext
{
    public class Banco
    {
        private string StringConection = System.Configuration.ConfigurationManager.AppSettings["connection"].ToString();
        private MySqlConnection MysConnection;
        private MySqlTransaction Transaction;
        private DataSet DataSets;
        private int Rows = 0;

        // Verifica e aber a conexão e montar a transação. 
        private bool Connection()
        {
            try
            {
                MysConnection = new MySqlConnection(StringConection);

                if (ConnectionState.Closed == MysConnection.State)
                {
                    MysConnection.Open();
                    Transaction = MysConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                    return true;
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }
            return false;
        }
        // Receber a quer,y somente consulta.
        public DataSet Read(string sql, MySqlParameter[] parametros = null)
        {
            try
            {
                Connection();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Transaction = Transaction;
                    cmd.Connection = MysConnection;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    if (parametros != null)
                        foreach (var param in parametros)
                        {
                            cmd.Parameters.Add(param);
                        }

                    MySqlDataAdapter DataAdapter = new MySqlDataAdapter(cmd);
                    DataSets = new DataSet();
                    DataAdapter.Fill(DataSets);
                }
            }
            catch (Exception ex)
            {
                new Exception(ex.Message.ToString());
            }
            finally
            {
                Close();
            }

            return DataSets;
        }

        public bool Execute(string sql, MySqlParameter[] parametros = null)
        {
            try
            {
                Connection();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Transaction = Transaction;
                    cmd.Connection = MysConnection;
                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    if (parametros != null)
                        foreach (var param in parametros)
                        {
                            cmd.Parameters.Add(param);
                        }

                    Rows = cmd.ExecuteNonQuery();
                    if (Rows > 0)
                    {
                        Transaction.Commit();
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                try
                {
                    Transaction.Rollback();
                }
                catch (Exception exx)
                {
                    new Exception(exx.Message.ToString());
                }
                finally
                {
                    Close();
                    Rows = 0;
                    sql = null;
                    parametros = null;
                }
            }


            return false;
        }

        private void Close()
        {
            if (ConnectionState.Open == MysConnection.State)
            {
                MysConnection.Close();
            }
        }

    }
}
