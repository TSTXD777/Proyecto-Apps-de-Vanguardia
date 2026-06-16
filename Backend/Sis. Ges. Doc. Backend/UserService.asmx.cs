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
