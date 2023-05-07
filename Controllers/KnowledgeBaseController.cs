using EmployeeHelpDeskWebApp.Data;
using EmployeeHelpDeskWebApp.Extensions;
using EmployeeHelpDeskWebApp.Models;
using EmployeeHelpDeskWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _env;

        public KnowledgeBaseController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [Authorize]
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                KnowledgeBaseVM knowledgeBaseVM = new KnowledgeBaseVM()
                {
                    Categories = _db.KBCategory,
                    Files = null,
                };
                return View(knowledgeBaseVM);
            }
            else
            {
                KnowledgeBaseVM knowledgeBaseVM = new KnowledgeBaseVM()
                {
                    Categories = _db.KBCategory,
                    Files = _db.KBFileModel.Include(i => i.KBCategory).Where(i => i.KBCategoryId == id),
                };
                knowledgeBaseVM.SelectedCategoryName = _db.KBCategory.Find(id).Name;
                return View(knowledgeBaseVM);
            }
        }

        [Authorize]
        public IActionResult Management()
        {
            IEnumerable<KBCategory> taskTypeList = _db.KBCategory;
            return View(taskTypeList);
        }

        [Authorize]
        public IActionResult CreateKBCategory()
        {
            KBCategory category = new KBCategory();
            return View(category);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKBCategory(KBCategory category)
        {
            if (ModelState.IsValid)
            {
                _db.KBCategory.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Management");
            }

            return View();
        }


        [Authorize]
        public IActionResult EditKBCategory(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.KBCategory.Find(id);
            category.Files = _db.KBFileModel.Where(i => i.KBCategoryId == id).ToList();

            if (category == null)
                return NotFound();

            return View(category);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditKBCategory(KBCategory category)
        {
            if (ModelState.IsValid)
            {
                _db.KBCategory.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Management");
            }
            return View(category);
        }

        [Authorize]
        public IActionResult DeleteKBCategory(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.KBCategory.Find(id);
            category.Files = _db.KBFileModel.Where(i => i.KBCategoryId == id).ToList();

            if (category == null)
                return NotFound();

            return View(category);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteKBCategoryConfirm(int? id)
        {
            var category = _db.KBCategory.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            category.Files = _db.KBFileModel.Where(i => i.KBCategoryId == id).ToList();


            foreach (var file in category.Files)
            {
                string upload = _env.WebRootPath + WC.FilePath;
                var oldFile = Path.Combine(upload, file.GuidName);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
            }

            _db.KBCategory.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Management");
        }

        [Authorize]
        public IActionResult AddFile(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.KBCategory.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFile(KBCategory category)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string upload = _env.WebRootPath + WC.FilePath;
                    string extension = Path.GetExtension(files[0].FileName);
                    KBFileModel file = new KBFileModel()
                    {
                        GuidName = Guid.NewGuid().ToString() + extension,
                        DateAdded = DateTime.Now,
                        Name = category.FileName,
                        KBCategory = category,
                    };

                    category.Files.Add(file);

                    using (var fileStream = new FileStream(Path.Combine(upload, file.GuidName), FileMode.Create))
                    {
                        await files[0].CopyToAsync(fileStream);
                    }
                }

                category.FileName = null;
                _db.Update(category);
                _db.SaveChanges();

                return RedirectToAction("EditKBCategory", new { id = category.Id });
            }

            return View();
        }

        [Authorize]
        public IActionResult EditFile(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var file = _db.KBFileModel.Find(id);

            if (file == null)
                return NotFound();

            return View(file);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFile(KBFileModel file)
        {
            if (ModelState.IsValid)
            {
                KBFileModel newFile = _db.KBFileModel.FirstOrDefault(i => i.Id == file.Id);
                newFile.Name = file.Name;
                _db.KBFileModel.Update(newFile);
                _db.SaveChanges();
                return RedirectToAction("EditKBCategory", new { id = newFile.KBCategoryId });
            }
            return View(file);
        }

        [Authorize]
        public IActionResult DeleteFile(int? id)
        {
            if (id != 0)
            {
                KBFileModel file = _db.KBFileModel.Find(id);
                return View(file);
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFileConfirm(int? id)
        {
            if (id != 0)
            {
                KBFileModel file = _db.KBFileModel.Find(id);

                string upload = _env.WebRootPath + WC.FilePath;
                var oldFile = Path.Combine(upload, file.GuidName);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                _db.KBFileModel.Remove(file);
                _db.SaveChanges();
                return RedirectToAction("EditKBCategory", new { id = file.KBCategoryId });
            }
            return View();
        }

    }
}
