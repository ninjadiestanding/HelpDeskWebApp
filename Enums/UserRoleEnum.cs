using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Enums
{
    public enum UserRoleEnum
    {
        [Display(Name = "Администратор")]
        Administrator = 1,
        [Display(Name = "Сотрудник")]
        Employee = 2
    }
}
