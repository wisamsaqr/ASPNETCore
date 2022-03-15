using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRPALProject.Controllers
{
    public class FrontBaseController : Controller
    {
        protected IRPALProject.Models.IRPALG1ProjectEntities Db = new Models.IRPALG1ProjectEntities();


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.Db = Db;
        }


        protected void Paging(string ActionURL, int PageId, int RowsCount, int PageRowsCount, int PagesBeforeOrAfterTheCurrentPage)
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




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}