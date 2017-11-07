using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuestionNaireMVC4C.Controllers
{
    public class AnswerQn
    {
        public string id { get; set; }
        public string title { get; set; }
        public string introduction { get; set; }
        public string is_open { get; set; }
        public string is_answer { get; set; }
        public string publish_code { get; set; }
    }

    public class Answer
    {
        public string answer { get; set; }
        public string qid { get; set; }
    }

    public class UserController : Controller
    {

        public ActionResult CheckQn()
        {
            string sql = "SELECT id,title,publish_code FROM questionnaire ORDER BY id DESC";
            var questions = DB.GetResult(sql).Rows;
            ViewBag.questions = questions;

            return View();
        }

        public ActionResult ShowQnByCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                string sql = string.Format("SELECT id,title,introduction FROM questionnaire WHERE publish_code='{0}'", code);
                var res = DB.GetResult(sql);
                if (res.Rows.Count > 0)
                {
                    string id = res.Rows[0][0].ToString();
                    try
                    {
                        int qnid = Int32.Parse(id);
                        var qnr = res.Rows[0];
                        QuestionNaire qn = new QuestionNaire();
                        qn.id = qnid.ToString();
                        qn.title = res.Rows[0]["title"].ToString();
                        qn.introduction = res.Rows[0]["introduction"].ToString().Replace("\r", "").Replace("\n", "<br>").Replace(" ", "&nbsp;").Replace("\t", "&emsp;");
                        qn.code = code;
                        ViewBag.qn = qn;

                        return View("ShowQnWelcome");
                    }
                    catch
                    {

                    }
                }
            }
            return RedirectToAction("Message", "Home", new { id = "不存在此问卷。" });
        }

       
        public ActionResult ShowQn(string age, string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                    try
                    {

                        ////限制每个ip限答一次
                        //string uid = CheckIP.GetIP();
                        //sql = string.Format("SELECT answer.id FROM answer LEFT JOIN question  ON answer.belong_q=question.id WHERE belong_user='{0}' and question.belong_qn='{1}'", uid, qnid);
                        //if (DB.Exists(sql))
                        //{
                        //    //此ip已经回答过此问题了。
                        //    return RedirectToAction("Message", "Home", new { id = "您已回答了本问卷，感谢您的参与！" });
                        //}


                        string sql = string.Format("SELECT id,title FROM questionnaire WHERE publish_code='{0}'", code);
                        var qnr = DB.GetResult(sql);
                        QuestionNaire qn = new QuestionNaire();
                        qn.id = qnr.Rows[0]["id"].ToString();
                        qn.title = qnr.Rows[0]["title"].ToString();
                        List<Question> qs = new List<Question>();
                        sql = string.Format("SELECT id,type,introduction,options,scores FROM question WHERE belong_qn='{0}' and age='{1}' ORDER BY type", qn.id,age);
                        var qst = DB.GetResult(sql);
                        for (int i = 0; i < qst.Rows.Count; i++)
                        {
                            Question nq = new Question();
                            nq.type = qst.Rows[i]["type"].ToString();
                            nq.introduction = qst.Rows[i]["introduction"].ToString();
                            nq.options = qst.Rows[i]["options"].ToString();
                            nq.id = Int32.Parse(qst.Rows[i]["id"].ToString());
                            qs.Add(nq);
                        }
                        qn.questions = qs;
                        ViewBag.qn = qn;

                        return View("ShowQn");
                    }
                    catch
                    {

                    }
                }
            return RedirectToAction("Message", "Home", new { id = "不存在此问卷。" });
        }


        

        [HttpPost]
        public ActionResult SubmitQn(List<Answer> answers)
        {
            Random ra = new Random();
            string uid = CheckIP.GetIP();

            string sql = "";
            int allscore = 0;
            foreach (var a in answers)
            {
                ////删除此ip之前的回答记录
                //sql = string.Format("DELETE FROM answer WHERE belong_user='{0}' AND belong_q='{1}'", uid, a.qid);
                //DB.ExecuteSql(sql);
                

                sql = string.Format("select scores from question where id='{0}'", a.qid);
                string res = DB.GetResult(sql).Rows[0]["scores"].ToString();
                int index = int.Parse(a.answer);
                int score = int.Parse(res.Split(',')[index - 1]);
                allscore += score;

                a.answer = a.answer.Replace("'", "\"");
                sql = string.Format("INSERT INTO answer(answer,belong_user,belong_q) VALUES('{0}','{1}','{2}')", a.answer, uid, a.qid);
                DB.ExecuteSql(sql);
            }
            
            return Content(allscore.ToString());
        }

        public ActionResult Result()
        {
            string title = Request.Form["title"];
            int score = int.Parse(Request.Form["score"]);
            ViewBag.title = title;
            string s11, s12, s21, s22, s31, s32;
            s12 = "家长任意取出一种颜色的小球，让孩子取颜色相同的小球进行配对。如果和别的小朋友一起玩，还可以进行看谁拿的对和快的比赛。提示：也可以准备一些颜色相同但形状不同的物体，让孩子分类，配对，以训练孩子对图形的观察和判断能力。";
            s22 = "妈妈选择一则会出现声音的童话故事给孩子读，一边读，一边发出声音。然后问孩子：樵夫砍柴的时候会发出什么声音呢？孩子可以回答：咚咚。再问孩子：狮子突然出现了，会发出什么声音呢？孩子：吼~~~家长一边读童话琏找出可以让孩子发出声音的事物，让孩子觉得讯童话真的很有趣。提示：在进行这项训练时，如果孩子答不上来，妈妈可以适当提醒。";
            s32 = "准备两件不同的玩具，如一只玩具狗，一只玩具猪。妈妈先把小狗放在椅子上面，告诉孩子：小狗在椅子上面。再把小猪放在椅子下面，告诉孩子，小猪在下面。让孩子根据妈妈的要求，分别把小狗，小猪放到椅子的上面或下面。提示：下一次进行这项训练时，可以换一些不同的玩具，也可以将玩具放在桌子上面或下面。";

            if (score < 14)
            {
                s11 = "依照本次测试结果看，您的孩子可能和同龄孩子相比处于稍微偏低水平，家长不要担心，我们的测试题目均采用普遍儿童的大众标准，每个孩子因个体差异和其他因素的不同可能会与本次测评有出入。只要家长平时与孩子多说话，多爱抚孩子，结合我们的专家建议，这样能很好的促进孩子的健康成长。";
                s21 = "依照本次测试结果看，您的孩子可能和同龄孩子相比处于稍微偏低水平，家长不要担心，我们的测试题目均采用普遍儿童的大众标准，每个孩子因个体差异和其他因素的不同可能会与本次测评有出入。只要家长平时与孩子多说话，多爱抚孩子，结合我们的专家建议，这样能很好的促进孩子的健康成长。";
                s31 = "依照本次测试结果看，您的孩子可能和同龄孩子相比处于稍微偏低水平，家长不要担心，我们的测试题目均采用普遍儿童的大众标准，每个孩子因个体差异和其他因素的不同可能会与本次测评有出入。只要家长平时与孩子多说话，多爱抚孩子，结合我们的专家建议，这样能很好的促进孩子的健康成长。";
            }
            else if (score < 29)
            {
                s11 = "您的宝宝在同龄宝宝中已处于正常水平发育，家长应继续努力，同时要保持与宝宝的沟通，结合我们的专家建议，以促进宝宝健康的成长。";
                s21 = "您的宝宝在同龄宝宝中已处于正常水平发育，家长应继续努力，同时要保持与宝宝的沟通，结合我们的专家建议，以促进宝宝健康的成长。";
                s31 = "您的宝宝在同龄宝宝中已处于正常水平发育，家长应继续努力，同时要保持与宝宝的沟通，结合我们的专家建议，以促进宝宝健康的成长。";
            }
            else
            {
                s11 = "您的宝宝在同龄宝宝中已处于偏高水平发育，家长应继续努力，同时要保持与宝宝的沟通，以促进宝宝健康的成长。";
                s21 = "您的宝宝在同龄宝宝中已处于偏高水平发育，家长应继续努力，同时要保持与宝宝的沟通，以促进宝宝健康的成长。";
                s31 = "您的宝宝在同龄宝宝中已处于偏高水平发育，家长应继续努力，同时要保持与宝宝的沟通，结合我们的专家建议，以促进宝宝健康的成长。";

            }
            ViewBag.s11 = s11;
            ViewBag.s12 = s12;
            ViewBag.s21 = s21;
            ViewBag.s22 = s22;
            ViewBag.s31 = s31;
            ViewBag.s32 = s32;
            return View();
        }
    }
}
