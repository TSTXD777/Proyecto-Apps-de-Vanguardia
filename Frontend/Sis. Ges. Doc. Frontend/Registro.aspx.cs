using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Registro : System.Web.UI.Page
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
            HttpPostedFile postedFile = fileUpload.PostedFile;
            string nombre = txtNombre.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string categoria = ddlCategoria.SelectedValue;
            string fecha = txtFecha.Value;

            DocService.DocServiceSoapClient docService = new DocService.DocServiceSoapClient();

            docService.InsertarDocumento(postedFile.FileName, nombre, descripcion, int.Parse(categoria), 1);

        }
    }
}