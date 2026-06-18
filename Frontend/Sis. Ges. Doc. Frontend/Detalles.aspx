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
            margin-bottom: 8px;
            font-size: 14px;
        }

        .detail-input {
            width: 100%;
            padding: 10px;
            border: none;
            border-bottom: 1px solid #999;
            font-size: 14px;
            font-family: Arial, sans-serif;
        }

        .detail-textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            font-size: 14px;
            font-family: Arial, sans-serif;
            resize: vertical;
            min-height: 80px;
        }

        .detail-input:focus,
        .detail-textarea:focus {
            outline: none;
            border-color: #666;
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
                        <span class="pagination">Pag. <asp:TextBox ID="txtPageNumber" runat="server" Text="1" style="width: 30px; text-align: center; border: none; background-color: transparent;"></asp:TextBox></span>
                        <button type="button" class="preview-btn">▶</button>
                    </div>
                </div>

                <!-- Document Details Section -->
                <div class="details-section">
                    <div class="detail-group">
                        <label class="detail-label">Nombre del Doc.</label>
                        <asp:TextBox ID="txtDocumentName" runat="server" CssClass="detail-input" placeholder="Ingrese el nombre del documento"></asp:TextBox>
                    </div>

                    <div class="detail-group">
                        <label class="detail-label">Descripción</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="detail-textarea" TextMode="MultiLine" placeholder="Ingrese la descripción del documento"></asp:TextBox>
                    </div>

                    <div class="detail-group">
                        <label class="detail-label">Categoría:</label>
                        <asp:TextBox ID="txtCategory" runat="server" CssClass="detail-input" placeholder="Ingrese la categoría"></asp:TextBox>
                    </div>

                    <div class="detail-group">
                        <label class="detail-label">Fecha del Doc:</label>
                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="detail-input" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="detail-group">
                        <label class="detail-label">Subido por Usuario:</label>
                        <asp:TextBox ID="txtDocUser" runat="server" CssClass="detail-input" placeholder="Usuario"></asp:TextBox>
                    </div>

                    <div class="detail-group">
                        <label class="detail-label">Fecha de Registro: </label>
                        <asp:TextBox ID="txtUploadDate" runat="server" CssClass="detail-input" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="detail-group">
                        <label class="detail-label">Fecha de Última Modificación: </label>
                        <asp:TextBox ID="txtModificationDate" runat="server" CssClass="detail-input" TextMode="Date"></asp:TextBox>
                    </div>

                    <div class="detail-group">
                        <label class="detail-label">SHA256: </label>
                        <asp:TextBox ID="txtSHA256" runat="server" CssClass="detail-input" placeholder="SHA256 Hash"></asp:TextBox>
                    </div>

                    <div class="button-group">
                        <asp:Button ID="btnEditor" runat="server" Text="✎ Editor" CssClass="btn btn-editor" />
                        <asp:Button ID="btnDescargar" runat="server" Text="⬇ Descargar" CssClass="btn btn-descargar" />
                    </div>
                </div>

                <!-- Right Scrollbar Decoration -->
                <div class="scrollbar-decoration"></div>
            </div>
        </div>
    </form>
</body>
</html>
