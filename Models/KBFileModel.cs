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
    public class KBFileModel
    {
        [Key]
        public int Id { get; set; }
        public string GuidName { get; set; }
        [DisplayName("Наименование")]
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }


        public int KBCategoryId { get; set; }
        [ForeignKey("KBCategoryId")]
        public KBCategory KBCategory { get; set; }
    }
}
