using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionNaireMVC4C.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                string type = Session["type"].ToString();
                switch (type)
                {
                    case "1":
                        return RedirectToAction("Index", "Admin");
                        break;
                    default: break;
                }
            }
            catch
            {

            }
            return RedirectToAction("CheckQn", "User");
            //return View("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Userinfo()
        {
            return View();
        }

        public ActionResult Message(string id = "")
        {
            ViewBag.message = id;
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string pw)
        {
            string sql = String.Format("SELECT id,name,[type] FROM [user] WHERE name='{0}' and pw='{1}'", name, pw);
            if (DB.Exists(sql))
            {
                var user = DB.GetResult(sql).Rows[0];
                Session["id"] = user[0].ToString();
                Session["name"] = user[1].ToString();
                Session["type"] = user[2].ToString();
                switch (user[2].ToString())
                {
                    case "1":
                        return RedirectToAction("Index", "Admin");
                        break;
                    case "2":
                        return RedirectToAction("Index", "ProjectUser");
                        break;
                    default: break;
                }
            }
            ViewBag.error = "用户名或密码错误。";
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Login");
        }

        [HttpPost]
        public ActionResult ChangePw(string oldPw, string newPw, string newPw2)
        {
            if (!object.Equals(Session["id"], null))
            {
                string sql = string.Format("SELECT id FROM [user] WHERE id='{0}' AND pw='{1}'", Session["id"].ToString(), oldPw);
                if (DB.Exists(sql))
                {
                    sql = string.Format("UPDATE [user] SET pw='{0}' WHERE id='{1}'", newPw, Session["id"].ToString());
                    DB.ExecuteSql(sql);
                    return RedirectToAction("Logout");
                }

            }
            return RedirectToAction("Userinfo");

        }

    }
}
