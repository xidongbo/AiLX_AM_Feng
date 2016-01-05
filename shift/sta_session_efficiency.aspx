<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sta_session_efficiency.aspx.cs" Inherits="sta_session_efficiency" %>

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
            .style7
            {
                width: 5%;
            }
            .style12
            {
                width: 15%;
            }
            .style14
            {
                width: 19%;
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
                                <TD class="style7" align=left>


                                 <TABLE cellSpacing=0 cellPadding=0 width='100%' align=center border=0>   
        <TBODY>   
            <tr>
        <td align=left class="style12">
                Dept.<asp:TextBox ID="txtDept" runat="server" Width="119px"></asp:TextBox>
            </td>
            <td align=left class="style14">
                &nbsp;</td>
            
            <td>
                &nbsp;</td>
             <td>
                 Time</td>
             <td>
                        <asp:TextBox ID="txt_dt1"  runat="Server"   onClick="WdatePicker()"
                            style="background-position:155px center;" Height="21px" 
                        Width="136px"  ></asp:TextBox>
                      ~
                        <asp:TextBox ID="txt_dt2"  runat="Server"   onClick="WdatePicker()"
                            style="background-position:155px center;" Height="21px" Width="136px"  ></asp:TextBox>
                      
                    </td>
            </tr>
	        <TR>
	          <td>
                <asp:Button ID="Button1" runat="server" Text="Statistic" style="margin-left: 0px" 
                onclick="Button1_Click" Width="76px" />
                </td>
              <td>
                  &nbsp;</td>
              <td>
                  &nbsp;</td>
              <td>
                 <asp:Label ID="lb_id" runat="server" Text="Label" Visible="False"></asp:Label>

                </td>
              <td align=right>
                <asp:Button ID="Button4" runat="server" Text="Return" style="margin-left: 0px"  
                    onclick="Button4_Click" Width="69px" />

                </td>
              
            </TR>


        </TABLE>




                                  </TD>
                              </TR>
                        </TABLE>

   <!--按钮-->
    


<!--信息 -->
            <table width="100%">
                <tr>
                <td>
                       <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
                            ForeColor="#333333"   GridLines="None" Width=100% AllowPaging="True" AllowSorting="True" 
                           AutoGenerateColumns="False"  OnRowDataBound="GridView1_RowDataBound" OnRowCommand="Gridview1_RowCommand" 
                            OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="50"  onsorting="GridView1_Sorting" >
                         <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                         <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="dept" HeaderText="Dept" />
                            <asp:BoundField DataField="total_hr" HeaderText="Total Hour" />
                            <asp:BoundField DataField="c_out" HeaderText="Ture out" />                             
                             <asp:BoundField DataField="c_eff" HeaderText="Efficiency" />
        
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
