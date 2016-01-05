using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// SQLHelper 的摘要说明
/// </summary>
public class SQLHelper
{
    public SQLHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public static readonly string connectionString = ConfigurationManager.ConnectionStrings["SQLCONN"].ConnectionString;
    /// <summary>
    /// 返回Command对象的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="parames">参数列表</param>
    /// <returns>SqlCommand对象</returns>
    public static void f_debug(string ps_parm)
    {
        ExecuteNonQuery("insert into t_debug1(c_str) values('" + ps_parm + "')");
    }

    private static SqlCommand GetCommand(string sql, params SqlParameter[] parames)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(sql, connection);
        cmd.CommandTimeout = 1200;
        //加入参数列表
        if (parames != null)
            cmd.Parameters.AddRange(parames);
        //返回对象
        return cmd;
    }
    /// <summary>
    /// 返回读取器的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="parames">参数类表</param>
    /// <returns>SqlDataReader读取器</returns>
    public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parames)
    {
        SqlCommand cmd = GetCommand(sql, parames);
        cmd.CommandTimeout = 1200;
        SqlDataReader reader = null;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return reader;
    }

    public static string col_type(string ls_table, string ls_col)
    {
        string ls_type = "";
        System.Data.DataTable ldt = SQLHelper.GetDataTable("SELECT ST.name AS ColType, C.Length" +
            " FROM dbo.sysobjects T " +
            " LEFT JOIN dbo.syscolumns C ON T.id=C.id " +
            " LEFT JOIN dbo.systypes ST ON C.xtype=ST.xusertype " +
            " WHERE T.xtype='U' and t.name='" + ls_table + "' and C.name='" + ls_col + "'" +
            " ORDER BY T.name,C.colid;");

        if (ldt.Rows.Count > 0)
        {
            ls_type = ldt.Rows[0]["ColType"].ToString().ToLower();
            if (ls_type == "varchar" || ls_type == "char" || ls_type == "nvarchar")
                ls_type = "str";
            else if (ls_type == "bigint" || ls_type == "int" || ls_type == "smallint" || ls_type == "decimal" || ls_type == "numeric"
                || ls_type == "float" || ls_type == "real")
                ls_type = "num";
            else if (ls_type == "datetime")
                ls_type = "date";
        }
        return ls_type;
    }

    /// <summary>
    /// 增,删,改的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="parames">参数类表</param>
    /// <returns>int数字</returns>
    public static int ExecuteNonQuery(string sql, params SqlParameter[] parames)
    {
        SqlCommand cmd = GetCommand(sql, parames);
        cmd.CommandTimeout = 1200;
        int result = 0;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }
        return result;
    }
    /// <summary>
    /// 无参数增,删,改的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>      
    /// <returns>int数字</returns>
    public static int ExecuteNonQuery(string sql)
    {
        SqlCommand cmd = GetCommand(sql, null);
        cmd.CommandTimeout = 1200;
        int result = 0;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }
        return result;
    }
    /// <summary>
    /// 用于执行事务方法
    /// </summary>
    /// <param name="trans"></param> 
    /// <param name="sql"></param>
    /// <param name="parames"></param>
    /// <returns></returns>
    public static int ExecuteNonQuery(SqlTransaction trans, SqlConnection con, string sql, params SqlParameter[] parames)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandTimeout = 1200;
        cmd.Transaction = trans;
        cmd.Connection = con;
        cmd.CommandText = sql;
        if (parames != null)
            cmd.Parameters.AddRange(parames);
        int result = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return result;

    }
    /// <summary>
    /// 返回一行一列的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="parames">参数类表</param>
    /// <returns>object对象</returns>
    public static object ExecuteScalar(string sql, params SqlParameter[] parames)
    {
        SqlCommand cmd = GetCommand(sql, parames);
        cmd.CommandTimeout = 1200;
        object result;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }
        return result;
    }

    /// <summary>
    /// 返回dataset对象的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="tableName">待填充的datatable表</param>
    /// <param name="parames">参数列表</param>
    /// <returns>DataSet对象</returns>
    public static DataSet GetDataSet(string sql, string tableName, params SqlParameter[] parames)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter dapter = new SqlDataAdapter();
        dapter.SelectCommand = GetCommand(sql, parames);
        try
        {
            dapter.Fill(ds, tableName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
   
    /// <summary>
    /// 返回dataset对象的方法
    /// </summary>
    /// <returns>DataSet对象</returns>
    public static DataSet GetDataSet(string sql)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter dapter = new SqlDataAdapter();
        dapter.SelectCommand = GetCommand(sql);
        try
        {
            dapter.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }


    /// <summary>
    /// 返回DataTable对象的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="tableName">待填充的datatable表</param>
    /// <param name="parames">参数列表</param>
    /// <returns>DataTable对象</returns>
    public static DataTable GetDataTable(string sql, string tableName, params SqlParameter[] parames)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter dapter = new SqlDataAdapter();
        dapter.SelectCommand = GetCommand(sql, parames);
        try
        {
            dapter.Fill(ds, tableName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds.Tables[0];
    }
    /// <summary>
    /// 重载
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public static DataTable GetDataTable(string sql)
    {

        try
        {
            DataSet ds = new DataSet();
            SqlDataAdapter dapter = new SqlDataAdapter();
            dapter.SelectCommand = GetCommand(sql, null);
            dapter.Fill(ds);
            return ds.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    /// <summary> 
    /// 将DataTable中数据批量数据库表中
    /// <param name="dt">源数据集</param>  
    /// <param name="ExcelTitle">Excel数据题头</param>  
    /// <param name="DBcolumn">要插入数据的表的对应字段</param>   
    /// <param name="DestinationTableName">写入数据的数据库表名</param>  
    /// </summary>  
    public static void SqlBulkCopyData(System.Data.DataTable dt, string[] DataTableTitle, string[] DBcolumn, string DestinationTableName)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLCONN"].ToString());
        //初始化连接字符串  
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        SqlBulkCopy bcp = new SqlBulkCopy(conn);
        //指定目标数据库表名
        bcp.DestinationTableName = DestinationTableName;
        //指定源列和目标列之间的对应关系
        for (int i = 0; i < DataTableTitle.Length; i++)
        {
            bcp.ColumnMappings.Add(DataTableTitle[i], DBcolumn[i]);
        }
        //写入数据库表 
        bcp.WriteToServer(dt);
        bcp.Close();
        conn.Close();
    }


    /// <summary>
    /// 返回数字的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="parames">参数类表</param>
    /// <returns>object对象</returns>
    public static double ReturnDouble(string sql)
    {
        string ls_str;
        SqlCommand cmd = GetCommand(sql, null);
        cmd.CommandTimeout = 1200;
        object result;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value || result.ToString() == string.Empty)
            {
                ls_str = "0";
            }
            else
            {
                ls_str = result.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }
        return Convert.ToDouble(ls_str);
    }

    public static int ReturnInt(string sql)
    {
        string ls_ret;
        SqlCommand cmd = GetCommand(sql, null);
        cmd.CommandTimeout = 1200;
        object result;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value || result.ToString() == string.Empty)
            {
                ls_ret = "0";
            }
            else
            {
                ls_ret = result.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }
        return Convert.ToInt32(ls_ret);
    }


    /// <summary>
    /// 返回字符串的方法
    /// </summary>
    /// <param name="sql">要执行的SQL语句</param>
    /// <param name="parames">参数类表</param>
    /// <returns>object对象</returns>
    public static string ReturnStr(string sql)
    {
        string ls_ret;
        SqlCommand cmd = GetCommand(sql, null);
        cmd.CommandTimeout = 1200;
        object result;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value || result.ToString() == string.Empty)
            {
                ls_ret = "";
            }
            else
            {
                ls_ret = result.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }
        return ls_ret;
    }

    public static DateTime ReturnDate(string sql)
    {
        string ls_ret;
        SqlCommand cmd = GetCommand(sql, null);
        cmd.CommandTimeout = 1200;
        object result;
        try
        {
            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();
            result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value || result.ToString() == string.Empty)
            {
                ls_ret = "1900-1-1";
            }
            else
            {
                ls_ret = result.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
                cmd.Connection.Close();
        }

        return DateTime.Parse(ls_ret);
    }
}
