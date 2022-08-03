using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Street_Vendors.Models;

namespace Street_Vendors.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.ApplicationUser);

            var id = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.user = user;

            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ItemName,ItemDescription,ItemPrice,ItemImage,ItemRating,UserId")] Item item, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                foreach (string img in Request.Files)
                {
                    ImageFile = Request.Files[img];
                    string extension = Path.GetExtension(ImageFile.FileName);

                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (ImageFile != null && ImageFile.ContentLength > 0)
                        {

                            string file_name = Path.GetFileNameWithoutExtension(ImageFile.FileName) + Path.GetExtension(ImageFile.FileName);

                            string path = Path.Combine(Server.MapPath("~/image"), file_name);
                            ImageFile.SaveAs(path);
                            item.ItemImage = "~/image/" + file_name;

                            db.Items.Add(item);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["msg"] = "Invalid File Type";

                    }
                }
                ModelState.Clear();



                //db.Items.Add(item);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", item.UserId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            Session["imgPath"] = item.ItemImage;
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", item.UserId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ItemName,ItemDescription,ItemPrice,ItemImage,ItemRating,UserId")] Item item, HttpPostedFileBase ImageFile)
        {
            if (ImageFile != null)
            {
                if (ModelState.IsValid)
                {
                    foreach (string img in Request.Files)
                    {
                        ImageFile = Request.Files[img];
                        string extension = Path.GetExtension(ImageFile.FileName);

                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                        {
                            if (ImageFile.ContentLength > 0)
                            {

                                string file_name = Path.GetFileNameWithoutExtension(ImageFile.FileName) + Path.GetExtension(ImageFile.FileName);

                                string path = Path.Combine(Server.MapPath("~/image"), file_name);
                                ImageFile.SaveAs(path);
                                item.ItemImage = "~/image/" + file_name;

                                db.Entry(item).State = EntityState.Modified;
                                string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                                db.SaveChanges();

                                if (System.IO.File.Exists(oldImgPath))
                                {
                                    System.IO.File.Delete(oldImgPath);
                                }
                                TempData["msg"] = "Data Updated";
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            TempData["msg"] = "Invalid File Type";

                        }

                    }
                    ModelState.Clear();
                }
                ViewBag.UserId = new SelectList(db.Users, "Id", "Email", item.UserId);
                return View(item);
            }

            else
            {
                item.ItemImage = Session["imgPath"].ToString();
                db.Entry(item).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    TempData["msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }

            }
            /*
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            */
            //ViewBag.UserId = new SelectList(db.ApplicationUsers, "Id", "Email", item.UserId);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            string currentImg = Request.MapPath(item.ItemImage);
            if (System.IO.File.Exists(currentImg))
            {
                System.IO.File.Delete(currentImg);
            }
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
