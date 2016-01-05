using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using Microsoft.Office.Interop.Excel;

/// <summary>
/// Report 的摘要说明
/// </summary>
public class du_excel
{

    public static void DataTable2Excel(System.Data.DataTable dtData)
    {
        System.Web.UI.WebControls.DataGrid dgExport = null;
        // 当前对话
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        // IO用于导出并返回excel文件
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;
        //设置导出格式
        string style = @"<style> .text {mso-number-format:\@} </script>";
        if (dtData != null)
        {
            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.AddHeader("content-disposition", "attachment;");
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "";

            // 导出excel文件
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

            // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid
            dgExport = new System.Web.UI.WebControls.DataGrid();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();

            // 返回客户端
            curContext.Response.Write(style);
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }

    public static void f_save_file(System.Web.UI.Page page, ref Application app, ref _Workbook workBook, ref Sheets workSheets, ref  _Worksheet workSheet, string outputFile)
    {
        //SQLHelper.ExecuteNonQuery("insert into t_debug values('workBook.SaveAs0')");
        FileInfo file = new FileInfo(outputFile);
        if (file.Exists)
            file.Delete();

        //SQLHelper.ExecuteNonQuery("insert into t_debug values('workBook.SaveAs1')");

        workBook.SaveAs(outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value,
            Missing.Value, Missing.Value);
        SQLHelper.ExecuteNonQuery("insert into t_debug values('workBook.SaveAs2')");
        //Close the workbook
    //    workBook.Close(false, null, false);
    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
        //Close the Excel Process
    //    app.Quit();
    //    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
    //    app = null;
    //    GC.Collect();
    //    PageCommon.outExcel(outputFile);


        int generation1 = System.GC.GetGeneration(app);
        GC.Collect(generation1);
        generation1 = System.GC.GetGeneration(workSheet);
        GC.Collect(generation1);
        generation1 = System.GC.GetGeneration(workBook);
        GC.Collect(generation1);
        generation1 = System.GC.GetGeneration(workSheets);
        GC.Collect(generation1);

        //System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheets);
        //Close the workbook
        workBook.Close(false, null, false);

        System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
        //Close the Excel Process        
        app.Quit();
        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
        app = null;
        workSheet = null;
        workSheets = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();

        //下载到浏览器
        //PageCommon.outExcel(outputFile);
        //SQLHelper.ExecuteNonQuery("insert into t_debug values('PageCommon.outExcel')");
    }

    public static string f_open_excel(ref Application app, ref  Workbook workbook, string file_na)
    {
        if (app == null)
            app = new Application();
        if (app == null)
            return "create excel application failed";
        object oMissiong = System.Reflection.Missing.Value;
        try
        {
            workbook =  app.Workbooks.Open(file_na, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                    oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);
        }
        catch (Exception e)
        {
            f_end(app, workbook);
            return e.StackTrace;
        }
        finally
        {
        }
        return "";
    }

    //使用方法
    //Microsoft.Office.Interop.Excel.Workbook workbook = null;
    //Microsoft.Office.Interop.Excel.Application app = null;
    //du_excel.f_new_excel(ref app, ref workbook);

    public static string f_new_excel(ref Application app, ref  Workbook workbook)
    {
        app = new Application();
        if (app == null)
            return "create excel application failed";
        object oMissiong = System.Reflection.Missing.Value;
        try
        {
            app.Application.Workbooks.Add(true);
            workbook = (Workbook)app.ActiveWorkbook;
        }
        catch (Exception e)
        {
            f_end(app, workbook);
            return e.StackTrace;
        }
        finally
        {
        }
        return "";
    }

    public static void f_cpy_sheet(string ps_template, Application app, Workbook workbook_tar,string sheet_tar_na)
    {
        //打开模板文件
        Workbook workbook_src = app.Workbooks.Open(ps_template, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        Worksheet worksheet_src = (Worksheet)workbook_src.Sheets[1];

        //拷贝到目标文件
        worksheet_src.Name = sheet_tar_na;
        worksheet_src.Copy(workbook_tar.Sheets[1], Type.Missing);
        
        f_end(workbook_src);
    }

    public static void f_end_wkbook(Workbook workbook)
    {
        int generation1;
        object oMissiong = System.Reflection.Missing.Value;

        generation1 = System.GC.GetGeneration(workbook);
        GC.Collect(generation1);
        workbook.Close(false, oMissiong, oMissiong);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        generation1 = System.GC.GetGeneration(workbook);
        GC.Collect(generation1);
        workbook = null;
    }

    public static void f_end(Application app, Workbook workbook)
    {
        int generation1;
        object oMissiong = System.Reflection.Missing.Value;

        generation1 = System.GC.GetGeneration(workbook);
        GC.Collect(generation1);
        workbook.Close(false, oMissiong, oMissiong);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        generation1 = System.GC.GetGeneration(workbook);
        GC.Collect(generation1);
        workbook = null;
        
        //app.Workbooks.Close();
        app.Quit();
        generation1 = System.GC.GetGeneration(app);
        GC.Collect(generation1);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
        generation1 = System.GC.GetGeneration(app);
        GC.Collect(generation1);
        app = null;
        GC.Collect();     
    }

    public static void f_end(Workbook workbook)
    {
        object oMissiong = System.Reflection.Missing.Value;
        workbook.Close(false, oMissiong, oMissiong);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        workbook = null;
        //app.Workbooks.Close();
        GC.Collect();
    }

    //public static bool f_open_excel(System.Web.UI.Page page, ref Application app, ref  _Workbook workBook, string template)
    //{
    //    GC.Collect();
    //    //Create a New Excel Application
    //    //SQLHelper.ExecuteNonQuery("insert into t_debug values('new Application();1')");
    //    try
    //    {
    //        app = new ApplicationClass();
    //        //new Application();
    //    }
    //    catch (Exception e)
    //    {
    //        //SQLHelper.ExecuteNonQuery("insert into t_debug values('1234" + e.ToString() + "')");
    //        return false;
        
    //    }
    //    //SQLHelper.ExecuteNonQuery("insert into t_debug values('anew Application();2')");

    //    if (app == null)
    //    {
    //        return false;
    //    }
    //    app.Visible = false;
    //    app.UserControl = true;
    //    //Create a New Excel File on the Template
    //    try
    //    {
    //        SQLHelper.ExecuteNonQuery("insert into t_debug values('app.Workbooks.Open1')");
    //        workBook = app.Workbooks.Open(template, Missing.Value, Missing.Value,
    //        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
    //        Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
    //        workBook.RefreshAll();
    //        SQLHelper.ExecuteNonQuery("insert into t_debug values('app.Workbooks.Open2')");
    //    }
    //    catch (Exception e)
    //    {
    //        app.Quit();
    //        GC.Collect();
    //        throw e;
    //    }
    //    return true;
    //}
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// 
    /// begin du
    /// </summary>
    /// <param name="loadlist_url"></param>
    /// 


    //获取sheet名
    //public static string f_sheet_name_2_ddlb(string FilePath,  DropDownList drp)
    //{

    //    //登陆用户名+日期时间作为文件名,避免文件名冲突
        
    //    //SQLHelper.ExecuteNonQuery("update UploadFile set up_file='" + UploadFileName + "' where username='" + username + "'");

        

    //    //文件改名
    //    File.Move(FilePath, path);

    //    //读取Excel中的所有sheet名,并加载到dropdownlist中
    //    string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + path + "';Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
    //    OleDbConnection conn = new OleDbConnection(strConn);
    //    conn.Open();
    //    System.Data.DataTable sheetNames = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
    //    //System.Data.DataTable sheetNames = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
    //    conn.Close();
    //    if (sheetNames != null && sheetNames.Rows.Count > 0)
    //    {
    //        drp.Items.Clear();
    //        string tname = " ";
    //        foreach (DataRow row in sheetNames.Rows)
    //        {
    //            tname = row["TABLE_NAME"].ToString().Trim();
    //            tname = tname.Replace("$", "");
    //            if (tname.Substring(tname.Length - 1).CompareTo("_") != 0)
    //            {
    //                if (tname.Substring(tname.Length - 1).CompareTo("'") == 0 && tname.Substring(0, 1).CompareTo("'") == 0)
    //                {
    //                    tname = tname.Substring(1, tname.Length - 2);
    //                }
    //                drp.Items.Add(tname.Trim());
    //            }
    //        }

    //    }
    //}

    //

    public static void f_read_excel_cell_by_cell(string loadlist_url)
    {
        Application oExcel = new Application();
        Workbooks oBooks;
        Workbook oBook;
        Sheets oSheets;
        Worksheet oSheet;
        Range oCells;
        Range range1;
        String url = AppDomain.CurrentDomain.BaseDirectory;
        string sFile = "", sTemplate = "";

        sTemplate = url + "LoadingList.xls";
        //
        oExcel.Visible = false;
        oExcel.DisplayAlerts = false;
        //定义一个新的工作簿
        oBooks = oExcel.Workbooks;
        oBooks.Open(sTemplate, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        oBook = oBooks.get_Item(1);
        oSheets = oBook.Worksheets;
        oSheet = (Worksheet)oSheets.get_Item(1);
        ////命名该sheet
        //oSheet.Name = "HUList";

        oCells = oSheet.Cells;

        //填充数据  从datatable中填入excel文件中
        //for (int m = 0; m < 100; m++)
        //{
        //    for (int n = 0; n < 100; n++)
        //    {
               string ls= oCells.Cells[21, 21].ToString() ;
        //    }
        //}

        
        oBook.Close(false, Type.Missing, Type.Missing);
        //退出Excel，并且释放调用的COM资源
        oExcel.Quit();
        GC.Collect();
    }


    //如何使用
    //注意：cell的定位，是从1开始的
    public static void f_using_excel()
    {
        #region 模式1_填充一个excel的sheet
        //Workbook workbook = null;
        //Worksheet worksheet;
        //Application app = null;
        //string ls_ret;
        //app = new Application();
        //string ls_template = Server.MapPath("~/template/") + "Production Output.xlsx";
        //ls_ret = du_excel.f_open_excel(ref app, ref workbook, ls_template);
        //if (ls_ret.Length > 0)
        //{
        //    du_excel.f_end(app, workbook);
        //    JScript.Alert(ls_ret);
        //    return;
        //}
        
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    worksheet = (Worksheet)workbook.Sheets[1];
        //    ((Range)worksheet.Cells[3, 4]).set_Value(Type.Missing, dt.Rows[i]["ename"].ToString());
        //}

        //string ls_tar = Server.MapPath("~/App_Data/") + "Production Output" + 
        //DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "") + ".xlsx";
        //workbook.SaveAs(ls_tar);
        //du_excel.f_end(app, workbook);
        #endregion

        #region 模式2_先完成一个sheet数据的改造，然后拷贝到另一个excel中
        //    Workbook workbook_src = null, workbook_tar = null;
    //    Application app = null;
    //    string ls_no;
    //    app = new Application();
    //    string ls_template = Server.MapPath("~/template/") + "Employee work time.xlsx";

    //    //目标文件        不知道为什么，一定要先目标文件，才能模板文件，先后次序不能弄错
    //    string ls_tar = Server.MapPath("~/App_Data/") + Session["user_login"].ToString() + DateTime.Now.ToString("yyMMddHHmmss") + ".xlsx";
    //    string ls_ret = du_excel.f_new_excel(ref app, ref workbook_tar);
    //    if (ls_ret.Length > 0)
    //    {
    //        du_excel.f_end(app, workbook_tar);
    //        JScript.Alert(ls_ret);
    //        return;
    //    }

    //    //模板文件
    //     ls_ret = du_excel.f_open_excel(ref app, ref workbook_src, ls_template);
    //    if (ls_ret.Length > 0)
    //    {
    //        du_excel.f_end(app, workbook_src);
    //        JScript.Alert(ls_ret);
    //        return;
    //    }
        
    //    Worksheet worksheet_src ;

    //    //拷贝到目标文件
        
    //    for (int i = 0; i <GridView1.Rows.Count; i++)
    //    {
    //        worksheet_src = (Worksheet)workbook_src.Sheets[1];
    //        ls_no =GridView1.Rows[i].Cells[0].Text;
    //        worksheet_src.Name = ls_no;
    //        ((Range)worksheet_src.Cells[3, 4]).set_Value(Type.Missing, dt.Rows[i]["ename"].ToString());
    //        ((Range)worksheet_src.Cells[3, 11]).set_Value(Type.Missing, dt.Rows[i]["the_id"].ToString());
    //        ((Range)worksheet_src.Cells[5, 2]).set_Value(Type.Missing, dt.Rows[i]["dept"].ToString());
    //        worksheet_src.Copy(workbook_tar.Sheets[1], Type.Missing);
    //        worksheet_src = null;
    //    }

    //    workbook_tar.SaveAs(ls_tar);
    //    du_excel.f_end_wkbook(workbook_tar);
    //    du_excel.f_end(app, workbook_src);
#endregion

        #region 改变格式
        ////合并
        //worksheet.get_Range("B" + li_cur_row, "E" + (li_cur_row)).Merge(worksheet.get_Range("B" + li_cur_row, "E" + (li_cur_row)).MergeCells);
        //((Range)worksheet.Cells[li_cur_row, 2]).set_Value(Type.Missing, "Summary");
        ////居中
        //((Range)worksheet.Cells[li_cur_row, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
        ////背景色
        //((Range)worksheet.Cells[li_cur_row, 2]).Interior.ColorIndex = 39;
        //((Range)worksheet.Cells[li_cur_row, 6]).set_Value(Type.Missing, li_plan_sum.ToString());
        //((Range)worksheet.Cells[li_cur_row, 7]).set_Value(Type.Missing, li_out_sum.ToString());
        //((Range)worksheet.Cells[li_cur_row, 8]).set_Value(Type.Missing, li_bottom_sum.ToString());
        ////合并family
        //worksheet.get_Range("A" + li_begin_fam, "A" + (li_cur_row)).Merge(worksheet.get_Range("A" + li_begin_fam, "A" + (li_cur_row)).MergeCells);

        ////设置边框线
        //worksheet.get_Range("A1", "I" + (li_cur_row)).Borders.LineStyle = 1;
        #endregion
    }
}



/*
/// <summary>
     /// 功能说明：套用模板输出Excel，并对数据进行分页
     /// </summary>
        /// <summary>
        /// 构造函数，需指定模板文件和输出文件完整路径
        /// </summary>
        /// <param name="templetFilePath">Excel模板文件路径</param>
        /// <param name="outputFilePath">输出Excel文件路径</param>
    public void ExcelHelper(string templetFilePath, string outputFilePath)
    {
        if (templetFilePath == null)
            throw new Exception("Excel模板文件路径不能为空！");

        if (outputFilePath == null)
            throw new Exception("输出Excel文件路径不能为空！");

        if (!File.Exists(templetFilePath))
            throw new Exception("指定路径的Excel模板文件不存在！");

        this.templetFile = templetFilePath;
        this.outputFile = outputFilePath;

    }

    //public void DataTableToExcel(System.Data.DataTable dt, int rows, int top, int left, string sheetPrefixName)
    public void DataTableToExcel(System.Data.DataTable dt, int top, int left, string sheetPrefixName)
    {
        int rowCount = dt.Rows.Count;        //源DataTable行数
        int colCount = dt.Columns.Count;    //源DataTable列数
        DateTime beforeTime;
        DateTime afterTime;

        if (sheetPrefixName == null || sheetPrefixName.Trim() == "")
            sheetPrefixName = "Sheet";

        beforeTime = DateTime.Now;
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = true;
        afterTime = DateTime.Now;

        //打开模板文件，得到WorkBook对象
        Excel.Workbook workBook = app.Workbooks.Open(templetFile, missing, missing, missing, missing, missing,
                             missing, missing, missing, missing, missing, missing, missing, missing, missing);

        //得到WorkSheet对象
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        //将dt中的数据写入WorkSheet
        for (int j = 0; j < rowCount; j++)
        {
            for (int k = 0; k < colCount; k++)
            {
                workSheet.Cells[top + j, left + k] = dt.Rows[j][k].ToString();
            }
        }
        //输出Excel文件并退出
        try
        {
            workBook.SaveAs(outputFile, missing, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlExclusive, missing, missing, missing, missing, missing);

            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
            string path = Server.MapPath(DateTime.Now.ToString("yyyyMMddhhmm") + "-Sum.xls");

            System.IO.FileInfo file = new System.IO.FileInfo(path);
            Response.Clear();
            Response.Charset = "gb2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
            Response.AddHeader("content-disposition", "attachment; filename=" + Server.UrlDecode(file.Name));
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度
            Response.AddHeader("content-length", file.Length.ToString());

            // 指定返回的是一个不能被客户端读取的流，必须被下载
            Response.ContentType = "application/ms-excel";

            // 把文件流发送到客户端
            Response.WriteFile(file.FullName);
            // 停止页面的执行

            Response.End();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            Process[] myProcesses;
            DateTime startTime;
            myProcesses = Process.GetProcessesByName("Excel");

            //得不到Excel进程ID，暂时只能判断进程启动时间
            foreach (Process myProcess in myProcesses)
            {
                startTime = myProcess.StartTime;

                if (startTime > beforeTime && startTime < afterTime)
                {
                    myProcess.Kill();
                }
            }
        }
    }

    protected void BtnToExcelTemp_Click(object sender, EventArgs e)
    {
        //BLL.Common.BLLReport.ExcelHelp("D:\\Excel\\SumModel.xls", "D:\\Excel\\Sum.xls");
        //ExcelHelper("D:\\Excel\\SumModel.xls", "D:\\Excel\\" + DateTime.Now.ToShortDateString().ToString()+ "-Sum.xls");
        ExcelHelper(Server.MapPath("SumModel.xls"), Server.MapPath(DateTime.Now.ToString("yyyyMMddhhmm") + "-Sum.xls"));
        DataSet ds = new DataSet();
        ds = BLLChildren.ChildReportSumOne();
        System.Data.DataTable dt = ds.Tables[0];
        DataTableToExcel(dt, 6, 1, "");
        
    }
*/
