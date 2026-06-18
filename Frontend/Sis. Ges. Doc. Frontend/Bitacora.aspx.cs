using System;
using System.Web.UI;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Bitacora : System.Web.UI.Page
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
                ddlUsuarios.Items.Clear();

                ddlUsuarios.Items.Add("Todos");

                if (Session["Usuario"] != null)
                {
                    ddlUsuarios.Items.Add(
                        Session["NombreUsuario"] != null
                            ? Session["NombreUsuario"].ToString()
                            : Session["Usuario"].ToString()
                    );
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            // Aquí irá el filtro cuando conectemos la BD
        }
    }
}
