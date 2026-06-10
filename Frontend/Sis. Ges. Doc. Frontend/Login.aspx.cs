using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Sis.Ges.Doc.Frontend.DAL;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    using (SqlConnection cn = Conexion.ObtenerConexion())
                    {
                        cn.Open();
                        Response.Write("<script>alert('Conexión Exitosa a SQL Server');</script>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "") + "');</script>");
                }
            }
        }

        protected void btnLoginUser(object sender, EventArgs e)
        {
            // Temporalmente dejamos el login como estaba
            Response.Redirect("Dashboard.aspx");
        }
    }
}