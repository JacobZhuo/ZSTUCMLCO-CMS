using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSTUZCGLC.Models;
using System.Xml;
using System.Web.Mvc;

namespace ZSTUZCGLC.DAL.DAO
{
    public class CommonDAO
    {
        public List<BannerImgModel> GetBannerbyXML()
        {
            List<BannerImgModel> bim = new List<BannerImgModel>();
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpRuntime.AppDomainAppPath.ToString() + @"/App_Data/Index.xml");
            XmlNode xn = doc.SelectSingleNode("banner");
            XmlNodeList xnl = xn.ChildNodes;
            int index = 0;
            foreach (XmlNode banner in xnl)
            {                                
                bim.Add(new BannerImgModel(int.Parse(banner.ChildNodes[0].InnerText), banner.ChildNodes[1].InnerText, banner.ChildNodes[2].InnerText,index));
                index++;
            }
            return bim;
        }
        public void InsertBannerIntoXML(List<BannerImgModel> lbi)
        {           
            XmlDocument doc = new XmlDocument();
            string path = HttpRuntime.AppDomainAppPath.ToString() + @"/App_Data/Index.xml";
            doc.Load(path);
            XmlNode xn = doc.SelectSingleNode("banner");
            xn.RemoveAll();
            foreach(var bim in lbi)
            {
                XmlNode level1 = doc.CreateElement("img");
                xn.AppendChild(level1);
                XmlNode level2_1 = doc.CreateElement("id");
                level2_1.InnerText = bim.id.ToString();
                level1.AppendChild(level2_1);
                XmlNode level2_2 = doc.CreateElement("title");
                level2_2.InnerText = bim.title.ToString();
                level1.AppendChild(level2_2);
                XmlNode level2_3 = doc.CreateElement("url");
                level2_3.InnerText = bim.imgurl.ToString();
                level1.AppendChild(level2_3);
                doc.Save(path);
            }
        }
    }
}