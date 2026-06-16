using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;

namespace Sis.Ges.Doc.Frontend
{
    /// <summary>
    /// Summary description for DownloadHandler
    /// </summary>
    public class DownloadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = context.Request.QueryString["file"];
            if (!string.IsNullOrEmpty(fileName))
            {
                string filePath = context.Server.MapPath("~/Uploads/" + fileName);
                if (File.Exists(filePath))
                {
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                    context.Response.WriteFile(filePath);
                    context.Response.End();
                }
                else
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/html";
                    context.Response.Write("<script>alert('¡Archivo no encontrado!');</script>");
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                context.Response.Write("Falta el parámetro de archivo.");
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