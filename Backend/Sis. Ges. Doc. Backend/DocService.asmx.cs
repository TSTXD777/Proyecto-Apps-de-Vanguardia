using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Security.Cryptography;

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
    public class DocService : System.Web.Services.WebService
    {

        [WebMethod]
        public static string MetadataGeneration(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath must be provided.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.", filePath);

            using (SHA256 sha256 = SHA256.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                byte[] hash = sha256.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        // ========== CRUD de la BD ==========

        private string connectionString = ConfigurationManager.ConnectionStrings["DBVanguardia"].ConnectionString;

        // INSERT
        [WebMethod]
        public string InsertarDocumento(string filePath, string nombreDocumento, string descripcion, int idCategoria, int idUsuarioResponsable)
        {
            string resultado = "";

            try
            {
                // Compute SHA256 hash and file naming
                string fileNameOriginal = Path.GetFileName(filePath);
                string sha256Hash = MetadataGeneration(filePath);
                string baseName = Path.GetFileNameWithoutExtension(fileNameOriginal);
                string extension = Path.GetExtension(fileNameOriginal);
                string nombreArchivoHash = string.Format("{0}_{1}{2}", baseName, sha256Hash, extension);
                string rutaArchivo = "/Uploads/" + nombreArchivoHash;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarDocumento", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Parámetros
                    cmd.Parameters.AddWithValue("@NombreDocumento", nombreDocumento);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("@IdUsuarioResponsable", idUsuarioResponsable);
                    cmd.Parameters.AddWithValue("@NombreArchivoOriginal", fileNameOriginal);
                    cmd.Parameters.AddWithValue("@NombreArchivoHash", nombreArchivoHash);
                    cmd.Parameters.AddWithValue("@RutaArchivo", rutaArchivo);
                    cmd.Parameters.AddWithValue("@HashDocumento", sha256Hash);
                    cmd.Parameters.AddWithValue("@FechaDocumento", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IdUsuarioModificacion", idUsuarioResponsable);

                    cmd.ExecuteNonQuery();
                    resultado = "Documento insertado correctamente.";
                }
            }
            catch (Exception ex)
            {
                resultado = "Error: " + ex.Message;
            }

            return resultado;
        }

        // SELECT
        [WebMethod]
        public DataSet ObtenerDocumentos()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerDocumentos", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (SqlException ex)
            {
                // Log error (e.g., to file, DB, or monitoring system)
                // Optionally rethrow or wrap in a custom exception
                throw new Exception("Error retrieving documents.", ex);
            }
            return ds;
        }

        // SELECT (por ID)
        [WebMethod]
        public DataSet ObtenerDocumentoPorId(int idDocumento)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerDocumentoPorId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdDocumento", idDocumento);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Error retrieving document with ID {idDocumento}.", ex);
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
