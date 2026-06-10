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

        .titulo {
            text-align: center;
            font-size: 36px;
            margin-bottom: 30px;
            color: #444;
        }

        .acciones {
            margin-bottom: 20px;
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

        <div class="acciones">
            <asp:Button ID="btnNuevoUsuario"
                runat="server"
                Text="+ Nuevo Usuario"
                CssClass="btn" />
        </div>

        <asp:GridView ID="gvUsuarios"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="grid">

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

            </Columns>

        </asp:GridView>

    </div>

</form>

</body>
</html>