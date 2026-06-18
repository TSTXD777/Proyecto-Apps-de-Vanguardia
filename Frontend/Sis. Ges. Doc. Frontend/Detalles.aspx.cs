using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Detalles : System.Web.UI.Page
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
                CargarDocumento();
            }
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            string rutaArchivo = ViewState["RutaArchivo"] as string;

            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                return;
            }

            string nombreArchivo = Path.GetFileName(rutaArchivo);

            if (string.IsNullOrWhiteSpace(nombreArchivo))
            {
                return;
            }

            Response.Redirect("DownloadHandler.ashx?file=" + Server.UrlEncode(nombreArchivo), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnEditor_Click(object sender, EventArgs e)
        {
            if (!UsuarioPuedeEditar())
            {
                btnEditor.Visible = false;
                return;
            }

            CargarCategoriasEditor();
            txtEditNombre.Text = ViewState["NombreDocumento"] as string ?? string.Empty;
            txtEditDescripcion.Text = ViewState["Descripcion"] as string ?? string.Empty;
            SeleccionarCategoriaEditor(ConvertirEntero(ViewState["IdCategoria"]));

            DateTime? fechaRegistro = ConvertirFecha(ViewState["FechaRegistro"]);
            txtEditFechaRegistro.Text = fechaRegistro.HasValue ? fechaRegistro.Value.ToString("yyyy-MM-dd") : string.Empty;

            lblEditorMensaje.Text = string.Empty;
            pnlEditorModal.Visible = true;
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            pnlEditorModal.Visible = false;
            lblEditorMensaje.Text = string.Empty;
        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            if (!UsuarioPuedeEditar())
            {
                pnlEditorModal.Visible = false;
                btnEditor.Visible = false;
                return;
            }

            int idDocumento = ConvertirEntero(ViewState["IdDocumento"]);
            int idCategoria;
            DateTime fechaRegistro;

            if (idDocumento <= 0 ||
                !int.TryParse(ddlEditCategoria.SelectedValue, out idCategoria) ||
                !DateTime.TryParse(txtEditFechaRegistro.Text, out fechaRegistro))
            {
                lblEditorMensaje.Text = "Revise la categoría y la fecha de registro.";
                pnlEditorModal.Visible = true;
                return;
            }

            int idUsuarioResponsable = ConvertirEntero(ViewState["IdUsuarioResponsable"]);
            int idUsuarioModificacion = ObtenerIdUsuarioSesionVerificado();

            DocService.DocServiceSoapClient docService = new DocService.DocServiceSoapClient();
            string resultado = docService.ActualizarDocumento(
                idDocumento,
                txtEditNombre.Text.Trim(),
                txtEditDescripcion.Text.Trim(),
                idCategoria,
                fechaRegistro,
                idUsuarioResponsable,
                idUsuarioModificacion);

            if (resultado.IndexOf("Error", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                lblEditorMensaje.Text = resultado;
                pnlEditorModal.Visible = true;
                return;
            }

            pnlEditorModal.Visible = false;
            CargarDocumento();
        }

        private void CargarDocumento()
        {
            int idDocumento;
            if (!int.TryParse(Request.QueryString["id"], out idDocumento) || idDocumento <= 0)
            {
                MostrarDocumentoNoEncontrado();
                return;
            }

            try
            {
                DocService.DocServiceSoapClient docService = new DocService.DocServiceSoapClient();
                DataSet documentoDataSet = docService.ObtenerDocumentoPorId(idDocumento);

                if (documentoDataSet == null || documentoDataSet.Tables.Count == 0 || documentoDataSet.Tables[0].Rows.Count == 0)
                {
                    MostrarDocumentoNoEncontrado();
                    return;
                }

                DataRow documento = documentoDataSet.Tables[0].Rows[0];
                int estado = ConvertirEntero(ObtenerValor(documento, "Estado", "estado"), 1);

                if (estado != 1)
                {
                    MostrarDocumentoNoEncontrado();
                    return;
                }

                DateTime? fechaRegistro = ConvertirFecha(ObtenerValor(documento, "FechaRegistro", "fechaRegistro"));
                DateTime? fechaModificacion = ConvertirFecha(ObtenerValor(
                    documento,
                    "FechaModificacion",
                    "FechaUltimaModificacion",
                    "fechaModificacion",
                    "fechaUltimaModificacion"));
                int idUsuarioResponsable = ConvertirEntero(ObtenerValor(documento, "IdUsuarioResponsable", "IdUsuario", "idUsuarioResponsable", "idUsuario"));
                int idCategoria = ConvertirEntero(ObtenerValor(documento, "IdCategoria", "idCategoria"));
                string nombreDocumento = ObtenerTexto(ObtenerValor(documento, "NombreDocumento", "nombreDocumento"));
                string descripcion = ObtenerTexto(ObtenerValor(documento, "Descripcion", "descripcion"));

                lblDocumentName.Text = nombreDocumento;
                lblDescription.Text = descripcion;
                lblCategory.Text = ObtenerCategoria(documento);
                lblDocDate.Text = FormatearFecha(ConvertirFecha(ObtenerValor(documento, "FechaDocumento", "fechaDocumento")));
                lblDocUser.Text = ObtenerUsuario(documento);
                lblUploadDate.Text = FormatearFecha(fechaRegistro);
                lblSHA256.Text = ObtenerTexto(ObtenerValor(documento, "HashDocumento", "SHA256", "Sha256", "sha256", "docHash"));

                pnlModificationDate.Visible = fechaModificacion.HasValue
                    && (!fechaRegistro.HasValue || fechaModificacion.Value.Date != fechaRegistro.Value.Date);
                lblModificationDate.Text = FormatearFecha(fechaModificacion);

                ViewState["RutaArchivo"] = ObtenerTexto(ObtenerValor(documento, "RutaArchivo", "rutaArchivo"));
                ViewState["IdDocumento"] = idDocumento;
                ViewState["IdUsuarioResponsable"] = idUsuarioResponsable;
                ViewState["IdCategoria"] = idCategoria;
                ViewState["NombreDocumento"] = nombreDocumento;
                ViewState["Descripcion"] = descripcion;
                ViewState["FechaRegistro"] = fechaRegistro;
                btnDescargar.Enabled = !string.IsNullOrWhiteSpace(ViewState["RutaArchivo"] as string);
                btnEditor.Visible = UsuarioPuedeEditar();

                pnlMensaje.Visible = false;
                pnlDetalles.Visible = true;
            }
            catch
            {
                MostrarDocumentoNoEncontrado();
            }
        }

        private string ObtenerCategoria(DataRow documento)
        {
            string categoria = ObtenerTexto(ObtenerValor(documento, "Categoria", "NombreCategoria", "nombreCategoria"));

            if (!string.IsNullOrWhiteSpace(categoria))
            {
                return categoria;
            }

            int idCategoria = ConvertirEntero(ObtenerValor(documento, "IdCategoria", "idCategoria"));
            return idCategoria > 0 ? idCategoria.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        private string ObtenerUsuario(DataRow documento)
        {
            string usuarioDocumento = ObtenerTexto(ObtenerValor(documento, "Usuario", "Username", "NombreUsuario", "usuario"));

            if (!string.IsNullOrWhiteSpace(usuarioDocumento))
            {
                return usuarioDocumento;
            }

            int idUsuario = ConvertirEntero(ObtenerValor(documento, "IdUsuario", "IdUsuarioResponsable", "idUsuario", "idUsuarioResponsable"));

            if (idUsuario <= 0)
            {
                return string.Empty;
            }

            UserService.UserServiceSoapClient userService = new UserService.UserServiceSoapClient();
            DataSet usuarioDataSet = userService.ObtenerUsuarioPorId(idUsuario);

            if (usuarioDataSet == null || usuarioDataSet.Tables.Count == 0 || usuarioDataSet.Tables[0].Rows.Count == 0)
            {
                return string.Empty;
            }

            DataRow usuario = usuarioDataSet.Tables[0].Rows[0];
            return ObtenerTexto(ObtenerValor(usuario, "Usuario", "Username", "usuario"));
        }

        private void MostrarDocumentoNoEncontrado()
        {
            lblMensaje.Text = "El documento solicitado no se ha podido encontrar";
            pnlMensaje.Visible = true;
            pnlDetalles.Visible = false;
            pnlEditorModal.Visible = false;
        }

        private void CargarCategoriasEditor()
        {
            CategoryService.CategoryServiceSoapClient categoryService = new CategoryService.CategoryServiceSoapClient();
            DataSet categoriasDataSet = categoryService.ObtenerCategorias();

            ddlEditCategoria.Items.Clear();
            ddlEditCategoria.DataSource = categoriasDataSet.Tables[0];
            ddlEditCategoria.DataTextField = "nombreCategoria";
            ddlEditCategoria.DataValueField = "idCategoria";
            ddlEditCategoria.DataBind();
        }

        private void SeleccionarCategoriaEditor(int idCategoria)
        {
            if (idCategoria <= 0)
            {
                return;
            }

            string valor = idCategoria.ToString(CultureInfo.InvariantCulture);
            if (ddlEditCategoria.Items.FindByValue(valor) != null)
            {
                ddlEditCategoria.SelectedValue = valor;
            }
        }

        private bool UsuarioPuedeEditar()
        {
            if (Session["Rol"] != null && Session["Rol"].ToString().ToUpper() == "ADMIN")
            {
                return true;
            }

            int idUsuarioSesion = ObtenerIdUsuarioSesionVerificado();
            int idUsuarioResponsable = ConvertirEntero(ViewState["IdUsuarioResponsable"]);

            return idUsuarioSesion > 0 && idUsuarioSesion == idUsuarioResponsable;
        }

        private int ObtenerIdUsuarioSesionVerificado()
        {
            int idUsuario;
            if (Session["Usuario"] == null || !int.TryParse(Session["Usuario"].ToString(), out idUsuario) || idUsuario <= 0)
            {
                return 0;
            }

            UserService.UserServiceSoapClient userService = new UserService.UserServiceSoapClient();
            DataSet usuarioDataSet = userService.ObtenerUsuarioPorId(idUsuario);

            if (usuarioDataSet == null || usuarioDataSet.Tables.Count == 0 || usuarioDataSet.Tables[0].Rows.Count == 0)
            {
                return 0;
            }

            return idUsuario;
        }

        private object ObtenerValor(DataRow row, params string[] nombresColumnas)
        {
            foreach (string nombreColumna in nombresColumnas)
            {
                DataColumn columna = row.Table.Columns.Cast<DataColumn>()
                    .FirstOrDefault(c => string.Equals(c.ColumnName, nombreColumna, StringComparison.OrdinalIgnoreCase));

                if (columna != null)
                {
                    return row[columna];
                }
            }

            return null;
        }

        private string ObtenerTexto(object valor)
        {
            return valor == null || valor == DBNull.Value ? string.Empty : valor.ToString();
        }

        private int ConvertirEntero(object valor, int valorPredeterminado = 0)
        {
            int resultado;
            return int.TryParse(ObtenerTexto(valor), out resultado) ? resultado : valorPredeterminado;
        }

        private DateTime? ConvertirFecha(object valor)
        {
            DateTime resultado;
            return DateTime.TryParse(ObtenerTexto(valor), out resultado) ? resultado : (DateTime?)null;
        }

        private string FormatearFecha(DateTime? fecha)
        {
            return fecha.HasValue ? fecha.Value.ToString("dd/MM/yyyy") : string.Empty;
        }
    }
}
