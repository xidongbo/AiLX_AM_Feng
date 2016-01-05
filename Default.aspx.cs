using System;
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

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (object.Equals(Session["user_login"], null))
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            initpage();
        }
    }
    protected void initpage()
    {
        //Label2.Text = Session["UserID"].ToString();
        //string userid = Session["UserID"].ToString();
        //DropDownList1.Items.Clear();
        //DataTable dt = new DataTable();
        //dt.Columns.Add("c_roleID");
        //dt.Columns.Add("c_roleName");

        //string sql = "select c_roleID from t_user_role where c_userID='" + userid + "'";
        DataSet ds = new DataSet();
        Label2.Text = Session["user_login"].ToString();
        //DataTable dtt = new DataTable();
        //dtt.Columns.Add("c_roleID");
        //ds = myMeans.search(sql);
        //dtt = ds.Tables[0];

        //DataRow dr = dtt.NewRow();
        //dr["c_roleID"] = Session["Role"];
        //dtt.Rows.Add(dr);

        //for (int i = 0; i < dtt.Rows.Count; i++)
        //{
            //string sqls = "select roleDescribe from Role where roleID=" + dtt.Rows[i][0] + " ;";
        //string sqls = "select roleDescribe from Role where roleID=" + Session["RoleID"] + " ;";
        //    ds = myMeans.search(sqls);
        //    DataTable dt1 = new DataTable();
        //    dt1 = ds.Tables[0];

        //    DataRow drr = dt.NewRow();
        //    drr["c_roleID"] = dtt.Rows[i][0];
        //    drr["c_roleName"] = dt1.Rows[0][0];
        //    dt.Rows.Add(drr);

        //}
        //int j=0;
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DropDownList1.Items.Add(new ListItem(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString()));
        //    if (Session["RoleID"].ToString() == dt.Rows[i][0].ToString())
        //        j = i;
        //}
        //DropDownList1.SelectedIndex = j;
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string roleID = DropDownList1.SelectedItem.Value;
        //Session["RoleID"] = roleID;
        //SqlDataReader rolelist = myMeans.getcom("select PermissionLocation from Role_Permission, Permission where RoleID='" + Session["RoleID"] + "' and Role_Permission.PermissionID=Permission.PermissionID");
        //while (rolelist.Read())
        //    Session[rolelist[0].ToString()] = 1;
        //Response.Redirect("Default.aspx");
    }
}
