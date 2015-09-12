<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="emailtest.aspx.cs" Inherits="SPLC.Donor.RSVP.emailtest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>Email Test</h1>

        <table>
            <tr>
                <th>To:</th>
                <td><asp:TextBox ID="txtTo" runat="server" /></td>
            </tr>
            <tr>
                <th>Subject:</th>
                <td><asp:TextBox ID="txtSubject" runat="server" /></td>
            </tr>
            <tr>
                <th>Body:</th>
                <td><asp:TextBox ID="txtBody" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Send Email Test" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblError" runat="server" ForeColor="Red" /></td>

            </tr>
        </table>



    </div>
    </form>
</body>
</html>
