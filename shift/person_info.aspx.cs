using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class shift_person_info : System.Web.UI.Page
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
        //Grid_view 绑定
        String sql = "select * from t_person order by c_work_number asc";//按工号增序排列
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
            Response.Redirect("person_info_edit.aspx?c_id=" + id);//跳转并传入id
        }
        else if (e.CommandName == "my_delete")//写成delete会调RowDeleting函数
        {//删除
            SQLHelper.ExecuteNonQuery("delete from t_person where c_id=" + id);
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
        String s_number = work_number.Text.ToString();
        String s_name = name.Text.ToString();
        String s_position = position.Text.ToString();
        String s_dept = dept.Text.ToString();
        String sql = "select * from t_person where ";
        if (s_number == "" && s_name == "" && s_position == "" && s_dept== "")
        {
            //Response.Write("<script type='text/javascript'>alert('Name and operator are empty!');</script>");
            init_grid_view();
        }
        else
        {
            if (s_number != "")
                sql += "c_work_number='" + s_number + "' and ";
            if (s_name != "")
                sql += "c_name='" + s_name + "' and ";
            if (s_position != "")
                sql += "c_position='" + s_position + "' and ";
            if (s_dept != "")
                sql += "c_dept='" +s_dept + "'";
            else
              sql=sql.Substring(0,sql.Length-4);//去掉末尾and 
            DataSet ds = SQLHelper.GetDataSet(sql);
            grid_view.DataSource = ds;
            grid_view.DataBind();
        }
    }


    protected void add_from_excel(object sender, EventArgs e)
    {
        Response.Redirect("upload_person_info.aspx");
    }
  
}