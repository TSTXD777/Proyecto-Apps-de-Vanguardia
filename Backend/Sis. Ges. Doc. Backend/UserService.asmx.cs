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
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {

        [WebMethod]
        public string PasswordEncrypt()
        {
            return "Hash en SHA256";
        }

        // ========== CRUD de la BD ==========

        private string connectionString = ConfigurationManager.ConnectionStrings["DBVanguardia"].ConnectionString;

        // INSERT
        [WebMethod]
        public string InsertarCategoria(string nombreCategoria, string descripcion)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreCategoria", nombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                    cmd.ExecuteNonQuery();
                    return "Categoría insertada correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error al insertar categoría: " + ex.Message;
            }
        }

        // READ ALL
        [WebMethod]
        public DataSet ObtenerCategorias()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerCategorias", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                // In case of error, return empty dataset with error info
                DataTable dt = new DataTable("Error");
                dt.Columns.Add("Message");
                dt.Rows.Add("Error al obtener categorías: " + ex.Message);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        // READ BY ID
        [WebMethod]
        public DataSet ObtenerCategoriaPorId(int idCategoria)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerCategoriaPorId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable("Error");
                dt.Columns.Add("Message");
                dt.Rows.Add("Error al obtener categoría: " + ex.Message);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        // UPDATE
        [WebMethod]
        public string ActualizarCategoria(int idCategoria, string nombreCategoria, string descripcion)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarCategoria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("@NombreCategoria", nombreCategoria);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                    cmd.ExecuteNonQuery();
                    return "Categoría actualizada correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar categoría: " + ex.Message;
            }
        }

        // DELETE (Lógico)
        [WebMethod]
        public string EliminarCategoria(int idCategoria)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    cmd.ExecuteNonQuery();
                    return "Categoría eliminada (inactivada) correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error al eliminar categoría: " + ex.Message;
            }
        }

    }

    public class Usuario
    {
        string completo { get; set; }
        string correo { get; set; }
        string passwordHash { get; set; }
        int departamento { get; set; }
        string rol { get; set; }
        int estado { get; set; }
        DateTime fechaCrecion { get; set; }
    }
}
