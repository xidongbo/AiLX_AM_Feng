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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Collections;

/// <summary>
/// PageCommon 的摘要说明
/// </summary>
public class PageCommon
{
	public PageCommon()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//ii
	}
    
    public static string returnFileUploadPath(FileUpload fu)
    {
        if (fu.PostedFile.FileName == "")
        {
            return "";
        }
        string path = "~/UpLoadFiles/" + DateTime.Now.ToString("yyyyMMddhhmmss") + fu.FileName;
        fu.PostedFile.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));
        return System.Web.HttpContext.Current.Server.MapPath(path);
    }

    //从控件中得到文件路径
    public static string UploadXls(FileUpload fu,Page page)
    {
        if (!fu.HasFile)
        {
            JScript.AjaxAlert(page, "Please choose excel file first!");
            return "";
        }

        //string path = "~/UpLoadFiles/" + DateTime.Now.ToString("yyyyMMddhhmmss") + fu.FileName;
        string path = "~/UpLoadFiles/" + fu.FileName;

        string fileextend;
        fileextend = fu.FileName.Substring(fu.FileName.LastIndexOf(".") + 1);
        if (fileextend != "xls")
        {
            JScript.AjaxAlert(page, "Sorry,it is not excel file!");
            return "";
        }
        else
        {
            fu.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));
        }

        return System.Web.HttpContext.Current.Server.MapPath(path);
    }

    //从控件中得到文件路径
    public static string UploadTxt(FileUpload fu, Page page)
    {
        if (!fu.HasFile)
        {
            JScript.AjaxAlert(page, "Please choose Text file first!");
            return "";
        }

        //string path = "~/UpLoadFiles/" + DateTime.Now.ToString("yyyyMMddhhmmss") + fu.FileName;
        string path = "~/UpLoadFiles/" + fu.FileName;

        string fileextend;
        fileextend = fu.FileName.Substring(fu.FileName.LastIndexOf(".") + 1);
        if (fileextend.ToLower() != "txt")
        {
            JScript.AjaxAlert(page, "Sorry,it is not Text file!");
            return "";
        }
        else
        {
            fu.SaveAs(System.Web.HttpContext.Current.Server.MapPath(path));
        }

        return System.Web.HttpContext.Current.Server.MapPath(path);
    }


    public static string fileCopy(string oriPath,string desPath)
    {
        string path = System.Web.HttpContext.Current.Server.MapPath("~/Output/" + desPath);
        if (File.Exists(path))
        {           
           File.Delete(path);          
        }
        File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/Template/"+oriPath),path);
        return path;
    }


    public static void outExcel(string strOutputFile, string out_file_na)
    {

        string strResult = string.Empty;
        string strFile = string.Format(@"{0}\{1}", strOutputFile, out_file_na);

        using (FileStream fs = new FileStream(strOutputFile, FileMode.Open))
        {
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" +
                HttpUtility.UrlEncode(out_file_na, System.Text.Encoding.UTF8));
            System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
    }

    //public static void outExcel(string strOutputFile)
    //{
    //    try
    //    {
    //        FileInfo file = new FileInfo(strOutputFile);
    //        if (file.Exists)
    //        {
    //            string attachment = "attachment; filename=" + file.Name;               
    //            System.Web.HttpContext.Current.Response.Buffer = true;
    //            System.Web.HttpContext.Current.Response.Clear();
    //            System.Web.HttpContext.Current.Response.ClearHeaders();
    //            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    //            System.Web.HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

    //            System.Web.HttpContext.Current.Response.WriteFile(strOutputFile);
    //            System.Web.HttpContext.Current.Response.Flush();
    //            file.Delete();
    //            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    public static void outWord(string strOutputFile)
    {
        try
        {
            FileInfo file = new FileInfo(strOutputFile);
            if (file.Exists)
            {
                string attachment = "attachment; filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8);
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", file.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                System.Web.HttpContext.Current.Response.WriteFile(strOutputFile);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
                file.Delete();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void outPDF(string strOutputFile)
    {
        try
        {
            FileInfo file = new FileInfo(strOutputFile);
            if (file.Exists)
            {
                string attachment = "attachment; filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8);
                System.Web.HttpContext.Current.Response.Buffer = true;
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ClearHeaders();
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
                System.Web.HttpContext.Current.Response.AppendHeader("Content-Length", file.Length.ToString());
                System.Web.HttpContext.Current.Response.ContentType = "application/PDF";
                System.Web.HttpContext.Current.Response.WriteFile(strOutputFile);
                System.Web.HttpContext.Current.Response.Flush();
                System.Web.HttpContext.Current.Response.End();
                file.Delete();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
        
    public static void ClearPageAllTextBox(ControlCollection objControlCollection)
    { 
       foreach(System.Web.UI.Control objControl in objControlCollection)
       {
           if (objControl.HasControls())
           {
               ClearPageAllTextBox(objControl.Controls);
           }
           else
           {
               if (objControl is System.Web.UI.WebControls.TextBox)
               {
                   ((TextBox)objControl).Text = string.Empty;
               }
               if (objControl is System.Web.UI.WebControls.DropDownList)
               {
                   ((DropDownList)objControl).Text = "All";
               }
           }
       }
    }
    public static string[,] listTranverse(List<List<string>> excelValue,int type)
    {
        if (type == 0)
        {
            //By Week
            string[,] Value = new string[excelValue[0].Count, excelValue.Count];
            for (int i = 0; i < excelValue.Count; i++)
            {
                for (int j = 0; j < excelValue[0].Count; j++)
                {
                    Value[j, i] = excelValue[i][j];
                }
            }
            return Value;
        }
        else
        { 
           //By Customer
            string[,] Value = new string[excelValue.Count, excelValue[0].Count];
            for (int i = 0; i < excelValue.Count; i++)
            {
                for (int j = 0; j < excelValue[0].Count; j++)
                {
                    Value[i, j] = excelValue[i][j];
                }
            }
            return Value;
        }
    }
    public static string[,] dtToArray(DataTable dt)
    {
        string[,] desData = new string[dt.Rows.Count, dt.Columns.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
            for (int j = 0; j < dt.Columns.Count;j++ )
                desData[i,j] = dt.Rows[i][j].ToString();
                return desData;
    }
    public static string[,] dtToArray(DataTable dt,ArrayList ar)
    {
        string[,] desData = new string[dt.Rows.Count + 1, dt.Columns.Count];
        for (int i = 0; i < dt.Rows.Count+1; i++)
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (i == 0)
                {
                    desData[i, j] = ar[j].ToString();
                }
                else
                {
                    desData[i, j] = dt.Rows[i - 1][j].ToString();
                }
            }
        return desData;
    }
    static public bool SendMailTo(string sendTo,string mailSubject,string mailContent)//,string attachmentPath)
    { 
        string userName="dull_tiger";
        string userPWD = "duqingwei";
        string sendFrom = "dull_tiger@163.com";

        MailMessage msg = new System.Net.Mail.MailMessage();       
        msg.To.Add(sendTo); //收件人

        //发件人信息
        msg.From = new MailAddress(sendFrom, userName, System.Text.Encoding.UTF8);
        msg.Subject = mailSubject;   //邮件标题
        msg.SubjectEncoding = System.Text.Encoding.UTF8;    //标题编码
        msg.Body = mailContent; //邮件主体
        msg.BodyEncoding = System.Text.Encoding.UTF8;
        msg.IsBodyHtml = true;  //是否HTML
        msg.Priority = MailPriority.High;   //优先级
     //   msg.Attachments.Add(new Attachment(attachmentPath)); //添加一个附件

        SmtpClient client = new SmtpClient();
        //设置GMail邮箱和密码 
        client.Credentials = new System.Net.NetworkCredential(userName, userPWD);
        //client.Port = 25;
        client.Host = "smtp.163.com";  //smtp.eapac.ericsson.se   25 eapac/
        client.EnableSsl = false;
        object userState = msg;
        try
        {
            client.Send(msg);
            client.Dispose();
            msg.Dispose();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static string OpenOutLook(string strBody)
    {
        string emailString = string.Empty;
        StringBuilder sbEmail = new StringBuilder();
        sbEmail.Append("mailto:");
        sbEmail.Append(HttpUtility.UrlEncode(" ", System.Text.Encoding.Default).Replace("+", "%20"));
        sbEmail.Append("?body=");
        string body = strBody;
        sbEmail.Append(HttpUtility.UrlEncode(body, System.Text.Encoding.Default));
        emailString = sbEmail.ToString();
        return emailString;
    }  
}
