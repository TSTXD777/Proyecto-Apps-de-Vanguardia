<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Bitacora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Bitácora</title>

    <style>

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            background: #f5f5f5;
        }

        .header {
            background: #2e7d32;
            color: white;
            padding: 15px 25px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .header a {
            color: white;
            text-decoration: none;
            font-size: 24px;
        }

        .titulo {
            font-size: 40px;
            font-weight: bold;
        }

        .container {
            padding: 30px;
        }

        .filtros {
            background: white;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 20px;

            display: flex;
            gap: 20px;
            align-items: end;
            flex-wrap: wrap;
        }

        .campo {
            display: flex;
            flex-direction: column;
        }

        .campo label {
            margin-bottom: 5px;
            font-weight: bold;
        }

        .campo select {
            padding: 8px;
            min-width: 220px;
        }

        .btn {
            background: #2e7d32;
            color: white;
            border: none;
            padding: 10px 18px;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background: #256428;
        }

        .grid {
            width: 100%;
            background: white;
        }

        .grid th {
            background: #2e7d32;
            color: white;
            padding: 10px;
        }

        .grid td {
            padding: 10px;
            border-bottom: 1px solid #ddd;
        }

        .grid tr:nth-child(even) {
            background: #fafafa;
        }

    </style>

</head>
<body>

<form id="form1" runat="server">

    <div class="header">

        <a href="Dashboard.aspx">🏠</a>

        <div class="titulo">Bitácora</div>

        <a href="Perfil.aspx">👤</a>

    </div>

    <div class="container">

        <div class="filtros">

            <div class="campo">
                <label>Usuario</label>

                <asp:DropDownList ID="ddlUsuarios"
                    runat="server">
                </asp:DropDownList>
            </div>

            <div class="campo">
                <label>Operación</label>

                <asp:DropDownList ID="ddlOperacion"
    runat="server">

    <asp:ListItem Text="Todas" Value=""></asp:ListItem>
    <asp:ListItem Text="Crear Usuario" Value="CREAR"></asp:ListItem>
    <asp:ListItem Text="Editar Usuario" Value="EDITAR"></asp:ListItem>
    <asp:ListItem Text="Registrar Documento" Value="INSERT"></asp:ListItem>
    <asp:ListItem Text="Editar Documento" Value="UPDATE"></asp:ListItem>

</asp:DropDownList>
            </div>

            <asp:Button ID="btnBuscar"
    runat="server"
    Text="Buscar"
    CssClass="btn"
    OnClick="btnBuscar_Click" />

        </div>

        <asp:GridView ID="gvBitacora"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="grid">

            <Columns>

                <asp:BoundField
                    DataField="Usuario"
                    HeaderText="Usuario" />

                <asp:BoundField
                    DataField="Operacion"
                    HeaderText="Operación" />

                <asp:BoundField
                    DataField="DatosNuevos"
                    HeaderText="Detalle" />

                <asp:BoundField
                    DataField="FechaOperacion"
                    HeaderText="Fecha y Hora" />

            </Columns>

        </asp:GridView>

    </div>

</form>

</body>
</html>