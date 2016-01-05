<%@ Page Language="C#" AutoEventWireup="true" CodeFile="permission_manager.aspx.cs" Inherits="permission_manager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head runat="server"><title>
</title><link rel="stylesheet" href="css/Site_Css.css" type="text/css" />
    <script language="javascript" src="js/checkform.js" charset="utf-8"></script>
    <script language="javascript" src="js/date/date.js"  charset="utf-8"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script language='javascript' type='text/javascript'>
        function OnTreeNodeChecked() {
            var ele = event.srcElement;
            if (ele.type == 'checkbox') {
                var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
                var div = document.getElementById(childrenDivID);
                if (div == null) {

                }
                else {
                    var checkBoxs = div.getElementsByTagName('INPUT');
                    for (var i = 0; i < checkBoxs.length; i++) {
                        if (checkBoxs[i].type == 'checkbox')
                            checkBoxs[i].checked = ele.checked;
                    }
                }

                var parentDiv = event.srcElement.parentElement.parentElement.parentElement.parentElement.parentElement;
                var parentParentDiv = event.srcElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement;
                if (parentDiv != null && parentDiv != undefined) {
                    var parentCheckBoxID = parentDiv.id.replace('Nodes', 'CheckBox');
                    var checkBoxs = parentDiv.getElementsByTagName('INPUT');
                    var childNodeChecked = false;
                    for (var i = 0; i < checkBoxs.length; i++) {
                        if (checkBoxs[i].type == 'checkbox' && checkBoxs[i].checked)
                            childNodeChecked = true;
                    }
                    if (document.getElementById(parentCheckBoxID) != null) {
                        document.getElementById(parentCheckBoxID).checked = childNodeChecked;
                    }
                }

                if (parentParentDiv != null && parentParentDiv != undefined) {
                    var parentParentCheckBoxID = parentParentDiv.id.replace('Nodes', 'CheckBox');
                    var checkBoxs = parentParentDiv.getElementsByTagName('INPUT');
                    var childNodeChecked = false;
                    for (var i = 0; i < checkBoxs.length; i++) {
                        if (checkBoxs[i].type == 'checkbox' && checkBoxs[i].checked)
                            childNodeChecked = true;
                    }

                    if (document.getElementById(parentParentCheckBoxID) != null) {
                        document.getElementById(parentParentCheckBoxID).checked = childNodeChecked;
                    }
                }
            }
        }
    </script>

    <style type="text/css">
        .style2
        {
            width: 145px;
        }
        .style3
        {
            width: 92px;
        }
    </style>
  


    </head>
    <link rel="stylesheet" href='./css/table/default/Css.css' type="text/css">
 
<!--#include-bak file="/inc/page_guage.aspx"-->
<body bgColor="#FFFFFF" topMargin="5" >    
    <div>
        <form id="aspnetForm" runat="server">

 
        
   
        
    <!-- 选项卡 Start -->
        <TABLE cellSpacing=0 cellPadding=0 width='100%' align=center border=0>   
        <TBODY>   
	       
	        <TR>
	        <TD bgColor=#ffffff>           
		        <TABLE cellSpacing=0 cellPadding=0 width='100%' border=0 > 
		        <TBODY>
                <TR>
			        <TD width=1 background='images/Menu/tab_bg.gif'><IMG  height=1 src='images/Menu/tab_bg.gif'  width=1></TD>
			        <TD style='PADDING-RIGHT: 15px; PADDING-LEFT: 15px; PADDING-BOTTOM: 15px; PADDING-TOP: 15px; HEIGHT: 100px' vAlign=top>
                <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" style="table-layout:fixed;"> 
                   <TR width="100%" class=style4>


<TD class="style3" align=right>
            
    <asp:Label ID="L_role_id" runat="server" Text="角色权限:" Visible="False"></asp:Label>
            
                       </TD>

 </TR></TABLE>
<table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" style="table-layout:fixed;"> 
   

<tr>
<td class="style2">

    &nbsp;&nbsp;<asp:Button ID="Button5" runat="server" Text="保存"  class="button_bak" 
                  onclick="Button5_Click" Width="61px"/>
            
   
            
</td>
<td> <asp:Button ID="Button6" runat="server" Text="Return" 
        onclick="Button6_Click" /></td>
</tr>
</table>
<table width=100%>
<tr>
<td width=75% valign=top align=left>

      <asp:TreeView ID="TreeView1" runat="server" ShowCheckBoxes=All
           NodeIndent="10">
            </asp:TreeView>


          </td>
        </tr>
        </table>
       


        </DIV><!--内容框End-->
			         
                        
			        </TD>
			        
		        </TR>
		        </TBODY>
		        </TABLE>
	        </TD>
	        </TR>
	        
        </TBODY>
        </TABLE>
        <!--选项卡 End--> 
    </form>
    <br />
    </div>
</body>
</html>
