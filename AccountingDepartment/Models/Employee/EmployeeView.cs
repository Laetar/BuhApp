using AccountingDepartment.Web.Models.Department;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountingDepartment.Web.Models.Employee
{
    public class EmployeeView
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отдел")]
        public int DepartmentId { get; set; }

        [Display(Name = "Зарплата")]
        public double Salary { get; set; }

        [Display(Name = "Навыки")]
        public IEnumerable<int> SkillIds { get; set; }
    }
}
