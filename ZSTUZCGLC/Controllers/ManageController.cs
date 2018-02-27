using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSTUZCGLC.Models;
using ZSTUZCGLC.DAL.DAO;
using ZSTUZCGLC.Common;
using Webdiyer.WebControls.Mvc;
using System.Xml;

namespace ZSTUZCGLC.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        //GET: Manage/Article
        public ActionResult Article(int id=1)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login");
            }
            List<ArticleViewModel> am = new ArticleDAO().GetBreifArticleList();
            PagedList<ArticleViewModel> pam = am.ToPagedList(id, 8);
            return View(pam);
        }

        //POST: Manage/Article

        [HttpPost]
        [MultiButton(Name = "delete", Argument = "pid")]
        public ActionResult Article(string pid,int id=1)
        {
            new ArticleDAO().DeleteArticle(pid);
            PagedList<ArticleViewModel> pam = (new ArticleDAO().GetBreifArticleList()).ToPagedList(id,8);
            return View(pam);
        }

        public ActionResult Edit(int id)
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login");
            }
            if (id == 0)
            {
                EditViewModel ev = new EditViewModel();
                ev.category = new CategoryDAO().GetCategory();
                return View(ev);
            }
            else
            {
                EditViewModel ev = new ArticleDAO().GetArticle(id);
                ev.category = new CategoryDAO().GetCategory();
                return View(ev);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(EditViewModel ev, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(ev);
            }
            if (string.IsNullOrEmpty(ev.category_id))
            {
                ev.category_id = "10";
            }
            if (id == 0)//增加
            {
                ev.edittime = DateTime.Now;
                ev.user_id = Int32.Parse(Session["id"].ToString());
                ev.summary = util.GetContentSummary(ev.content, 30, true);
                new ArticleDAO().AddArticle(ev);
                return RedirectToAction("Article");
            }
            else//删除
            {
                ev.edittime = DateTime.Now;
                ev.user_id = Int32.Parse(Session["id"].ToString());
                ev.summary = util.GetContentSummary(ev.content, 30, true);
                new ArticleDAO().EditArticle(ev);
                return RedirectToAction("Article");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel lv)
        {
            LoginViewModel real = new UserDAO().GetInfoByid(lv.userid);
            if (real.password == lv.password)
            {
                Session["username"] = real.username;
                Session["id"] = real.id;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "无效的登录尝试。");
                return View(lv);
            }
        }

        public ActionResult LogOff()
        {
            Session["id"] = null;
            Session["username"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Home()
        {
            if (Session["id"] == null)
            {
                return RedirectToAction("login");
            }
            HomeViewModel hvm = new HomeViewModel();
            hvm.article = new ArticleDAO().GetArticleList();
            hvm.banner = new CommonDAO().GetBannerbyXML();
            return View(hvm);
        }

        [HttpPost]
        [MultiButton(Name = "save")]
        public ActionResult Home(HomeViewModel hvm)
        {
            hvm.banner.Sort();
            new CommonDAO().InsertBannerIntoXML(hvm.banner);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [MultiButton(Name = "remove", Argument = "id")]
        public ActionResult Home(HomeViewModel hvm,int id)
        {
            hvm.banner.RemoveAt(id);
            hvm.banner.Sort();
            foreach(var b in hvm.banner)
            {
                b.indexd = hvm.banner.IndexOf(b);
            }
            hvm.article = new ArticleDAO().GetArticleList();
            //new CommonDAO().InsertBannerIntoXML(hvm.banner);
            return View(hvm);
        }
        [HttpPost]
        [MultiButton(Name = "add")]
        public ActionResult Home(HomeViewModel hvm, string add)
        {
            hvm.banner.Sort();
            hvm.banner.Add(new BannerImgModel(0, "", "", hvm.banner.Count));
            hvm.article = new ArticleDAO().GetArticleList();
            return View(hvm);
        }
        [HttpPost]
        public string uploadImg()
        {
            var img = HttpContext.Request.Files[0];
            if (img == null)
            {
                return "error|图片为空";
            }
            string path = Server.MapPath("~/Upload/Images/");
            string originalFileName = img.FileName;
            string fileExtension = originalFileName.Substring(originalFileName.LastIndexOf('.'), originalFileName.Length - originalFileName.LastIndexOf('.'));
            string filename = (new Random()).Next() + fileExtension;
            string imgpath = path + filename;
            img.SaveAs(imgpath);
            string imgurl = "http://"+Request.Url.Authority + "/Upload/Images/" + filename;
            return imgurl;
        }



    }
}
