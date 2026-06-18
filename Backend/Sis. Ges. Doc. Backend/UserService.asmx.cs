using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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

        //============ CRUD de la BD ========== https://localhost:44331/UserService.asmx

        private string connectionString = ConfigurationManager.ConnectionStrings["DBVanguardia"].ConnectionString;

        private static string EnsureSha256Hash(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && Regex.IsMatch(value, "^[a-fA-F0-9]{64}$"))
            {
                return value.ToLowerInvariant();
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value ?? string.Empty));
                StringBuilder builder = new StringBuilder(bytes.Length * 2);

                foreach (byte item in bytes)
                {
                    builder.Append(item.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // INSERT
        [WebMethod]
        public string InsertarUsuario(string nombreCompleto, string correo, string usuario, string passwordHash, int idDepartamento, string rol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarUsuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@PasswordHash", EnsureSha256Hash(passwordHash));
                    cmd.Parameters.AddWithValue("@IdDepartamento", idDepartamento);
                    cmd.Parameters.AddWithValue("@Rol", rol);

                    cmd.ExecuteNonQuery();
                    return "Usuario insertado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error al insertar usuario: " + ex.Message;
            }
        }

        // READ ALL
        [WebMethod]
        public DataSet ObtenerUsuarios()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarios", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                // Devuelve un DataSet vacío con un mensaje de error
                DataTable dt = new DataTable("Error");
                dt.Columns.Add("Mensaje");
                dt.Rows.Add("Error al obtener usuarios: " + ex.Message);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        public DataSet ObtenerUsuariosFull()
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerUsuariosFull", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                // Devuelve un DataSet vacío con un mensaje de error
                DataTable dt = new DataTable("Error");
                dt.Columns.Add("Mensaje");
                dt.Rows.Add("Error al obtener usuarios: " + ex.Message);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        // READ BY ID
        [WebMethod]
        public DataSet ObtenerUsuarioPorId(int idUsuario)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_ObtenerUsuarioPorId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable("Error");
                dt.Columns.Add("Mensaje");
                dt.Rows.Add("Error al obtener usuario: " + ex.Message);
                ds.Tables.Add(dt);
            }
            return ds;
        }

        // UPDATE
        [WebMethod]
        public string ActualizarUsuario(int idUsuario, string nombreCompleto, string correo, string usuario, string passwordHash, int idDepartamento, string rol)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarUsuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Usuario", usuario);
                    cmd.Parameters.AddWithValue("@PasswordHash", EnsureSha256Hash(passwordHash));
                    cmd.Parameters.AddWithValue("@IdDepartamento", idDepartamento);
                    cmd.Parameters.AddWithValue("@Rol", rol);

                    cmd.ExecuteNonQuery();
                    return "Usuario actualizado correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error al actualizar usuario: " + ex.Message;
            }
        }

        // DELETE (Lógico)
        [WebMethod]
        public string EliminarUsuario(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    cmd.ExecuteNonQuery();
                    return "Usuario eliminado (inactivado) correctamente.";
                }
            }
            catch (Exception ex)
            {
                return "Error al eliminar usuario: " + ex.Message;
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
