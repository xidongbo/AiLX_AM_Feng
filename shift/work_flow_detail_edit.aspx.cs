using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class shift_work_flow_detail_edit : System.Web.UI.Page
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
        //DropDownList 绑定
        sql = "select * from t_person";
        DataSet ds = SQLHelper.GetDataSet(sql);
        step_op_name.DataSource = ds;
        step_op_name.DataBind();

        if (id.CompareTo("0") > 0)
        {//id>0 , modify ,显示当前要更改的信息
            sql = "select * from t_work_flow_detail where c_id='" + id + "'";
            dt = SQLHelper.GetDataTable(sql);
            if (dt.Rows.Count < 1)
                return;

            step_name.Text = dt.Rows[0][2].ToString();
            step_number.Text = dt.Rows[0][3].ToString();
            step_op_name.SelectedValue = dt.Rows[0][4].ToString();
           // step_op_name.Text = dt.Rows[0][4].ToString();
            step_op_detail.Text = dt.Rows[0][5].ToString();
          
        }
    }
    protected void commit_Click(object sender, EventArgs e)
    {
        String id;
        String p_id;
        String sql;
        id = Request.QueryString["c_id"];
        p_id = Request.QueryString["c_p_id"];
        if (id.CompareTo("0") > 0)//id>0 , modify 
            sql = "update t_work_flow_detail set c_step_num='" + step_number.Text + "',c_step_name='" + step_name.Text + "',c_step_op_name='" + step_op_name.Text + "',c_step_op_detail='" + step_op_detail.Text + "' where c_id='" + id + "'";
        else if (id.CompareTo("0") == 0)//id==0, add
            sql = "insert into t_work_flow_detail (c_p_id,c_step_num,c_step_name,c_step_op_name,c_step_op_detail) values('" + p_id + "','" + step_number.Text + "','" + step_name.Text + "','" + step_op_name.Text + "','" + step_op_detail.Text + "')";
        else return;
        SQLHelper.ExecuteNonQuery(sql);
        Response.Write("<script>alert('Info. has been changed');window.location='work_flow_detail.aspx?c_p_id=" + p_id + "'" + "</script>");
    }
    protected void return_Click(object sender, EventArgs e)
    {
        String p_id = Request.QueryString["c_p_id"];
        Response.Redirect("work_flow_detail.aspx?c_p_id=" + p_id);
    }
}