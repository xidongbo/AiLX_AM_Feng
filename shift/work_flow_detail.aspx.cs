using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class shift_work_flow_detail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init_grid_view();
        }

    }
    protected void init_grid_view()
    {
        String p_id = Request.QueryString["c_p_id"];//父表id
        String sql = "select * from t_work_flow_detail where c_p_id='" + p_id + "' order by c_step_num";//按c_step_num排序
        DataSet ds = SQLHelper.GetDataSet(sql);
        grid_view.DataSource = ds;
        grid_view.DataBind();

        
    }
    protected void grid_view_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {//grid_view 翻页功能
        grid_view.PageIndex = e.NewPageIndex;
        init_grid_view();
    }

    protected void grid_view_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grid_view_RowCommand(object sender, GridViewCommandEventArgs e)
    {//RowCommand函数可以得到LinkButton的CommandName和CommandArgument
        //CommandArgument可以得到该行的主键

        String id = e.CommandArgument.ToString();//该行主键 c_id
        if (e.CommandName == "Modify")
        {//更改
            String p_id = Request.QueryString["c_p_id"];//父表id
            Response.Redirect("work_flow_detail_edit.aspx?c_id=" + id + "&c_p_id=" + p_id);//跳转并传入id
        }
        else if (e.CommandName == "my_delete")//写成delete会调RowDeleting函数
        {//删除
            SQLHelper.ExecuteNonQuery("delete from t_work_flow_detail where c_id=" + id);
            init_grid_view();
        }
    }
    protected void grid_view_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色

            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#9aadcd',this.style.fontWeight='';");

            //当鼠标离开的时候 将背景颜色还原的以前的颜色

            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
    }
    protected void query_Click(object sender, EventArgs e)
    {
        String number = step_number.Text.ToString();
        String name = step_name.Text.ToString();
        String op_name = step_op_name.Text.ToString();
        String op_detail = step_op_detail.Text.ToString();
        String sql = "select * from t_work_flow_detail where ";
        if (number == "" && name == "" && op_name == "" && op_detail == "")
        {
            //Response.Write("<script type='text/javascript'>alert('Name and operator are empty!');</script>");
            init_grid_view();
        }
        else
        {
            String p_id = Request.QueryString["c_p_id"];//父表id
            if (number != "")
                sql += "c_step_num='" + number + "' and ";
            if (name != "")
                sql += "c_step_name='" + name + "' and ";
            if (op_name != "")
                sql += "c_step_op_name='" + op_name + "' and ";
            if (op_detail != "")
                sql += "c_step_op_detail='" + op_detail + "' and ";
            sql += "c_p_id='" + p_id + "'";
            // else
            // sql=sql.Substring(0,sql.Length-4);//去掉末尾and 
            DataSet ds = SQLHelper.GetDataSet(sql);
            grid_view.DataSource = ds;
            grid_view.DataBind();
        }
    }
    protected void add_Click(object sender, EventArgs e)
    {
        String p_id = Request.QueryString["c_p_id"];//父表id
        Response.Redirect("work_flow_detail_edit.aspx?c_id=0&c_p_id=" + p_id);//跳转并传入c_id==0表示add，c_id>0表示modify
    }
    protected void return_Click(object sender, EventArgs e)
    {
        Response.Redirect("work_flow.aspx");
    }
  
}