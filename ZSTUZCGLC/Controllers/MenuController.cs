using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZSTUZCGLC.DAL.DAO;

namespace ZSTUZCGLC.Controllers
{
    public abstract class MenuController : Controller
    {
        public MenuController()
        {
            ViewData["category"] = new CategoryDAO().GetCategory();
        }
    }
}