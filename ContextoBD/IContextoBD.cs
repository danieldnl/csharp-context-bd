using System.Data.SqlClient;

namespace ContextoBD
{
    public interface IContextoBD
    {
        SqlTransaction BeginTransaction();
        int ExecutaComando(string strQuery, SqlCommand com = null, bool retornarId = false);
        SqlDataReader ExecutaComandoComRetorno(string strQuery, SqlCommand com = null);
        void Dispose();
    }
}
