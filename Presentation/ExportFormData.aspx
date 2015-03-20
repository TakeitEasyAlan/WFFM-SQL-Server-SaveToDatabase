<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportFormData.aspx.cs" Inherits="WFFM.SQLServer.SaveToDatabase.Presentation.ExportFormData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:PlaceHolder ID="NotLoggedLoggedInPlaceHolder" runat="server" Visible="false">
                <div style="color: red">You have to be logged in and or missing item id or name querystring</div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="WithDateRange" runat="server" Visible="false">
                From Date:<asp:TextBox ID="txtFrom" runat="server"></asp:TextBox><asp:Label ID="warnFrom" runat="server" ForeColor="Red" ViewStateMode="Disabled"></asp:Label>
                To Date:<asp:TextBox ID="txtTo" runat="server"></asp:TextBox><asp:Label ID="warnTo" runat="server" ForeColor="Red" ViewStateMode="Disabled"></asp:Label>
                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" Text="Export" />
                
            </asp:PlaceHolder>
        </div>
    </form>
</body>
</html>
