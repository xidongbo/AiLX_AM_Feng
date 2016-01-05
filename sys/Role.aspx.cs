using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Role : System.Web.UI.Page
{
    int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (object.Equals(Session["user_login"], null))
        {
            Response.Redirect("~/login.aspx");
        }
        if (!IsPostBack)
        {
            BindGrid();
        }
    }
   
    private void BindGrid()
    {

        String sql = "select c_id,c_na from t_role where not c_na='admin' and c_system='" + du_tools.gcs_sytem + "'";
        DataTable dt = SQLHelper.GetDataTable(sql);
        
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        
    }

    // protected void rptUser_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        System.Data.DataRowView record = (System.Data.DataRowView)e.Item.DataItem;
    //        int userId = int.Parse(record["userID"].ToString());
    //        if (userId != id)
    //        {
    //            ((Panel)e.Item.FindControl("plItem")).Visible = true;
    //            ((Panel)e.Item.FindControl("plEdit")).Visible = false;
    //        }
    //        else
    //        {
    //            ((Panel)e.Item.FindControl("plItem")).Visible = false;
    //            ((Panel)e.Item.FindControl("plEdit")).Visible = true;
    //        }
    //    }
    //}

   
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("addRole.aspx?c_id=");
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression.ToString();
        string sortDirection = "ASC";
        if (sortExpression == GridView1.Attributes["SortExpression"])
        {
            sortDirection = (GridView1.Attributes["SortDirection"].ToString() == sortDirection ? "DESC" : "ASC");
        }
        GridView1.Attributes["SortExpression"] = sortExpression;
        GridView1.Attributes["SortDirection"] = sortDirection;
        BindGrid();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid(); //重新绑定GridView数据的函数
    }

    protected void edit_click(object sender, EventArgs e)
    {
        string str = ((Button)sender).CommandArgument.ToString();
        Response.Write("<script language=javascript>");
        Response.Write("window.location.href('AddRole.aspx?c_id=" + str + "');");
        Response.Write("</script>");
    }

    protected void del_click(object sender, EventArgs e)
    {
        string str = ((Button)sender).CommandArgument.ToString();
        SQLHelper.ExecuteNonQuery("delete from t_role where c_id=" + str);
        BindGrid();
    }

    protected void Auth_click(object sender, EventArgs e)
    {
        string str = ((Button)sender).CommandArgument.ToString();
        Response.Write("<script language=javascript>");
        Response.Write("window.location.href('permission_manager.aspx?c_id=" + str + "');");
        Response.Write("</script>");
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}