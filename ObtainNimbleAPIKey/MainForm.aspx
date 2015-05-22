<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainForm.aspx.cs" Inherits="ObtainNimbleAPIKey.MainForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <asp:Label ID="Label1" runat="server" Text="Authorization Status:"></asp:Label>
        &nbsp;<asp:Label ID="labelStatus" runat="server" Text="NOT Authorized"></asp:Label>
        <br />
        <br />
        <asp:Button ID="ButtonRun" runat="server" OnClick="ButtonRun_Click" Text="Run Authorization Process" />
        <br />
        <br />
        <asp:Label ID="labelTokenDescription" runat="server" Text="Access token value:" Visible="False"></asp:Label>
        &nbsp;<asp:Label ID="labelToken" runat="server" Font-Bold="True" Text="Label" Visible="False"></asp:Label>
    
    </div>
    </form>
</body>
</html>
