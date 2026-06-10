<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Perfil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Perfil</title>

    <style>
        body {
            font-family: Arial;
            background-color: #f5f5f5;
        }

        .perfil-card {
            width: 450px;
            margin: 80px auto;
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0,0,0,.15);
        }

        .titulo {
            text-align: center;
            margin-bottom: 25px;
        }

        .dato {
            margin: 15px 0;
            font-size: 18px;
        }

        .btn {
            display: block;
            width: 100%;
            margin-top: 10px;
            padding: 12px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div class="perfil-card">

            <h2 class="titulo">Mi Perfil</h2>

            <div class="dato">
                <strong>Usuario:</strong>
                <asp:Label ID="lblUsuario" runat="server"></asp:Label>
            </div>

            <div class="dato">
                <strong>Rol:</strong>
                <asp:Label ID="lblRol" runat="server"></asp:Label>
            </div>

            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">

                <asp:Panel ID="Panel1" runat="server" Visible="false">

                    <asp:Button ID="Button1"
                        runat="server"
                        CssClass="btn"
                        Text="Administrar Usuarios" />

                </asp:Panel>

                                <asp:Button ID="btnAdministrarUsuarios"
                    runat="server"
                    CssClass="btn"
                    Text="Administrar Usuarios"
                    PostBackUrl="~/Usuarios.aspx" />

            </asp:Panel>

            <asp:Button ID="btnCerrarSesion"
                runat="server"
                CssClass="btn"
                Text="Cerrar Sesión"
                OnClick="btnCerrarSesion_Click" />

        </div>

    </form>
</body>
</html>