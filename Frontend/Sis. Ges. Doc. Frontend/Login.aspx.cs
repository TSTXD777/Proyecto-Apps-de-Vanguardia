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

                    string sql = @"SELECT IdUsuario, Rol
                           FROM Usuarios
                           WHERE Usuario = @Usuario
                           AND PasswordHash = @Password
                           AND Estado = 1";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@Usuario", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", SecurityHelper.Sha256Hash(txtPassword.Text));

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        Session["Usuario"] = Convert.ToInt32(dr["IdUsuario"]);
                        Session["NombreUsuario"] = txtUser.Text.Trim();
                        Session["Rol"] = dr["Rol"].ToString();

                        if (chkRememberMe.Checked)
                        {
                            Response.Cookies["Usuario"].Value = txtUser.Text.Trim();
                            Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(30);
                        }

                        Response.Redirect("Dashboard.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
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
