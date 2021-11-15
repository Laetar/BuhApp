using System.Collections.Generic;

namespace AccountingDepartment.Web.Models.Employee
{
    public class EmployeeApiResult
    {
        public string Name { get; set; }
        public IEnumerable<string> Skills { get; set; }

        public int Salary { get; set; }
    }
}
