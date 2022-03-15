using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRPALProject.Areas.CMS.Controllers
{
    [Authorize]
    public class CMSBaseController : Controller
    {
        public int AdminId = 0;
        protected IRPALProject.Models.IRPALG1ProjectEntities Db = new Models.IRPALG1ProjectEntities();


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ViewBag.Db = Db;
            //var user = Db.AspNetUsers.Single(aspu => aspu.Email == User.Identity.Name);  //Trainer

            // Here we want to send to view the admin of the current signed in user to use its properties in the view,
            //and here we need the FullName of the admin.
            //So, by using User.Identity.Name we get the username of the current user. Then we use this username
            //to get the current user from AspNetUsers, which in turn used to get the admin from Admin table using
            //the relationship between AspNetUsers and Admin tables.
            //We has put the code inside the Base Controller, because we need to show the user name inside each view,
            //of each action of each class, So, if we need the user name in each action of a controller, we just
            //retrieve we would need to get the user name in each action, or simply instead we get in 
            var user = Db.AspNetUsers.Single(aspu => aspu.UserName == User.Identity.Name);  //here we retrieved the current admin model from AspNetUsers DbSet, why? to be used for retrieving the admin model inside Admins DbSet next statement which we need
            var admin = Db.Admins.Single(a => a.AspNetUserId == user.Id);   //here we retrieved the current admin model from Admins DbSet using the current admin model inside AspNetUsers DbSet
            AdminId = admin.Id;
            ViewBag.Admin = admin;



            //System.Diagnostics.Debug.WriteLine("@@@::" + User.Identity.Name + " | " + user.UserName + " | " + user.Email);
        }


        protected void ResizeImage(string Image)
        {
            using (var img = System.Drawing.Image.FromFile(Server.MapPath("/Content/Images/Original/") + Image))
            {
                int w, h;

                w = 250;
                h = w * img.Height / img.Width;
                using (var imgThumb = new System.Drawing.Bitmap(img, w, h))
                {
                    imgThumb.Save(Server.MapPath("/Content/Images/Thumb/" + Image), System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                w = 500;
                if (img.Width < w) w = img.Width;
                h = w * img.Height / img.Width;
                using (var imgMedium = new System.Drawing.Bitmap(img, w, h))
                {
                    imgMedium.Save(Server.MapPath("/Content/Images/Medium/" + Image), System.Drawing.Imaging.ImageFormat.Jpeg);
                }

                w = img.Width < 750 ? img.Width : 750;
                h = w * img.Height / img.Width;
                using (var imgLarge = new System.Drawing.Bitmap(img, w, h))
                {
                    imgLarge.Save(Server.MapPath("/Content/Images/Large/" + Image), System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
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
            if(disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}