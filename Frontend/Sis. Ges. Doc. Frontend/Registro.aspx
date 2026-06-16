<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Docs.</title>
    <style>
        body { font-family: Arial, Helvetica, sans-serif; background: #f5f5f5; }
        .header { height:48px; background:#eee; border-bottom:2px solid #ddd; display:flex; align-items:center; justify-content:space-between; padding:0 12px; }
        .header .icon { width:36px; height:36px; border:1px solid #ccc; display:flex; align-items:center; justify-content:center; background:white; }
        .container { max-width:980px; margin:28px auto; padding:20px; background:transparent; }
        h1 { text-align:center; font-size:36px; margin:6px 0 24px 0; }
        .main { display:flex; gap:40px; align-items:flex-start; }
        .upload { width:260px; height:240px; border:4px dashed #bdbdbd; background:white; display:flex; flex-direction:column; align-items:center; justify-content:center; border-radius:12px; }
        .upload .fileupload { opacity:0; position:absolute; width:200px; height:200px; cursor:pointer; }
        .upload .box { width:200px; height:160px; border:2px solid #ccc; display:flex; align-items:center; justify-content:center; border-radius:8px; }
        .hint { margin-top:8px; font-size:14px; color:#666; }
        .form { flex:1; max-width:520px; }
        .form label { display:block; font-weight:600; margin-top:8px; color:#444; }
        .input, .textarea { width:100%; padding:8px 10px; font-size:14px; border:1px solid #ccc; border-radius:4px; box-sizing:border-box; }
        .textarea { min-height:120px; resize:vertical; }
        .row { display:flex; gap:12px; align-items:center; }
        .date { width:180px; }
        .btn { margin-top:16px; padding:10px 22px; border-radius:20px; border:1px solid #c0c0c0; background:#fff; cursor:pointer; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
              <a href="Dashboard.aspx" class="icon" style="text-decoration:none;">
        🏠
    </a>
            <div class="icon" title="Profile">👤</div>
        </div>

        <div class="container">
            <h1>Registro de Docs.</h1>

            <div class="main">
                <div style="position:relative;">
                    <div class="upload">
                        <div class="box">
                            <asp:FileUpload ID="fileUpload" runat="server" CssClass="fileupload" />
                            <div style="text-align:center;">
                                <div style="font-size:48px; color:#777;">⬆</div>
                            </div>
                        </div>
                        <div class="hint">Seleccionar Docs</div>
                        <div id="uploadStatus" style="font-size:13px;color:#2b7a2b;margin-top:6px;"></div>
                    </div>
                </div>

                <div class="form">
                    <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre" />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="input" />

                    <asp:Label ID="lblDescripcion" runat="server" AssociatedControlID="txtDescripcion" Text="Descripción" />
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="6" CssClass="textarea" />

                    <asp:Label ID="lblCategoria" runat="server" AssociatedControlID="ddlCategoria" Text="Categoria" />
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="input" />
                    <asp:Label ID="lblFecha" runat="server" AssociatedControlID="txtFecha" Text="Fecha Doc" />
                    <input id="txtFecha" type="date" runat="server" class="date" />

                    <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn" OnClick="btnRegistrar_Click" OnClientClick="return onRegistrarClick();" />
                </div>
            </div>
        </div>
        <script src="Scripts/scriptsRegistro.js" type="text/javascript"></script>
        <script type="text/javascript">
            // All functionality is implemented in Scripts/scriptsRegistro.js
            if (window.setupUpload) {
                window.setupUpload(
                    '<%= fileUpload.ClientID %>',
                    'uploadStatus',
                    '<%= ResolveUrl("~/UploadHandler.ashx") %>',
                    '<%= btnRegistrar.UniqueID %>',
                    '<%= btnRegistrar.ClientID %>'
                );
            }
            if (window.setupValidation) {
                window.setupValidation(
                    '<%= fileUpload.ClientID %>',
                    '<%= txtNombre.ClientID %>',
                    '<%= txtDescripcion.ClientID %>',
                    '<%= ddlCategoria.ClientID %>',
                    '<%= txtFecha.ClientID %>',
                    '<%= btnRegistrar.ClientID %>',
                    'uploadStatus'
                );
            }
        </script>
    </form>
</body>
</html>
