using System;
using System.Web.UI;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Perfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                lblUsuario.Text = Session["Usuario"].ToString();

                if (Session["Rol"] != null)
                {
                    lblRol.Text = Session["Rol"].ToString();

                    if (Session["Rol"].ToString().ToUpper() == "ADMIN")
                    {
                        pnlAdmin.Visible = true;
                    }
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("Login.aspx");
        }
    }
}