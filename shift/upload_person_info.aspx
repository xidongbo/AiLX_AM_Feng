<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload_person_info.aspx.cs" Inherits="shift_upload_person_info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>    <style type="text/css">
        #form1
        {
            width: 669px;
        }
        .tabGg
        {
            width: 104%;
        }
        .style1
        {
            height: 22px;
        }
        .style2
        {
            width: 199px;
        }
        .style3
        {
            height: 22px;
            width: 199px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
   <table cellpadding="1" cellspacing="1" class="tabGg">
            <tr>
                <th   bgcolor="#FFFFFF" class="style3">Please choose excel&nbsp; &nbsp;</th>
                <td  align="left" bgcolor="#FFFFFF" class="right_bg">     
                    <asp:FileUpload ID="FileUpload1"  runat="server"/></td>
           </tr>
           <tr>
               <td>
                   <asp:label ID="file_name" style="display:none" runat="server"/>
               </td>
           </tr>
            <tr>
                <th bgcolor="#ffffff" class="style3"></th>
                <td align="left" bgcolor="#ffffff" class="right_bg">
                    <asp:Button  ID="btnUpload" runat="server" Text="UploadExcel" CssClass="submit" 
                     Width="90px" onclick="btnUpload_Click" Height="20px"  style="cursor: pointer;"  /> &nbsp;
                   <asp:Button  ID="btnReturn" runat="server" Text="Return" CssClass="submit" 
                     Width="90px"  Height="20px"  
                        style="cursor: pointer;" onclick="btnReturn_Click"  />
                 </td>
            </tr>
        </table> 
    </form>
</body>
</html>
