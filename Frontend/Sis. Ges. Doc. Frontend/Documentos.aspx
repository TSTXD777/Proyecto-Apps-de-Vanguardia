<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Documentos.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Documentos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Consultar Docs</title>
    <style>
        body { font-family: Arial, Helvetica, sans-serif; margin:0; padding:0; background:#f7f7f7; }
        .topbar { background:#fff; height:56px; border-bottom:2px solid #e0e0e0; display:flex; align-items:center; padding:0 16px; }
        .topbar .left, .topbar .right { width:48px; }
        .topbar .center { flex:1; text-align:center; font-weight:bold; font-size:18px; color:#555; }
        .page-title { text-align:center; margin:28px 0 8px; font-size:36px; color:#666; font-weight:600; }
        .content { display:flex; padding:0 32px 32px; gap:24px; }
        .sidebar { width:220px; background:#fff; border:1px solid #ddd; padding:16px; box-sizing:border-box; }
        .sidebar h3 { margin:0 0 12px; font-size:18px; color:#666; }
        .filter-group { border-top:1px solid #eee; padding-top:12px; margin-top:8px; }
        .filter-item { margin:8px 0; color:#444; }
        .filter-control { width:100%; box-sizing:border-box; padding:8px; border:1px solid #ddd; border-radius:4px; margin:4px 0 10px; }
        .filter-list label { display:block; margin:8px 0; color:#444; }
        .clear-btn { width:100%; background:#fff; border:1px solid #ccc; padding:9px 10px; border-radius:6px; cursor:pointer; color:#444; margin-top:12px; }
        .main { flex:1; }
        .search-wrapper { display:flex; align-items:center; gap:8px; margin-bottom:18px; }
        .search-input { flex:1; padding:10px 12px; border:2px solid #e0e0e0; border-radius:6px; font-size:16px; }
        .search-btn { background:#fff; border:2px solid #e0e0e0; padding:8px 12px; border-radius:6px; cursor:pointer; }
        .doc-list { background:#fff; border:1px solid #ddd; min-height:300px; max-height:520px; overflow:auto; padding:12px 16px; }
        .doc-table { width:100%; border-collapse:collapse; color:#444; }
        .doc-table th { text-align:left; padding:10px 8px; border-bottom:1px solid #ddd; color:#666; font-size:14px; }
        .doc-table td { padding:12px 8px; border-bottom:1px dashed #e6e6e6; vertical-align:middle; }
        .doc-table tr:last-child td { border-bottom:none; }
        .doc-title { font-size:16px; color:#2f6fad; text-decoration:none; }
        .doc-title:hover { text-decoration:underline; }
        .empty-message { color:#666; padding:16px 8px; }
        .error-message { color:#b00020; margin-bottom:12px; }
        .filter-list { list-style-type: none; padding: 0; margin: 0; }
        .filter-list li { display: flex; align-items: center; margin: 8px 0; }
        .filter-list input[type="checkbox"] { margin-right: 8px; }
        .filter-list label { display: inline; margin: 0; color:#444; }
        .category-dropdown { cursor: pointer; user-select: none; }
        .category-items { display: none; padding-left: 16px; }
        .category-items.show { display: block; }
        .document-card{
    background:#fff;
    border-radius:18px;
    padding:20px;
    margin-bottom:15px;
    display:flex;
    align-items:center;
    justify-content:space-between;
    box-shadow:0 5px 15px rgba(0,0,0,.08);
    transition:.3s;
}

.document-card:hover{
    transform:translateY(-3px);
    box-shadow:0 10px 25px rgba(0,0,0,.12);
}

.document-icon{
    font-size:40px;
    margin-right:20px;
}

.document-info{
    flex:1;
}

.document-title a{
    text-decoration:none;
    font-size:18px;
    font-weight:600;
    color:#1b5e20;
}

.document-title a:hover{
    color:#2e7d32;
}

.document-meta{
    margin-top:6px;
    color:#666;
    font-size:14px;
}

.view-btn{
    background:#2e7d32;
    color:white;
    text-decoration:none;
    padding:10px 18px;
    border-radius:10px;
    font-weight:600;
    transition:.3s;
}

.view-btn:hover{
    background:#256428;
}

@media(max-width:768px){

    .document-card{
        flex-direction:column;
        align-items:flex-start;
        gap:15px;
    }

    .document-action{
        width:100%;
    }

    .view-btn{
        display:block;
        text-align:center;
    }

}
        /* responsive */
        @media(max-width:800px){ .content{flex-direction:column;} .sidebar{width:100%;} }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="topbar">
              <a href="Dashboard.aspx" class="icon" style="text-decoration:none;">
        🏠
    </a>
            <div class="center">&nbsp;</div>
            <div class="right" style="text-align:right">👤</div>
        </div>

        <div class="page-title">Consultar Docs</div>

        <div class="content">
            <aside class="sidebar">
                <h3>Filtros</h3>
                <div class="filter-group">
                            <div class="category-dropdown" style="font-weight:600; margin-bottom:8px;" onclick="toggleCategoryDropdown()">Categoría ▾</div>
                            <div class="category-items" id="categoryItems">
                                <asp:CheckBoxList ID="cblCategorias" runat="server" CssClass="filter-list" AutoPostBack="true" OnSelectedIndexChanged="Filter_Changed" />
                            </div>
                        </div>
                <div class="filter-group">
                    <div style="font-weight:600; margin-bottom:8px;">Fecha</div>
                    <label class="filter-item" for="txtFechaInicio">Inicio</label>
                    <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="filter-control" TextMode="Date" AutoPostBack="true" OnTextChanged="Filter_Changed" />
                    <label class="filter-item" for="txtFechaFin">Fin</label>
                    <asp:TextBox ID="txtFechaFin" runat="server" CssClass="filter-control" TextMode="Date" AutoPostBack="true" OnTextChanged="Filter_Changed" />
                </div>
                <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar filtros" CssClass="clear-btn" OnClick="btnLimpiarFiltros_Click" />
            </aside>

            <section class="main">
                <div class="search-wrapper">
                    <asp:TextBox ID="txtBuscar" runat="server" placeholder="Buscar..." CssClass="search-input" AutoPostBack="true" OnTextChanged="Filter_Changed" />
                    <asp:Button ID="btnBuscar" runat="server" Text="🔍" CssClass="search-btn" OnClick="Filter_Changed" />
                </div>

                <div class="doc-list" id="docList">
                    <asp:Label ID="lblError" runat="server" CssClass="error-message" Visible="false" />
                    <asp:Repeater ID="rptDocumentos" runat="server">

    <ItemTemplate>

        <div class="document-card">

            <div class="document-icon">
                📄
            </div>

            <div class="document-info">

                <div class="document-title">
                    <a href='<%# "detalles.aspx?id=" + Eval("IdDocumento") %>'>
                        <%#: Eval("NombreDocumento") %>
                    </a>
                </div>

                <div class="document-meta">
                    Categoría:
                    <%#: Eval("Categoria") %>
                </div>

                <div class="document-meta">
                    Fecha:
                    <%#: Eval("FechaDocumentoTexto") %>
                </div>

            </div>

            <div class="document-action">

                <a class="view-btn"
                    href='<%# "detalles.aspx?id=" + Eval("IdDocumento") %>'>
                    Ver Documento
                </a>

            </div>

        </div>

    </ItemTemplate>

</asp:Repeater>
                    <asp:Panel ID="pnlSinResultados" runat="server" CssClass="empty-message" Visible="false">
                        No se encontraron documentos.
                    </asp:Panel>
                </div>
            </section>
        </div>
    </form>
    <script>
        function toggleCategoryDropdown() {
            var items = document.getElementById('categoryItems');
            items.classList.toggle('show');
            var arrow = document.querySelector('.category-dropdown');
            if (items.classList.contains('show')) {
                arrow.innerHTML = 'Categoría △';
            } else {
                arrow.innerHTML = 'Categoría ▾';
            }
        }
    </script>
</body>
</html>
