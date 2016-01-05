<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<html>
<head>
<link rel="stylesheet" href="css/Site_Css.css" type="text/css"/>
<script language="javascript" src="js/checkform.js"></script>
<title>Ericsson 排班管理系统</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <style type="text/css">
            .style1
            {
                height: 49px;
            }
            .style2
            {
                width: 36px;
            }
            .style3
            {
                width: 114px;
            }
         </style>
</head>

<body scroll="no" leftMargin="0" topMargin="0" marginheight="0" marginwidth="0">

<table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
<tr> 
    <td width="100%" height="7%" colspan="3" style="border-bottom: 1px solid #000000">
			<table height="49" border="0" cellspacing="0" cellpadding="0" width="100%" class="font_size">
              <tr> 

			  <td  style="background-image: url(images/top-gray.gif); background-repeat: no-repeat; background-position: right top" 
                      class="style1" >
			  			  <b>Ericsson Production report system</b><br/>
			<font size="2" color="#999999" face="Verdana, Arial, Helvetica, sans-serif">Version 1.0.0</font>			  

			  </td>
			  </tr>
		   </table>
    </td>

  </tr>
 
  <tr> 
    <td colspan="3" height="90%" width="100%"> 

	
<table width="100%" border="0" cellspacing="0" cellpadding="0" height="100%" >
  <form id="Form1" name="login" method="post" runat="server" DefaultFocus="txtUserName" onSubmit="javascript:return checkForm(this)">
  <tr>
    <td>  
<table WIDTH="457" height="324" BORDER="0" CELLPADDING="0" CELLSPACING="0" align="center" 
    background="images/bckgrd.jpg" 
    style="background-repeat:no-repeat; background-position:top">
  <tr>
    <td height="116" class="style3">&nbsp;&nbsp;&nbsp;
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/eri.JPG" />
      </td>
    <td colspan =4 >
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri" 
            Font-Size="X-Large" Font-Strikeout="False" Text="Production Report System"></asp:Label>
      </td> 
  </tr>
  <tr>
    <td height="30" class="style3">&nbsp;</td>
    <td align="right" class="style2">账号：</td>
    <td>
        <asp:TextBox ID="txtUserName"  runat="server" CssClass="text_input"  Width=140 
            title="请输入账号~!"></asp:TextBox>                                    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="30" class="style3">&nbsp;</td>
    <td align="right" class="style2">密码：</td>
    <td>
        <asp:TextBox ID="txtPWD" runat="server"  CssClass="text_input" 
            title="请输入密码~!" TextMode="Password" Width=140 ></asp:TextBox>                                    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="36" class="style3">&nbsp;</td>
    <td align="right" class="style2">&nbsp;</td>
    <td>
        &nbsp;&nbsp;&nbsp;&nbsp;                                    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="39" class="style3">&nbsp;</td>
    <td align="right" class="style2">&nbsp;</td>
    <td>
        <asp:Button ID="Button1" runat="server" Text="确定" class="button_bak" 
            onclick="lbtLogin_Click" />
&nbsp;&nbsp;&nbsp;
        <input type="reset"  value="重填" class="button_bak"/>                                    </td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="42" class="style3">&nbsp;</td>
    <td align="right" class="style2">&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  
</table>

	
		
    </td>
  </tr>
</form>
</table>	
	
	
    </td>
  </tr>
  <tr>
  <td colspan="3" height="3%" height="100%">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" height="20">
      <tr>
        <td class="down_text" >Powered By <a href="" target="_blank"><font color="#ffffff">***</font></a> </td>
  
 
      </tr>
      </table>	  
  </td>
  </tr>
</table>

</body>
</html>

<script language="JavaScript" type="text/javascript"><!-- 

    // The Central Randomizer 1.3 (C) 1997 by Paul Houle (houle@msc.cornell.edu) 

    // See: http://www.msc.cornell.edu/~houle/javascript/randomizer.html 

    rnd.today=new Date(); 

    rnd.seed=rnd.today.getTime(); 

    function rnd() { 

     rnd.seed = (rnd.seed*9301+49297) % 233280; 

      return rnd.seed/(233280.0); 

    }; 

    function rand(number) { 

       return Math.ceil(rnd()*number); 

    }; 

    // end central randomizer. --> 

    </script>
<script language="javascript" type="text/javascript">
//    ChangeCodeImg();
//    function ChangeCodeImg()
//    {
//             a = document.getElementById("ImageCheck");
//             a.src = "Codeimg.aspx?"+rand(10000000);
//             document.all.Button1.disabled = true;
//    }
//    
    function Open_Submit()
    {
        document.all.Button1.disabled = "";
    }
    
    if(top!=self)
    {
        top.location.href = "login.aspx";
    }
    
    if(navigator.appVersion.indexOf("MSIE")   ==   -1   ){   
        
    }   
    
</script>