using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// JScript 的摘要说明
/// </summary>
public class du_tools
{
    public const string gcs_sytem = "shift";
    public du_tools()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// 弹出JavaScript小窗口
    /// </summary>
    /// <param name="js">窗口信息</param>
    public static void f_ok_2_url(string message, string url)
    {
        string js = "<script language=javascript>alert('"+message+"');window.location.replace('"+url+"')</script>";
        HttpContext.Current.Response.Write(js);
    }


    /// <summary>
    /// 在网页中弹出消息警告对话框,并关闭当前网页
    /// </summary>
    /// <param name="page">要注册javascript脚本的web窗体对象</param>
    /// <param name="message">警告消息内容</param>
    public static void CloseWindow(Page page, string message)
    {
        string js = string.Format("alert('{0}');window.close();", message);
        page.ClientScript.RegisterStartupScript(page.GetType(), "error", js, true);
    }


    /// <summary>
    /// 弹出JavaScript小窗口,并返回上一步
    /// </summary>
    /// <param name="message">窗口信息</param>
    public static void AlertAndGoBack(string message)
    {
        string js = @"<Script language='JavaScript'>
                    alert('" + message + "');history.go(-1);</Script>";
        HttpContext.Current.Response.Write(js);
    }

    public static void AlertForSave(string message)
    {
        string RamCode = DateTime.Now.ToString("yyyyMMddHHmmssff");
        string Url = HttpContext.Current.Request.Url.ToString();
        if (Url.IndexOf('?') > 0)
            Url = string.Format("{0}&RamCode={1}", Url, RamCode);
        else
            Url = string.Format("{0}?RamCode={1}", Url, RamCode);

        string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
        HttpContext.Current.Response.Write(string.Format(js, message, Url));
    }

    /// <summary>
    /// 回到历史页面
    /// </summary>
    /// <param name="value">-1/1</param>
    public static void GoHistory(int value)
    {
        #region
        string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
        HttpContext.Current.Response.Write(string.Format(js, value));
        #endregion
    }

    /// <summary>
    /// 关闭当前窗口
    /// </summary>
    public static void CloseWindow()
    {
        #region
        string js = @"<Script language='JavaScript'>
                    parent.opener=null;window.close();  
                  </Script>";
        HttpContext.Current.Response.Write(js);
        HttpContext.Current.Response.End();
        #endregion
    }

    
    /// <summary>
    /// 刷新打开窗口
    /// </summary>
    public static void RefreshOpener()
    {
        #region
        string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
        HttpContext.Current.Response.Write(js);
        #endregion
    }


    /// <summary>
    /// 打开指定大小的新窗体
    /// </summary>
    /// <param name="url">地址</param>
    /// <param name="width">宽</param>
    /// <param name="heigth">高</param>
    /// <param name="top">头位置</param>
    /// <param name="left">左位置</param>
    public static void OpenWebFormSize(string url, int width, int heigth, int top, int left)
    {
        #region
        string js = @"<Script language='JavaScript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";

        HttpContext.Current.Response.Write(js);
        #endregion
    }

    /// <summary>
    /// 打开指定大小位置的模式对话框
    /// </summary>
    /// <param name="webFormUrl">连接地址</param>
    /// <param name="width">宽</param>
    /// <param name="height">高</param>
    /// <param name="top">距离上位置</param>
    /// <param name="left">距离左位置</param>
    public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left)
    {
        #region
        string features = "dialogWidth:" + width.ToString() + "px"
            + ";dialogHeight:" + height.ToString() + "px"
            + ";dialogLeft:" + left.ToString() + "px"
            + ";dialogTop:" + top.ToString() + "px"
            + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
        ShowModalDialogWindow(webFormUrl, features);
        #endregion
    }

    public static void ShowModalDialogWindow(string webFormUrl, string features)
    {
        string js = ShowModalDialogJavascript(webFormUrl, features);
        HttpContext.Current.Response.Write(js);
    }

    public static string ShowModalDialogJavascript(string webFormUrl, string features)
    {
        #region
        string js = @"<script language=javascript>							
							showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
        return js;
        #endregion
    }



    public static void ShowMsg(EeekSoft.Web.PopupWin PopupWin1, string Message)
    {
        //设置为默认的消息窗口 
        PopupWin1.ActionType = EeekSoft.Web.PopupAction.MessageWindow;
        //设置窗口的标题，消息文字 
        PopupWin1.Title = "Warning！";
        PopupWin1.Message = Message;
        PopupWin1.Text = Message;
        //设置颜色风格 
        PopupWin1.ColorStyle = EeekSoft.Web.PopupColorStyle.Blue;
        //设置窗口弹出和消失的时间 
        PopupWin1.HideAfter = 3000;
        PopupWin1.ShowAfter = 100;
        PopupWin1.Visible = true;
    }

    public static bool IsInteger(string s)
    {
        string pattern = @"^\d*$";
        return System.Text.RegularExpressions.Regex.IsMatch(s, pattern);

    }

    public static void f_data_2_ddlb(string ps_sql, System.Web.UI.Control pddlb)
    {
        DropDownList ddlb = (DropDownList)pddlb;
        ddlb.Items.Clear();
        ddlb.Items.Add("");
        System.Data.DataTable ldt = SQLHelper.GetDataTable(ps_sql);
        for (int li_i = 0; li_i < ldt.Rows.Count; li_i++)
        {
            ddlb.Items.Add(ldt.Rows[li_i][0].ToString().Trim());
        }
    }

    //给datatable排序
    public static DataTable f_dt_order(DataTable dt, string ps_order)
    {
        DataView dv = dt.DefaultView;
        dv.Sort = ps_order;
        System.Data.DataTable dt2 = dv.ToTable();
        return dt2;
    }
}
