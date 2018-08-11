using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentityDBfirstPOC.Models.ViewModels;
using IdentityDBfirstPOC.Repository;
using IdentityDBfirstPOC.Service;

namespace IdentityDBfirstPOC.Controllers
{
    //登入之後才能夠使用的功能
    //http://kevintsengtw.blogspot.com/2013/11/aspnet-mvc.html
    [Authorize]
    public class MoneyController : Controller
    {
        private readonly AccountbookService _accountbookService;
        private readonly UnitOfWork _unitOfWork;

        public MoneyController()
        {
            _unitOfWork = new UnitOfWork();
            _accountbookService = new AccountbookService(_unitOfWork);
        }

        // GET: Money
        public ActionResult Index()
        {
            return View();
        }

        // GET: Money/Details/5
        public ActionResult Details(Guid id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountbook = _accountbookService.GetSingle(id);
            if (accountbook == null)
            {
                return HttpNotFound();
            }

            return View(accountbook);
        }

        // GET: Money/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Money/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TYPE,DATE,AMOUMT,REMARK")]
                                 SpendingTrackerViewModel accountbook)
        {
            if (ModelState.IsValid)
            {
                accountbook.ID = Guid.NewGuid();
                _accountbookService.Add(accountbook);
   
                _unitOfWork.Commit();
                return RedirectToAction("List");
            }

            return View(accountbook);
        }

        // GET: Money/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountbook = _accountbookService.GetSingle(id);
            if (accountbook == null)
            {
                return HttpNotFound();
            }

            return View(accountbook);
        }

        // POST: Money/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TYPE,DATE,AMOUMT,REMARK")]
                                 SpendingTrackerViewModel accountbook)
        {
            if (ModelState.IsValid)
            {
                _accountbookService.Edit(accountbook.ID, accountbook);
                _unitOfWork.Commit();

                return RedirectToAction("List");
            }

            return View(accountbook);
        }


        // POST: Money/Delete/5
        //[HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountbook = _accountbookService.GetSingle(id.Value);
            if (accountbook == null)
            {
                return HttpNotFound();
            }

            return View(accountbook);
        }

        // GET: Money
        public ActionResult List()
        {
            return View(_accountbookService.Lookup());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _accountbookService.Delete(id);
            _unitOfWork.Commit();

            return RedirectToAction("List");
        }

    }
}
