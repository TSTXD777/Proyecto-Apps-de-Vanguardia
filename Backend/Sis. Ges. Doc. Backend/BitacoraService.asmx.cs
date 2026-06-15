using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Sis.Ges.Doc.Backend
{
    /// <summary>
    /// Summary description for BitacoraService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BitacoraService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetUserIPAddress()
        {
            return "192.1.0.16";
        }
    }

    public class Bitacora
    {
        int documentoId { get; set; }
        int usuarioId { get; set; }
        string operacion { get; set; }
        string datosAnteriores { get; set; }
        string datosNuevos { get; set; }
        string ipUsuario { get; set; }
        DateTime fechaOperacion { get; set; }
    }
}
