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

                lblDocumentName.Text = ObtenerTexto(ObtenerValor(documento, "NombreDocumento", "nombreDocumento"));
                lblDescription.Text = ObtenerTexto(ObtenerValor(documento, "Descripcion", "descripcion"));
                lblCategory.Text = ObtenerCategoria(documento);
                lblDocDate.Text = FormatearFecha(ConvertirFecha(ObtenerValor(documento, "FechaDocumento", "fechaDocumento")));
                lblDocUser.Text = ObtenerUsuario(documento);
                lblUploadDate.Text = FormatearFecha(fechaRegistro);
                lblSHA256.Text = ObtenerTexto(ObtenerValor(documento, "HashDocumento", "SHA256", "Sha256", "sha256", "docHash"));

                pnlModificationDate.Visible = fechaModificacion.HasValue
                    && (!fechaRegistro.HasValue || fechaModificacion.Value.Date != fechaRegistro.Value.Date);
                lblModificationDate.Text = FormatearFecha(fechaModificacion);

                ViewState["RutaArchivo"] = ObtenerTexto(ObtenerValor(documento, "RutaArchivo", "rutaArchivo"));
                btnDescargar.Enabled = !string.IsNullOrWhiteSpace(ViewState["RutaArchivo"] as string);

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
