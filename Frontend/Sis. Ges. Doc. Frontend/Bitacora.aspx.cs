using System;
using System.Data;
using System.Data.SqlClient;
using Sis.Ges.Doc.Frontend.DAL;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Bitacora : System.Web.UI.Page
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
                CargarUsuarios();
                CargarBitacora();
            }
        }

        private void CargarUsuarios()
        {
            ddlUsuarios.Items.Clear();
            ddlUsuarios.Items.Add("Todos");

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = "SELECT Usuario FROM Usuarios ORDER BY Usuario";

                SqlCommand cmd = new SqlCommand(sql, cn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ddlUsuarios.Items.Add(dr["Usuario"].ToString());
                }

                dr.Close();
            }
        }

        private void CargarBitacora()
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"
        SELECT
            U.Usuario,
            CASE
                WHEN B.Operacion = 'INSERT' THEN 'Registró documento'
                WHEN B.Operacion = 'UPDATE' THEN 'Editó documento'
                WHEN B.Operacion = 'CREAR' THEN 'Creó usuario'
                WHEN B.Operacion = 'EDITAR' THEN 'Editó usuario'
                ELSE B.Operacion
            END AS Operacion,

CASE
    WHEN B.Operacion IN ('INSERT','UPDATE')
    THEN LEFT(
        SUBSTRING(
            B.DatosNuevos,
            CHARINDEX('NombreDocumento=', B.DatosNuevos) + 16,
            200
        ),
        CHARINDEX(
            ';',
            SUBSTRING(
                B.DatosNuevos,
                CHARINDEX('NombreDocumento=', B.DatosNuevos) + 16,
                200
            ) + ';'
        ) - 1
    )
    ELSE B.DatosNuevos
END AS DatosNuevos,

DATEADD(HOUR, -8, B.FechaOperacion) AS FechaOperacion
        FROM Bitacora B
        INNER JOIN Usuarios U
            ON B.IdUsuario = U.IdUsuario
        ORDER BY B.FechaOperacion DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, cn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvBitacora.DataSource = dt;
                gvBitacora.DataBind();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                cn.Open();

                string sql = @"
        SELECT
            U.Usuario,
            CASE
                WHEN B.Operacion = 'INSERT' THEN 'Registró documento'
                WHEN B.Operacion = 'UPDATE' THEN 'Editó documento'
                WHEN B.Operacion = 'CREAR' THEN 'Creó usuario'
                WHEN B.Operacion = 'EDITAR' THEN 'Editó usuario'
                ELSE B.Operacion
            END AS Operacion,

CASE
    WHEN B.Operacion IN ('INSERT','UPDATE')
    THEN LEFT(
        SUBSTRING(
            B.DatosNuevos,
            CHARINDEX('NombreDocumento=', B.DatosNuevos) + 16,
            200
        ),
        CHARINDEX(
            ';',
            SUBSTRING(
                B.DatosNuevos,
                CHARINDEX('NombreDocumento=', B.DatosNuevos) + 16,
                200
            ) + ';'
        ) - 1
    )
    ELSE B.DatosNuevos
END AS DatosNuevos,

DATEADD(HOUR, -8, B.FechaOperacion) AS FechaOperacion
        FROM Bitacora B
        INNER JOIN Usuarios U
            ON B.IdUsuario = U.IdUsuario
        WHERE
            (@Usuario = 'Todos' OR U.Usuario = @Usuario)
        AND
            (@Operacion = '' OR B.Operacion = @Operacion)
        ORDER BY B.FechaOperacion DESC";

                SqlCommand cmd = new SqlCommand(sql, cn);

                cmd.Parameters.AddWithValue("@Usuario",
                    ddlUsuarios.SelectedItem.Text);

                cmd.Parameters.AddWithValue("@Operacion",
                    ddlOperacion.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvBitacora.DataSource = dt;
                gvBitacora.DataBind();
            }
        }
    }
}