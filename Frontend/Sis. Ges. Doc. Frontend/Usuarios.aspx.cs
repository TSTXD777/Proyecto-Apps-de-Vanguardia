using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Sis.Ges.Doc.Frontend.DAL;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["Rol"] == null ||
                Session["Rol"].ToString().ToUpper() != "ADMIN")
            {
                Response.Redirect("Dashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    cn.Open();

                    string sql = @"
                        SELECT
                            IdUsuario,
                            NombreCompleto,
                            Usuario,
                            Correo,
                            Rol,
                            CASE
                                WHEN Estado = 1 THEN 'Activo'
                                ELSE 'Inactivo'
                            END AS Estado
                        FROM Usuarios";

                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvUsuarios.DataSource = dt;
                    gvUsuarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(
                    "<script>alert('Error: " +
                    ex.Message.Replace("'", "") +
                    "');</script>");
            }
        }
    }
}