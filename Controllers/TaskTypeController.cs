using EmployeeHelpDeskWebApp.Data;
using EmployeeHelpDeskWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Controllers
{
    public class TaskTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<TaskType> taskTypeList = _db.TaskType;
            return View(taskTypeList);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskType tasktype)
        {
            if (ModelState.IsValid)
            {
                _db.TaskType.Add(tasktype);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.TaskType.Find(id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskType tasktype)
        {
            if (ModelState.IsValid)
            {
                _db.TaskType.Update(tasktype);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tasktype);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.TaskType.Find(id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int? id)
        {
            var obj = _db.TaskType.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.TaskType.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
