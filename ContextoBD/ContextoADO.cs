using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ContextoBD
{
    public class ContextoADO : IContextoBD, IDisposable
    {
        private readonly SqlConnection Conexao;

        public ContextoADO(string nome)
        {
            Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings[nome].ConnectionString);
            Conexao.Open();
        }

        public SqlTransaction BeginTransaction()
        {
            return Conexao.BeginTransaction();
        }

        public virtual int ExecutaComando(string strQuery, SqlCommand com = null, bool retornarId = false)
        {
            if (com == null)
            {
                com = new SqlCommand();
            }

            com.CommandText = strQuery;
            com.CommandType = CommandType.Text;
            com.Connection = Conexao;

            try
            {
                int valor = retornarId ? Convert.ToInt32(com.ExecuteScalar()) : com.ExecuteNonQuery();
                int id = retornarId ? valor : -1;
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual SqlDataReader ExecutaComandoComRetorno(string strQuery, SqlCommand com = null)
        {
            if (com == null)
            {
                com = new SqlCommand();
            }

            com.CommandText = strQuery;
            com.CommandType = CommandType.Text;
            com.Connection = Conexao;

            return com.ExecuteReader();
        }

        public void Dispose()
        {
            if (Conexao.State == ConnectionState.Open)
                Conexao.Close();
        }
    }
}
