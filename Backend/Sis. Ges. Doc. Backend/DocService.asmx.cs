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
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public static void UploadHandler()
        {
            
        }

        public static void DownloadHandler()
        {

        }

        public static void MetadataGeneration()
        {

        }

        // ========== CRUD de la BD ==========

        private string connectionString = ConfigurationManager.ConnectionStrings["DBVanguardia"].ConnectionString;

        // INSERT
        [WebMethod]
        public string InsertarDocumento(string nombreDocumento, string descripcion, int idCategoria, int idUsuarioResponsable)
        {
            string resultado = "";
            

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarDocumento", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // TODO: Reemplazar valores de ejemplo por datos reales del documento a insertar
                    // Parámetros
                    cmd.Parameters.AddWithValue("@NombreDocumento", nombreDocumento);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("@IdUsuarioResponsable", idUsuarioResponsable);
                    cmd.Parameters.AddWithValue("@NombreArchivoOriginal", "archivo.pdf");
                    cmd.Parameters.AddWithValue("@NombreArchivoHash", "hash123");
                    cmd.Parameters.AddWithValue("@RutaArchivo", @"C:\Docs\archivo.pdf");
                    cmd.Parameters.AddWithValue("@HashDocumento", "XYZ987");
                    cmd.Parameters.AddWithValue("@FechaDocumento", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IdUsuarioModificacion", idUsuarioResponsable);

                    cmd.ExecuteNonQuery();
                    resultado = "Documento insertado correctamente.";
                }
                catch (Exception ex)
                {
                    resultado = "Error: " + ex.Message;
                }
            }

            return resultado;
        }

        // SELECT (todos)
        [WebMethod]
        public DataSet ObtenerDocumentos()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDocumentos", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            return ds;
        }

        // SELECT (por ID)
        [WebMethod]
        public DataSet ObtenerDocumentoPorId(int idDocumento)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_ObtenerDocumentoPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdDocumento", idDocumento);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            return ds;
        }

        // UPDATE
        [WebMethod]
        public string ActualizarDocumento(int idDocumento, string nombreDocumento, string descripcion, int idCategoria, int idUsuarioResponsable, int idUsuarioModificacion)
        {
            string resultado = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarDocumento", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdDocumento", idDocumento);
                    cmd.Parameters.AddWithValue("@NombreDocumento", nombreDocumento);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("@IdUsuarioResponsable", idUsuarioResponsable);
                    cmd.Parameters.AddWithValue("@IdUsuarioModificacion", idUsuarioModificacion);

                    cmd.ExecuteNonQuery();
                    resultado = "Documento actualizado correctamente.";
                }
                catch (Exception ex)
                {
                    resultado = "Error: " + ex.Message;
                }
            }
            return resultado;
        }

        // DELETE (lógico)
        [WebMethod]
        public string EliminarDocumento(int idDocumento, int idUsuarioModificacion)
        {
            string resultado = "";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarDocumento", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdDocumento", idDocumento);
                    cmd.Parameters.AddWithValue("@IdUsuarioModificacion", idUsuarioModificacion);

                    cmd.ExecuteNonQuery();
                    resultado = "Documento eliminado (inactivado) correctamente.";
                }
                catch (Exception ex)
                {
                    resultado = "Error: " + ex.Message;
                }
            }
            return resultado;
        }



    }



    public class Documento
    {
        string nombre { get; set; }
        string descripcion { get; set; }
        int categoría { get; set; }
        int usuarioResponsable { get; set; }
        string fileNameOriginal { get; set; }
        string fileNameHash { get; set; }
        string docHash { get; set; }
        DateTime fechaDoc { get; set; }
        DateTime fechaRegistro { get; set; }
        DateTime fechaUltimaModificacion { get; set; }
        int usuarioModificacion { get; set; }
        int estado { get; set; }
    }
}
