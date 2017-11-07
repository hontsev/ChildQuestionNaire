using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;

namespace QuestionNaireMVC4C.Controllers
{
    public class Question
    {
        public int id { get; set; }
        public string introduction { get; set; }
        public string type { get; set; }
        public string options { get; set; }
        public string scores { get; set; }
        public string age { get; set; }
    }

    public class QuestionNaire
    {
        public string id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string introduction { get; set; }
        public List<Question> questions { get; set; }
        public QuestionNaire()
        {
            id = "";
            code = "";
            title = "";
            introduction = "";
            questions = new List<Question>();
        }
    }


    public class QResult
    {
        public string type { get; set; }
        public string title { get; set; }
        public string[,] options { get; set; }
    }
    public class Result
    {
        //参与人数
        public int number { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public List<QResult> answers { get; set; }
    }

    public class AdminController : Controller
    {
        ///   <summary>
        ///   将指定字符串按指定长度进行剪切，
        ///   </summary>
        ///   <param   name="oldStr "> 需要截断的字符串 </param>
        ///   <param   name="maxLength "> 字符串的最大长度 </param>
        ///   <param   name="endWith "> 超过长度的后缀 </param>
        ///   <returns> 如果超过长度，返回截断后的新字符串加上后缀，否则，返回原字符串 </returns>
        public string StringTruncat(string oldStr, int maxLength = 25, string endWith = "...")
        {
            if (string.IsNullOrEmpty(oldStr))
                //   throw   new   NullReferenceException( "原字符串不能为空 ");
                return oldStr + endWith;
            if (maxLength < 1)
                return "";
            if (oldStr.Length > maxLength)
            {
                string strTmp = oldStr.Substring(0, maxLength);
                if (string.IsNullOrEmpty(endWith))
                    return strTmp;
                else
                    return strTmp + endWith;
            }
            return oldStr;
        }

        
        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult CheckQn()
        {
            string sql = "SELECT id,title,introduction FROM questionnaire ORDER BY id DESC";
            var questions = DB.GetResult(sql).Rows;
            //防止简介过长，截断
            for (int i = 0; i < questions.Count; i++)
            {
                questions[i][2] = StringTruncat(questions[i][2].ToString());
            }
            ViewBag.questions = questions;

            return View();
        }

        public ActionResult CheckQnPublish(string id)
        {
            string sql = string.Format("SELECT publish_code,title FROM questionnaire WHERE id='{0}'", id);
            var res = DB.GetResult(sql).Rows;
            if (res.Count > 0)
            {
                string code = res[0][0].ToString();
                string name = res[0][1].ToString();
                string url = Url.Action("ShowQnByCode", "User", new { code = code });
                ViewBag.qnurl = url;
                ViewBag.qnname = name;
            }

            return View();
        }


        

        public ActionResult DeleteQn(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string sql = string.Format("DELETE FROM questionnaire WHERE id='{0}'", id);
                DB.ExecuteSql(sql);
                sql = string.Format("DELETE FROM answer WHERE belong_q in (SELECT id FROM question WHERE belong_qn='{0}')", id);
                DB.ExecuteSql(sql);
                sql = string.Format("DELETE FROM question WHERE belong_qn='{0}'", id);
                DB.ExecuteSql(sql);
            }
            return RedirectToAction("CheckQn");
        }


        public ActionResult AddQn(string id = "")
        {
            
            if (!string.IsNullOrWhiteSpace(id))
            {
                // 编辑问卷
                try
                {
                    int qnid = Int32.Parse(id);
                    string sql = string.Format("SELECT * FROM questionnaire WHERE id='{0}'", qnid);
                    var qnr = DB.GetResult(sql).Rows[0];
                    QuestionNaire qn = new QuestionNaire();
                    qn.id = qnid.ToString();
                    qn.title = qnr[1].ToString();
                    qn.introduction = qnr[2].ToString();
                    List<Question> qs = new List<Question>();
                    sql = string.Format("SELECT type,introduction,options,scores,age FROM question WHERE belong_qn='{0}' ORDER BY age,type", qnid);
                    var qst = DB.GetResult(sql);
                    for (int i = 0; i < qst.Rows.Count; i++)
                    {
                        Question nq = new Question();
                        nq.type = qst.Rows[i]["type"].ToString();
                        nq.introduction = qst.Rows[i]["introduction"].ToString();
                        nq.options = qst.Rows[i]["options"].ToString();
                        nq.scores = qst.Rows[i]["scores"].ToString();
                        nq.age = qst.Rows[i]["age"].ToString();
                        qs.Add(nq);
                    }
                    qn.questions = qs;
                    ViewBag.qn = qn;
                }
                catch
                {

                }
            }
            
            return View();
        }


        /// <summary>
        /// 生成发布用的问卷专属编码
        /// (与调用时的服务器时刻有关)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getPublishCode(string str)
        {
            string code = "";

            string timestr = DateTime.Now.ToLongTimeString();
            string beforestr = str + timestr;

            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            code = BitConverter.ToString(MD5.ComputeHash(Encoding.GetEncoding("gb2312").GetBytes(beforestr))).Replace("-", "");
            //md5太长，取前7位
            code = code.Substring(0, 7);

            return code;
        }

        [HttpPost]
        public ActionResult SaveQn(QuestionNaire qn)
        {

            if (string.IsNullOrEmpty(qn.id))
            {
                //new 
                string code = getPublishCode(qn.title);
                string sql = String.Format("INSERT INTO questionnaire(title,introduction,publish_code) VALUES('{0}','{1}','{2}')", qn.title, qn.introduction, code);
                DB.ExecuteSql(sql);
                sql = String.Format("SELECT max(id) FROM questionnaire");
                qn.id = DB.GetResult(sql).Rows[0][0].ToString();

                //foreach (var q in qn.questions)
                //{
                //    sql = string.Format("INSERT INTO question(introduction,type,options,scores,belong_qn) VALUES('{0}','{1}','{2}','{3}','{4}')", q.introduction, q.type, q.options, q.scores, qn.id);
                //    DB.ExecuteSql(sql);
                //}
            }
            else
            {
                //update
                string sql = String.Format("UPDATE questionnaire SET title='{0}',introduction='{1}' WHERE id='{2}'", qn.title, qn.introduction, qn.id);
                DB.ExecuteSql(sql);

                ////删除以前的题目记录和答题结果
                //sql = string.Format("DELETE FROM answer WHERE belong_q in (SELECT id FROM question WHERE belong_qn='{0}')", qn.id);
                //DB.ExecuteSql(sql);
                //sql = String.Format("DELETE FROM question WHERE belong_qn='{0}'", qn.id);
                //DB.ExecuteSql(sql);

                //foreach (var q in qn.questions)
                //{
                //    sql = string.Format("INSERT INTO question(introduction,type,options,scores,belong_qn) VALUES('{0}','{1}','{2}','{3}','{4}')", q.introduction, q.type, q.options, q.scores, qn.id);
                //    DB.ExecuteSql(sql);
                //}
            }

            return Content("Success");
        }







        /// <summary>
        /// 判断上传文件的后缀是否合法
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool isAllow(string filename)
        {
            string[] allowExtensions = { ".xls", ".xlsx" };
            string fileExtension = System.IO.Path.GetExtension(filename).ToLower();
            for (int i = 0; i < allowExtensions.Length; i++)
            {
                if (fileExtension == allowExtensions[i])
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult ClearQuestions(string id)
        {
            //string id = Request.Form["qn_id"];

            string sql = string.Format("DELETE FROM question WHERE belong_qn='{0}'", id);
            DB.ExecuteSql(sql);

            return RedirectToAction("AddQn", new { id = id });
        }

        public ActionResult UploadQuestion()
        {
            HttpPostedFileBase file = Request.Files["file"];
            string id = Request.Form["qn_id"];
            if (file != null && isAllow(file.FileName))
            {
                //存储文件
                string root = HttpContext.Server.MapPath("~/Uploads");
                if (!Directory.Exists(root)) Directory.CreateDirectory(root);
                string filepath = Path.Combine(root, Path.GetFileName(file.FileName));
                file.SaveAs(filepath);
                //读取文件
                DataTable dt = ExcelHelper.GetExcelForm(filepath);
                var info = dt.Rows;
                for (int i = 0; i < info.Count; i++)
                {
                    DataRow row = info[i];
                    string introduction = row[0].ToString();
                    string type = row[1].ToString();
                    string age = row[2].ToString();
                    string items = "";
                    for (int s = 3; s < 6; s++)
                    {
                        items += row[s].ToString() + ",";
                    }
                    if (items.EndsWith(",")) items = items.Substring(0, items.Length - 1);

                    //string sql = "DELETE FROM question WHERE introduction='" + introduction + "'";
                    //DB.ExecuteSql(sql);

                    //如果课程编号重复，就不导入
                    string sql = string.Format(
                        "INSERT INTO question("
                        + "introduction,type,age,options,scores,belong_qn"
                        + ") VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                        introduction,
                        type,
                        age,
                        items,
                        "3,2,1",
                        id
                    );
                    DB.ExecuteSql(sql);
                }
                
                //删除文件
                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);
            }
            return RedirectToAction("AddQn", new { id=id });
        }

        public ActionResult CheckUser()
        {
            string sql = "SELECT id,name,type FROM [user]";
            var info = DB.GetResult(sql);
            ViewBag.info = info.Rows;
            return View();
        }

        public ActionResult AddUser()
        {
            string u_id = Request["u_id"].ToString();
            string u_pw = "123";
            //string u_type = Request["u_type"].ToString();
            if (!string.IsNullOrEmpty(u_id)
               // && !string.IsNullOrEmpty(u_type)
                && !DB.Exists(string.Format("SELECT id FROM [user] WHERE name='{0}'", u_id)))
            {
                string sql = string.Format("INSERT INTO [user](name,pw,type) VALUES('{0}','{1}','1')", u_id, u_pw);
                DB.ExecuteSql(sql);
            }
            return RedirectToAction("CheckUser");
        }

        public ActionResult DeleteUser(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string sql = string.Format("DELETE FROM [user] WHERE id='{0}'", id);
                DB.ExecuteSql(sql);
            }
            return RedirectToAction("CheckUser");
        }

        public ActionResult ResetPw(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string sql = string.Format("UPDATE [user] SET pw='123' WHERE id='{0}'", id);
                DB.ExecuteSql(sql);
            }
            return RedirectToAction("CheckUser");
        }
    }
}
