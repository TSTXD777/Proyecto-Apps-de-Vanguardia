using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Documentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
            }

            CargarDocumentos();

            // Ensure category dropdown stays expanded after postback
            if (IsPostBack)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "expandCategories", "document.getElementById('categoryItems').classList.add('show'); document.querySelector('.category-dropdown').innerHTML = 'Categoría △';", true);
            }
        }

        protected void Filter_Changed(object sender, EventArgs e)
        {
            CargarDocumentos();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;
            txtFechaFin.Text = string.Empty;

            foreach (ListItem item in cblCategorias.Items)
            {
                item.Selected = false;
            }

            CargarDocumentos();
        }

        private void CargarCategorias()
        {
            Dictionary<int, string> categorias = ObtenerCategorias();

            cblCategorias.DataSource = categorias.Select(c => new
            {
                IdCategoria = c.Key,
                NombreCategoria = c.Value
            }).ToList();
            cblCategorias.DataTextField = "NombreCategoria";
            cblCategorias.DataValueField = "IdCategoria";
            cblCategorias.DataBind();
        }

        private void CargarDocumentos()
        {
            try
            {
                lblError.Visible = false;

                Dictionary<int, string> categorias = ObtenerCategorias();
                List<DocumentoVista> documentos = ObtenerDocumentos(categorias);
                List<DocumentoVista> resultado = AplicarFiltros(documentos).ToList();

                rptDocumentos.DataSource = resultado;
                rptDocumentos.DataBind();

                pnlSinResultados.Visible = resultado.Count == 0;
            }
            catch (Exception ex)
            {
                rptDocumentos.DataSource = null;
                rptDocumentos.DataBind();
                pnlSinResultados.Visible = false;
                lblError.Text = "No se pudieron cargar los documentos: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private IEnumerable<DocumentoVista> AplicarFiltros(IEnumerable<DocumentoVista> documentos)
        {
            string busqueda = txtBuscar.Text.Trim();
            if (!string.IsNullOrEmpty(busqueda))
            {
                documentos = documentos.Where(d => d.NombreDocumento.IndexOf(busqueda, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            List<int> categoriasSeleccionadas = cblCategorias.Items.Cast<ListItem>()
                .Where(item => item.Selected)
                .Select(item => int.Parse(item.Value))
                .ToList();

            if (categoriasSeleccionadas.Count > 0)
            {
                documentos = documentos.Where(d => categoriasSeleccionadas.Contains(d.IdCategoria));
            }

            DateTime fechaInicio;
            if (DateTime.TryParse(txtFechaInicio.Text, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaInicio))
            {
                documentos = documentos.Where(d => d.FechaDocumento.HasValue && d.FechaDocumento.Value.Date >= fechaInicio.Date);
            }

            DateTime fechaFin;
            if (DateTime.TryParse(txtFechaFin.Text, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFin))
            {
                documentos = documentos.Where(d => d.FechaDocumento.HasValue && d.FechaDocumento.Value.Date <= fechaFin.Date);
            }

            return documentos;
        }

        private List<DocumentoVista> ObtenerDocumentos(Dictionary<int, string> categorias)
        {
            DocService.DocServiceSoapClient docService = new DocService.DocServiceSoapClient();
            DataSet documentosDataSet = docService.ObtenerDocumentos();

            if (documentosDataSet == null || documentosDataSet.Tables.Count == 0)
            {
                return new List<DocumentoVista>();
            }

            DataTable tabla = documentosDataSet.Tables[0];
            List<DocumentoVista> documentos = new List<DocumentoVista>();

            foreach (DataRow row in tabla.Rows)
            {
                int idDocumento = ConvertirEntero(ObtenerValor(row, "IdDocumento", "idDocumento"));
                int idCategoria = ConvertirEntero(ObtenerValor(row, "IdCategoria", "idCategoria"));
                DateTime? fechaDocumento = ConvertirFecha(ObtenerValor(row, "FechaDocumento", "fechaDocumento"));
                string categoria = ObtenerTexto(ObtenerValor(row, "Categoria", "NombreCategoria", "nombreCategoria"));

                if (string.IsNullOrWhiteSpace(categoria))
                {
                    categoria = categorias.ContainsKey(idCategoria) ? categorias[idCategoria] : idCategoria.ToString();
                }

                documentos.Add(new DocumentoVista
                {
                    IdDocumento = idDocumento,
                    NombreDocumento = ObtenerTexto(ObtenerValor(row, "NombreDocumento", "nombreDocumento")),
                    IdCategoria = idCategoria,
                    Categoria = categoria,
                    FechaDocumento = fechaDocumento,
                    FechaDocumentoTexto = fechaDocumento.HasValue ? fechaDocumento.Value.ToString("dd/MM/yyyy") : string.Empty
                });
            }

            return documentos;
        }

        private Dictionary<int, string> ObtenerCategorias()
        {
            Dictionary<int, string> categorias = new Dictionary<int, string>();
            CategoryService.CategoryServiceSoapClient categoryService = new CategoryService.CategoryServiceSoapClient();
            DataSet categoriasDataSet = categoryService.ObtenerCategorias();

            if (categoriasDataSet == null || categoriasDataSet.Tables.Count == 0)
            {
                return categorias;
            }

            foreach (DataRow row in categoriasDataSet.Tables[0].Rows)
            {
                int idCategoria = ConvertirEntero(ObtenerValor(row, "IdCategoria", "idCategoria"));
                string nombreCategoria = ObtenerTexto(ObtenerValor(row, "NombreCategoria", "nombreCategoria"));

                if (idCategoria > 0 && !categorias.ContainsKey(idCategoria))
                {
                    categorias.Add(idCategoria, nombreCategoria);
                }
            }

            return categorias;
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

        private int ConvertirEntero(object valor)
        {
            int resultado;
            return int.TryParse(ObtenerTexto(valor), out resultado) ? resultado : 0;
        }

        private DateTime? ConvertirFecha(object valor)
        {
            DateTime resultado;
            return DateTime.TryParse(ObtenerTexto(valor), out resultado) ? resultado : (DateTime?)null;
        }

        private class DocumentoVista
        {
            public int IdDocumento { get; set; }
            public string NombreDocumento { get; set; }
            public int IdCategoria { get; set; }
            public string Categoria { get; set; }
            public DateTime? FechaDocumento { get; set; }
            public string FechaDocumentoTexto { get; set; }
        }
    }
}
