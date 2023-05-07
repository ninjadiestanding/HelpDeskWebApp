using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeHelpDeskWebApp.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeHelpDeskWebApp.Models.ViewModels
{
    public class EmployeeTaskListVM
    {
        public IEnumerable<EmployeeTask> EmployeeTaskList { get; set; }
        public IEnumerable<SelectListItem> CriticalityList { get; set; }
        public IEnumerable<SelectListItem> ExecutionStatusList { get; set; }
        public IEnumerable<SelectListItem> ResponsibleUserList { get; set; }

        public string Criticality { get; set; }
        public string ExecutionStatus { get; set; }
        public string Department { get; set; }
        public string ResponsibleUser { get; set; }
    }
}
