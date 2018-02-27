using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSTUZCGLC.Models;
using System.Data;
using ZSTUZCGLC.Common;
namespace ZSTUZCGLC.DAL.DAO
{
    public class ArticleDAO:SqlDAO
    {
        /// <summary>
        /// 获取文章id和title列表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<ArticleListModel> GetArticleList()
        {
            DataRow[] dr = ExecuteReader("SELECT article.id, article.title FROM article ORDER BY article.edittime DESC; ");
            List<ArticleListModel> la = new List<ArticleListModel>();
            foreach (DataRow dr1 in dr)
            {
                la.Add(GetArticleList(dr1));
            }
            return la;
        }
        /// <summary>
        /// 根据文章Id获取内容
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        public EditViewModel GetArticle(int id)
        {
            DataRow[] dr = ExecuteReader("Select * from article where article.id=" + id.ToString());
            return GetArticle(dr[0]);
        }
        public DetailViewModel GetArticleView(int id)
        {
            DataRow[] dr = ExecuteReader("Select * from article where article.id=" + id.ToString());
            return GetDetailView(dr[0]);
        }
       public List<ThemeViewModel> GetBriefArticlebyCID(int cid)
        {
            DataRow[] dr = ExecuteReader("SELECT article.id, article.title, article.summary, article.edittime FROM article WHERE (((article.category_id)="+cid + ")) ORDER BY article.id DESC");
            List<ThemeViewModel> tv = new List<ThemeViewModel>();
            foreach (DataRow dr1 in dr)
            {
                tv.Add(GetThemeView(dr1));
            }
            return tv;
        }
        public List<ThemeViewModel> GetBriefArticlebyCID(int cid,int x)
        {
            DataRow[] dr = ExecuteReader("SELECT TOP " + x + " article.id,article.title, article.edittime, article.summary FROM article WHERE(((article.category_id) =" + cid +
                "))ORDER BY article.edittime DESC;"); List<ThemeViewModel> tv = new List<ThemeViewModel>();
            foreach (DataRow dr1 in dr)
            {
                tv.Add(GetThemeView(dr1));
            }
            return tv;
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <returns></returns>
        public List<ArticleViewModel> GetBreifArticleList()
        {
            DataRow[] dr = ExecuteProcedure("BreifArticleList");
            List<ArticleViewModel> la = new List<ArticleViewModel>();
            foreach (DataRow dr1 in dr)
            {
                la.Add(GetBreifArticle(dr1));
            }
            return la;
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns></returns>
        public int DeleteArticle(string id)
        {
            return ExecuteNonQuery("Delete * from article where article.id=" + id);
        }
        /// <summary>
        /// 增加文章
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public int AddArticle(EditViewModel ev)
        {
            object[] s ={ev.title, HttpUtility.HtmlEncode(ev.content),ev.edittime,ev.user_id,ev.category_id,ev.summary};
            string sql= "INSERT INTO article (title, content, edittime, user_id, category_id,summary)"+
                string.Format("values(\"{0}\",\"{1}\",#{2}#,{3},{4},\"{5}\")", s);
            return ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="ev"></param>
        /// <returns></returns>
        public int EditArticle(EditViewModel ev)
        {
            object[] s = { ev.title, HttpUtility.HtmlEncode(ev.content), ev.edittime, ev.user_id, ev.category_id,ev.summary,ev.id};
            string sql = string.Format("UPDATE article SET article.title = \"{0} \", article.content = \"{1}\",article.edittime=#{2}#," +
                "article.user_id={3},article.category_id={4}, article.summary = \"{5}\" WHERE(((article.id) = {6}))", s);
            return ExecuteNonQuery(sql);
        }
        /// <summary>
        /// 获取非列表分类的第一篇文章id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetNoneListThemeArticleId(int id)
        {
            string sql = "SELECT Top 1 article.id FROM article WHERE(((article.category_id) =" + id + ")) ORDER BY article.id DESC";
            DataRow[] dr = ExecuteReader(sql);
            if (dr.Length == 1)
            {
                return GetInt32(dr[0]["id"]);
            }
            else
            {
                return 0;
            }
        }
        internal static EditViewModel GetArticle(DataRow dr)
        {
            EditViewModel ev = new EditViewModel(GetInt32(dr["id"]), GetString(dr["title"]), HttpUtility.HtmlDecode(GetString(dr["content"])),
                GetDateTime(dr["edittime"]), GetInt32(dr["user_id"]), GetString(dr["category_id"]));
            return ev;
        }
        internal static ArticleViewModel GetBreifArticle(DataRow dr)
        {
            ArticleViewModel am = new ArticleViewModel(GetInt32(dr["id"]), GetString(dr["title"]),GetDateTime(dr["edittime"]),
                GetString(dr["username"]), util.GetFullCategoryPath(GetString(dr["categoryname"]), GetInt32(dr["category_id"])));
            return am;
        }
        internal static DetailViewModel GetDetailView(DataRow dr)
        {
            DetailViewModel dv = new DetailViewModel(GetString(dr["title"]), GetDateTime(dr["edittime"]), HttpUtility.HtmlDecode(GetString(dr["content"])),GetInt32(dr["category_id"]));
            return dv;
        }
        internal static ThemeViewModel GetThemeView(DataRow dr)
        {
           ThemeViewModel tv = new ThemeViewModel(GetInt32(dr["id"]), GetDateTime(dr["edittime"]), GetString(dr["title"]),GetString(dr["summary"]));
            return tv;
        }
        internal static ArticleListModel GetArticleList(DataRow dr)
        {
            ArticleListModel al = new ArticleListModel(GetInt32(dr["id"]), GetString(dr["title"]));
            return al;
        }
        //internal static IndexViewModel GetIndexView(DataRow dr)
        //{
        //    IndexViewModel iv = new IndexViewModel(GetInt32(dr["id"]), GetDateTime(dr["edittime"]), GetString(dr["title"]),GetInt32(dr["category_id"]),GetString(dr["summary"]));
        //    return iv;
        //}
    }
}