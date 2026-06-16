using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace Sis.Ges.Doc.Frontend
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];
                string savePath = context.Server.MapPath("~/Uploads/" + file.FileName);


                file.SaveAs(savePath);


                context.Response.ContentType = "application/json";
                context.Response.Write("{\"mensaje\": \"Archivo subido exitosamente.\"}");

            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Write("No se seleccionó ningún archivo.");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}