using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ZSTUZCGLC.Models
{
    public class ArticleViewModel
    {
        //简略信息
        public ArticleViewModel(int id, string title, DateTime edittime, string username, string categoryname)
        {
            this.id = id;
            this.title = title;
            this.edittime = edittime;
            this.username = username;
            this.categoryname = categoryname;
        }
        public int id { set; get; }
        public string title { set; get; }
        public string content { set; get; }
        public DateTime edittime { set; get; }
        public string username { set; get; }
        public int category_id { set; get; }
        public string categoryname { set; get; }
    }
    public class EditViewModel
    {        
        public EditViewModel(int id, string title, string content, DateTime edittime, int user_id, string category_id)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.edittime = edittime;
            this.user_id = user_id;
            this.category_id = category_id;
        }
        public EditViewModel()
        {

        }

        public int id { set; get; }

        [Required]
        [Display(Name ="标题")]
        public string title { set; get; }

        [Required]
        [Display(Name = "正文")]
        [AllowHtml]
        public string content { set; get; }

        public DateTime edittime { set; get; }
        public int user_id { set; get; }
        public string username { set; get; }
        public string category_id { set; get; }
        public string categoryname { set; get; }
        public DataTable category { set; get; }
        public string url { set; get; }
        public string summary { set; get; }
    }
    public class LoginViewModel
    {
        public LoginViewModel() { }
        public LoginViewModel(int id,string userid,string password,string username)
        {
            this.id = id;
            this.userid = userid;
            this.password = password;
            this.username = username;
        }
        [Required]
        [Display(Name = "用户名")]
        public string userid { set; get; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string password { set; get; }

        public int id { set; get; }
        public string username { set; get; }
    }
    public class HomeViewModel
    {
        public List<BannerImgModel> banner{ set; get; }
        public List<ArticleListModel> article { set; get; }
    }
    public class ArticleListModel
    {
        public ArticleListModel(int id,string title)
        {
            this.id = id;
            this.title = title;
        }
        public int id { set; get; }
        public string title { set; get; }
    }
}