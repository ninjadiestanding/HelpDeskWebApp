using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models.ViewModels
{
    public class KnowledgeBaseVM
    {
        public IEnumerable<KBCategory> Categories { get; set; }
        public IEnumerable<KBFileModel> Files { get; set; }

        public string SelectedCategoryName { get; set; }
    }
}
