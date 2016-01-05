<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="left" %>

<html>
<head>
    <title>-后台管理</title>
    <style type="text/css">
                .ttl { CURSOR: pointer; COLOR: #ffffff; PADDING-TOP: 4px }
                A:active{COLOR: #000000;TEXT-DECORATION: none}
                A:hover{COLOR: #000000;TEXT-DECORATION: none}
                A:link{COLOR: #000000;TEXT-DECORATION: none}
                A:visited{COLOR: #000000;TEXT-DECORATION: none}
                TD {
	            FONT-SIZE: 12px; FONT-FAMILY: "Verdana", "Arial", "细明体", "sans-serif"
                }
.table_body {	
BACKGROUND-COLOR: #EDF1F8;
height:18px;
CURSOR: pointer; 
}

.table_none {	
BACKGROUND-COLOR: #FFFFFF;
height:18px;
CURSOR: pointer; 
}
			</style>

    <script language="javascript">
	        function showHide(obj){
          var oStyle = obj.parentElement.parentElement.parentElement.rows[1].style;
          oStyle.display == "none" ? oStyle.display = "block" : oStyle.display = "none";
         }

    </script>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body bgcolor="#9aadcd" leftmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <br>
     <asp:Repeater ID="LeftMenu" runat="server"  OnItemDataBound="LeftMenu_ItemDataBound">
      <ItemTemplate>
        <table cellSpacing="0" cellPadding="0" width="179" align="center" border="0">
          <tr>
           <td width="23"><img height="25" src="images/Menu/box_topleft.gif" width="23"></td>
           <td class="ttl" onclick="JavaScript:showHide(this);" width="149" background="images/Menu/box_topbg.gif"><%# Eval("c_lvl1")%></td>
           <td width="7"><img height="25" src="images/Menu/box_topright.gif" width="7"></td>
          </tr>
          <tr style="DISPLAY: none" id="tr">
           <td background='images/Menu/box_bg.gif' colSpan='3'>          
             <table width='100%'>
                 <tbody>
                 
                 <asp:Repeater ID="LeftMenu_Sub" Runat="server" >
                 <ItemTemplate>
               <tr>
               
               <td><img height='7' hspace='5' src='images/Menu/arrow.gif' width='5' align='absMiddle'>
                <asp:HyperLink ID ="Hyperlink1" Runat ="server" Target ="mainFrame" NavigateUrl='<%# Eval("c_link")%>'>
                <%# Eval("c_lvl2")%>
                </asp:HyperLink>
               </td>
               
              </tr>
              </ItemTemplate>
              </asp:Repeater>
             
                </tbody>
             </table>
           </td>
          </tr>
            <tr>
            <td colSpan="3"><img height='10' src='images/Menu/box_bottom.gif' width='179'></td>
            </tr>
         </table> 
        </ItemTemplate>
</asp:Repeater> 
</br>   
    </form>
</body>
</html>
