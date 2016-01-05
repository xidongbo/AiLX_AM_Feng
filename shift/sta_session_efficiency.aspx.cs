using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Reflection;

public partial class sta_session_efficiency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (object.Equals(Session["user_login"], null))
        {
            Response.Redirect("~/login.aspx");
        }
        if (!IsPostBack)
        {
            //获得父表的id
            lb_id.Text = Request.QueryString["c_id"];
            initPage();
            //InitGridView();
        }
    }
    protected void initPage()
    {
        txt_dt2.Text = du_Date.Today_str();
        txt_dt1.Text = du_Date.getFirstDayThisMonth();
    }


    ////初始化学院下拉框
    //protected void initXSH()
    //{
    //    DataSet ds = new DataSet();
    //    System.Data.DataTable dt = new System.Data.DataTable();
    //    string sql = "select xsh,xsm from CODE_XSB where xsh='" + Session["xsh"] + "'";
    //    ds = myMeans.search_pub(sql);
    //    dt = ds.Tables[0];
    //    DropDownList2.Items.Clear();
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        DropDownList2.Items.Add(new ListItem(dt.Rows[i]["xsm"].ToString(), dt.Rows[i]["xsh"].ToString()));
    //    }
    //    DropDownList2.Enabled = false;
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitGridView();
    }
    protected System.Data.DataTable getDB()
    {

        string ls_sql = " from allperson where (1=1)";
        string ls_dept;
        int  li_out, li_add;
        double li_total;

        if (txtDept.Text.Trim().Length > 0)
        {
            ls_sql += "and(Dept like '%" + txtDept.Text.Trim() + "%')";
        }

        string sql = "select distinct dept " + ls_sql;
        System.Data.DataTable dt =SQLHelper.GetDataTable(sql);
        dt.Columns.Add("total_hr");
        dt.Columns.Add("c_out");
        dt.Columns.Add("c_eff");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ls_dept = dt.Rows[i]["dept"].ToString();
            //出勤时间
            li_out = SQLHelper.ReturnInt("select sum(c_hour) from t_shift_shift_detail s, t_shift_shift p " +
                " where s.c_p_id=p.c_id and c_no in(select the_id from allperson where dept='" + ls_dept + "' ) " +
                "and c_date>='" + txt_dt1.Text + "' and c_date<='" + txt_dt2.Text + "'");
            dt.Rows[i]["c_out"] = li_out.ToString();

            //订单工时，是订单个数乘以标准工时
            li_total = SQLHelper.ReturnDouble("select  sum(convert(float,c_real*t_shift_result.c_ratio)/c_pers) from t_shift_result, t_shift_shift_detail" +
                " where t_shift_result.c_p_id=t_shift_shift_detail.c_p_id and c_dept='" + ls_dept + "' and c_date>='" + txt_dt1.Text +
                "' and c_date<='" + txt_dt2.Text + "'");
            dt.Rows[i]["total_hr"] = li_total.ToString();
            
            //额外加的工时
            li_add = SQLHelper.ReturnInt("select sum(c_hr) from t_shift_addition where c_no in(select the_id from allperson where dept='" + ls_dept + "' ) "+
                " and c_date>='" + txt_dt1.Text + "' and c_date<='" + txt_dt2.Text + "'");

            dt.Rows[i]["c_eff"] = Math.Round(((li_total+li_add) / (li_out * 6.9 * 0.95 * 0.95 / 8)), 2).ToString();
        }
        return dt;
    }

    private System.Data.DataTable InitGridView()
    {

        // 获取GridView排序数据列及排序方向   
        string sortExpression = this.GridView1.Attributes["SortExpression"];
        string sortDirection = this.GridView1.Attributes["SortDirection"];
        // 调用业务数据获取方法   
        
        System.Data.DataTable dtBind = this.getDB();
        // 根据GridView排序数据列及排序方向设置显示的默认数据视图   
        if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
        {
            dtBind.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
        }
        // GridView绑定并显示数据   
        this.GridView1.DataSource = dtBind;
        this.GridView1.DataBind();
        return dtBind;
    }
    //Gridview的分页功能
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        InitGridView();// 重新绑定一下GridView数据函数
    }
    /// <summary>
    /// GridView的Sorting事件方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        BindView();
    }

    protected void edit_click(object sender, EventArgs e)
    {
    }

    protected void Gridview1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        if (e.CommandName == "Edit")
        {
            Response.Redirect("per_info_edit.aspx?c_id=" + id);
        }
    }

    #region
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //public void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    string sortExpression = e.SortExpression.ToUpper();
    //    SortColumn = e.SortExpression;
    //    string newSortOrder="";
    //    if (ViewState[SortColumn] == null)
    //        ViewState[SortColumn] = "ASC";
    //    if (GridViewSortDirection == SortDirection.Ascending)
    //    {
    //        GridViewSortDirection = SortDirection.Descending;
    //        //排序並重新綁定
    //        SortGridView(sortExpression, "DESC");
    //        newSortOrder = "DESC";
    //    }
    //    else if (GridViewSortDirection == SortDirection.Descending)
    //    {
    //        GridViewSortDirection = SortDirection.Ascending;
    //        //排序並重新綁定
    //        SortGridView(sortExpression, "ASC");
    //        newSortOrder = "ASC";
    //    }
    //    ViewState[SortColumn] = newSortOrder;
    //}
    //private void SortGridView(string sortExpression, string direction)
    //{
    //    //DataSet ds =; //查找数据源
        
    //    System.Data.DataTable dt = getDB();

    //    DataView dv = new DataView(dt);
    //    dv.Sort = sortExpression + " " + direction;
    //    GridView1.DataSource = dv; //将DataView绑定到GridView上
    //    GridView1.DataBind();

    //}
    /// <summary>
    /// 排序方向屬性
    ///// </summary>
    //public SortDirection GridViewSortDirection
    //{
    //    get
    //    {
    //        if (ViewState["sortDirection"] == null)
    //        {
    //            ViewState["sortDirection"] = SortDirection.Ascending;
               
    //        }
    //        return (SortDirection)ViewState["sortDirection"];
    //    }
    //    set
    //    {
    //        ViewState["sortDirection"] = value;
    //    }
    //}
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    for (int i = 0; i < GridView1.Rows.Count; i++)
    //    {
    //        if (((System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].Cells[0].Controls[1]).Checked == true)
    //        {
    //            download((GridView1.Rows[i].FindControl("c_na") as System.Web.UI.WebControls.Label).Text,(GridView1.Rows[i].FindControl("c_cls") as System.Web.UI.WebControls.Label).Text,(GridView1.Rows[i].FindControl("c_no") as System.Web.UI.WebControls.Label).Text,(GridView1.Rows[i].FindControl("c_plan") as System.Web.UI.WebControls.Label).Text,(GridView1.Rows[i].FindControl("c_real") as System.Web.UI.WebControls.Label).Text,(GridView1.Rows[i].FindControl("c_gap") as System.Web.UI.WebControls.Label).Text,(GridView1.Rows[i].FindControl("c_handl_rlt") as System.Web.UI.WebControls.Label).Text);
    //            //sql1 += "update t_normal_rlt set c_sch_chg='同意', c_chg_op='" + username + "', c_chg_time='"
    //            //    + DateTime.Now.ToLocalTime().ToString() + "' where c_no='" + (GridView1.Rows[i].FindControl("c_no") as Label).Text +
    //            //    "' and c_lvl='" + DropDownList6.SelectedItem.Text + "';";

    //        }
    //    }
    //    InitGridView();
    //}
    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        for (int i = 0; i < GridView1.Columns.Count; i++)
    //        {
    //            if (GridView1.Columns[i].SortExpression == SortColumn)
    //            {
    //                System.Web.UI.WebControls.Label labela = new System.Web.UI.WebControls.Label();
    //                labela.Text = (string)ViewState[SortColumn] == "ASC" ? "▼":"▲" ;
    //                e.Row.Cells[i].Controls.Add(labela);
    //            }
    //        }
    //    }
    //}
    //protected void edit_click(object sender, EventArgs e)
    //{
    //    System.Web.UI.WebControls.Button bb = (System.Web.UI.WebControls.Button)sender;
    //    DataControlFieldCell dcf = (DataControlFieldCell)bb.Parent;
    //    GridViewRow gvr = (GridViewRow)dcf.Parent;
    //    GridView1.SelectedIndex = gvr.RowIndex;
    //    int i = GridView1.SelectedIndex;
    //    download((GridView1.Rows[i].FindControl("c_na") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_cls") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_no") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_plan") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_real") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_gap") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_handl_rlt") as System.Web.UI.WebControls.Label).Text);
    //    //downloadExcel((GridView1.Rows[i].FindControl("c_na") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_cls") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_no") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_plan") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_real") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_gap") as System.Web.UI.WebControls.Label).Text, (GridView1.Rows[i].FindControl("c_handl_rlt") as System.Web.UI.WebControls.Label).Text);
    //}
    //protected void download(string xm,string bh,string xh,string plan,string real,string gap,string handl)
    //{
    //     Microsoft.Office.Interop.Word._Application appWord = new Microsoft.Office.Interop.Word.ApplicationClass();
    //    Microsoft.Office.Interop.Word._Application appWord = new Microsoft.Office.Interop.Word.Application();
    //    Microsoft.Office.Interop.Word._Document doc = null;
    //    try
    //    {
    //        appWord = new Microsoft.Office.Interop.Word.ApplicationClass();
    //        appWord = new Microsoft.Office.Interop.Word.Application();
    //        appWord.Visible = false;
    //        object objTrue = true;
    //        object objFalse = false;
    //        object objTemplate = @"f:\5.dot";//模板绝对路径
    //        // object objTemplate = Server.MapPath("person.dot");//模板相对路径
    //        object objTemplate = Server.MapPath(@"dot//6.dot");//
    //        object objDocType = WdDocumentType.wdTypeDocument;
    //        doc = (DocumentClass)appWord.Documents.Add(ref objTemplate, ref objFalse, ref objDocType, ref objTrue);
    //        第一步生成word文档
    //        定义书签变量
    //        object name1 = "xm1";
    //        object name2 = "xm2";
    //        object class1 = "bh1";
    //        object class2 = "bh2";
    //        object number1 = "xh1";
    //        object number2 = "xh2";
    //        object planscore1 = "plan1";
    //        object planscore2 = "plan2";
    //        object realscore1 = "real1";
    //        object realscore2 = "real2";
    //        object gapscore1 = "gap1";
    //        object gapscore2 = "gap2";
    //        object handlrlt1 = "handl1";
    //        object handlrlt2 = "handl2";
    //        第三步 给书签赋值
    //        给书签赋值
    //        doc.Bookmarks.get_Item(ref name1).Range.Text = xm;
    //        doc.Bookmarks.get_Item(ref name2).Range.Text = xm;
    //        doc.Bookmarks.get_Item(ref class1).Range.Text = bh;
    //        doc.Bookmarks.get_Item(ref class2).Range.Text = bh;
    //        doc.Bookmarks.get_Item(ref number1).Range.Text = xh;
    //        doc.Bookmarks.get_Item(ref number2).Range.Text = xh;
    //        doc.Bookmarks.get_Item(ref planscore1).Range.Text = plan;
    //        doc.Bookmarks.get_Item(ref planscore2).Range.Text = plan;
    //        doc.Bookmarks.get_Item(ref realscore1).Range.Text = real;
    //        doc.Bookmarks.get_Item(ref realscore2).Range.Text = real;
    //        doc.Bookmarks.get_Item(ref gapscore1).Range.Text = gap;
    //        doc.Bookmarks.get_Item(ref gapscore2).Range.Text = gap;
    //        doc.Bookmarks.get_Item(ref handlrlt1).Range.Text = handl;
    //        doc.Bookmarks.get_Item(ref handlrlt2).Range.Text = handl;
    //        第四步 生成word  
    //        DateTime dtt = DateTime.Now;
    //        object filename = Server.MapPath(@"doc//") + "表" + dtt.Ticks.ToString() + ".doc";
    //        object miss = System.Reflection.Missing.Value;
    //        doc.SaveAs(ref filename, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);

    //        //退出word,同时释放资源
    //        object missingValue = Type.Missing;
    //        object doNotSaveChanges = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
    //        doc.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);
    //        appWord.Quit(ref miss, ref miss, ref miss);
    //        doc = null;
    //        appWord = null;
            
    //         导出到客户端
    //        string fileName = System.IO.Path.Combine(Server.MapPath(@"doc\\"), "表" + dtt.Ticks.ToString() + ".doc");
    //        FileInfo fi = new FileInfo(fileName);
    //        if (fi.Exists)
    //        {
    //            Response.Clear();
    //            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("表" + dtt.Ticks.ToString() + ".doc", System.Text.Encoding.UTF8));
    //            Response.AddHeader("Content-Length", fi.Length.ToString());
    //            Response.ContentType = "application/octet-stream";
    //            Response.Filter.Close();
    //            Response.WriteFile(fi.FullName);
    //            Response.End();
    //        }
    //        KillProcessexcel("WORD");
    //        System.IO.File.Delete(filename.ToString());
    //        System.IO.File.Delete(fileName);
    //        if (File.Exists(filename.ToString()))
    //        {
    //            File.Delete(filename.ToString());
    //        }
    //    }catch(Exception ex){
    //        捕捉异常，如果出现异常则清空实例，退出word,同时释放资源
    //        throw ex;
    //        string aa = ex.ToString();
    //        object miss = System.Reflection.Missing.Value;
    //        object missingValue = Type.Missing;
    //        object doNotSaveChanges = WdSaveOptions.wdDoNotSaveChanges;
    //        doc.Close(ref doNotSaveChanges, ref missingValue, ref missingValue);
    //        appWord.Quit(ref miss, ref miss, ref miss);
    //        doc = null;
    //        appWord = null;
    //        Response.Write("<script language=javascript>");
    //        Response.Write("window.alert('下载出错！" + ex.Message.ToString() + "');");
    //        Response.Write("</script>");
    //    }
    //}
    //protected void downloadExcel(string xm, string bh, string xh, string plan, string real, string gap, string handl)
    //{
    //    //操作excel
    //    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
    //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
    //    xlWorkBook = new Microsoft.Office.Interop.Excel.Application().Workbooks.Add(Type.Missing);
    //    xlWorkBook.Application.Visible = false;
    //    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Sheets[1];
    //    //第一行 设置标题
    //    Microsoft.Office.Interop.Excel.Range range1 = xlWorkSheet.get_Range("A1", "F1");//选择操作块
    //    range1.Value2 = "学业警示存单";
    //    xlWorkBook.Application.DisplayAlerts = false;//使合并操作不提示警告信息
    //    range1.Merge(false);//参数为True则为每一行合并为一个单元格
    //    xlWorkBook.Application.DisplayAlerts = true;
    //    ///设置字体，长宽等
    //    //设置高度为自动          
    //    range1.EntireColumn.AutoFit();
    //    range1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//设置标题格式为居中对齐 
    //    //设置字体
    //    range1.Cells.Font.Size = 26;
    //    range1.Cells.Font.Name = "黑体";
    //    ///第二行
    //    xlWorkSheet.Cells[2, 1] = "姓名";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 1]).Columns.RowHeight = 28;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 1]).Columns.ColumnWidth = 13;
    //    xlWorkSheet.Cells[2, 2] = xm;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 2], xlWorkSheet.Cells[2, 2]).Columns.ColumnWidth = 11;
    //    xlWorkSheet.Cells[2, 3] = "班级";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 3], xlWorkSheet.Cells[2, 3]).Columns.ColumnWidth = 7;
    //    xlWorkSheet.Cells[2, 4] = bh;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 4], xlWorkSheet.Cells[2, 4]).Columns.ColumnWidth = 15;
    //    xlWorkSheet.Cells[2, 5] = "学号";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 5], xlWorkSheet.Cells[2, 5]).Columns.ColumnWidth = 5;
    //    xlWorkSheet.Cells[2, 6] = xh;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 6], xlWorkSheet.Cells[2, 6]).Columns.ColumnWidth = 15;
    //    //设置字体
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 1]).Font.Size = 11;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 2], xlWorkSheet.Cells[2, 2]).Font.Size = 11;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 3], xlWorkSheet.Cells[2, 3]).Font.Size = 11;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 4], xlWorkSheet.Cells[2, 4]).Font.Size = 11;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 5], xlWorkSheet.Cells[2, 5]).Font.Size = 11;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 6], xlWorkSheet.Cells[2, 6]).Font.Size = 11;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 1]).Font.Name = "宋体";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 2], xlWorkSheet.Cells[2, 2]).Font.Name = "宋体";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 3], xlWorkSheet.Cells[2, 3]).Font.Name = "宋体";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 4], xlWorkSheet.Cells[2, 4]).Font.Name = "宋体";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 5], xlWorkSheet.Cells[2, 5]).Font.Name = "宋体";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[2, 6], xlWorkSheet.Cells[2, 6]).Font.Name = "宋体";
    //    //第三行
    //    xlWorkSheet.Cells[3, 1] = "学分获得情况";
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 1]).Columns.RowHeight = 56;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 1]).Font.Size = 11;
    //    xlWorkSheet.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 1]).Font.Name = "宋体";
    //    range1 = xlWorkSheet.get_Range("B3", "F3");
    //    range1.Value2 = "当前应修学分：" + plan + "  ; 已修学分： " + real + "  ;  所差学分：" + gap + "  ;";
    //    xlWorkBook.Application.DisplayAlerts = false;//使合并操作不提示警告信息
    //    range1.Merge(false);//参数为True则为每一行合并为一个单元格
    //    xlWorkBook.Application.DisplayAlerts = true;
    //    range1.Font.Name = "宋体";
    //    range1.Font.Size = 11;
        
    //    //第四行
    //    xlWorkSheet.Cells[4, 1] = "警示情况";
    //    range1 = xlWorkSheet.get_Range("B4", "F4");
    //    range1.Value2 = handl;
    //    xlWorkBook.Application.DisplayAlerts = false;//使合并操作不提示警告信息
    //    range1.Merge(false);//参数为True则为每一行合并为一个单元格
    //    xlWorkBook.Application.DisplayAlerts = true;
    //    //第五行
    //    range1 = xlWorkSheet.get_Range("A5", "F5");
    //    range1.Value2 = "本人已收到本次学业警示通知，并决定制定合理学习计划，努力补齐所欠学分。";
    //    xlWorkBook.Application.DisplayAlerts = false;//使合并操作不提示警告信息
    //    range1.Merge(false);//参数为True则为每一行合并为一个单元格
    //    xlWorkBook.Application.DisplayAlerts = true;
    //    // 第六行
    //    range1 = xlWorkSheet.get_Range("A6", "F6");
    //    range1.Value2 = "本人签名：               日期：       年      月      日 ";
                
    //    xlWorkBook.Application.DisplayAlerts = false;//使合并操作不提示警告信息
    //    range1.Merge(false);//参数为True则为每一行合并为一个单元格
    //    xlWorkBook.Application.DisplayAlerts = true;
    //    // 保存excel文件
    //    string name = System.DateTime.Now.ToString().Replace(":", "").Replace("/", "").Replace(" ", "") + "xyjsd.xlsx";
    //    string filePath = Server.MapPath("excel\\") + name;
    //    //Label1.Text += filePath;
    //    xlWorkBook.SaveAs(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
    //    //xlWorkBook.SaveCopyAs(filePath);
    //    xlWorkBook.Application.Quit();
    //    xlWorkSheet = null;
    //    xlWorkBook = null;
    //    GC.Collect();
    //    System.GC.WaitForPendingFinalizers();

    //    // 导出到客户端
    //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
    //    Response.AppendHeader("content-disposition", "attachment;filename=" + name);
    //    Response.ContentType = "Application/excel";
    //    Response.WriteFile(filePath);
    //    Response.End();
    //    KillProcessexcel("EXCEL");
    //    //Label1.Text += filePath;
    //    //if (File.Exists(filePath))
    //    //{
    //    //    File.Delete(filePath);
    //    //}
    //}

    #endregion

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色

            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#9aadcd',this.style.fontWeight='';");

            //当鼠标离开的时候 将背景颜色还原的以前的颜色

            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
    }

    //private void KillProcessexcel(string processName)
    //{ //获得进程对象，以用来操作
    //    System.Diagnostics.Process myproc = new System.Diagnostics.Process();
    //    //得到所有打开的进程
    //    try
    //    {
    //        //获得需要杀死的进程名
    //        foreach (Process thisproc in Process.GetProcessesByName(processName))
    //        { //立即杀死进程
    //            thisproc.Kill();
    //        }
    //    }
    //    catch (Exception Exc)
    //    {
    //        throw new Exception("", Exc);
    //    }
    //}
    /// <summary>
    /// 为排序添加上下箭头
    /// </summary>
    /// <param name="sortDirection"></param>
    /// <param name="sortExpression"></param>
    protected void SortForImage(string sortDirection, string sortExpression)
    {
        if (!string.IsNullOrEmpty(sortExpression))
        {
            GridViewRow headRow = GridView1.HeaderRow;
            Image sortImage = new Image();
            if (sortDirection == "ASC")
            {
                sortImage.ImageUrl = "~/images/up01.gif";
                sortImage.Style.Add("vertical-align", "bottom");
            }
            else
            {
                sortImage.ImageUrl = "~/images/down01.gif";
                sortImage.Style.Add("vertical-align", "bottom");
            }
            int num = 0;
            foreach (DataControlField field in GridView1.Columns)
            {
                if (field.SortExpression == sortExpression.ToString().Trim())
                {
                    num = GridView1.Columns.IndexOf(field);
                }
            }
            headRow.Cells[num].Controls.Add(sortImage);
        }
    }
    private void BindView()
    {
        System.Data.DataTable dt = this.getDB();
        if (dt.Rows.Count > 0)
        {
            ViewState["PageCount"] = dt.Rows.Count.ToString();
            string sortExpression = GridView1.Attributes["SortExpression"];
            string sortDirection = GridView1.Attributes["SortDirection"];
            if ((!string.IsNullOrEmpty(sortExpression)) && (!string.IsNullOrEmpty(sortDirection)))
            {
                dt.DefaultView.Sort = string.Format("{0} {1}", sortExpression, sortDirection);
            }
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataBind();
            SortForImage(GridView1.Attributes["SortDirection"], GridView1.Attributes["SortExpression"]);
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/welcome.aspx");
    }
}