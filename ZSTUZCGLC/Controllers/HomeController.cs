using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSTUZCGLC.Models;
using ZSTUZCGLC.DAL.DAO;
using Webdiyer.WebControls.Mvc;
using System.Data;
using System.Xml;

namespace ZSTUZCGLC.Controllers
{
    public class HomeController : MenuController
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewData["category_id"] = 0;
            IndexViewModel iv = new IndexViewModel();
            iv.left = new ArticleDAO().GetBriefArticlebyCID(31, 5);
            iv.middle = new ArticleDAO().GetBriefArticlebyCID(30, 4);
            iv.right= new ArticleDAO().GetBriefArticlebyCID(40, 5);
            iv.banner = new CommonDAO().GetBannerbyXML();
            return View(iv);
        } 
        //栏目id
        public ActionResult Theme(int id,int page=1)
        {
            DataTable dt = (DataTable)ViewData["category"];
            if (!(bool)dt.Select("id=" + id)[0]["islist"])
            {
                int did = new ArticleDAO().GetNoneListThemeArticleId(id);
                if (did == 0)
                {
                    ViewData["category_id"] = id;
                    List<ThemeViewModel> ntv = new List<ThemeViewModel>();
                    PagedList<ThemeViewModel> nptv = ntv.ToPagedList(0,0);
                    return View(nptv);                    
                }
                else
                {
                    return RedirectToAction("Detail", new { id = did });
                }
            }
            ViewData["category_id"] = id;
            List<ThemeViewModel> ltv = new ArticleDAO().GetBriefArticlebyCID(id);
            PagedList<ThemeViewModel> ptv = ltv.ToPagedList(page, 8);
            DataTable dtz = (DataTable)ViewData["category"];
            ViewBag.Title = dtz.Select("id=" + ViewData["category_id"].ToString())[0]["categoryname"].ToString();
            return View(ptv);
        }
        //文章id
        public ActionResult Detail(int id)
        {
            DetailViewModel dv = new ArticleDAO().GetArticleView(id);
            ViewData["category_id"] = dv.category_id;
            DataTable dtz = (DataTable)ViewData["category"];
            ViewBag.Title = dtz.Select("id=" + ViewData["category_id"].ToString())[0]["categoryname"].ToString();
            return View(dv);
        }
    }
}