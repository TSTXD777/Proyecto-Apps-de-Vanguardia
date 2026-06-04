<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>GestorDOC - Iniciar Sesión</title>
    <link href="Styles/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="login" runat="server">
        
        <img src="Assets/GestorDoc_Logo.png" alt="GestorDOC Logo" class="app-logo" />
        <div class="login-card">

            <asp:Label ID="lblError" runat="server" EnableViewState="false"></asp:Label>

            <div class="field">
                <label for="username">Usuario:</label>
                <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
            </div>

            <div class="field">
                <label for="password">Contraseña:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>

            <div class="remember-row">
                <asp:CheckBox ID="chkRememberMe" runat="server" />
                <label for="chkRememberMe">Recordar Sesión</label>
            </div>

            <div class="divider"></div>

            <asp:Button ID="btnLogIn" runat="server" Text="Iniciar Sesión" CssClass="btn-signin" OnClick="btnLoginUser" />

        </div>
    </form>
</body>
</html>
