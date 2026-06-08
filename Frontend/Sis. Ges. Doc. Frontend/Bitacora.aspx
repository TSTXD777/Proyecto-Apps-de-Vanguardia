<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Bitacora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Bitácora</title>
    <style>
        body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin:0; padding:0; background:#f5f5f5; }
        .page { min-height:100vh; display:flex; flex-direction:column; }
        .header { background:#fff; padding:12px 20px; border-bottom:3px solid #ddd; display:flex; align-items:center; justify-content:space-between; }
        .brand { display:flex; align-items:center; gap:12px; }
        .icon { width:38px; height:38px; border-radius:6px; background:#eee; display:flex; align-items:center; justify-content:center; font-size:18px; }
        .title { font-size:48px; font-weight:600; color:#444; text-align:center; flex:1; }
        .content { display:flex; gap:20px; padding:24px; flex:1; }
        .sidebar { width:260px; background:#fff; padding:18px; border:1px solid #ddd; border-radius:6px; }
        .sidebar h3 { margin:0 0 8px 0; font-size:20px; }
        .field { margin:10px 0; }
        .field label { display:block; font-size:13px; color:#666; margin-bottom:6px; }
        .field input[type=text], .field input[type=date] { width:100%; padding:8px 10px; border:1px solid #ccc; border-radius:4px; }
        .ops { margin-top:8px; }
        .ops label { display:flex; align-items:center; gap:8px; font-size:14px; color:#333; }
        .main { flex:1; background:#fff; border:1px solid #ddd; border-radius:6px; padding:12px; display:flex; flex-direction:column; }
        .table-wrap { overflow:auto; flex:1; border-top:1px solid #eee; margin-top:6px; }
        table { width:100%; border-collapse:collapse; min-width:800px; }
        th, td { padding:10px 12px; border-bottom:1px solid #eee; text-align:left; vertical-align:middle; }
        th { background:#fafafa; font-weight:600; border-right:1px solid #eee; }
        td.center { text-align:center; }
        .download-area { padding:16px; display:flex; justify-content:center; }
        .download-btn { background:#fff; border:2px solid #b3b3b3; padding:10px 22px; border-radius:24px; cursor:pointer; font-size:16px; }
        .download-btn:hover { background:#f0f0f0; }
        /* scrollbar for table wrap */
        .table-wrap::-webkit-scrollbar { width:12px; }
        .table-wrap::-webkit-scrollbar-thumb { background:#ddd; border-radius:6px; }
        @media(max-width:900px){ .content{flex-direction:column;} .sidebar{width:100%} .title{font-size:36px} }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="page">
        <div class="header">
    <a href="Dashboard.aspx" class="icon" style="text-decoration:none;">
        🏠
    </a>

    <div class="title">Bitácora</div>

    <a href="Login.aspx" class="icon" style="text-decoration:none;">
    👤
</a>
</div>

        <div class="content">
            <aside class="sidebar">
                <h3>Filtros</h3>
                <div class="field">
                    <label for="txtUser">User</label>
                    <input type="text" id="txtUser" />
                </div>
                <div class="field">
                    <label for="txtDoc">Doc</label>
                    <input type="text" id="txtDoc" />
                </div>
                <div class="field">
                    <label>Operación</label>
                    <div class="ops">
                        <label><input type="checkbox" /> Crear</label>
                        <label><input type="checkbox" /> Modificar</label>
                        <label><input type="checkbox" /> Descargar</label>
                        <label><input type="checkbox" /> Eliminar</label>
                    </div>
                </div>
                <div class="field">
                    <label for="fechaIni">Fecha Inicial</label>
                    <input type="date" id="fechaIni" />
                </div>
                <div class="field">
                    <label for="fechaFin">Fecha Final</label>
                    <input type="date" id="fechaFin" />
                </div>
            </aside>

            <section class="main">
                <div style="flex:0 0 auto; padding:6px 4px; color:#666;">&nbsp;</div>
                <div class="table-wrap">
                    <table>
                        <thead>
                            <tr>
                                <th style="width:40px;"><input type="checkbox" /></th>
                                <th style="width:140px;">User</th>
                                <th>Doc</th>
                                <th style="width:160px;">Operación</th>
                                <th style="width:200px;">Fecha y Hora</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="center"><input type="checkbox" /></td>
                                <td>juan.perez</td>
                                <td>Documento de muestra.pdf</td>
                                <td>Descargar</td>
                                <td>01/01/2026 10:30:12 AM</td>
                            </tr>
                            <tr>
                                <td class="center"><input type="checkbox" /></td>
                                <td>maria.lopez</td>
                                <td>Contrato.docx</td>
                                <td>Modificar</td>
                                <td>02/01/2026 02:12:05 PM</td>
                            </tr>
                            <tr>
                                <td class="center"><input type="checkbox" /></td>
                                <td>admin</td>
                                <td>Informe.xlsx</td>
                                <td>Crear</td>
                                <td>03/01/2026 09:00:00 AM</td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="download-area">
                    <input type="button" value="Descargar Informe" class="download-btn" />
                </div>
            </section>
        </div>
    </form>
</body>
</html>
//Oa 