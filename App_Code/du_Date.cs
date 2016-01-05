using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///Class1 的摘要说明
/// </summary>
public class du_Date
{
    public du_Date()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public static bool IsDate(string strDate)
    {
        try
        {
            DateTime.Parse(strDate);
            return true;
        }
        catch
        {
            return false;
        }
    }

    //获得某一个日期是所在周的第几天
    public static int GetDayofWeek(DateTime datetime)
    {
        //星期天为第一天  

        int weeknow = Convert.ToInt32(datetime.DayOfWeek);
        if (weeknow == 0)
            weeknow = 7;
        
        return weeknow;
    } 

    //获得某一个日期所在周的第一天
    public static DateTime GetWeekFirstDay(DateTime datetime)
    {
        //星期天为第一天  

        int weeknow = Convert.ToInt32(datetime.DayOfWeek) ;
        if (weeknow == 0)
            weeknow = 6;
        else
            weeknow = weeknow - 1;
        int daydiff = (-1) * weeknow;

        //本周第一天  
        string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
        return Convert.ToDateTime(FirstDay);
    }

    //获得今天的日期字符串
    public static string Today_str()
    {
        return Date_2_Str(DateTime.Today);
    }

    //将日期换成只剩下日期部分
    public static string Date_2_Str(DateTime dt)
    {
        string ls_s;
        ls_s = dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString();
        return ls_s;
    }

    //获得本月的第一天
    public static String getFirstDayThisMonth()
    {
        DateTime dt = DateTime.Today;
        int Year, Month;
        Year = dt.Year;
        Month = dt.Month;
        return Year.ToString() + "-" + Month.ToString() + "-1";
    }

    //获得某一月的最后一天
    public static DateTime getMonthLastDay(DateTime dt)
    {
        int Year, Month;
        Year = dt.Year;
        Month = dt.Month;
        int Days = DateTime.DaysInMonth(Year, Month);
        return Convert.ToDateTime(Year.ToString() + "-" + Month.ToString() + "-" + Days.ToString());
    }
}