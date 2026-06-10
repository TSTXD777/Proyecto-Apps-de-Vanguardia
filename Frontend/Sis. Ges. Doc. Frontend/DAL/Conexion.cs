using System.Configuration;
using System.Data.SqlClient;

namespace Sis.Ges.Doc.Frontend.DAL
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager
                .ConnectionStrings["VanguardiaDB"]
                .ConnectionString;

            return new SqlConnection(cadena);
        }
    }
}