using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }
        public string GuidName { get; set; }
        public string Name { get; set; }


        public int EmployeeTaskId { get; set; }
        [ForeignKey("EmployeeTaskId")]
        public EmployeeTask EmployeeTask { get; set; }
    }
}
