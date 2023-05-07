using EmployeeHelpDeskWebApp.Data;
using EmployeeHelpDeskWebApp.Enums;
using EmployeeHelpDeskWebApp.Extensions;
using EmployeeHelpDeskWebApp.Models;
using EmployeeHelpDeskWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHelpDeskWebApp.Controllers
{
    [Authorize]
    public class EmployeeTaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _env;

        public EmployeeTaskController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        [Authorize]
        public IActionResult Index(string searchFullName = null, string searchPhone = null, string Department = null, string Criticality = null,
            string ExecutionStatus = null, string searchId = null, string ResponsibleUser = null)
        {
            EmployeeTaskListVM employeeTaskListVM = new EmployeeTaskListVM();

            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>());

            if (applicationUser.UserRole == UserRoleEnum.Administrator)
            {
                employeeTaskListVM.EmployeeTaskList = _db.EmployeeTask;
                

                foreach (var employeeTask in employeeTaskListVM.EmployeeTaskList)
                {
                    employeeTask.ApplicationUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == employeeTask.ApplicationUserId);
                    employeeTask.ResponsibleUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == employeeTask.ResponsibleUserId);
                }

                employeeTaskListVM.CriticalityList = WC.CriticalityList.ToList().Select(i => new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
                employeeTaskListVM.ExecutionStatusList = WC.ExecutionStatusList.ToList().Select(i => new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });

                List<string> ResponsibleUserFullNameList = _db.ApplicationUser.Where(i => i.UserRole == UserRoleEnum.Administrator).Select(i => i.FullName).ToList();

                employeeTaskListVM.ResponsibleUserList = ResponsibleUserFullNameList.Select(i => new SelectListItem
                {
                    Text = i,
                    Value = i
                });

                if (!string.IsNullOrEmpty(searchId))
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.Id.ToString().Contains(searchId));
                }
                if (!string.IsNullOrEmpty(searchFullName))
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.ApplicationUser.FullName.ToLower().Contains(searchFullName.ToLower()));
                }
                if (!string.IsNullOrEmpty(searchPhone))
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.ApplicationUser.PhoneNumber.ToLower().Contains(searchPhone.ToLower()));
                }
                if (!string.IsNullOrEmpty(Department) && Department != "- Филиал -")
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.Department.ToString().Contains(Enum.GetName(typeof(DepartmentEnum), Convert.ToInt32(Department))));
                }
                if (!string.IsNullOrEmpty(Criticality) && Criticality != "- Критичность -")
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.Criticality.ToString().Contains(Criticality));
                }
                if (!string.IsNullOrEmpty(ExecutionStatus) && ExecutionStatus != "- Статус исполнения -")
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.ExecutionStatus.ToString().Contains(ExecutionStatus));
                }
                if (!string.IsNullOrEmpty(ResponsibleUser) && ResponsibleUser != "- Ответственный -")
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.ResponsibleUser.FullName.Contains(ResponsibleUser));
                }
            }
            else
            {
                employeeTaskListVM.EmployeeTaskList = _db.EmployeeTask.Where(i => i.ApplicationUserId == applicationUser.Id);

                foreach (var employeeTask in employeeTaskListVM.EmployeeTaskList)
                {
                    employeeTask.ApplicationUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == employeeTask.ApplicationUserId);
                    employeeTask.ResponsibleUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == employeeTask.ResponsibleUserId);
                }

                employeeTaskListVM.ExecutionStatusList = WC.ExecutionStatusList.ToList().Select(i => new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });

                if (!string.IsNullOrEmpty(searchId))
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.Id.ToString().Contains(searchId));
                }   
                if (!string.IsNullOrEmpty(ExecutionStatus) && ExecutionStatus != "- Статус исполнения -")
                {
                    employeeTaskListVM.EmployeeTaskList = employeeTaskListVM.EmployeeTaskList.Where(u => u.ExecutionStatus.ToString().ToLower().Contains(ExecutionStatus.ToLower()));
                }
            }

            return View(employeeTaskListVM);
        }

        [Authorize]
        public IActionResult Create()
        {
            EmployeeTaskVM employeeTaskVM = new EmployeeTaskVM()
            {
                EmployeeTask = new EmployeeTask() 
                {
                    ApplicationUserId = User.GetLoggedInUserId<string>()
                },
                TaskTypeSelectList = _db.TaskType.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ApplicationUser = new ApplicationUser()
            };

            employeeTaskVM.ApplicationUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>());

            return View(employeeTaskVM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeTask employeetask)
        {
            if (ModelState.IsValid)
            {
                MessageModel messageModel = new MessageModel()
                {
                    Message = employeetask.Message,
                    DateOfWriting = DateTime.Now,
                    ApplicationUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>()),
                };

                employeetask.Messages.Add(messageModel);

                _db.EmployeeTask.Add(employeetask);
                _db.SaveChanges();

                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string upload = _env.WebRootPath + WC.FilePath;
                    string extension;

                    foreach (var file in files)
                    {
                        extension = Path.GetExtension(file.FileName);
                        FileModel fileModel = new FileModel()
                        {
                            GuidName = Guid.NewGuid().ToString() + extension,
                            Name = file.FileName,
                            EmployeeTask = employeetask,
                        };

                        employeetask.Files.Add(fileModel);

                        using (var fileStream = new FileStream(Path.Combine(upload, fileModel.GuidName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                }

                employeetask.DateOfCreation = DateTime.Now;
                _db.Update(employeetask);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize]
        public IActionResult FirstView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                EmployeeTaskVM employeeTaskVM = new EmployeeTaskVM()
                {
                    EmployeeTask = _db.EmployeeTask.Find(id),
                    TaskTypeSelectList = _db.TaskType.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    ApplicationUser = new ApplicationUser(),
                    FileModel = _db.FileModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    MessageModel = _db.MessageModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    ResponsibleUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>())
                };

                employeeTaskVM.EmployeeTask.Message = null;
                employeeTaskVM.ApplicationUser = _db.ApplicationUser.Find(employeeTaskVM.EmployeeTask.ApplicationUserId);

                foreach (var item in employeeTaskVM.MessageModel)
                {
                    item.ApplicationUser = _db.ApplicationUser.Find(item.ApplicationUserId);
                }

                return View(employeeTaskVM);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FirstView(EmployeeTaskVM employeeTaskVM)
        {
            EmployeeTask newEmployeeTask = _db.EmployeeTask.FirstOrDefault(i => i.Id == employeeTaskVM.EmployeeTask.Id);

            if (newEmployeeTask == null)
            {
                return NotFound();
            }
            else
            {
                newEmployeeTask.ExecutionStatus = ExecutionStatusEnum.Выполняется;
                newEmployeeTask.ResponsibleUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>());
                _db.Update(newEmployeeTask);
                _db.SaveChanges();
                return RedirectToAction("Edit", new { id = newEmployeeTask.Id });
            }
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                EmployeeTaskVM employeeTaskVM = new EmployeeTaskVM()
                {
                    EmployeeTask = _db.EmployeeTask.Find(id),
                    TaskTypeSelectList = _db.TaskType.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    ApplicationUser = new ApplicationUser(),
                    FileModel = _db.FileModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    MessageModel = _db.MessageModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    ResponsibleUser = new ApplicationUser(),
                };

                employeeTaskVM.EmployeeTask.Message = null;
                employeeTaskVM.ApplicationUser = _db.ApplicationUser.Find(employeeTaskVM.EmployeeTask.ApplicationUserId);
                employeeTaskVM.ResponsibleUser = _db.ApplicationUser.Find(employeeTaskVM.EmployeeTask.ResponsibleUserId);

                foreach (var item in employeeTaskVM.MessageModel)
                {
                    item.ApplicationUser = _db.ApplicationUser.Find(item.ApplicationUserId);
                }

                return View(employeeTaskVM);
            }      
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeTaskVM employeeTaskVM)
        {
            if (User.IsInRole(UserRoleEnum.Administrator.ToString()))
            {
                EmployeeTask newEmployeeTask = _db.EmployeeTask.AsNoTracking().FirstOrDefault(i => i.Id == employeeTaskVM.EmployeeTask.Id);

                if (newEmployeeTask == null)
                {
                    return NotFound();
                }
                else
                {
                    if (employeeTaskVM.EmployeeTask.Message != null)
                    {
                        MessageModel messageModel = new MessageModel()
                        {
                            Message = employeeTaskVM.EmployeeTask.Message,
                            DateOfWriting = DateTime.Now,
                            ApplicationUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>()),
                        };

                        newEmployeeTask.Messages.Add(messageModel);
                        employeeTaskVM.EmployeeTask.Message = null;
                    }
                    

                    newEmployeeTask.ExecutionStatus = employeeTaskVM.EmployeeTask.ExecutionStatus;

                    var files = HttpContext.Request.Form.Files;

                    if (files.Count > 0)
                    {
                        string upload = _env.WebRootPath + WC.FilePath;
                        string extension;

                        foreach (var file in files)
                        {
                            extension = Path.GetExtension(file.FileName);
                            FileModel fileModel = new FileModel()
                            {
                                GuidName = Guid.NewGuid().ToString() + extension,
                                Name = file.FileName,
                                EmployeeTask = newEmployeeTask,
                            };

                            newEmployeeTask.Files.Add(fileModel);

                            using (var fileStream = new FileStream(Path.Combine(upload, fileModel.GuidName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                    }

                    _db.Update(newEmployeeTask);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            if (User.IsInRole(UserRoleEnum.Employee.ToString()))
            {
                if (ModelState.IsValid)
                {
                    if (employeeTaskVM.EmployeeTask.Id == 0)
                    {
                        return NotFound();
                    }
                    else
                    {
                        EmployeeTask newEmployeeTask = _db.EmployeeTask.AsNoTracking().FirstOrDefault(i => i.Id == employeeTaskVM.EmployeeTask.Id);

                        if (employeeTaskVM.EmployeeTask.Message != null)
                        {
                            MessageModel messageModel = new MessageModel()
                            {
                                Message = employeeTaskVM.EmployeeTask.Message,
                                DateOfWriting = DateTime.Now,
                                ApplicationUser = _db.ApplicationUser.Find(User.GetLoggedInUserId<string>()),
                            };

                            newEmployeeTask.Messages.Add(messageModel);
                            newEmployeeTask.Message = null;
                        }

                        var files = HttpContext.Request.Form.Files;

                        if (files.Count > 0)
                        {
                            string upload = _env.WebRootPath + WC.FilePath;
                            string extension;

                            foreach (var file in files)
                            {
                                extension = Path.GetExtension(file.FileName);
                                FileModel fileModel = new FileModel()
                                {
                                    GuidName = Guid.NewGuid().ToString() + extension,
                                    Name = file.FileName,
                                    EmployeeTask = newEmployeeTask,
                                };

                                newEmployeeTask.Files.Add(fileModel);

                                using (var fileStream = new FileStream(Path.Combine(upload, fileModel.GuidName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                }
                            }
                        }

                        _db.Update(newEmployeeTask);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View();
                }
            }

            return View();

        }

        [Authorize]
        public IActionResult Close(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                EmployeeTaskVM employeeTaskVM = new EmployeeTaskVM()
                {
                    EmployeeTask = _db.EmployeeTask.Find(id),
                    TaskTypeSelectList = _db.TaskType.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    ApplicationUser = new ApplicationUser(),
                    FileModel = _db.FileModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    MessageModel = _db.MessageModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    ResponsibleUser = new ApplicationUser(),
                };

                employeeTaskVM.EmployeeTask.Message = null;
                employeeTaskVM.ApplicationUser = _db.ApplicationUser.Find(employeeTaskVM.EmployeeTask.ApplicationUserId);
                employeeTaskVM.ResponsibleUser = _db.ApplicationUser.Find(employeeTaskVM.EmployeeTask.ResponsibleUserId);

                foreach (var item in employeeTaskVM.MessageModel)
                {
                    item.ApplicationUser = _db.ApplicationUser.Find(item.ApplicationUserId);
                }

                return View(employeeTaskVM);
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Close(EmployeeTaskVM employeeTaskVM)
        {
            var newEmployeeTask = _db.EmployeeTask.Find(employeeTaskVM.EmployeeTask.Id);

            if (newEmployeeTask == null)
            {
                return NotFound();
            }
            else
            {
                newEmployeeTask.PerformanceEvaluation = employeeTaskVM.EmployeeTask.PerformanceEvaluation;
                newEmployeeTask.ExecutionStatus = ExecutionStatusEnum.Закрыто;
                newEmployeeTask.ClosingDate = DateTime.Now;
                _db.EmployeeTask.Update(newEmployeeTask);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                EmployeeTaskVM employeeTaskVM = new EmployeeTaskVM()
                {
                    EmployeeTask = _db.EmployeeTask.Find(id),
                    TaskTypeSelectList = _db.TaskType.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                    ApplicationUser = new ApplicationUser(),
                    FileModel = _db.FileModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    MessageModel = _db.MessageModel.Where(i => i.EmployeeTaskId == id).ToList(),
                    ResponsibleUser = new ApplicationUser(),
                };

                employeeTaskVM.EmployeeTask.Message = null;
                employeeTaskVM.ApplicationUser = _db.ApplicationUser.Find(employeeTaskVM.EmployeeTask.ApplicationUserId);
                employeeTaskVM.ResponsibleUser = _db.ApplicationUser.Find(employeeTaskVM.EmployeeTask.ResponsibleUserId);

                foreach (var item in employeeTaskVM.MessageModel)
                {
                    item.ApplicationUser = _db.ApplicationUser.Find(item.ApplicationUserId);
                }

                return View(employeeTaskVM);
            }
        }

        [Authorize]
        public FileResult DownloadFile(string filename)
        {
            string path = _env.WebRootPath + WC.FilePath + filename;
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "application/octet-stream", filename);
        }
    }
}
