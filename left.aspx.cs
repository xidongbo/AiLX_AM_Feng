using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class left : System.Web.UI.Page
{
    public static DataTable MyDS_Grid;
    public DataSet DSet;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindMenu();
    }
    private void BindMenu()
    {
        string ls_role, ls_sql, ls_usr;
        ls_usr=Session["user_login"].ToString();

        if (ls_usr == "admin")
            ls_sql = "select DISTINCT c_lvl1_no,c_lvl1  from t_module where c_system='" + du_tools.gcs_sytem + "'";
        else
        {
            ls_role = SQLHelper.ReturnStr("select c_role from t_users where c_na='" + ls_usr + "' and c_system='" + du_tools.gcs_sytem + "'");
            ls_sql = "select DISTINCT t_module.c_lvl1_no,t_module.c_lvl1 " +
                " from t_module, t_role_permission " +
                " where t_role_permission.c_role='" + ls_role +
                    "' and t_module.c_lvl2_no=t_role_permission.c_mod_id and t_module.c_system='" + du_tools.gcs_sytem + 
                    "' and t_role_permission.c_system='" + du_tools.gcs_sytem + "'";
        }
        ls_sql = ls_sql + " order by c_lvl1_no";
        MyDS_Grid = SQLHelper.GetDataTable(ls_sql);
        LeftMenu.DataSource = MyDS_Grid;
        LeftMenu.DataBind();//为控件绑定数据源        
    }

    protected void LeftMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rows = (DataRowView)e.Item.DataItem;
        int ModuleID = Convert.ToInt32(rows["c_lvl1_no"]);
        string Vsql = "";

        string ls_role,  ls_usr;
        ls_usr = Session["user_login"].ToString();

        if (ls_usr == "admin")
            Vsql = "select DISTINCT c_lvl2,c_lvl2_no,c_link from t_module where " +
                " c_lvl1_no=" + ModuleID + " and c_system='" + du_tools.gcs_sytem + "' order by c_lvl2_no";
        else
        {
            ls_role = SQLHelper.ReturnStr("select c_role from t_users where c_na='" + ls_usr + "' and c_system='" + du_tools.gcs_sytem + "'");
            Vsql = "select DISTINCT t_module.c_lvl2,t_module.c_lvl2_no,t_module.c_link " +
                " from t_module, t_role_permission " +
                " where t_role_permission.c_role= '" + ls_role + "' and t_role_permission.c_mod_id= t_module.c_lvl2_no and " +
                " t_module.c_lvl1_no=" + ModuleID + "  and t_module.c_system='" + du_tools.gcs_sytem +
                    "' and t_role_permission.c_system='" + du_tools.gcs_sytem + "' order by t_module.c_lvl2_no";

        }
        Repeater LeftSub = (Repeater)e.Item.FindControl("LeftMenu_Sub");//查找指定id的控件

        DataTable mytb = SQLHelper.GetDataTable(Vsql);
        if (mytb.Rows.Count != 0)
        {
            LeftSub.DataSource = mytb;
            LeftSub.DataBind();
        }
    }
   
}