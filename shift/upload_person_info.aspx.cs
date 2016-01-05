using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Collections;

public partial class shift_upload_person_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    //改excel文件的名字
    //如果返回值为空，则正常，否则为提示的错误
    protected string f_get_file_na()
    {
        string ls_path = "";
        if (!FileUpload1.HasFile)//如果没有文件
        {
            return "Please choose a excel.";
        }
        else
        {
            string fileextend;
            fileextend = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf(".") + 1);

            if (fileextend != "xls" && fileextend != "xlsx")
            {
                return "The type of upload file is not excel.";
            }
            else
            {
                ls_path = Server.MapPath("~/App_Data/") + DateTime.Now.ToString("yyMMddHHmmss") + "." + fileextend;
                FileUpload1.SaveAs(ls_path);
                file_name.Text = ls_path;
            }
        }
        return "";
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string ls_err = f_get_file_na();
        if (ls_err.Length > 0)//如果没有文件或格式不正确
        {
            JScript.Alert(ls_err);
            return;
        }


        ls_err = f_read_per();

        if (ls_err.Length > 0)//出错
        {
            JScript.Alert(ls_err);
        }
        else
        {
            JScript.Alert("Upload ok!");
           
        }
    }
    //读excel，将sheet1插入数据库
    //如果返回值为空，则正常，否则为提示的错误
    private string f_read_per()
    {
        int li_row = 2;

        Application app = null;
        Workbook workbook = null;

        string open_err= du_excel.f_open_excel(ref app, ref workbook, file_name.Text);

        if (open_err.Length > 0)//打开excel失败
            return open_err;

        try
        {
            //将数据读入到DataTable中
            Worksheet worksheet = (Worksheet)workbook.Worksheets.get_Item(1);//读取第一张表  
            if (worksheet == null)
            {
                return "get excel sheet failed";
            }

            //开始读取
            string work_number = ((Range)worksheet.Cells[li_row, 1]).Text.ToString();
            string name,position,dept,sql;
            while (work_number.Length > 0)
            {
                name = ((Range)worksheet.Cells[li_row, 2]).Text.ToString();
                position = ((Range)worksheet.Cells[li_row, 3]).Text.ToString();
                dept = ((Range)worksheet.Cells[li_row, 4]).Text.ToString();

                sql= "insert into t_person (c_work_number,c_name,c_position,c_dept)" +
                     "values('" + work_number + "','" + name + "','" + position + "','" + dept + "')";

                SQLHelper.ExecuteNonQuery(sql);

                li_row++;
                work_number = ((Range)worksheet.Cells[li_row, 1]).Text.ToString();
            }
        }
        catch (Exception e)
        {
            du_excel.f_end(app, workbook);
            return e.StackTrace;
        }
        finally
        {
        }
        du_excel.f_end(app, workbook);
        return "";
    }

  
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("person_info.aspx");
    }
}
