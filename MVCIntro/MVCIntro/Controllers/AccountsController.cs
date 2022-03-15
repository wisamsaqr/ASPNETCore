using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCIntro.Models;

namespace MVCIntro.Controllers
{
    public class AccountsController : Controller
    {
        private IRPALG1Entities db = new IRPALG1Entities();

        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Country);
            return View(accounts.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,Gender,Active,CountryId,DOB")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                //TempData["msgClass"] = "alert-info";
                TempData["msg"] = "i:Account has been added successfully";
                TempData["msg2"] = "info:Account has been added successfully";
                return RedirectToAction("Create");
            }

            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", account.CountryId);
            //return View(account);

            /*
			* Our Modification	the next code is right because ModelState fills the elements, so it would determine the country id in the list
			* and we don't need to pass the account as model to the View method because the data of ModelState is also used to fill the elements
			* however, of course we need to create the list of countries and send it to the view full of all countries and the ModelState is responsible
			* of determining the selected country using the CountryId.
			* (Caution: When the ModelState is full of data, whether the data is valid or invalid, if View() is called, then the MVC would render the view
			* and fill its elemnts using the ModelState and finally send it to the client)
            */
			//ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            //return View();
			
			//the exactly previous code is does the same as the Http.Get action, so we can just call the Create method, and as we know
			//any data stored to ViewBag inside called method, remains as is.
            return Create();

            //return RedirectToAction("Create");    // ModelState loses the data when RedirectToAction is carried out
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", account.CountryId);
            ViewBag.CountriesList = new SelectList(db.Countries, "Id", "Name", account.CountryId);
            //ViewBag.CountriesList = new SelectList(db.Countries, "Id", "Name");   //this would work because I think when an element is associated to a property, it takes its value
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,Gender,Active,CountryId,DOB")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "s:Account has been updated successfully";
                TempData["msg2"] = "success:Account has been added successfully";
                return RedirectToAction("Index");
            }
            //ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", account.CountryId);
            //return View(account);

            return Edit(account.Id);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            TempData["msg"] = "d:Account has been deleted successfully";
            TempData["msg2"] = "danger:Account has been added successfully";
            return RedirectToAction("Index");
        }



        ///////////////////////////////////////////////////////////////

        
        
        #region Create Action with model & parameter
        public ActionResult CreateWithModelAndParameter()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithModelAndParameter([Bind(Include = "Id,FullName,Email,Gender,Active,CountryId,DOB")] Account account, string Parameter)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();

                TempData["msg"] = "i:Account has been added successfully";
                TempData["msg2"] = "info:Account has been added successfully";

                return RedirectToAction("CreateWithModelAndParameter");
            }

            return CreateWithModelAndParameter();
        }
        #endregion Create Action with model & parameter



        #region Deleting Messages
        public ActionResult DeleteDirectly(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            TempData["msg"] = "d:Account has been deleted successfully";
            TempData["msg2"] = "danger:Account has been deleted successfully";
            return RedirectToAction("Index");
        }
        #endregion Deleting Messages



        #region File Uploading
        public ActionResult CreateWithFileUpload()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateWithFileUpload([Bind(Include = "Id,FullName,Email,Gender,Active,CountryId,DOB")] Account account, HttpPostedFileBase AccountFile)
        {
            if (ModelState.IsValid && AccountFile != null)
            {
                db.Accounts.Add(account);
                db.SaveChanges();

                //File Uploading
                AccountFile.SaveAs(Server.MapPath("~/Content/Uploads/") + AccountFile.FileName);

                TempData["msg"] = "i:Account has been added successfully";
                TempData["msg2"] = "info:Account has been added successfully";

                return RedirectToAction("CreateWithFileUpload");
            }
            else
            {
                TempData["msg"] = "d:Wrong..";
                TempData["msg2"] = "danger:Wrong..";
            }

            return CreateWithFileUpload();
        }
        #endregion Uploading File



        #region Index With Pagination
        public ActionResult IndexWithPagination(int id = 1)
        {
            int PageId = id - 1;
            int PageRowsCount = 10;

            int Skip = PageId * PageRowsCount;
            var accounts = db.Accounts.Include(a => a.Country).OrderBy(a => a.Id).Skip(Skip).Take(PageRowsCount);

            ViewBag.ActionURL = "/Accounts/IndexWithPagination";
            ViewBag.PageId = PageId;
            ViewBag.PagesCount = (int)Math.Ceiling((double)db.Accounts.Count() / PageRowsCount);
            ViewBag.PagesBeforeOrAfterTheCurrentPage = 5;

            return View(accounts.ToList());
        }

        ///////////////////////////////////////////////////////

        public ActionResult IndexWithPagination2(int id = 1)
        {
            int PageId = id - 1;
            int PageRowsCount = 10;

            int Skip = PageId * PageRowsCount;
            var accounts = db.Accounts.Include(a => a.Country).OrderBy(a => a.Id).Skip(Skip).Take(PageRowsCount);

            Paging("/Accounts/IndexWithPagination2", PageId, db.Accounts.Count(), PageRowsCount, 5);

            return View("IndexWithPagination", accounts.ToList());
        }
        private void Paging(string ActionURL, int PageId, int RowsCount, int PageRowsCount, int PagesBeforeOrAfterTheCurrentPage)
        {
            ViewBag.ActionURL = ActionURL;
            ViewBag.PageId = PageId;

            int PagesCount = (int)Math.Ceiling((double)RowsCount / PageRowsCount);
            ViewBag.PagesCount = PagesCount;

            int Start, End;
            if (PagesCount <= (PagesBeforeOrAfterTheCurrentPage * 2 + 1))
            {
                Start = 0;
                End = PagesCount - 1;
            }
            else if (PageId <= PagesBeforeOrAfterTheCurrentPage)
            {
                Start = 0;
                End = PagesBeforeOrAfterTheCurrentPage * 2;
            }
            else if (PageId >= PagesCount - PagesBeforeOrAfterTheCurrentPage)
            {
                Start = PagesCount - 1 - PagesBeforeOrAfterTheCurrentPage * 2;
                End = PagesCount - 1;
            }
            else
            {
                Start = PageId - PagesBeforeOrAfterTheCurrentPage;
                End = PageId + PagesBeforeOrAfterTheCurrentPage;
            }
            ViewBag.Start = Start;
            ViewBag.End = End;
        }

        ///////////////////////////////////////////////////////

        public ActionResult IndexWithPaginationObject(int id = 1)
        {
            Pagination Pagination = new Pagination
            (
                id-1,
                "/Accounts/IndexWithPaginationObject/",
                db.Accounts.Count(),
                PagesBeforeOrAfterTheCurrentPage :5,
                PageRowsCount : 10
            );

            int Skip = Pagination.PageId * Pagination.PageRowsCount;
            var accounts = db.Accounts.Include(a => a.Country).OrderBy(a => a.Id).Skip(Skip).Take(Pagination.PageRowsCount);

            ViewBag.Pagination = Pagination;

            return View("IndexWithPagination", accounts.ToList());
        }

        ///////////////////////////////////////////////////////

        public ActionResult IndexWithPaginationFullURL(int id = 1)
        {
            int PageId = id - 1;
            int PageRowsCount = 10;

            int Skip = PageId * PageRowsCount;
            var accounts = db.Accounts.Include(a => a.Country).OrderBy(a => a.Id).Skip(Skip).Take(PageRowsCount);

            ViewBag.ActionURL = "/Accounts/IndexWithPaginationFullURL/";
            ViewBag.PageId = PageId;
            ViewBag.PagesCount = (int)Math.Ceiling((double)db.Accounts.Count() / PageRowsCount);
            ViewBag.PagesBeforeOrAfterTheCurrentPage = 5;

            return View("IndexWithPagination", accounts.ToList());
        }

        #endregion Index With Pagination



        #region Search
        public ActionResult IndexWithSearch(string q = "")
        {
            var accounts = db.Accounts.Where(a => a.FullName.Contains(q)
                           || a.Email.Contains(q)).Include(a => a.Country);
            ViewBag.q = q;
            return View(accounts.ToList());
        }

        public ActionResult IndexWithAdvancedSearch(bool? Active, int? CountryId, string q = "", string Gender = "")
        {
            var Accounts = db.Accounts.Where
			(
				a => (a.FullName.Contains(q) || a.Email.Contains(q))
                &&    a.Gender.Contains(Gender)
                &&   (a.Active == Active || Active == null)
                &&   (a.CountryId == CountryId || CountryId == null)
            )
            .Include(a => a.Country);

            ViewBag.q = q;
            ViewBag.Gender = Gender;
            //In case the Active parameter is null, ViewBag wouldn't take it to the view,
            //so we concatenate it with an empty string
            ViewBag.Active = Active + "";
            ViewBag.Countries = new SelectList(db.Countries, "Id", "Name", CountryId);

            #region Way2 to add countries List
            //ViewBag.CountryId = CountryId;
            //ViewBag.Countries = db.Countries;
            #endregion Way 2 to add countries List

            return View(Accounts);
        }
        #endregion Search



        #region Search With Pagination
        public ActionResult IndexWithSearchPagination(string q = "", int PageId = 1)
        {
            PageId--;

            int PageRowsCount = 10;
            int Skip = PageId * PageRowsCount;
            var accounts = db.Accounts.Where(a => a.FullName.Contains(q) || a.Email.Contains(q))
                .Include(a => a.Country)
                .OrderBy(a => a.Id)
                .Skip(Skip)
                .Take(PageRowsCount);

            ViewBag.ActionURL = "/Accounts/IndexWithSearchPagination";
            ViewBag.q = q;
            ViewBag.PageId = PageId;
            ViewBag.PagesCount = (int)Math.Ceiling((double)db.Accounts.Count(a => a.FullName.Contains(q) || a.Email.Contains(q)) / PageRowsCount);
            /* The next statement does not work, because accounts only have 10 items. The count of linq query returned items
             * is equal to the previous statment, but at last it takes only 10 items, since it calls Take() to which
             * PageRowsCount variable is passed (and this variable is equal to 10). Thus, we want to calculate the count of
             * total pages needed for displaying the entire data, therefore using accounts.Count() isn't right and we need to
             * use db.Accounts.Count(a => a.FullName.Contains(q) || a.Email.Contains(q))
            */
            //ViewBag.PagesCount = (int)Math.Ceiling((double)accounts.Count() / PageRowsCount);
            ViewBag.PagesBeforeOrAfterTheCurrentPage = 5;

            return View(accounts.ToList());
        }

        ////////////////////////////////////////////////////

        public ActionResult IndexWithSearchPaginationFullURLForAllActions(string q = "", int PageId = 1)
        {
            PageId--;

            int PageRowsCount = 10;
            int Skip = PageId * PageRowsCount;
            var accounts = db.Accounts.Where(a => a.FullName.Contains(q) || a.Email.Contains(q))
                .Include(a => a.Country)
                .OrderBy(a => a.Id)
                .Skip(Skip)
                .Take(PageRowsCount);

            ViewBag.ActionURL = "/Accounts/IndexWithSearchPaginationFullURLForAllActions?q=" + q + "&PageId=";
            ViewBag.q = q;
            ViewBag.PageId = PageId;
            ViewBag.PagesCount = (int)Math.Ceiling((double)db.Accounts.Count(a => a.FullName.Contains(q) || a.Email.Contains(q)) / PageRowsCount);
            ViewBag.PagesBeforeOrAfterTheCurrentPage = 5;

            return View(accounts.ToList());
        }

        #endregion Search With Pagination












        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class Pagination
    {
        public int PageId { get; set; } = 0;//
        public string ActionURL { get; set; }//

        public int PagesCount { get; set; }//
        public int RowsCount { get; set; }
        public int PageRowsCount { get; set; }

        public int PagesBeforeOrAfterTheCurrentPage { get; set; }//

        public int Start { get; set; }//
        public int End { get; set; }//

        public Pagination
        (
            int PageId, string ActionURL, int RowsCount,
            int PageRowsCount = 10, int PagesBeforeOrAfterTheCurrentPage = 5
        )
        {
            this.PageId = PageId;
            this.ActionURL = ActionURL;
            this.RowsCount = RowsCount;
            this.PageRowsCount = PageRowsCount;
            this.PagesBeforeOrAfterTheCurrentPage = PagesBeforeOrAfterTheCurrentPage;

            PagesCount = (int)Math.Ceiling((double)RowsCount / PageRowsCount);

            if (PagesCount <= (PagesBeforeOrAfterTheCurrentPage * 2 + 1))
            {
                Start = 0;
                End = PagesCount - 1;
            }
            else if (PageId <= PagesBeforeOrAfterTheCurrentPage)
            {
                Start = 0;
                End = PagesBeforeOrAfterTheCurrentPage * 2;
            }
            else if (PageId >= PagesCount - PagesBeforeOrAfterTheCurrentPage)
            {
                Start = PagesCount - 1 - PagesBeforeOrAfterTheCurrentPage * 2;
                End = PagesCount - 1;
            }
            else
            {
                Start = PageId - PagesBeforeOrAfterTheCurrentPage;
                End = PageId + PagesBeforeOrAfterTheCurrentPage;
            }
        }
    }
}
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.