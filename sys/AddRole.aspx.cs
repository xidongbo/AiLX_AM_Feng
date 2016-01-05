using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;


public partial class addRole : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (object.Equals(Session["user_login"], null))
        {
            Response.Redirect("~/login.aspx");
        }
        if (!IsPostBack)
        {
            initPage();
        }
    }
    protected void initPage()
    {
        string ls_id;
        String sql;
        DataTable dt;

        ls_id = Request.QueryString["c_id"];
        if (ls_id.Length > 0)
        {
            sql = "select c_id,c_na from t_role where c_id="+ls_id;
            dt = SQLHelper.GetDataTable(sql);
            id.Text=dt.Rows[0][0].ToString();
            rolename.Text = dt.Rows[0][1].ToString();
           
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //string s = owner.Text.ToString().Trim();
        string ls_sql=" and (c_system='"+du_tools.gcs_sytem+"')";
        string ls_id = id.Text;
        int li_cnt;
        string role_name = rolename.Text.Trim();
  
        if (ls_id.Length > 0)
        {
            li_cnt=SQLHelper.ReturnInt("select count(*) from t_role where c_na='"+role_name+"' and c_id<>"+ls_id +ls_sql);
            if (li_cnt > 0)
            {
                JScript.Alert("Role name has been in role list ,pls. check it");
                return;
            }
            SQLHelper.ExecuteNonQuery("update t_role set c_na='" + role_name + "' where c_id=" + ls_id+ls_sql);
            Response.Write("<script>alert('Role info. has been changed');window.location='role.aspx'</script>");
        }
        else
        {

            li_cnt = SQLHelper.ReturnInt("select count(*) from t_role where c_na='" + role_name + "'" + ls_sql);
            if (li_cnt > 0)
            {
                JScript.Alert("Role name has been in role list ,pls. check it");
                return;
            }
            li_cnt = SQLHelper.ReturnInt("select max(c_id) from t_role ");
            li_cnt++;
            SQLHelper.ExecuteNonQuery("insert into t_role(c_id,c_system, c_na)values" +
                "("+li_cnt+",'"+du_tools.gcs_sytem+"','" + rolename.Text.Trim() + "')");
            Response.Write("<script>alert('Role added');window.location='role.aspx'</script>");
        }
            // cmdstr = "select count(*) from USERS where user_id='" + txtUserName.Text.ToString.Trim() + "' and pwd='" + txtPWD.Text.ToString.Trim() + "'"
        
        //Response.Redirect("user.aspx");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("role.aspx");
    }
}