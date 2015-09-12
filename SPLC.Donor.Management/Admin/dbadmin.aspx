<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dbadmin.aspx.cs" Inherits="SPLC.Donor.Management.Admin.dbadmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
            <th><asp:Label ID="lblConn" runat="server" /></th>
        </tr>
        <tr>
            <th><asp:Label ID="lblConnTest" runat="server" /></th>
        </tr>
        <tr>
            <td><asp:Button ID="btnRun" runat="server" Text="Run SQL" OnClick="btnRun_Click" /></td>
        </tr>
        <tr>
            <td>
            <asp:TextBox ID="txtSQL" runat="server" TextMode="MultiLine" Width="600" Height="100" />
                </td>
        </tr>
        <tr>
            <th><asp:Label ID="lblMessage" runat="server" /></th>
        </tr>
    </table>
        <br /><br />
        <asp:GridView ID="gvResults" runat="server" AutoGenerateColumns="true"></asp:GridView>
    </div>
    </form>
</body>
</html>
