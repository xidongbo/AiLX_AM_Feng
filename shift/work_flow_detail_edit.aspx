<%@ Page Language="C#" AutoEventWireup="true" CodeFile="work_flow_detail_edit.aspx.cs" Inherits="shift_work_flow_detail_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 100%;
           height:100%;
           position:absolute;
        }
        table.table
        {
            border:0; 
            text-align:center;
            margin:auto;
             
        }
        table.table1
        {
            border:0; 
            text-align:center;
            margin:auto;
            height:100%;
            width:100%;
        }
        td.width1 
        {
            width:120px;
            text-align:left;
            vertical-align:top;
        }
         td.width2
        {
            width:250px;
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


<body>
    <form id="form1" runat="server">
        <table class="table1" >
            <tr class="width1" >
                <td>
                    </td>
                </tr>
             <tr class="width2" >
                <td align="center" >
                    <table class="table">
        <tr>
        <td class="width1">Step No.</td>
        <td class="width2">
            <asp:TextBox ID="step_number" runat="server" Width="90%" 
                AutoCompleteType="Disabled" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="step_number" ErrorMessage="Step No. is empey!"></asp:RequiredFieldValidator>
            <br/>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="step_number" 
               ValidationExpression="^[0-9]*[1-9][0-9]*$" runat="server"  ErrorMessage="Please input positive integer!"></asp:RegularExpressionValidator>
        </td>
        </tr>

        <tr>
        <td class="width1">Step Name</td>
        <td class="width2">
            <asp:TextBox ID="step_name" runat="server" Width="90%"
                AutoCompleteType="Disabled"  ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="step_name" ErrorMessage="Step Name is empty!" ></asp:RequiredFieldValidator>
        </td>     
        </tr>

        <tr>    
        <td class="width1">Step Op. Name</td>
        <td class="width2">
             <asp:DropDownList ID="step_op_name" DataTextField="c_name" runat="server" Width="90%" ></asp:DropDownList> 
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="step_op_name" ErrorMessage="Step Op. Name is empty!"></asp:RequiredFieldValidator>
        </td>    
        </tr>

        <tr>    
        <td class="width1">Step Op. Detail</td>
        <td class="width2">
            <asp:TextBox ID="step_op_detail" runat="server" Width="90%" 
                AutoCompleteType="Disabled" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="step_op_detail" ErrorMessage="Step Op. Detail is empty!"></asp:RequiredFieldValidator>
        </td>      
        </tr>

        <tr>
           <td colspan ="2">
            <asp:Button ID="commit" runat="server" onclick="commit_Click" Text="Commit" 
                Width="70px"/>
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

