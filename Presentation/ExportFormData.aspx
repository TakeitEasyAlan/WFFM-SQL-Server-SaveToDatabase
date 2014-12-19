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
            <div style="color:red">You have to be logged in and or missing item id or name querystring</div>
        </asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
