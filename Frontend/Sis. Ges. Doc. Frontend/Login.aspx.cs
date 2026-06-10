using System;
using System.Data.SqlClient;
using System.Web.UI;
using Sis.Ges.Doc.Frontend.DAL;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginUser(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    cn.Open();

                    string sql = @"SELECT Rol
                           FROM Usuarios
                           WHERE Usuario = @Usuario
                           AND PasswordHash = @Password
                           AND Estado = 1";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@Usuario", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());

                    object resultado = cmd.ExecuteScalar();

                    if (resultado != null)
                    {
                        Session["Usuario"] = txtUser.Text.Trim();
                        Session["Rol"] = resultado.ToString();

                        if (chkRememberMe.Checked)
                        {
                            Response.Cookies["Usuario"].Value = txtUser.Text.Trim();
                            Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(30);
                        }

                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        lblError.Text = "Usuario o contraseña incorrectos.";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}