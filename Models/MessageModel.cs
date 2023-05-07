using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateOfWriting { get; set; }

        //public List<FileModel> Files { get; set; } = new List<FileModel>();

        public int EmployeeTaskId { get; set; }
        [ForeignKey("EmployeeTaskId")]
        public EmployeeTask EmployeeTask { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
