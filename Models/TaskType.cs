using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models
{
    public class TaskType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Наименование")]
        public string Name { get; set; }
    }
}
