using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Sis.Ges.Doc.Backend
{
    /// <summary>
    /// Summary description for BitacoraService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BitacoraService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetUserIPAddress()
        {
            return "192.1.0.16";
        }

        //============ SELECTS de la BD ========== https://localhost:44331/BitacoraService.asmx

        private string connectionString = ConfigurationManager.ConnectionStrings["DBVanguardia"].ConnectionString;

        [WebMethod]
        public DataSet ObtenerBitacora()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerBitacora", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                // Return error info in a DataTable for consistency
                DataTable dtError = new DataTable("Error");
                dtError.Columns.Add("Message", typeof(string));
                dtError.Rows.Add(ex.Message);
                ds.Tables.Add(dtError);
            }

            return ds;
        }

        [WebMethod]
        public DataSet ObtenerBitacoraPorId(int idBitacora)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ObtenerBitacoraPorId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parameter
                    cmd.Parameters.AddWithValue("@IdBitacora", idBitacora);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                DataTable dtError = new DataTable("Error");
                dtError.Columns.Add("Message", typeof(string));
                dtError.Rows.Add(ex.Message);
                ds.Tables.Add(dtError);
            }

            return ds;
        }
    }

    public class Bitacora
    {
        int documentoId { get; set; }
        int usuarioId { get; set; }
        string operacion { get; set; }
        string datosAnteriores { get; set; }
        string datosNuevos { get; set; }
        string ipUsuario { get; set; }
        DateTime fechaOperacion { get; set; }
    }
}
