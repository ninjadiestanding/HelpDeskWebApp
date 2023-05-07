using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeHelpDeskWebApp.Enums;

namespace EmployeeHelpDeskWebApp
{
    public static class WC
    {
        public static string FilePath = @"\files\";

        //public static string AdministratorRole = "Administrator";
        //public static string EmployeeRole = "Employee";

        public static IEnumerable<CriticalityEnum> CriticalityList { get; set; } = Enum.GetValues(typeof(CriticalityEnum)).Cast<CriticalityEnum>().ToList();
        public static IEnumerable<ExecutionStatusEnum> ExecutionStatusList { get; set; } = Enum.GetValues(typeof(ExecutionStatusEnum)).Cast<ExecutionStatusEnum>().ToList();
    }
}
