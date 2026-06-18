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
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

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
                int idUsuarioResponsable = ObtenerIdUsuarioResponsable();

                DocService.DocServiceSoapClient docService = new DocService.DocServiceSoapClient();

                System.Diagnostics.Debug.WriteLine("Archivo seleccionado: " + postedFile.FileName);
                System.Diagnostics.Debug.WriteLine("Archivo guardado: " + nombreArchivoHash);
                System.Diagnostics.Debug.WriteLine("Nombre: " + nombre);
                System.Diagnostics.Debug.WriteLine("Descripcion: " + descripcion);
                System.Diagnostics.Debug.WriteLine("Categoria Index: " + categoriaIndex);
                System.Diagnostics.Debug.WriteLine("Fecha: " + fecha.ToString());

                string resultado = docService.InsertarDocumento(upload.TempFilePath, nombre, descripcion, categoriaIndex, fecha, idUsuarioResponsable);
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

        private int ObtenerIdUsuarioResponsable()
        {
            int idUsuario;
            if (Session["Usuario"] == null || !int.TryParse(Session["Usuario"].ToString(), out idUsuario) || idUsuario <= 0)
            {
                throw new InvalidOperationException("No se pudo identificar el usuario responsable de la sesión.");
            }

            UserService.UserServiceSoapClient userService = new UserService.UserServiceSoapClient();
            DataSet usuarioDataSet = userService.ObtenerUsuarioPorId(idUsuario);

            if (usuarioDataSet == null || usuarioDataSet.Tables.Count == 0 || usuarioDataSet.Tables[0].Rows.Count == 0)
            {
                throw new InvalidOperationException("El usuario responsable de la sesión no existe.");
            }

            return idUsuario;
        }
    }
}
