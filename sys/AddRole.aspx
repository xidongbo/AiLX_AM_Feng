<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addRole.aspx.cs" Inherits="addRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 226px;
        }
        #form1
        {
            width: 323px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <table style="margin:auto;">
    <tr>
        <td colspan="2" align="center">
            <asp:TextBox ID="id" runat="server" Visible="False"></asp:TextBox>
            <br />
            <br />
        </td>
        
    </tr>
        <tr>
        <td>Role:</td>
        <td class="style1">
            <asp:TextBox ID="rolename" runat="server" Width="277px" 
                AutoCompleteType="Disabled"></asp:TextBox></td>
    </tr>
        <tr>
        <td colspan ="2" align="center" >
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="提交" />
            &nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Return" />
            </td>
      
    </tr>
    </table>
    </form>
</body>
</html>
