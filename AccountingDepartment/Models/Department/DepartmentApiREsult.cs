using AccountingDepartment.Web.Models.Employee;
using System.Collections.Generic;

namespace AccountingDepartment.Web.Models.Department
{
    public class DepartmentApiResult
    {
        public IEnumerable<EmployeeApiResult> Employees { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
