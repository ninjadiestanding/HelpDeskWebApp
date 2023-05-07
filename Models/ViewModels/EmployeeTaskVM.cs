using EmployeeHelpDeskWebApp.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models.ViewModels
{
    public class EmployeeTaskVM
    {
        public EmployeeTask EmployeeTask { get; set; }

        public IEnumerable<SelectListItem> TaskTypeSelectList { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ApplicationUser ResponsibleUser { get; set; }

        public List<FileModel> FileModel { get; set; }

        public List<MessageModel> MessageModel { get; set; }
    }
}
