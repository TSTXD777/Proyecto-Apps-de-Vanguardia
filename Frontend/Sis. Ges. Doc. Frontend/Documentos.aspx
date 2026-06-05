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
        .main { flex:1; }
        .search-wrapper { display:flex; align-items:center; gap:8px; margin-bottom:18px; }
        .search-input { flex:1; padding:10px 12px; border:2px solid #e0e0e0; border-radius:6px; font-size:16px; }
        .search-btn { background:#fff; border:2px solid #e0e0e0; padding:8px 12px; border-radius:6px; cursor:pointer; }
        .doc-list { background:#fff; border:1px solid #ddd; min-height:300px; max-height:520px; overflow:auto; padding:12px 16px; }
        .doc-item { padding:12px 8px; border-bottom:1px dashed #e6e6e6; color:#444; display:flex; justify-content:space-between; align-items:center; }
        .doc-item:last-child { border-bottom:none; }
        .doc-title { font-size:16px; }
        .doc-action { width:48px; text-align:center; color:#6aa84f; font-weight:bold; }
        /* responsive */
        @media(max-width:800px){ .content{flex-direction:column;} .sidebar{width:100%;} }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="topbar">
            <div class="left">🏠</div>
            <div class="center">&nbsp;</div>
            <div class="right" style="text-align:right">👤</div>
        </div>

        <div class="page-title">Consultar Docs</div>

        <div class="content">
            <aside class="sidebar">
                <h3>Filtros</h3>
                <div class="filter-group">
                    <div style="font-weight:600; margin-bottom:8px;">Categoría 1 ▾</div>
                    <label class="filter-item"><input type="checkbox" /> Item</label>
                    <label class="filter-item"><input type="checkbox" /> Item</label>
                    <label class="filter-item"><input type="checkbox" /> Item</label>
                </div>
            </aside>

            <section class="main">
                <div class="search-wrapper">
                    <input type="text" placeholder="Buscar..." class="search-input" />
                    <button type="button" class="search-btn">🔍</button>
                </div>

                <div class="doc-list" id="docList">
                    <div class="doc-item"><div class="doc-title">Doc 1...</div><div class="doc-action">›</div></div>
                    <div class="doc-item"><div class="doc-title">Doc 2...</div><div class="doc-action">›</div></div>
                    <div class="doc-item"><div class="doc-title">Doc 3...</div><div class="doc-action">›</div></div>
                    <div class="doc-item"><div class="doc-title">Doc 4...</div><div class="doc-action">›</div></div>
                    <div class="doc-item"><div class="doc-title">Doc 5...</div><div class="doc-action">›</div></div>
                    <div class="doc-item"><div class="doc-title">Doc 6...</div><div class="doc-action">›</div></div>
                    <div class="doc-item"><div class="doc-title">Doc 7...</div><div class="doc-action">›</div></div>
                    <div class="doc-item"><div class="doc-title">Doc 8...</div><div class="doc-action">›</div></div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
