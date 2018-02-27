using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZSTUZCGLC.Models;
using System.Data;
using ZSTUZCGLC.Common;

namespace ZSTUZCGLC.DAL.DAO
{
    public class UserDAO:SqlDAO
    {
        public LoginViewModel GetInfoByid(string userid)
        {
            return GetUserInfo(ExecuteReader("SELECT user.* FROM [user] WHERE (((user.userid)=\"" + userid + "\"))")[0]);
        }
        internal static LoginViewModel GetUserInfo(DataRow dr)
        {
            LoginViewModel lv = new LoginViewModel(GetInt32(dr["id"]), GetString(dr["userid"]), GetString(dr["password"]), GetString(dr["username"]));
            return lv;
        }
    }
}