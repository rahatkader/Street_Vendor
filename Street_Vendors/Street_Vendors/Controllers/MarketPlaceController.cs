using Street_Vendors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Street_Vendors.Controllers
{
    [Authorize]
    public class MarketPlaceController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public MarketPlaceController()
        {

        }
        // GET: MarketPlace
        public ActionResult Index()
        {
            ViewBag.sellers = _context.Users.ToList();
            ViewBag.items = _context.Items.ToList();
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _context.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }
        public ActionResult ProfileDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.items = _context.Items.ToList();
            return View(user);
        }

        public ActionResult likeItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _context.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            item.ItemRating += 1;
            _context.SaveChanges();
            return RedirectToAction("Details", new { id });
        }

        public ActionResult dislikeItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _context.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            item.ItemRating -= 1;
            _context.SaveChanges();
            return RedirectToAction("Details", new { id });
        }
    }
}