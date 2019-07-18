using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JTableMVC.Models;
using System.Data.Entity;

namespace JTableMVC.Controllers
{
    public class DemoController : Controller
    {
        TharEntities session = new TharEntities();
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Getbirds(int jtstartindex = 0, int jtpagesize = 0, string jtsorting = null)
        {
            var res = session.Birds.ToList();
            switch (jtsorting)
            {
                case "ID ASC":
                   res= res.OrderBy(x => x.ID).ToList();
                    break;
                case "ID DESC":
                    res = res.OrderByDescending(x => x.ID).ToList();
                    break;
                case "BirdName ASC":
                    res = res.OrderBy(x => x.BirdName).ToList();
                    break;
                case "BirdName DESC":
                    res = res.OrderByDescending(x => x.BirdName).ToList();
                    break;
                default:
                    break;
            }
            int count = res.Count();
            res = res.Skip(jtstartindex).ToList();
            res = res.Take(jtpagesize).ToList();
            return Json(new { Result = "OK", Records = res, totalrecordcount = count });
        }
        //public ActionResult Getbirds(int id)
        //{
        //    var res = session.Birds.Where(x => x.ID == id);
        //    int count = res.Count();
        //    return Json(new { Result = "OK", Records = res, totalrecordcount = count });
        //}
        public ActionResult AddBird(Bird b)
        {
            session.Birds.Add(b);
            session.SaveChanges();
            return Json(new { Result = "OK", Record = b });
        }

        public ActionResult Deletebird(Bird b)
        {
            session.Entry(b).State = EntityState.Deleted;
            session.SaveChanges();
            return Json(new { Result = "OK" });
        }
        public ActionResult Updatebird(Bird b)
        {
            session.Entry(b).State = EntityState.Modified;
            session.SaveChanges();
            return Json(new { Result = "OK" });
        }

    }
}