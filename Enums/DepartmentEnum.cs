using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Enums
{
    public enum DepartmentEnum
    {
        [Display(Name = "Главное здание, ул. Академика Сахарова, д. 23")]
        [Description("Главное здание, ул. Академика Сахарова, д. 23")]
        Главноездание = 1,
        [Display(Name = "Второе отделение, ул. Ломжинская, д. 5")]
        [Description("Второе отделение, ул. Ломжинская, д. 5")]
        Второеотделение = 2,
        [Display(Name = "Женская консультация № 11, ул. Хайдара Бигичева, д. 13а")]
        [Description("Женская консультация № 11, ул. Хайдара Бигичева, д. 13а")]
        Женскаяконсультация = 3,
        [Display(Name = "Офис врача общей практики, ул. Грачиная, д. 50А")]
        [Description("Офис врача общей практики, ул. Грачиная, д. 50А")]
        Офисврача = 4
    }
}
