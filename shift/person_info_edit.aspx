<%@ Page Language="C#" AutoEventWireup="true" CodeFile="person_info_edit.aspx.cs" Inherits="shift_person_info_edit" %>

<!DOCTYPE html>

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
        <td class="width1">Work No.</td>
        <td class="width2">
            <asp:TextBox ID="work_number" runat="server" Width="90%" 
                AutoCompleteType="Disabled" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="work_number" ErrorMessage="Work No. is empey!"></asp:RequiredFieldValidator>
             </td>
        </tr>

        <tr>
        <td class="width1">Name</td>
        <td class="width2">
            <asp:TextBox ID="name" runat="server" Width="90%"
                AutoCompleteType="Disabled"  ></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="name" ErrorMessage="Name is empty!" ></asp:RequiredFieldValidator>
        </td>     
        </tr>

        <tr>    
        <td class="width1">Position</td>
        <td class="width2">
            <asp:TextBox ID="position" runat="server" Width="90%" 
                AutoCompleteType="Disabled" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="position" ErrorMessage="Position is empty!"></asp:RequiredFieldValidator>
        </td>    
        </tr>

        <tr>    
        <td class="width1">Dept</td>
        <td class="width2">
            <asp:TextBox ID="dept" runat="server" Width="90%" 
                AutoCompleteType="Disabled" ></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="dept" ErrorMessage="Dept is empty!"></asp:RequiredFieldValidator>
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


