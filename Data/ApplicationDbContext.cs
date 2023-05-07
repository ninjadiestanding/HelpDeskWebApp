using EmployeeHelpDeskWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<TaskType> TaskType { get; set; }
        public DbSet<EmployeeTask> EmployeeTask { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<MessageModel> MessageModel { get; set; }
        public DbSet<FileModel> FileModel { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsFileModel> NewsFileModel { get; set; }
        public DbSet<KBCategory> KBCategory { get; set; }
        public DbSet<KBFileModel> KBFileModel { get; set; }

    }
}
