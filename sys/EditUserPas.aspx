<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditUserPas.aspx.cs" Inherits="System_EditUserPas" %>

<html>
<head id="Head1" runat="server">
    <title>Change Password</title>
    <link href="../css/adminCss.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src="../js/submit.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td  style="height: 22px">
                    &nbsp;<asp:Label ID="lblwz" runat="server" height="22">ChangePassword</asp:Label></td>
                </tr>
        </table>
        <table width="98%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td bgcolor="#9AA29A" height="5px">
                </td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="1" class="tabGg" width="98%">
            <tr>
                <th width="13%"  bgcolor="#FFFFFF" class="r_bg">
                    User Name :</th>
                <td width="87%" align="left" bgcolor="#FFFFFF" class="right_bg">
              <asp:Label ID="username" runat="server" Text="Label" ForeColor="Black" Width="327px"></asp:Label></td>
            </tr>           
            <tr>
                <th  bgcolor="#FFFFFF" class="r_bg">
                    New Password :</th>
                <td align="left" bgcolor="#FFFFFF" class="right_bg">
                    <span style="color: #ff0000">
                        <asp:TextBox ID="pass1" runat="server" CssClass="input1" check_null="Please input new password!" check_pass="密码不一致" TextMode="Password"></asp:TextBox></span></td>
            </tr>
            <tr>
                <th  bgcolor="#FFFFFF" class="r_bg">
                    Confirm Password :</th>
                <td align="left" bgcolor="#FFFFFF" class="right_bg">
                    <asp:TextBox ID="pass2" runat="server" CssClass="input1" check_null="Please input confirm password!" check_pass="密码不一致" TextMode="Password"></asp:TextBox><a></a></td>
            </tr>
            
            <tr>
                <th bgcolor="#ffffff" class="r_bg">
                </th>
                <td align="left" bgcolor="#ffffff" class="right_bg">
            <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="submit" OnClick="btnAdd_Click" OnClientClick="return checknull(this.form)" /></td>
            </tr>
        </table>
       
        <div style="text-align:center;">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <asp:HiddenField ID="hfuserid" runat="server" />
  
    </form>
</body>
</html>
