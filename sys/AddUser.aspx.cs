using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;


public partial class addUser : System.Web.UI.Page
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
        string ls_id, ls_role;
        String sql;
        DataTable dt;
        ListItem lli;
        //du_tools.f_data_2_ddlb("select distinct c_type from t_shift_line_info", arrange);
        sql = "select c_na from t_role where c_system='"+du_tools.gcs_sytem+"'";
        dt = SQLHelper.GetDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ls_role = dt.Rows[i][0].ToString();
            lli = new ListItem(ls_role, ls_role);
            role.Items.Add(lli);
        }

        //sql = "select distinct costcenter from users_finance";
        //dt = SQLHelper.GetDataTable(sql);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    ls_role_id = dt.Rows[i][0].ToString();
        //    lli = new ListItem(ls_role_id);
        //    costCenter.Items.Add(lli);
        //}

        ls_id = Request.QueryString["c_id"];
        if (ls_id.Length > 0)
        {
            sql = "select c_id,c_na,c_login,c_cc,c_email,c_dept, c_role,c_arrange  from t_users where c_system='" + du_tools.gcs_sytem + "' and c_id=" + ls_id;
            dt = SQLHelper.GetDataTable(sql);
            id.Text=dt.Rows[0][0].ToString();
            TxtName.Text = dt.Rows[0][1].ToString();
            owner.Text = dt.Rows[0][2].ToString();
            Txtcc.Text = dt.Rows[0][3].ToString();
            email.Text = dt.Rows[0][4].ToString();
            Txtdept.Text = dt.Rows[0][5].ToString();
            ls_role = dt.Rows[0][6].ToString();
            arrange.Text = dt.Rows[0][7].ToString();
            role.SelectedValue = ls_role;
            //sql = "select userid from users where id=" + ls_role_id;
            //dt = SQLHelper.GetDataTable(sql);
            //role.Text = dt.Rows[0][0].ToString();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //string s = owner.Text.ToString().Trim();
        string sql="",ls_id = id.Text;
        string ls_dept=Txtdept.Text.Trim();
        string ls_na=TxtName.Text.Trim();
        string ls_role_id = role.SelectedValue;
        string costCen = Txtcc.Text.ToString().Trim();
        string own = owner.Text.ToString().Trim();
        string ls_email=email.Text.Trim();
        //string classific = classification.Text.ToString().Trim();
        //string sql = "insert into Users_Finance(costCenter,owner,director,classification)values('costCen','own','dire','classific')";

        if (ls_id.Length > 0)
        {
            sql = "select count(*) from t_users where c_login='" + own + "' and c_system='" + du_tools.gcs_sytem + 
                "' and c_id<>'" + ls_id + "'";
            int li_cnt = SQLHelper.ReturnInt(sql);
            if (li_cnt > 0)
            {
                JScript.Alert("User name has been in users list ,pls. check it");
                return;
            }
            sql = "update t_users set c_login='" + own + "', c_na='" + ls_na + "', c_dept='" + ls_dept + 
                "', c_role='" + ls_role_id + "', c_cc='" + costCen + "', c_email='" + ls_email +
                "',c_arrange ='" + arrange.Text+ "' where c_id=" + ls_id;
            SQLHelper.ExecuteNonQuery(sql);
            Response.Write("<script>alert('User info. has been changed');window.location='user.aspx'</script>");
        }
        else
        {
            sql = "select count(*) from t_users where c_login='" + own + "' and c_system='" + du_tools.gcs_sytem + "'";
            int li_cnt = SQLHelper.ReturnInt(sql);
            if (li_cnt > 0)
            {
                JScript.Alert( "User name has been in users list ,pls. check it");
                return;
            }
            sql = "insert into t_users(c_system,c_login, c_na, c_pass, c_dept, c_role, c_cc,c_email,c_arrange)values" +
                "('" + du_tools.gcs_sytem + "','" + owner.Text.Trim() + "','" + TxtName.Text.Trim()+
                "','s','" + Txtdept.Text.Trim() + "','" + ls_role_id + "','" + Txtcc.Text.Trim() + "','" + email.Text.Trim() + "','" + arrange.Text + "')";
            SQLHelper.ExecuteNonQuery(sql);
           // JScript.AjaxAlert(this.Page, "User added, default password is 123456");
            Response.Write("<script>alert('User added');window.location='user.aspx'</script>");
        }
            // cmdstr = "select count(*) from USERS where user_id='" + txtUserName.Text.ToString.Trim() + "' and pwd='" + txtPWD.Text.ToString.Trim() + "'"
        
        //Response.Redirect("user.aspx");
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("user.aspx");
    }
}