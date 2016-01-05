<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_manday.aspx.cs" Inherits="rpt_manday" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head><title>
	
</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <style type="text/css">
            .style4
            {
                FONT-SIZE: 9pt;
                BACKGROUND-COLOR: #E9F2F7;
                height: 30px;
                PADDING-LEFT: 5px;
                PADDING-RIGHT: 5px;
                width: 28%;
                font-family: "Verdana", "Arial", "Helvetica", "sans-serif";
            }
            .style15
            {
                width: 72%;
            }
            .style16
            {
                width: 7%;
            }
            .style17
            {
                width: 8%;
            }
            </style>
    <script language="javascript" type="text/javascript">

        function Check(parentChk, pattern) {
            var elements = document.getElementsByTagName("INPUT");
            for (i = 0; i < elements.length; i++) {
                if (parentChk.checked == true) {
                    if (IsCheckBox(elements[i]) && IsMatch(elements[i].id, pattern)) {
                        elements[i].checked = true;
                    }
                }
                else {
                    if (IsCheckBox(elements[i]) && IsMatch(elements[i].id, pattern))
                    { elements[i].checked = false; }
                }
            }
        }
        function IsMatch(id, pattern) {
            var regularExpression = new RegExp(pattern);
            return id.match(regularExpression);
        }
        function IsCheckBox(chk) {
            return (chk.type == 'checkbox');
        }

        function onRadiobuttonClick(selectedrow){//(gvControlID, selectedControlId) {
           // var inputs = document.getElementById(gvControlID).getElementsByTagName("input");
            var rows = GridView2.rows;
            for (var i = 1; i < rows.length; i++) {
                //                var rb = rows[i].cells[6].childNodes[0].nextElementSibling;
                var rb = GridView2.rows(i).cells(6).childNodes[0];
//                alert(rb.type);
//                if (rb.type == 'radio') {
                    if (i == selectedrow+1)
                        rb.checked = true;
                    else
                        rb.checked = false;
//                }
            }
        }
    </script>

    </head>
  <script language="javascript" type="text/javascript" src="../js/My97DatePicker/WdatePicker.js"></script>
  

<!--#include-bak file="/inc/page_guage.aspx"-->
<body bgColor="#FFFFFF" topMargin="5" >    
        <form id="aspnetForm" runat="server">

 
        
    <!-- 头部菜单 Start -->
	        
        <!-- 头部菜单 End -->
        
    <!-- 选项卡 Start -->
         

              <table width="100%" border="0" cellspacing="1" cellpadding="3" align="center" style="table-layout:fixed;"> 
             
                           <TR width="100%" class=style4>
                                <td align=left class="style16">
                                        Name<asp:TextBox ID="txtNa" runat="server" Width="116px"></asp:TextBox>
                                </td>
                                <td align=left class="style16">
                                    Dept.<asp:TextBox ID="txtDept" runat="server" Width="119px"></asp:TextBox>
                                </td>
            
                                <td align=left class="style17">
                                    Year<asp:TextBox ID="txtYear" runat="server" Width="119px"></asp:TextBox>
                                </td>

                                <td align=left class="style17">
                                    Month<asp:DropDownList 
                                        ID="ddlb_month" runat="server" Width="110px"  
                        Height="20px">
                       <asp:ListItem>1</asp:ListItem>
                       <asp:ListItem>2</asp:ListItem>
                       <asp:ListItem>3</asp:ListItem>
                       <asp:ListItem>4</asp:ListItem>
                       <asp:ListItem>5</asp:ListItem>
                       <asp:ListItem>6</asp:ListItem>
                       <asp:ListItem>7</asp:ListItem>
                       <asp:ListItem>8</asp:ListItem>
                       <asp:ListItem>9</asp:ListItem>
                       <asp:ListItem>10</asp:ListItem>
                       <asp:ListItem>11</asp:ListItem>
                       <asp:ListItem>12</asp:ListItem>
                   </asp:DropDownList>
                                </td>
                              </TR>
                        </TABLE>

   <!--按钮-->
      <table width="100%"> 
        <tr>

            <td align=left class="style15">
               &nbsp;<asp:Button ID="Button1" runat="server" Text="Query" style="margin-left: 0px" 
                onclick="Button1_Click" Width="69px" />

            &nbsp;&nbsp;
               
                <asp:Button ID="bt_save" runat="server" Text="Excel" style="margin-left: 0px" 
                onclick="bt_save_Click" Width="69px" />

               &nbsp;&nbsp;

              

               </td>

            <td  width="50%" align=left> &nbsp;</td>
            <td align=right>
                <asp:Button ID="Button4" runat="server" Text="Return" style="margin-left: 0px"  
                    onclick="Button4_Click" Width="69px" />

            </td>
         </TR>
     </table>


<!--信息 -->
            <table width="100%">
                <tr>
 
                <td valign=top>
                       <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
                            ForeColor="#333333"   GridLines="None" Width=100% AllowPaging="True" AllowSorting="True" 
                           AutoGenerateColumns="False"  OnRowDataBound="GridView1_RowDataBound" OnRowCommand="Gridview1_RowCommand" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50"  onsorting="GridView1_Sorting" >
                         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="the_id" HeaderText="No." />
                            <asp:BoundField DataField="ename" HeaderText="Name" />
                            <asp:BoundField DataField="dept" HeaderText="Dept" />

        
                        </Columns>
                          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                          <EditRowStyle BackColor="#999999" />
                          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                       </asp:GridView>
                   </td>
                </tr>
            </table>        
    </form>
</body>
</html>
