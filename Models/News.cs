using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Текст")]
        public string Message { get; set; }
        public DateTime DateOfWriting { get; set; }

        public List<NewsFileModel> Files { get; set; } = new List<NewsFileModel>();

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
