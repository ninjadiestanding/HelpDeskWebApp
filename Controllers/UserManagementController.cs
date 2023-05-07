using EmployeeHelpDeskWebApp.Data;
using EmployeeHelpDeskWebApp.Models;
using EmployeeHelpDeskWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _db;

        UserManager<ApplicationUser> _userManager;

        public UserManagementController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public IActionResult Index() => View(_userManager.Users.ToList());


        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserVM model = new EditUserVM { Id = user.Id, FullName = user.FullName, Position = user.Position, Blocked = (bool)user.Blocked };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVM model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.Position = model.Position;
                    user.Blocked = model.Blocked;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
