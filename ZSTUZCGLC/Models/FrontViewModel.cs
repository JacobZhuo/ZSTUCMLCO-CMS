using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ZSTUZCGLC.Models
{
        public class DetailViewModel
        {
            public DetailViewModel(string title, DateTime edittime, string content, int category_id)
            {
                this.title = title;
                this.edittime = edittime;
                this.content = content;
                this.category_id = category_id;
            }
            public string title { set; get; }
            public string content { set; get; }
            public DateTime edittime { set; get; }
            public int category_id { set; get; }
        }
        //public class IndexViewModel
        // {
        //public IndexViewModel() { }
        //public IndexViewModel(int id,DateTime edittime,string title, int category_id,string summary)
        //{
        //    this.id = id;
        //    this.edittime = edittime;
        //    this.title = title;
        //    this.category_id = category_id;
        //    this.summary = summary;
        //}
        //public int id { set; get; }
        //public DateTime edittime { set; get; }
        //    public string title { set; get; }
        //    public int category_id { set; get; }
        //    public string summary { set; get; }
        //  }
        public class IndexViewModel
         {
            public IndexViewModel()
            {

            }
            public IndexViewModel(List<ThemeViewModel> left, List<ThemeViewModel> middel, List<ThemeViewModel> right)
            {
                this.left = left;
                this.middle = middle;
                this.right = right;
            }
              public List<ThemeViewModel> left { set; get; }
              public List<ThemeViewModel> middle { set; get; }
              public List<ThemeViewModel> right { set; get; }
              public List<BannerImgModel> banner { set; get; }
         }
    public class ThemeViewModel
    {
        public ThemeViewModel(int id,DateTime edittime, string title, string summary)
        {
            this.id = id;
            this.edittime = edittime;
            this.title = title;
            this.summary = summary;
        }
        public int id { set; get; }
        public DateTime edittime { set; get; }
        public string title { set; get; }
        public string summary { set; get; }
    }
    public class BannerImgModel:IComparable
    {
        public BannerImgModel() { }
        public BannerImgModel(int id,string title,string imgurl,int indexd)
        {
            this.id = id;
            this.title = title;
            this.imgurl = imgurl;
            this.indexd = indexd;
        }
        public int id { set; get; }
        public string title { set; get; }
        public string imgurl { set; get; }
        public int indexd { set; get; }
        public int CompareTo(object obj)
        {
            int res = 0;
            BannerImgModel sobj = (BannerImgModel)obj;
            if (this.indexd > sobj.indexd)
            {
                res = 1;
            }
            else if (this.indexd < sobj.indexd)
            {
                res = -1;
            }
            return res;
        }
    }
    
}