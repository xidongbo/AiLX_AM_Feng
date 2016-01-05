using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
/// <summary>
///Class1 的摘要说明
/// </summary>
public class du_File
{
	public du_File()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static void f_DeleteOverdueFile(string ps_dir)
    {
        
        //获取系统当前时间
        DateTime timenow = System.DateTime.Now;
        TimeSpan timespan;
        //获取App_Code文件夹下的所有文件
        string[] FileCollection = System.IO.Directory.GetFiles(ps_dir);
        for (int i = 0; i < FileCollection.Length; i++)
        {
            DateTime createtime = File.GetCreationTime(FileCollection[i]);
            timespan = timenow - createtime;

            //删除upload文件夹中的过期文件(180之前的文件)
            if (timespan.TotalDays > 3)
            {
                File.Delete(FileCollection[i]);
            }
        }

        //path = Server.MapPath("~/upload");
        //DeleteFile(path);
    }

}