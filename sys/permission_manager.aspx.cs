using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class permission_manager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ls_id;
        if (object.Equals(Session["user_login"], null))
        {
            Response.Redirect("~/login.aspx");
        }
        if (!IsPostBack)
        {
            ls_id = Request.QueryString["c_id"].ToString();
            L_role_id.Text = SQLHelper.ReturnStr("select c_na from t_role where c_id=" + ls_id +" and c_system='"+du_tools.gcs_sytem+"'");
            this.TreeView1.Attributes.Add("onclick", "OnTreeNodeChecked()");
            loadtree();
            initTreeView(L_role_id.Text);
        }
    }

    protected void loadtree()
    {
        TreeNode node;
        string sql = "select distinct c_lvl1_no, c_lvl1 from t_module where c_system='"+du_tools.gcs_sytem+"' order by c_lvl1_no";
        DataTable dt = SQLHelper.GetDataTable(sql);
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            node = new TreeNode();
            node.Text = dt.Rows[i][1].ToString();
            node.Value = dt.Rows[i][0].ToString();
            TreeView1.Nodes.Add(node);
            subnode(node);
        }
    }
    protected void subnode(TreeNode pnode)
    {
        TreeNode node;
        string sql = "select c_lvl2_no,c_lvl2 from t_module where c_lvl1_no=" + pnode.Value + " and c_system='" + du_tools.gcs_sytem + "' order by c_lvl2_no";
        
        DataTable dt = SQLHelper.GetDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                node = new TreeNode();
                node.Text = dt.Rows[i][1].ToString();
                node.Value = dt.Rows[i][0].ToString();
                pnode.ChildNodes.Add(node);
            }
        }
    }
    protected void initTreeView(string roleid)
    {
        string sql = "select * from t_role_permission where c_role='" + roleid + "' and c_system='"+du_tools.gcs_sytem+"';";
        DataTable dt;
        dt =SQLHelper.GetDataTable(sql);
        
        foreach (TreeNode item in this.TreeView1.Nodes)
        {
            item.Checked = false;
            foreach (TreeNode citem in item.ChildNodes)
            {
                citem.Checked = false;
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int j = 0;
            foreach (TreeNode item in this.TreeView1.Nodes)
            {
                j = 0;
                foreach (TreeNode citem in item.ChildNodes)
                {
                    if (citem.Value == dt.Rows[i]["c_mod_id"].ToString())
                    {
                        citem.Checked = true;
                        j++;
                    }
                }
                if (j > 0)
                    item.Checked = true;
            }
        }
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        //删除角色之前的所有权限
        string sql = "delete from t_role_permission where c_role='" + L_role_id.Text + "' and c_system='" + du_tools.gcs_sytem + "'";
        SQLHelper.ExecuteNonQuery(sql);
        //得到所有选择的权限
        GetSelectedGroup(this.TreeView1);
    }
  
    private void GetSelectedGroup(TreeView tv)
    {
        string sql = "";
        foreach (TreeNode item in tv.Nodes)
        {
            foreach (TreeNode citem in item.ChildNodes)
            {
                if (citem.Checked)
                {
                    sql += "insert into t_role_permission(c_system, c_role,c_mod_id,c_can) values(" +
                        "'"+du_tools.gcs_sytem+"','"+ L_role_id.Text + "'," + citem.Value + ",'1');";
                }
            }
        }
        if (SQLHelper.ExecuteNonQuery(sql) > 0)
        {
            Response.Write("<script language=javascript>");
            Response.Write("window.alert('保存成功！');");
            Response.Write("</script>");
        }
    }
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色

    //        e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#9aadcd',this.style.fontWeight='';");

    //        //当鼠标离开的时候 将背景颜色还原的以前的颜色

    //        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
    //    }
    //}
    protected void Button6_Click(object sender, EventArgs e)
    {
        Response.Redirect("role.aspx");
    }
}