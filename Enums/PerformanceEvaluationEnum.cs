using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Enums
{
    public enum PerformanceEvaluationEnum
    {
        Устраивает = 1,
        [Display(Name = "Устраивает частично")]
        УстраиваетЧастично = 2,
        [Display(Name = "Не устраивает")]
        Неустраивает = 3
    }
}
