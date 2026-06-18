<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Documentos</title>
    <script type="text/javascript">
        function ValidateForm() {
            var fileInput = document.getElementById('<%= fileUpload.ClientID %>');
            var nombre = document.getElementById('<%= txtNombre.ClientID %>');
            var descripcion = document.getElementById('<%= txtDescripcion.ClientID %>');
            var categoria = document.getElementById('<%= ddlCategoria.ClientID %>');
            var fecha = document.getElementById('<%= calFecha.ClientID %>');
            var submitBtn = document.getElementById('<%= btnRegistrar.ClientID %>');

            var isValid = fileInput.value.trim() !== '' &&
                nombre.value.trim() !== '' &&
                descripcion.value.trim() !== '' &&
                categoria.value !== '' &&
                fecha.value.trim() !== '';

            submitBtn.disabled = !isValid;
            return isValid;
        }

        function OnFileChange() {
            ValidateForm();
        }

        function OnNombreChange() {
            ValidateForm();
        }

        function OnDescripcionChange() {
            ValidateForm();
        }

        function OnCategoriaChange() {
            ValidateForm();
        }

        function OnFechaChange() {
            ValidateForm();
        }

        window.onload = function() {
            ValidateForm();
        };
    </script>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .form-container {
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #cccccc;
            border-radius: 5px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        input[type="text"],
        input[type="file"],
        textarea,
        select {
            width: 100%;
            padding: 8px;
            border: 1px solid #999999;
            border-radius: 3px;
            box-sizing: border-box;
        }
        textarea {
            resize: vertical;
            min-height: 100px;
        }
        button,
        input[type="submit"] {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 3px;
            cursor: pointer;
            font-size: 16px;
        }
        button:hover:enabled,
        input[type="submit"]:hover:enabled {
            background-color: #45a049;
        }
        button:disabled,
        input[type="submit"]:disabled {
            background-color: #cccccc;
            cursor: not-allowed;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="form-container">
            <h2>Registro de Documentos</h2>

            <div class="form-group">
                <label for="fileUpload">Archivo:</label>
                <asp:FileUpload ID="fileUpload" runat="server" onchange="OnFileChange();" />
            </div>

            <div class="form-group">
                <label for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" TextMode="SingleLine" onkeyup="OnNombreChange();"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtDescripcion">Descripción:</label>
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" onkeyup="OnDescripcionChange();"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="ddlCategoria">Categoría:</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" AppendDataBoundItems="true" onchange="OnCategoriaChange();">
                    <asp:ListItem Value="">-- Seleccione una categoría --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label for="calFecha">Fecha:</label>
                <asp:TextBox ID="calFecha" runat="server" TextMode="Date" onchange="OnFechaChange();"></asp:TextBox>
            </div>

            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" OnClientClick="return ValidateForm();" disabled="disabled" />
        </div>
    </form>
</body>
</html>
