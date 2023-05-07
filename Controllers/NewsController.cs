using EmployeeHelpDeskWebApp.Data;
using EmployeeHelpDeskWebApp.Extensions;
using EmployeeHelpDeskWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Controllers
{

    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;

        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _env;

        public NewsController(ILogger<NewsController> logger, ApplicationDbContext db, IWebHostEnvironment env)
        {
            _logger = logger;
            _db = db;
            _env = env;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<News> newsList = _db.News.Include(i => i.ApplicationUser).Include(i => i.Files);

            return View(newsList);
        }

        [Authorize]
        public IActionResult Management()
        {
            IEnumerable<News> newsList = _db.News.Include(i => i.ApplicationUser);

            //foreach (var item in newsList)
            //{
            //    item.ApplicationUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == item.ApplicationUserId);
            //}

            return View(newsList);
        }

        [Authorize]
        public IActionResult Create()
        {
            News news = new News();
            return View(news);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news)
        {
            if (ModelState.IsValid)
            {
                news.DateOfWriting = DateTime.Now;
                news.ApplicationUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>());

                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string upload = _env.WebRootPath + WC.FilePath;
                    string extension;

                    foreach (var file in files)
                    {
                        extension = Path.GetExtension(file.FileName);
                        NewsFileModel fileModel = new NewsFileModel()
                        {
                            GuidName = Guid.NewGuid().ToString() + extension,
                            Name = file.FileName,
                            News = news,
                        };

                        news.Files.Add(fileModel);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileModel.GuidName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }

                _db.News.Add(news);
                _db.SaveChanges();

                return RedirectToAction("Management");
            }

            return View();
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.News.Find(id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(News news)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string upload = _env.WebRootPath + WC.FilePath;
                    string extension;

                    foreach (var file in files)
                    {
                        extension = Path.GetExtension(file.FileName);
                        NewsFileModel fileModel = new NewsFileModel()
                        {
                            GuidName = Guid.NewGuid().ToString() + extension,
                            Name = file.FileName,
                            News = news,
                        };

                        news.Files.Add(fileModel);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileModel.GuidName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }

                _db.News.Update(news);
                _db.SaveChanges();
                return RedirectToAction("Management");
            }
            return View();
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var news = _db.News.Find(id);
            news.Files = _db.NewsFileModel.Where(i => i.NewsId == id).ToList();

            if (news == null)
                return NotFound();

            return View(news);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int? id)
        {
            var news = _db.News.Find(id);

            if (news == null)
            {
                return NotFound();
            }

            news.Files = _db.NewsFileModel.Where(i => i.NewsId == id).ToList();


            foreach (var newObj in news.Files)
            {
                string upload = _env.WebRootPath + WC.FilePath;
                var oldFile = Path.Combine(upload, newObj.GuidName);

                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
            }

            _db.News.Remove(news);
            _db.SaveChanges();
            return RedirectToAction("Management");
        }

        //[Authorize]
        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
