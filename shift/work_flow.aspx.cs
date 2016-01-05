using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class shift_work_flow : System.Web.UI.Page
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
        String sql = "select * from t_work_flow order by c_id desc";//最新的操作排在最前边
        DataSet ds = SQLHelper.GetDataSet(sql);
        grid_view.DataSource = ds;
        grid_view.DataBind();
    }
    protected void grid_view_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {//grid_view 翻页功能
        grid_view.PageIndex = e.NewPageIndex;
        init_grid_view();
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
    protected void grid_view_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grid_view_RowCommand(object sender, GridViewCommandEventArgs e)
    {//RowCommand函数可以得到LinkButton的CommandName和CommandArgument
        //CommandArgument可以得到该行的主键

        string id = e.CommandArgument.ToString();//该行主键 c_id
        if (e.CommandName == "Modify")
        {//更改
            Response.Redirect("work_flow_edit.aspx?c_id=" + id);//跳转并传入id
        }
        else if (e.CommandName == "my_delete")//写成delete会调RowDeleting函数
        {//删除,t_work_flow_detail中信息一并删除
            SQLHelper.ExecuteNonQuery("delete from t_work_flow where c_id=" + id);
            SQLHelper.ExecuteNonQuery("delete from t_work_flow_detail where c_p_id=" + id);
            init_grid_view();
        }
        else if (e.CommandName == "Detail")
        {//详细信息
            Response.Redirect("work_flow_detail.aspx?c_p_id=" + id);//跳转并传入id
        }

    }

    protected void query_Click(object sender, EventArgs e)
    {
        String name = text_name.Text.ToString();
        String opter = text_operator.Text.ToString();
        String sql;
        if (name == "" && opter == "")
        {
            //Response.Write("<script type='text/javascript'>alert('Name and operator are empty!');</script>");
            init_grid_view();
        }
        else
        {
            if (name == "")
                sql = "select * from t_work_flow where c_opter='" + opter + "'";
            else if (opter == "")
                sql = "select * from t_work_flow where c_name='" + name + "'";
            else
                sql = "select * from t_work_flow where c_name='" + name + "' and c_opter='" + opter + "'";
            DataSet ds = SQLHelper.GetDataSet(sql);
            grid_view.DataSource = ds;
            grid_view.DataBind();
        }


    }
    protected void add_Click(object sender, EventArgs e)
    {
        Response.Redirect("work_flow_edit.aspx?c_id=0");//跳转并传入0表示add，c_id>0表示modify
    }
}