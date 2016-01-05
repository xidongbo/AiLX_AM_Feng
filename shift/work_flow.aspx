<%@ Page Language="C#" AutoEventWireup="true" CodeFile="work_flow.aspx.cs" Inherits="shift_work_flow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
     <title>
    </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <style type="text/css">
            table.table
            {
                width:100%;
                border:0; 
                table-layout:fixed;
              
            }
            tr.head1
            {
                FONT-SIZE: 9pt;
                BACKGROUND-COLOR: #E9F2F7;
                PADDING-LEFT: 5px;
                PADDING-RIGHT: 5px;
                width: 100%;
                font-family: "Verdana", "Arial", "Helvetica", "sans-serif";
            }
            .right 
            {
                text-align :right;
            }
            td.width1
            {
                text-align:left ;
                width: 80px;
            }
            td.width2 
            {
                text-align :left;
                width:150px;
            }
            tr.height 
            {
                height :30px;
            }
            </style>
  </head>
 
  
<body >    
  <form id="aspnetForm" runat="server">
         
     <table class="table"> 
        <tr  class="head1">
            <td>
                   <table class="table">     
                            <tr class="height">
                             <td  class="width1">Name</td>
                              <td  class="width2"><asp:TextBox ID="text_name" runat="server" width="90%"></asp:TextBox></td>
                             <td class="width1" >Operator</td>
                              <td  class="width2"><asp:TextBox ID="text_operator" runat="server" width="90%"></asp:TextBox></td>           
                             <td ></td>
                            </tr>
	                        <tr  class="height">
	                              <td>
                                    <asp:Button ID="query" runat="server" Text="Query" style="margin-left: 0px" 
                                    onclick="query_Click" Width="70px" />
                                    </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                  <td class="right">
                                      <asp:Button ID="add" runat="server" onclick="add_Click" Text="Add" 
                                          Width="70px" />
                                  </td>       
                              </tr>
                    </table>
              </td>
         </tr>
       </table>

     <table class="table" >
                <tr>
                <td>
                       <asp:GridView ID="grid_view" runat="server" Width="100%"  AutoGenerateColumns="false"  ForeColor="#333333"
                        BackColor="White" BorderColor="#3366CC" BorderStyle="Inset" BorderWidth="1px" AllowPaging="true" PageSize ="20"
                        CellPadding="4"  OnSelectedIndexChanged="grid_view_SelectedIndexChanged" OnPageIndexChanging="grid_view_PageIndexChanging"
                        OnRowCommand="grid_view_RowCommand" OnRowDataBound="grid_view_RowDataBound">
                     
                          
                         


                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                           
                        <Columns>
                            <asp:BoundField DataField="c_name" SortExpression="c_name" HeaderText="Name" >
                                <ItemStyle HorizontalAlign="Center"  Width="15%"></ItemStyle>                               
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="c_memo" SortExpression="c_memo" HeaderText="Memo">
                                <ItemStyle HorizontalAlign="center" Width="45%"></ItemStyle>                     
                            </asp:BoundField>

                            <asp:BoundField DataField="c_opter" SortExpression="c_opter" HeaderText="Operator">
                                <ItemStyle HorizontalAlign="center" Width="15%"></ItemStyle>            
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="Modify" ShowHeader="True" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="Modify" runat="server" CausesValidation="false" 
                                        CommandName="Modify" CommandArgument='<%#Eval("c_id")%>' Text="Modify"></asp:LinkButton>
                                </ItemTemplate>
                             </asp:TemplateField>
                              <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Delete" runat="server" OnClientClick="return window.confirm('Confirm to delete!')" CausesValidation="false" 
                                        CommandName="my_delete" CommandArgument='<%#Eval("c_id")%>' Text="Delete" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Detail" ShowHeader="True">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Detail" runat="server" CausesValidation="false" 
                                        CommandName="Detail" CommandArgument='<%#Eval("c_id")%>' Text="Detail"></asp:LinkButton>
                                </ItemTemplate>
                             </asp:TemplateField>
                            
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

