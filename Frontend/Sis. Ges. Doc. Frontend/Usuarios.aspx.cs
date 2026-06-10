using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sis.Ges.Doc.Frontend.DAL;

namespace Sis.Ges.Doc.Frontend
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (Session["Rol"] == null ||
                Session["Rol"].ToString().ToUpper() != "ADMIN")
            {
                Response.Redirect("Dashboard.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    cn.Open();

                    string sql = @"
                        SELECT
                            IdUsuario,
                            NombreCompleto,
                            Usuario,
                            Correo,
                            Rol,
                            CASE
                                WHEN Estado = 1 THEN 'Activo'
                                ELSE 'Inactivo'
                            END AS Estado
                        FROM Usuarios";

                    SqlDataAdapter da = new SqlDataAdapter(sql, cn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    gvUsuarios.DataSource = dt;
                    gvUsuarios.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = ex.Message;
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    cn.Open();

                    // EDITAR
                    if (ViewState["IdUsuarioEditar"] != null)
                    {
                        string sql = @"
                            UPDATE Usuarios
                            SET
                                NombreCompleto = @Nombre,
                                Correo = @Correo,
                                Usuario = @Usuario,
                                Rol = @Rol
                            WHERE IdUsuario = @IdUsuario";

                        SqlCommand cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@IdUsuario", ViewState["IdUsuarioEditar"]);
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                        cmd.Parameters.AddWithValue("@Rol", ddlRol.SelectedValue);

                        cmd.ExecuteNonQuery();

                        ViewState["IdUsuarioEditar"] = null;

                        btnGuardarUsuario.Text = "Guardar Usuario";

                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Text = "Usuario actualizado correctamente.";
                    }
                    else
                    {
                        // CREAR
                        string sql = @"
                            INSERT INTO Usuarios
                            (
                                NombreCompleto,
                                Correo,
                                Usuario,
                                PasswordHash,
                                IdDepartamento,
                                Rol,
                                Estado
                            )
                            VALUES
                            (
                                @Nombre,
                                @Correo,
                                @Usuario,
                                @Password,
                                1,
                                @Rol,
                                1
                            )";

                        SqlCommand cmd = new SqlCommand(sql, cn);

                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                        cmd.Parameters.AddWithValue("@Rol", ddlRol.SelectedValue);

                        cmd.ExecuteNonQuery();

                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Text = "Usuario creado correctamente.";
                    }

                    txtNombre.Text = "";
                    txtCorreo.Text = "";
                    txtUsuario.Text = "";
                    txtPassword.Text = "";

                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = ex.Message;
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                int fila = Convert.ToInt32(e.CommandArgument);

                int idUsuario = Convert.ToInt32(
                    gvUsuarios.DataKeys[fila].Value);

                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    cn.Open();

                    string sql = @"
                        SELECT
                            IdUsuario,
                            NombreCompleto,
                            Correo,
                            Usuario,
                            Rol
                        FROM Usuarios
                        WHERE IdUsuario = @IdUsuario";

                    SqlCommand cmd = new SqlCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        ViewState["IdUsuarioEditar"] = idUsuario;

                        txtNombre.Text = dr["NombreCompleto"].ToString();
                        txtCorreo.Text = dr["Correo"].ToString();
                        txtUsuario.Text = dr["Usuario"].ToString();
                        ddlRol.SelectedValue = dr["Rol"].ToString();

                        btnGuardarUsuario.Text = "Actualizar Usuario";
                    }
                }
            }
        }
    }
}