using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sis.Ges.Doc.Frontend
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CategoryService.CategoryServiceSoapClient categoryService = new CategoryService.CategoryServiceSoapClient();
                DataSet categoriasDataSet = categoryService.ObtenerCategorias();

                ddlCategoria.DataSource = categoriasDataSet.Tables[0];
                ddlCategoria.DataTextField = "nombreCategoria";
                ddlCategoria.DataValueField = "idCategoria";
                ddlCategoria.DataBind();
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            PrintTestToConsole();

            HttpPostedFile postedFile = fileUpload.PostedFile;
            UploadHandler.PreparedUpload upload = null;

            try
            {
                upload = UploadHandler.PrepareUploadedFileForRegistration(postedFile, Server);
                string nombreArchivoHash = upload.HashedFileName;
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();
                int categoriaIndex = int.Parse(ddlCategoria.SelectedValue);
                DateTime fecha = DateTime.Parse(calFecha.Text);

                DocService.DocServiceSoapClient docService = new DocService.DocServiceSoapClient();

                System.Diagnostics.Debug.WriteLine("Archivo seleccionado: " + postedFile.FileName);
                System.Diagnostics.Debug.WriteLine("Archivo guardado: " + nombreArchivoHash);
                System.Diagnostics.Debug.WriteLine("Nombre: " + nombre);
                System.Diagnostics.Debug.WriteLine("Descripcion: " + descripcion);
                System.Diagnostics.Debug.WriteLine("Categoria Index: " + categoriaIndex);
                System.Diagnostics.Debug.WriteLine("Fecha: " + fecha.ToString());

                string resultado = docService.InsertarDocumento(upload.TempFilePath, nombre, descripcion, categoriaIndex, fecha, 1);
                System.Diagnostics.Debug.WriteLine("Resultado DocService: " + resultado);

                if (resultado.IndexOf("Error:", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    UploadHandler.DeletePreparedUpload(upload);
                    ClientScript.RegisterStartupScript(GetType(), "DocServiceError", "console.error('" + HttpUtility.JavaScriptStringEncode(resultado) + "');", true);
                    return;
                }

                UploadHandler.CompletePreparedUpload(upload);
                ClientScript.RegisterStartupScript(GetType(), "DocServiceSuccess", "console.log('" + HttpUtility.JavaScriptStringEncode(resultado) + "');", true);
            }
            catch (Exception ex)
            {
                UploadHandler.DeletePreparedUpload(upload);
                System.Diagnostics.Debug.WriteLine("Error al registrar documento: " + ex.Message);
                ClientScript.RegisterStartupScript(GetType(), "RegistroError", "console.error('" + HttpUtility.JavaScriptStringEncode(ex.Message) + "');", true);
            }
        }

        private void PrintTestToConsole()
        {
            ClientScript.RegisterStartupScript(GetType(), "PrintTest", "console.log('TEST');", true);
            System.Diagnostics.Debug.WriteLine("SERVER CLICK");
        }
    }
}
