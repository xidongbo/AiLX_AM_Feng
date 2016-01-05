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
using System.IO;
using System.DirectoryServices;

public partial class login : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtUserName.Focus();
        }
    }
    
    protected void f_real()
    {
  
        //LDAP验证
        string strPath = "LDAP://147.128.18.10";
        DirectoryEntry de;
        de = new DirectoryEntry(strPath, txtUserName.Text, txtPWD.Text.Trim(), AuthenticationTypes.None);
        DirectorySearcher deSearch = new DirectorySearcher();
        deSearch.SearchRoot = de;
        
        //验证LDAP用户名和密码
        if (VerifyUser(deSearch))
        {
            DataTable dtuser ;
            string sql = "select * from t_users where c_login='" + txtUserName.Text + "'and c_system='" + du_tools.gcs_sytem + "'";
            try
            {
                dtuser = SQLHelper.GetDataTable(sql);
                if (dtuser.Rows.Count > 0)
                {
                    Session["user_login"] = txtUserName.Text;
                    DeleteOverdueFile();
                    Response.Redirect("default.aspx");
                }
                else
                {
                    JScript.AjaxAlert(this.Page, "User Not Exits!");
                    return;
                }
            }
            catch (Exception ex)
            {
                JScript.AjaxAlert(this.Page, "There is something wrong" + ex.Message.Replace("\\", "/").Replace("\'", " "));
                return;
            }
        }
        else
        {
            JScript.AjaxAlert(this.Page, "LDAP failed！");
        }
		
    }
    
    protected void f_test()
    {

        DataTable dtuser;
        string sql = "select * from t_users where c_login='" + txtUserName.Text + "'and c_pass='" + txtPWD.Text.Trim() +
            "' and c_system='" + du_tools.gcs_sytem + "'";
        try
        {
            dtuser = SQLHelper.GetDataTable(sql);
            if (dtuser.Rows.Count > 0)
            {
                Session["user_login"] = txtUserName.Text;
                DeleteOverdueFile();
                Response.Redirect("default.aspx");

            }
            else
            {
                JScript.Alert("User Not Exits!");
                return;
            }
        } catch (Exception ex)  {
            JScript.Alert( "There is something wrong" + ex.Message.Replace("\\", "/").Replace("\'", " "));
            return;
        }
    }

    private void DeleteOverdueFile()
    {
        string path = Server.MapPath("~/App_Data");
        DeleteFile(path);

        //path = Server.MapPath("~/upload");
        //DeleteFile(path);
    }

    private void DeleteFile(string path)
    {
        //获取系统当前时间
        DateTime timenow = System.DateTime.Now;
        TimeSpan timespan;
        //获取App_Code文件夹下的所有文件
        string[] FileCollection = System.IO.Directory.GetFiles(path);
        for (int i = 0; i < FileCollection.Length; i++)
        {
            DateTime createtime = File.GetCreationTime(FileCollection[i]);
            timespan = timenow - createtime;

            //删除upload文件夹中的过期文件(180之前的文件)
            if (timespan.TotalDays >10)
            {
                if(!IsInUse(FileCollection[i]))
                {
                     File.Delete(FileCollection[i]);
                }
            }
        }
    }

    //LDAP验证
    public bool VerifyUser(DirectorySearcher searcher)
    {
        try
        {
            //执行以下方法时没抛出异常说明用户名密码正确
            SearchResultCollection rs = searcher.FindAll();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    /// <summary>
    /// 判断一个文件是否正在使用函数
    /// </summary>
    /// <param name="fileName">将要判断文件的文件名</param>
    /// <returns> bool</returns>
    public static bool IsInUse(string fileName)
    {
        bool inUse = true;
        if (File.Exists(fileName))
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                inUse = false;
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return inUse;           //true表示正在使用,false没有使用
        }
        else
        {
            return false;           //文件不存在则一定没有被使用
        }
    }

 

    protected void lbtLogin_Click()
    {
        bool lb_test = true;
        txtUserName.Text=txtUserName.Text.Trim();
        if (txtUserName.Text == "")
            return;
        f_sql_tmp();
        if (lb_test || txtUserName.Text=="admin")
        {
            f_test();
        }else
        {
            f_real();
        }
    }

    protected void f_sql_tmp()
    {
        //SQLHelper.ExecuteNonQuery("alter table tbl_fareport_1 alter column supplier_RMA_number varchar(400)");
        
 
    }
    protected void lbtLogin_Click(object sender, EventArgs e)
    {
        du_File.f_DeleteOverdueFile(Server.MapPath("~/App_Data"));
        lbtLogin_Click();
    }
}
