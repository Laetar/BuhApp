using AccountingDepartment.DataBase.Context.Models;
using AccountingDepartment.Web.Models.Employee;
using System.Collections.Generic;

namespace AccountingDepartment.Web.Models.Department
{
    public class DepartmentDetailsView : DepartmentView
    {
        public List<EmployeeView> Employees { get; set; }
    }
}
