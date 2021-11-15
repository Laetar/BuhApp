using System.ComponentModel.DataAnnotations;

namespace AccountingDepartment.Web.Models.Department
{
    public class DepartmentView
    {
        
        public int Id { get; set; }

        [Display(Name ="Название")]
        public string Name { get; set; }
    }
}
