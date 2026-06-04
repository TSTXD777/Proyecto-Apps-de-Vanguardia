using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLoginUser(object sender, EventArgs e)
        {
            //Lógica para autenticar al usuario

            if (chkRememberMe.Checked)
            {
                //Lógica para recordar al usuario mediante cookies
            }


            
            Response.Redirect("Dashboard.aspx"); // Redirige al usuario a la página principal después de iniciar sesión

        }
    }
}