<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addUser.aspx.cs" Inherits="addUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            height: 23px;
            width: 143px;
        }
        .style4
        {
            width: 143px;
        }
        .style5
        {
            width: 71px;
        }
        .style6
        {
            height: 23px;
            width: 71px;
        }
        .style7
        {
            width: 325px;
        }
        .style8
        {
            height: 23px;
            width: 325px;
        }
    </style>
</head>
<body style="width: 566px">
    <form id="form1" runat="server" >
    <table style="margin:auto; width: 528px;">
    <tr>
        <td class="style4"></td>
        <td colspan="2" align="center">
            <asp:TextBox ID="id" runat="server" Visible="False"></asp:TextBox>

        </td>

    </tr>
        <tr>
        <td class="style2">CC</td>
        <td class="style6"><asp:TextBox ID="Txtcc" runat="server" Width="277px" 
                AutoCompleteType="Disabled" EnableTheming="False" EnableViewState="False"></asp:TextBox></td>
        <td class="style8"></td>
    </tr>
    <tr>
        <td class="style4">Dept</td>
        <td class="style5"><asp:TextBox ID="Txtdept" runat="server" Width="277px" 
                AutoCompleteType="Disabled" EnableTheming="False" EnableViewState="False"></asp:TextBox></td>
        <td class="style7"></td>
    </tr>

        <tr>
        <td class="style2">Login name</td>
        <td class="style6"><asp:TextBox ID="owner" runat="server" Width="277px" 
                AutoCompleteType="Disabled" EnableTheming="False" EnableViewState="False"></asp:TextBox></td>
        <td class="style7"></td>
    </tr>
       <tr>
        <td class="style4">Name</td>
        <td class="style5"><asp:TextBox ID="TxtName" runat="server" Width="277px" 
                AutoCompleteType="Disabled" EnableTheming="False" EnableViewState="False"></asp:TextBox></td>
        <td class="style7"></td>
    </tr>
        <tr>
        <td class="style4">Role</td>
        <td class="style5">
            <asp:DropDownList ID="role" runat="server" Height="277px" Width="277px">
            </asp:DropDownList>
            </td>
        <td class="style7"></td>
    </tr>
        <tr>
        <td class="style2">email</td>
        <td class="style6">
            <asp:TextBox ID="email" runat="server" Width="277px" 
                AutoCompleteType="Disabled"></asp:TextBox>
            </td>
        <td class="style8"></td>
    </tr>
    </tr>
        <tr>
        <td class="style4">Arrange</td>
        <td class="style5">
            <asp:DropDownList ID="arrange" runat="server" Height="277px" Width="277px">
                <asp:ListItem>all</asp:ListItem>
                <asp:ListItem>dept</asp:ListItem>
            </asp:DropDownList>
            </td>
        <td class="style7">Empty is All</td>
    </tr>
        <tr>
        <td colspan ="2" align="center" >
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="提交" 
                Width="68px" />
            &nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Return" />
            </td>
        <td class="style7"></td>
    </tr>
    </table>
    </form>
</body>
</html>
