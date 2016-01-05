using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class shift_work_flow_edit : System.Web.UI.Page
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
            sql = "select * from t_work_flow where c_id='" + id + "'";
            dt = SQLHelper.GetDataTable(sql);
            if (dt.Rows.Count < 1)
                return;
            name.Text = dt.Rows[0][1].ToString();
            memo.Text = dt.Rows[0][2].ToString();
            opter.Text = dt.Rows[0][3].ToString();
        }
    }
    protected void commit_Click(object sender, EventArgs e)
    {
        String id;
        String sql;
        id = Request.QueryString["c_id"];
        if (id.CompareTo("0") > 0)//id>0 , modify 
            sql = "update t_work_flow set c_name='" + name.Text + "',c_memo='" + memo.Text + "',c_opter='" + opter.Text + "' where c_id='" + id + "'";
        else if (id.CompareTo("0") == 0)//id==0, add
            sql = "insert into t_work_flow (c_name,c_memo,c_opter) values('" + name.Text + "','" + memo.Text + "','" + opter.Text + "')";
        else return;
        SQLHelper.ExecuteNonQuery(sql);
        Response.Write("<script>alert('Info. has been changed');window.location='work_flow.aspx'</script>");
    }
    protected void return_Click(object sender, EventArgs e)
    {
        Response.Redirect("work_flow.aspx");
    }


}