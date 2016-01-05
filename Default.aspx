<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>


<html>
<head>
    <link rel="stylesheet" href="css/Site_Css.css" type="text/css" />
    <link rel="stylesheet" type="text/css" href="inc/FineMessBox/css/subModal.css" />

    <script type="text/javascript" src="inc/FineMessBox/js/common.js"></script>

    <script type="text/javascript" src="inc/FineMessBox/js/subModal.js"></script>

    <title>Ericsson Production Report System</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
.navPoint
	{
	font-family: Webdings;
	font-size:9pt;
	color:white;
	cursor:pointer;
	}
p{
	font-size:9pt;
}

.font_size {  font-family: "Verdana", "Arial", "Helvetica", "sans-serif"; font-size: 12px; font-weight: normal; font-variant: normal; text-transform: none}
</style>

<script language="JavaScript" type="text/javascript">

var DispClose = true;
var menuimg;
function CloseEvent()
{
    if (DispClose)
    {
        return "是否离开当前页面?";
    }
}
    rnd.today=new Date(); 

    rnd.seed=rnd.today.getTime(); 

    function rnd() { 

　　　　rnd.seed = (rnd.seed*9301+49297) % 233280; 

　　　　return rnd.seed/(233280.0); 

    }; 

    function rand(number) { 

　　　　return Math.ceil(rnd()*number); 

    }; 
    
    function AlertMessageBox(Messages)
    {
        DispClose = false; 
        window.location.href = location.href+"?"+rand(1000000);
        alert(Messages);
    }
    


function switchSysBar(){

 	if (document.all("frmTitle").style.display=="none") {
		document.all("frmTitle").style.display=""
		menuimg.src="images/Menu/close.gif";
	    menuimg.alt="隐藏左栏"
		}
	else {
		document.all("frmTitle").style.display="none"
		menuimg.src="images/Menu/open.gif";
		menuimg.alt="开启左栏"
	 }
	 
	 

}

 function menuonmouseover(){
 		if (document.all("frmTitle").style.display=="none") {
 		menuimg.src="images/Menu/open_on.gif";
 		}
 		else{
 		menuimg.src="images/Menu/close_on.gif";
 		}
 }
 function menuonmouseout(){
 		if (document.all("frmTitle").style.display=="none") {
 		menuimg.src="images/Menu/open.gif";
 		}
 		else{
 		menuimg.src="images/Menu/close.gif";
 		}
 }
     if(top!=self)
    {
        top.location.href = "default.aspx";
    }
</script>

</head>
<body scroll="no"  leftmargin="0" topmargin="0" marginheight="0" marginwidth="0">
    <form id="form1" runat="server">
    <table border="0" cellspacing="0" cellpadding="0" width="100%" height="100%">
        <tr>
            <td width="100%" height="50" colspan="3" style="border-bottom: 1px solid #000000">
                <table height="49" border="0" cellspacing="0" cellpadding="0" width="100%" class="font_size">
                    <tr>
                        <td width="20%">
                            <b>Ericsson Production Report
                            </b><br />
                            <font size="2" color="#999999" face="Verdana, Arial, Helvetica, sans-serif">
                                Version 1.0.0
                            </font>
                        </td>
                        <td style="background-image: url(images/top-gray.gif); background-repeat: no-repeat;
                            background-position: right top">
                            当前用户：
                            <asp:Label ID="Label2" runat="server" Text="">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%
            int MenuStyle = 0;
    switch (MenuStyle)
    {
        case 0:
        %>
        <tr>
            <td id="frmTitle" name="frmTitle" nowrap="nowrap" valign="middle" align="center"
                width="198" style="border-right: 1px solid #000000">
                <iframe name="BoardTitle" style="height: 100%; visibility: inherit; width: 198; z-index: 2"
                    scrolling="auto" frameborder="0" src="left.aspx"></iframe>
            </td>
            <td style="width: 10pt" bgcolor="#7898A8" width="10" title="关闭/打开左栏" class="navPoint">
                &nbsp;</td>
            <td style="width: 100%">
                <iframe id="mainFrame" name="mainFrame" style="height: 100%; visibility: inherit;
                    width: 100%; z-index: 1" scrolling="auto" frameborder="0" src="welcome.aspx"></iframe>
            </td>
        </tr>
        <%
            break;
        case 1:
            
        %>
        <tr>
            <td id="frmTitle" name="frmTitle" nowrap="nowrap" valign="middle" align="center"
                width="115" style="border-right: 1px solid #000000">
                <iframe name="BoardTitle" style="height: 100%; visibility: inherit; width: 100%; z-index: 2"
                    scrolling="no" frameborder="0" src="Menu1.aspx"></iframe>
            </td>
            <td style="width: 10pt" bgcolor="#7898A8" width="10" title="关闭/打开左栏" class="navPoint">
                <table border="0" cellpadding="0" cellspacing="0" width="11" height="100%" align="right">
                    <tr>
                        <td valign="middle" align="right" class="middleCss">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td style="width: 100%">
                <iframe id="Iframe" name="mainFrame" style="height: 100%; visibility: inherit;
                    width: 100%; z-index: 1" scrolling="auto" frameborder="0" src="rightSystemInfo.aspx"></iframe>
            </td>
        </tr>
        <%
            break;
            case 2:
        %>
        <tr>
            <td height="51px">
               <iframe name="MainTop" style="height: 100%; visibility: inherit;
                    width: 100%; z-index: 1" scrolling="no" frameborder="0" src="Menu2.aspx"></iframe>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <iframe id="Iframe" name="mainFrame" style="height: 100%; visibility: inherit;
                    width: 100%; z-index: 1" scrolling="auto" frameborder="0" src="welcome.aspx"></iframe>
            </td>
        </tr>
        <%
            break;

    }
        %>
        <tr>
            <td colspan="3" height="20">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="20">
                    <tr>
                        <td class="down_text">
                           <a href="" target="_blank"><font color="#ffffff"></font></a>
                            </td>
                            <td align="right" width="400" bgcolor="#000000">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    
                                    <td style="cursor:pointer;border-left:1px solid #FFFFFF;" onclick="javascript: window.mainFrame.location.href='welcome.aspx'">&nbsp;<img src="images/house.gif" style="margin-bottom: -3px">&nbsp;<font color="#FFFFFF">回到首页</font></td>
                                    <td style="cursor:pointer;border-left:1px solid #FFFFFF;" onclick="javascript: window.top.location.href = 'login.aspx'">&nbsp;<img src="images/logout.gif" style="margin-bottom: -3px">&nbsp;<font color="#FFFFFF">退出系统</font></td>
                                    <td style="cursor:pointer;border-left:1px solid #FFFFFF;" onclick="">&nbsp;</td>
                                </tr>
                            </table>
                            
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

