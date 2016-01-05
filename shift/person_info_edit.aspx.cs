using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class shift_person_info_edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            init();
    }
    protected void init()
    {
        String id;
        DataTable dt;
        String sql;
        id = Request.QueryString["c_id"];
        if (id.CompareTo("0") > 0)
        {//id>0 , modify ,显示当前要更改的信息
            sql = "select * from t_person where c_id='" + id + "'";
            dt = SQLHelper.GetDataTable(sql);
            if (dt.Rows.Count < 1)
                return;

            work_number.Text = dt.Rows[0][1].ToString();
            name.Text = dt.Rows[0][2].ToString();
            position.Text = dt.Rows[0][3].ToString();
            dept.Text = dt.Rows[0][4].ToString();
        }
    }
    protected void commit_Click(object sender, EventArgs e)
    {
        String id;
      
        String sql;
        id = Request.QueryString["c_id"];
        // modify 
            sql = "update t_person set c_work_number='" + work_number.Text + "',c_name='" + name.Text + "',c_position='" + position.Text + "',c_dept='" + dept.Text + "' where c_id='" + id + "'";
        SQLHelper.ExecuteNonQuery(sql);
        Response.Write("<script>alert('Info. has been changed');window.location='person_info.aspx';</script>");
    }
    protected void return_Click(object sender, EventArgs e)
    {
        Response.Redirect("person_info.aspx");
    }
}