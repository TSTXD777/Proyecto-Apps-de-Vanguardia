<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Usuarios</title>

    <style>
        body{
    font-family:'Segoe UI',sans-serif;
    background:#eef2f7;
    margin:0;
}

/* HEADER */

.header{
    background:linear-gradient(135deg,#1b5e20,#2e7d32);
    color:white;
    padding:20px 30px;
    display:flex;
    justify-content:space-between;
    align-items:center;
    box-shadow:0 4px 15px rgba(0,0,0,.15);
}

.header h2{
    margin:0;
    font-size:38px;
}

.header a{
    color:white;
    text-decoration:none;
    font-size:28px;
}

/* LAYOUT */

.container{
    padding:30px;
    display:flex;
    gap:25px;
    align-items:flex-start;
}

/* FORMULARIO */

.formulario{
    width:400px;
    background:white;
    border:none;
    border-radius:18px;
    padding:25px;
    box-shadow:0 5px 20px rgba(0,0,0,.08);
}

.formulario h3{
    margin-top:0;
    margin-bottom:25px;
    color:#2e7d32;
    font-size:26px;
}

.campo{
    margin-bottom:18px;
}

.campo label{
    display:block;
    margin-bottom:8px;
    font-weight:600;
    color:#333;
}

.campo input,
.campo select,
.formulario input[type=text],
.formulario input[type=password],
.formulario select{

    width:100%;
    padding:12px;
    border:1px solid #dcdcdc;
    border-radius:10px;
    font-size:15px;
    box-sizing:border-box;
}

/* BOTON */

.btn{
    width:100%;
    background:#2e7d32;
    color:white;
    border:none;
    padding:14px;
    border-radius:10px;
    font-size:15px;
    font-weight:600;
    cursor:pointer;
    transition:.3s;
}

.btn:hover{
    background:#256428;
}

/* MENSAJE */

.mensaje{
    display:block;
    margin-top:15px;
    font-weight:600;
}

/* TABLA */

.grid{
    flex:1;
    width:100%;
    background:white;
    border-collapse:collapse;
    border-radius:18px;
    overflow:hidden;
    box-shadow:0 5px 20px rgba(0,0,0,.08);
}

.grid th{
    background:#2e7d32;
    color:white;
    padding:16px;
    text-align:left;
    font-size:15px;
}

.grid td{
    padding:15px;
    border-bottom:1px solid #ececec;
}

.grid tr:nth-child(even){
    background:#fafafa;
}

.grid tr:hover{
    background:#f5faf5;
}

/* BOTON EDITAR */

.grid input[type=submit],
.grid input[type=button]{
    background:#1976d2;
    color:white;
    border:none;
    padding:8px 14px;
    border-radius:8px;
    cursor:pointer;
}

.grid input[type=submit]:hover,
.grid input[type=button]:hover{
    background:#1259a7;
}

/* RESPONSIVE */

@media(max-width:1000px){

    .container{
        flex-direction:column;
    }

    .formulario{
        width:100%;
    }

}background-color: #2e7d32;
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