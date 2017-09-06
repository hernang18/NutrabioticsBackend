using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NutraBioticsBackend.Models;
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;

namespace NutraBioticsBackend.Controllers
{
    public class UsersController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(u => u.Company).Include(u => u.CountryId).Include(u => u.Plant).Include(u => u.Vendor);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name");
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Description");
            ViewBag.PlantId = new SelectList(db.Plants, "PlantId", "PlantDescription");
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName");
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserId,CompanyId,FirstName,LastName,Email,Gender,IMEI,CountryId,State,City,Address1,PhoneNumber,VendorId,Password,PasswordConfirm,PlantId")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //check for Exception type as sql Exception 
                    if (ex.GetBaseException().GetType() == typeof(SqlException))
                    {
                        //Violation of primary key/Unique constraint can be handled here. Also you may //check if Exception Message contains the constraint Name 
                    }
                }
                // var useres = await db.Users
                //.Where(c => c.VendorId == user.VendorId)
                //.ToListAsync();

                //if(useres==null)
                //{

                //}
                //else
                //{
                     // MOstrar mensaje
                //}
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", user.CompanyId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Description", user.CountryId);
            ViewBag.PlantId = new SelectList(db.Plants, "PlantId", "PlantDescription", user.PlantId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName", user.VendorId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", user.CompanyId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Description", user.CountryId);
            ViewBag.PlantId = new SelectList(db.Plants, "PlantId", "PlantDescription", user.PlantId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName", user.VendorId);
            return View(user);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserId,CompanyId,FirstName,LastName,Email,Gender,IMEI,CountryId,State,City,Address1,PhoneNumber,VendorId,Password,PasswordConfirm,PlantId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                var registerVieModel = new ResetPasswordViewModel
                {                    
                    Email = user.Email,
                    Password = user.Password,
                    ConfirmPassword = user.PasswordConfirm
                };
                AccountController objAC = new AccountController();
                await objAC.ResetPassword(registerVieModel);
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "CompanyId", "Name", user.CompanyId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Description", user.CountryId);
            ViewBag.PlantId = new SelectList(db.Plants, "PlantId", "PlantDescription", user.PlantId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "VendorName", user.VendorId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
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
