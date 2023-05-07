using EmployeeHelpDeskWebApp.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHelpDeskWebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Полное имя")]
        [Required]
        public string FullName { get; set; }

        [DisplayName("Заблокирован")]
        public bool? Blocked { get; set; } = false;

        [DisplayName("Роль")]
        [Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public UserRoleEnum UserRole { get; set; }

        [DisplayName("Должность")]
        [Required]
        public string Position { get; set; }
    }
}
