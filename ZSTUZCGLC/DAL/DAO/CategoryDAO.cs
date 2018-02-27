using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSTUZCGLC.Models;
using System.Data;
using ZSTUZCGLC.Common;

namespace ZSTUZCGLC.DAL.DAO
{
    public class CategoryDAO:SqlDAO
    {
       public DataTable GetCategory()
        {
            return ExecuteReaderForTable("select * from category");
        }
    }
}