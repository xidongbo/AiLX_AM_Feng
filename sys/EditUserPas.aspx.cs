using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class System_EditUserPas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           this.username.Text = Request.Cookies["user"].Values["name"];
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //if (Common.FormatParameter(pass1.Text) != Common.FormatParameter(pass2.Text))
        //{
        //    JScript.AjaxAlert(this.Page,"New password and confirm password should be the same");
        //    return;
        //}
        //SqlParameter[] parames = {               
        //       new SqlParameter("@usr_pwd",Common.WebEncrypt(pass1.Text.Trim()))                         
        //    };
        //SQLHelper.ExecuteNonQuery("update tbl_usr set usr_pwd = @usr_pwd where id = '" + Request.Cookies["user"].Values["id"] + "' ", parames);
        //JScript.AjaxAlert(this.Page,"Modify success");
    }
}
