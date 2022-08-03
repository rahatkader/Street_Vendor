using Street_Vendors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Street_Vendors.Controllers
{
    [Authorize]
    public class MapController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllLocation()
        {
            var data = _context.Map.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}