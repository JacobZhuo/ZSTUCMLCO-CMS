using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Web;
using System.Web.Configuration;

namespace ZSTUZCGLC.DAL.DAO
{
    public class SqlDAO
    {
        public static readonly string connStr = WebConfigurationManager.ConnectionStrings["AccessConnectionString"].ConnectionString + HttpContext.Current.Server.MapPath(WebConfigurationManager.ConnectionStrings["Access_Path"].ConnectionString);
        private OleDbConnection conn = new OleDbConnection(connStr); 
               
        protected DataRow[] ExecuteReader(string sql)
        {
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow[] dr = dt.Select();
            conn.Close();
            return dr;
        }
        protected DataTable ExecuteReaderForTable(string sql)
        {
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];            
            conn.Close();
            return dt;
        }
        protected int ExecuteNonQuery(string sql)
        {
            conn.Open();
            OleDbCommand dc = new OleDbCommand();
            dc.Connection = conn;
            dc.CommandText = sql;
            int result = dc.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        /// <summary>
        /// 调用access查询
        /// </summary>
        /// <param name="Procedure">查询表名</param>
        /// <returns></returns>
        protected DataRow[] ExecuteProcedure(string Procedure)
        {
            conn.Open();
            OleDbCommand dc = new OleDbCommand();
            OleDbDataAdapter da = new OleDbDataAdapter();
            dc.Connection = conn;
            dc.CommandText = Procedure;
            dc.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = dc;
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataTable dt = ds.Tables[0];
            DataRow[] dr = dt.Select();
            return dr;
        }
        /// <summary>
        /// 获取string数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected static string GetString(object data)
        {
            if (data == DBNull.Value)
                return "";
            else
                return data.ToString();
        }
        /// <summary>
        /// 获取整型数据
        /// </summary>
        /// <param name="data">数据</param>
        protected static int GetInt32(object data)
        {
            if (data == DBNull.Value)
                return 0;
            else
                return Convert.ToInt32(data);
        }
        protected static DateTime GetDateTime(object data)
        {
            if (data == DBNull.Value)
                return DateTime.Now;
            else
                return Convert.ToDateTime(data);
        }
        protected void ConnClose()
        {
            conn.Close();
        }
    }
}