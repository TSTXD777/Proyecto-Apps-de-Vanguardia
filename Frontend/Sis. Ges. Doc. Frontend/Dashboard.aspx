<%@ Page Language="C#" MasterPageFile="~/WebApp.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Sis.Ges.Doc.Frontend.Dashboard" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .dashboard-container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 600px;
            gap: 40px;
            padding: 40px 20px;
        }

        .dashboard-button {
            background-color: #f5f5f5;
            border: 3px solid #999;
            border-radius: 8px;
            padding: 80px 60px;
            min-width: 200px;
            text-align: center;
            cursor: pointer;
            transition: all 0.3s ease;
            font-size: 18px;
            font-weight: 500;
            color: #666;
            text-decoration: none;
            display: flex;
            align-items: center;
            justify-content: center;
        }

        .dashboard-button:hover {
            background-color: #e8e8e8;
            border-color: #666;
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .dashboard-button:active {
            transform: translateY(0);
        }

        .dashboard-title {
            text-align: center;
            font-size: 36px;
            font-weight: 300;
            color: #666;
            margin-bottom: 60px;
        }
    </style>

    <div class="dashboard-title">Dashboard</div>

    <div class="dashboard-container">
        <asp:Button ID="btnRegistroDocs" runat="server" CssClass="dashboard-button" 
            Text="Registro de&#13;&#10;Docs" OnClick="btnRegistroDocs_Click" />

        <asp:Button ID="btnConsultaDocs" runat="server" CssClass="dashboard-button" 
            Text="Consulta de&#13;&#10;Docs" OnClick="btnConsultaDocs_Click" />

        <asp:Button ID="btnBitacora" runat="server" CssClass="dashboard-button" 
            Text="Bitácora" OnClick="btnBitacora_Click" />
    </div>
</asp:Content>
