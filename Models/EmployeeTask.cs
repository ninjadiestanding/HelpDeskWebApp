using EmployeeHelpDeskWebApp.Enums;
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
    public class EmployeeTask
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateOfCreation { get; set; }
        [DisplayName("Дата закрытия")]
        public DateTime ClosingDate { get; set; }

        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();
        public List<FileModel> Files { get; set; } = new List<FileModel>();




        [DisplayName("Название")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Сообщение")]
        public string Message { get; set; }

        [DisplayName("Кабинет")]
        [Required]
        public string Cabinet { get; set; }



        [DisplayName("Филиал")]
        [Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public DepartmentEnum Department { get; set; }
        [DisplayName("Критичность")]
        [Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public CriticalityEnum Criticality { get; set; }

        [DisplayName("Статус исполнения")]
        [Required]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public ExecutionStatusEnum ExecutionStatus { get; set; }

        [DisplayName("Оценка исполнения")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PerformanceEvaluationEnum PerformanceEvaluation { get; set; }




        [Display(Name = "Тип заявки")]
        public int TaskTypeId { get; set; }
        [ForeignKey("TaskTypeId")]
        public virtual TaskType TaskType { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        
        public string ResponsibleUserId { get; set; }
        [ForeignKey("ResponsibleUserId")]
        public ApplicationUser ResponsibleUser { get; set; }
    }
}
