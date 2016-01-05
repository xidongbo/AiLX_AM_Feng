<%@ Page Language="C#" AutoEventWireup="true" CodeFile="work_flow_edit.aspx.cs" Inherits="shift_work_flow_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
         table.table
        {
            border:0; 
            margin:auto;
            text-align:center;
          
            
        }
         table.table1
        {
            border:0; 
            margin:auto;
            text-align:center;
            height:100%;   
            width:100%;
        }
         #form1
        {
            width: 100%;
            height :100%;
            position:absolute;
            
        }
        td.width1 
        {
            width:80px;
            text-align:left ;
            vertical-align:top;
        }
         td.width2
        {
            width:200px;
            text-align:left;
        }
        tr.width1 
        {
            width:15%;
        }
         tr.width2
        {
             width:70%;
              FONT-SIZE: 9pt;
                BACKGROUND-COLOR: #E9F2F7;
                PADDING-LEFT: 5px;
                PADDING-RIGHT: 5px;
                width: 100%;
                font-family: "Verdana", "Arial", "Helvetica", "sans-serif";
        }
    </style>
</head>


<body >
   <form id="form1" runat="server">
   <table class="table1">
       <tr class="width1" >
           <td>
               </td>
           </tr>
        <tr class="width2">
            <td align="center">
            <table class="table" >
        <tr>
        <td class="width1">Name</td>
        <td class="width2">
            <asp:TextBox ID="name" runat="server" Width="90%" 
                AutoCompleteType="Disabled" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="name" ErrorMessage="Name is empey!"></asp:RequiredFieldValidator>
   
        </td>
        </tr>
        <tr>
        <td class="width1">memo</td>
        <td class="width2">
            <asp:TextBox ID="memo" runat="server" Width="90%"  TextMode="MultiLine"
                AutoCompleteType="Disabled"  ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="memo" ErrorMessage="Memo is empty!" ></asp:RequiredFieldValidator>
        </td>   
        </tr>
        <tr>    
        <td class="width1">Operator</td>
        <td class="width2">
            <asp:TextBox ID="opter" runat="server" Width="90%" 
                AutoCompleteType="Disabled" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="opter" ErrorMessage="Operator is empty!"></asp:RequiredFieldValidator>
        </td>     
        </tr>
        <tr>
          
            <td colspan ="2">
            <asp:Button ID="commit" runat="server" onclick="commit_Click" Text="Commit" 
                Width="70px" />
            &nbsp;&nbsp;
            <asp:Button ID="return" runat="server" onclick="return_Click" Text="Return" 
                Width="70px" CausesValidation="false"/>
            </td>
         </tr>
    </table>
                </td>
           </tr>
        <tr class="width1">
            <td>
                </td>
           </tr>
       </table>
    
  </form>
</body>
</html>
