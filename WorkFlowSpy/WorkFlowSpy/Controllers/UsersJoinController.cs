using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkFlowManager;
using WorkFlowManager.ModelsWFM;
using WorkFlowSpy.Models;
using WorkFlowSpy.Tools;

namespace WorkFlowSpy.Controllers
{
    public class UsersJoinController : Controller
    {
        private WorkFlowService wfs = new WorkFlowService("WorkFlowDbConnection");

        // GET: /UsersJoin/
        public ActionResult List()
        {
            List<EmployeeViewModel> viewEmployees = new List<EmployeeViewModel>();
            IEnumerable<EmployeeWFM> gottenEmployees = null;

            gottenEmployees = wfs.GetAllEmployees();
            if (gottenEmployees != null)
            {
                viewEmployees = new ViewModelConverter().CreateEmployeeRange(gottenEmployees);
            }

            return View(viewEmployees);
        }

        // GET: /UsersJoin/Create
        public ActionResult Create()
        {
            EmployeeViewModel employeeView = new EmployeeViewModel();
            employeeView.IdentityInfo = new ViewModelConverter().GetUserInfo();
            return View(employeeView);
        }

        // POST: /UsersJoin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="HolderCode,FirstName,LastName,HiredDate,Position,Terminated,IdentityId,IdentityFirstName,IdentityLastName")] EmployeeViewModel employeeviewmodel)
        {
            if (ModelState.IsValid)
            {
                EmployeeWFM inputEmployee = DataMapperView.DoMapping<EmployeeViewModel, EmployeeWFM>(employeeviewmodel);
                wfs.SaveEmployee(inputEmployee);
                return RedirectToAction("List");
            }

            return View(employeeviewmodel);
        }

        // GET: /UsersJoin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel employeeView = null;
            EmployeeWFM gottenEmployee = wfs.GetEmployeeByID((int)id);
            if (gottenEmployee!=null)
            {
                employeeView = new ViewModelConverter().CreateEmployee(gottenEmployee);
            }
            if (employeeView == null)
            {
                return HttpNotFound();
            }
            employeeView.IdentityInfo = new ViewModelConverter().GetUserInfo();
            return View(employeeView);
        }

        // POST: /UsersJoin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GUID,HolderCode,FirstName,LastName,HiredDate,Position,Terminated,IdentityId,IdentityFirstName,IdentityLastName")] EmployeeViewModel employeeView)
        {
            if (ModelState.IsValid)
            {
                EmployeeWFM employeeWFM = DataMapperView.DoMapping<EmployeeViewModel, EmployeeWFM>(employeeView);
                wfs.SaveEmployee(employeeWFM);
                return RedirectToAction("List");
            }

            return View(employeeView);
        }

        // GET: /UsersJoin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeViewModel employeeView = null;
            EmployeeWFM gottenEmployee = wfs.GetEmployeeByID((int)id);
            if (gottenEmployee != null)
            {
                employeeView = new ViewModelConverter().CreateEmployee(gottenEmployee);
            }
            if (employeeView == null)
            {
                return HttpNotFound();
            }
            return View(employeeView);
        }

        // POST: /UsersJoin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            wfs.RemoveEmployee(id);
            return RedirectToAction("List");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                wfs.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
