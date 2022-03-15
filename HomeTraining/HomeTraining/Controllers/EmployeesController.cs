using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeTraining.Models;
using HomeTraining.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HomeTraining.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext Db;

        public EmployeesController(ApplicationDbContext Db)
        {
            this.Db = Db;
        }


        public IActionResult Index()
        {
            return View(Db.Employees.Include(e => e.Department).Where(e => !e.IsDelete));
        }

        public IActionResult Details(int id)
        {
            var employee = Db.Employees.Include(e => e.Department).SingleOrDefault(e => e.Id == id);
            employee.Department = Db.Departments.Find(employee.DepartmentId);

            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(Db.Departments,"Id","Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            Db.Employees.Add(employee);
            Db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(Db.Employees.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            var dbEmployee = Db.Employees.Find(employee.Id);
            dbEmployee.Name = employee.Name;
            dbEmployee.Birthdate = employee.Birthdate;
            dbEmployee.DepartmentId = employee.DepartmentId;
            dbEmployee.UpdatedAt= DateTime.Now;

            Db.Update(dbEmployee);
            Db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = Db.Employees.Find(id);
            employee.IsDelete = true;
            employee.UpdatedAt = DateTime.Now;
            
            Db.Update(employee);
            Db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}