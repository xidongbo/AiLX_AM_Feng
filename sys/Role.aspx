<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Role" %>

<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       </head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style = "text-align:left;">
        &nbsp;
        <asp:Button ID="Button2" runat="server" Text="添加" onclick="Button2_Click" 
            Width="61px" />
        &nbsp;
        </div>
    &nbsp;<table width="100%">
<tr>
<td align=center > 
   <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
        ForeColor="#333333"   GridLines="None" Width=100% AllowSorting="True" 
       AutoGenerateColumns="False"  style =" word-break :break-all ; word-wrap:break-word "
         onsorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging"
        AllowPaging="True" 
        ShowHeaderWhenEmpty="True" 
          >

     <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
     <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
      

    <PagerSettings Mode="NumericFirstLast" />
      <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
      <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
      <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
       <Columns>
           <asp:BoundField DataField="c_id" Visible="False" />
           <asp:BoundField DataField="c_na" HeaderText="Role" SortExpression="role"/>
           <asp:TemplateField HeaderText="Edit">  
          <ItemTemplate>   
               <asp:Button ID="edit" runat="server"  Text="Edit" OnClick="edit_click" CommandArgument='<%# Eval("c_id") %>'></asp:Button>   
          </ItemTemplate>   
 
     </asp:TemplateField>

     <asp:TemplateField HeaderText="Delete"  >  
          <ItemTemplate>   
               <asp:Button ID="Delete" runat="server"  Text="Delete" OnClientClick="return confirm('Confirm to delete?')" OnClick="del_click" CommandArgument='<%# Eval("c_id") %>'></asp:Button>   
          </ItemTemplate>   
 
     </asp:TemplateField>

     <asp:TemplateField HeaderText="Authority"  >  
          <ItemTemplate>   
               <asp:Button ID="auth" runat="server"  Text="Authority" OnClick="Auth_click" CommandArgument='<%# Eval("c_id") %>'></asp:Button>   
          </ItemTemplate>   
 
     </asp:TemplateField>

       </Columns>
      <EditRowStyle BackColor="#999999" />
      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
   </asp:GridView>
   </td>
   </TR><tr>
   <td align=center>
    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" 
        OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="anpager" PageSize=50
        CurrentPageButtonClass="cpb" FirstPageText="首页" LastPageText="尾页" 
        NextPageText="后页" PrevPageText="前页">
    </webdiyer:AspNetPager>
    </td>
</tr>
		</table>

    
    </div>
    </form>
</body>

</html>

