using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLPr.Models;

namespace QLPr.Controllers
{
    public class AssignTasksController : Controller
    {
        private Model_Project_Context db = new Model_Project_Context();

        // GET: AssignTasks
        public ActionResult Index(string searchString, string depa, string comp)
        {
            var ass = from a in db.AssignTasks
                      join cl in db.Clients
                      on a.ClientId equals cl.ClientId
                      join e in db.Employees
                      on a.EmployeeId equals e.EmployeeId
                      join p in db.Projects
                      on a.ProjectId equals p.ProjecttId
                      select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                ass = ass.Where(cl => cl.Client.ClientName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchString)) {
                ass = ass.Where(e => e.Employee.EmployeeName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchString)) {
                ass = ass.Where(p => p.Project.ProjectName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchString)) {
                ass = ass.Where(p => p.Project.ProjectStart > DateTime.Now);
            }
            
            var DepartmentList = new List<String>();
            var DepartmentSQL = from dep in db.Employees
                                orderby dep.EmployeeId
                                select dep.EmployeeDepartment;
            DepartmentList.AddRange(DepartmentSQL.Distinct());
            ViewBag.depa = new SelectList(DepartmentList);


            var CompanyList = new List<String>();
            var CompanySql = from com in db.Clients
                             orderby com.ClientId
                             select com.ClientCompany;
            CompanyList.AddRange(CompanySql.Distinct());
            ViewBag.comp = new SelectList(CompanyList);




            var assignTasks = db.AssignTasks.Include(a => a.Client).Include(a => a.Employee);
            return View(assignTasks.ToList());
        }

        // GET: AssignTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            return View(assignTask);
        }

        // GET: AssignTasks/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        // POST: AssignTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignTaskId,EmployeeId,ClientId,ProjectId,Task,Note")] AssignTask assignTask)
        {
            if (ModelState.IsValid)
            {
                db.AssignTasks.Add(assignTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            return View(assignTask);
        }

        // GET: AssignTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            return View(assignTask);
        }

        // POST: AssignTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignTaskId,EmployeeId,ClientId,ProjectId,Task,Note")] AssignTask assignTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "ClientName", assignTask.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", assignTask.EmployeeId);
            return View(assignTask);
        }

        // GET: AssignTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTask assignTask = db.AssignTasks.Find(id);
            if (assignTask == null)
            {
                return HttpNotFound();
            }
            return View(assignTask);
        }

        // POST: AssignTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignTask assignTask = db.AssignTasks.Find(id);
            db.AssignTasks.Remove(assignTask);
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
