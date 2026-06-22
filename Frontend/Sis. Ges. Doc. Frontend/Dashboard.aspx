<%@ Page Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

<style>

    .dashboard-title{
        text-align:center;
        font-size:42px;
        font-weight:600;
        color:#2c3e50;
        margin:40px 0;
    }

    .dashboard-container{
        display:flex;
        justify-content:center;
        flex-wrap:wrap;
        gap:30px;
        padding:20px;
    }

    .dashboard-card{
        width:260px;
        height:220px;
        background:#ffffff;
        border:none;
        border-radius:20px;
        box-shadow:0 8px 20px rgba(0,0,0,.12);
        cursor:pointer;
        transition:.3s;
        font-size:22px;
        font-weight:600;
        color:#34495e;
        display:flex;
        flex-direction:column;
        justify-content:center;
        align-items:center;
    }

    .dashboard-card:hover{
        transform:translateY(-8px);
        box-shadow:0 15px 30px rgba(0,0,0,.18);
    }

    .dashboard-icon{
        font-size:60px;
        margin-bottom:20px;
    }

    .registro{
        color:#2e7d32;
    }

    .consulta{
        color:#1976d2;
    }

    .bitacora{
        color:#ff9800;
    }

    .dashboard-subtitle{
        text-align:center;
        color:#7f8c8d;
        margin-top:-20px;
        margin-bottom:40px;
        font-size:16px;
    }

</style>

<div class="dashboard-title">
    GestorDOC
</div>

<div class="dashboard-subtitle">
    Sistema de Gestión Documental
</div>

<div class="dashboard-container">

    <asp:Button ID="btnRegistroDocs"
        runat="server"
        CssClass="dashboard-card"
        Text="📄&#13;&#10;Registro de Documentos"
        OnClick="btnRegistroDocs_Click" />

    <asp:Button ID="btnConsultaDocs"
        runat="server"
        CssClass="dashboard-card"
        Text="🔍&#13;&#10;Consulta de Documentos"
        OnClick="btnConsultaDocs_Click" />

    <asp:Button ID="btnBitacora"
        runat="server"
        CssClass="dashboard-card"
        Text="📋&#13;&#10;Bitácora"
        OnClick="btnBitacora_Click" />

</div>

</asp:Content>