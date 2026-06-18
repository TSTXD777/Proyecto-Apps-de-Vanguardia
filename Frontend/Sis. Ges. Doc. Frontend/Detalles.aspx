<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalles.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Detalles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalles del Documento</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
        }

        .header {
            background-color: #e8e8e8;
            padding: 15px 30px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 2px solid #999;
        }

        .header-icon {
            font-size: 28px;
            color: #666;
        }

        .user-icon {
            width: 35px;
            height: 35px;
            border: 2px solid #999;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 20px;
            color: #666;
        }

        .container {
            padding: 30px;
            max-width: 1400px;
            margin: 0 auto;
        }

        .page-title {
            font-size: 36px;
            font-weight: 300;
            color: #333;
            margin-bottom: 30px;
            text-align: center;
        }

        .content-wrapper {
            display: flex;
            gap: 30px;
            min-height: 600px;
        }

        .preview-section {
            flex: 0 0 35%;
            background-color: white;
            border: 2px solid #999;
            padding: 20px;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }

        .doc-preview {
            flex: 1;
            background-color: #f0f0f0;
            border: 1px solid #ccc;
            display: flex;
            align-items: center;
            justify-content: center;
            color: #999;
            font-size: 18px;
            margin-bottom: 15px;
        }

        .preview-controls {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding-top: 10px;
            border-top: 1px solid #ddd;
        }

        .preview-btn {
            background-color: #f5f5f5;
            border: 1px solid #999;
            padding: 5px 10px;
            cursor: pointer;
            font-size: 14px;
        }

        .pagination {
            text-align: center;
            font-size: 14px;
            color: #666;
        }

        .details-section {
            flex: 1;
            background-color: white;
            border: 2px solid #999;
            padding: 30px;
        }

        .detail-group {
            margin-bottom: 25px;
        }

        .detail-label {
            font-weight: bold;
            color: #333;
            font-size: 14px;
            display: inline;
        }

        .detail-value {
            color: #333;
            font-size: 14px;
            font-family: Arial, sans-serif;
            line-height: 1.6;
            overflow-wrap: anywhere;
        }

        .not-found-message {
            color: #8a1f11;
            background-color: #fff3f0;
            border: 1px solid #e2b8ae;
            padding: 15px;
            text-align: center;
            font-size: 16px;
        }

        .scrollbar-decoration {
            width: 25px;
            flex-shrink: 0;
            border: 2px solid #999;
            border-radius: 15px;
            background-color: #f5f5f5;
        }

        .button-group {
            display: flex;
            gap: 15px;
            margin-top: 30px;
            justify-content: center;
        }

        .btn {
            padding: 12px 30px;
            border: 2px solid #999;
            background-color: white;
            color: #333;
            font-size: 16px;
            border-radius: 25px;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .btn:hover {
            background-color: #f0f0f0;
        }

        .btn-editor {
            min-width: 120px;
        }

        .btn-descargar {
            min-width: 150px;
        }

        .icon-download {
            margin-right: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Header -->
        <div class="header">
            <div class="header-icon">🏠</div>
            <div class="user-icon">👤</div>
        </div>

        <!-- Main Content -->
        <div class="container">
            <h1 class="page-title">Detalles del Doc</h1>

            <div class="content-wrapper">
                <!-- Document Preview Section -->
                <div class="preview-section">
                    <div class="doc-preview">Doc. Preview</div>
                    <div class="preview-controls">
                        <button type="button" class="preview-btn">◀</button>
                        <span class="pagination">Pag. 1</span>
                        <button type="button" class="preview-btn">▶</button>
                    </div>
                </div>

                <!-- Document Details Section -->
                <div class="details-section">
                    <asp:Panel ID="pnlMensaje" runat="server" CssClass="not-found-message" Visible="false">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </asp:Panel>

                    <asp:Panel ID="pnlDetalles" runat="server">
                        <div class="detail-group">
                            <span class="detail-label">Nombre del Doc.: </span>
                            <asp:Label ID="lblDocumentName" runat="server" CssClass="detail-value"></asp:Label>
                        </div>

                        <div class="detail-group">
                            <span class="detail-label">Descripción: </span>
                            <asp:Label ID="lblDescription" runat="server" CssClass="detail-value"></asp:Label>
                        </div>

                        <div class="detail-group">
                            <span class="detail-label">Categoría: </span>
                            <asp:Label ID="lblCategory" runat="server" CssClass="detail-value"></asp:Label>
                        </div>

                        <div class="detail-group">
                            <span class="detail-label">Fecha del Doc.: </span>
                            <asp:Label ID="lblDocDate" runat="server" CssClass="detail-value"></asp:Label>
                        </div>

                        <div class="detail-group">
                            <span class="detail-label">Subido por Usuario: </span>
                            <asp:Label ID="lblDocUser" runat="server" CssClass="detail-value"></asp:Label>
                        </div>

                        <div class="detail-group">
                            <span class="detail-label">Fecha de Registro: </span>
                            <asp:Label ID="lblUploadDate" runat="server" CssClass="detail-value"></asp:Label>
                        </div>

                        <asp:Panel ID="pnlModificationDate" runat="server" CssClass="detail-group">
                            <span class="detail-label">Fecha de Modificación: </span>
                            <asp:Label ID="lblModificationDate" runat="server" CssClass="detail-value"></asp:Label>
                        </asp:Panel>

                        <div class="detail-group">
                            <span class="detail-label">SHA256: </span>
                            <asp:Label ID="lblSHA256" runat="server" CssClass="detail-value"></asp:Label>
                        </div>

                        <div class="button-group">
                            <asp:Button ID="btnEditor" runat="server" Text="✎ Editor" CssClass="btn btn-editor" />
                            <asp:Button ID="btnDescargar" runat="server" Text="⬇ Descargar" CssClass="btn btn-descargar" OnClick="btnDescargar_Click" />
                        </div>
                    </asp:Panel>
                </div>

                <!-- Right Scrollbar Decoration -->
                <div class="scrollbar-decoration"></div>
            </div>
        </div>
    </form>
</body>
</html>
