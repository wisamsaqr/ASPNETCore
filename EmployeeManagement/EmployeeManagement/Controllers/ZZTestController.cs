using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.ZZTestModels;

namespace EmployeeManagement.Controllers
{
    public class ZZTestController : Controller
    {
        [HttpGet]
        public IActionResult AIndex()
        {
            // cause an exception, because the model which the is paased to the view
            // must be of the same type as the model the view is strongly typed with
            //var model = new AActionViewPassed() { MyProperty = "C00L" };
            var model = new AViewTyped() { MyProperty = "C00L" };
            return View(model);
        }

        [HttpPost]
        public IActionResult AIndex(AActionPara model)
        {
            return View();
        }

        /////////////////

        [HttpGet]
        public IActionResult BOuterInnerModel()
        {
            BOuterModel outerModel = new BOuterModel()
            {
                OuterModelProp = "Outer Model Property",
                InnerModel = new BInnerModel { InnerModelProp = "Inner Model Property" }
            };
            return View(outerModel);
        }

        [HttpPost]
        public IActionResult BOuterInnerModel(BOuterModel model)
        {
            return View();
        }

        /////////////////

        [HttpGet]
        public IActionResult CMyAction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CMyAction(int Num, CMyModel model)
        {
            return View();
        }

        ///////////////////

        [HttpGet]
        public IActionResult DMyAction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DMyAction(List<int> Num, CMyModel model)
        {
            return View();
        }

        ///////////////////

        [HttpGet]
        public IActionResult EMyAction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EMyAction(List<CMyModel> model)
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult EMyAction(List<CMyModel> model, List<CMyModel> xx)
        //{
        //    return View();
        //}

        ///////////////////

        [HttpGet]
        public IActionResult FMyAction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FMyAction(List<CMyModel> model, List<CMyModel> xx)
        {
            return View();
        }

        ///////////////////

        [HttpGet]
        public IActionResult GMyAction()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GMyAction(List<CMyModel> model, List<int> xx)
        {
            return View();
        }
    }
}