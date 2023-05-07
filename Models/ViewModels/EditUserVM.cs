using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models.ViewModels
{
    public class EditUserVM
    {
        public string Id { get; set; }
        [DisplayName("Полное имя")]
        public string FullName { get; set; }
        [DisplayName("Должность")]
        public string Position { get; set; }
        [DisplayName("Заблокирован")]
        public bool Blocked { get; set; }
    }
}
