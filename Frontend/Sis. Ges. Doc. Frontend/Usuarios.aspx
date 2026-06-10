<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Usuarios</title>

    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f5f5f5;
            margin: 0;
        }

        .header {
            background-color: #2e7d32;
            color: white;
            padding: 15px 25px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .header a {
            color: white;
            text-decoration: none;
            font-size: 22px;
        }

        .container {
            padding: 30px;
        }

        .formulario {
            background: white;
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 20px;
            margin-bottom: 25px;
        }

        .formulario h3 {
            margin-top: 0;
            color: #2e7d32;
        }

        .campo {
            margin-bottom: 15px;
        }

        .campo label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .campo input,
        .campo select {
            width: 350px;
            padding: 8px;
        }

        .btn {
            background-color: #2e7d32;
            color: white;
            border: none;
            padding: 10px 18px;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #256428;
        }

        .mensaje {
            margin-top: 10px;
            font-weight: bold;
        }

        .grid {
            width: 100%;
        }

        .grid th {
            background-color: #2e7d32;
            color: white;
            padding: 10px;
        }

        .grid td {
            padding: 10px;
            border-bottom: 1px solid #ddd;
        }

        .grid tr:nth-child(even) {
            background-color: #fafafa;
        }
    </style>
</head>
<body>

<form id="form1" runat="server">

    <div class="header">
        <a href="Dashboard.aspx">🏠</a>
        <h2>Gestión de Usuarios</h2>
        <a href="Perfil.aspx">👤</a>
    </div>

    <div class="container">

        <div class="formulario">

            <h3>Crear Nuevo Usuario</h3>

            <div class="campo">
                <label>Nombre Completo</label>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </div>

            <div class="campo">
                <label>Correo</label>
                <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
            </div>

            <div class="campo">
                <label>Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            </div>

            <div class="campo">
                <label>Contraseña</label>
                <asp:TextBox ID="txtPassword"
                    runat="server"
                    TextMode="Password"></asp:TextBox>
            </div>

            <div class="campo">
                <label>Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server">
                    <asp:ListItem Value="ADMIN">ADMIN</asp:ListItem>
                    <asp:ListItem Value="USUARIO">USUARIO</asp:ListItem>
                </asp:DropDownList>
            </div>

            <asp:Button ID="btnGuardarUsuario"
                runat="server"
                Text="Guardar Usuario"
                CssClass="btn"
                OnClick="btnGuardarUsuario_Click" />

            <br /><br />

            <asp:Label ID="lblMensaje"
                runat="server"
                CssClass="mensaje"></asp:Label>

        </div>

        <asp:GridView ID="gvUsuarios"
    runat="server"
    AutoGenerateColumns="False"
    CssClass="grid"
    DataKeyNames="IdUsuario"
    OnRowCommand="gvUsuarios_RowCommand">

            <Columns>

                <asp:BoundField DataField="IdUsuario" HeaderText="ID" />

                <asp:BoundField DataField="NombreCompleto"
                    HeaderText="Nombre Completo" />

                <asp:BoundField DataField="Usuario"
                    HeaderText="Usuario" />

                <asp:BoundField DataField="Correo"
                    HeaderText="Correo" />

                <asp:BoundField DataField="Rol"
                    HeaderText="Rol" />

                <asp:BoundField DataField="Estado"
                    HeaderText="Estado" />

                <asp:ButtonField
                    ButtonType="Button"
                    CommandName="Editar"
                    Text="Editar"
                    HeaderText="Acciones" />

            </Columns>

        </asp:GridView>

    </div>

</form>

</body>
</html>