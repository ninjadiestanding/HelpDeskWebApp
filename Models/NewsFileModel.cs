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
    public class NewsFileModel
    {
        [Key]
        public int Id { get; set; }
        public string GuidName { get; set; }
        public string Name { get; set; }


        public int NewsId { get; set; }
        [ForeignKey("NewsId")]
        public News News { get; set; }
    }
}
