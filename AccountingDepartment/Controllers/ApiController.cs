using AccountingDepartment.DataBase.Context;
using AccountingDepartment.Web.Models.Department;
using AccountingDepartment.Web.Models.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AccountingDepartment.Web.Controllers
{
    [Route("api/[action]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ApiController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentApiResult>> GetDepartments()
        {
            var employee = await dbContext.Employees.Include(x => x.Department).Include(x => x.Skills).ToListAsync();

            var result = employee.GroupBy(x => x.Department.Id, 
                (baseResult, empl) => new DepartmentApiResult()
                {
                    Id = baseResult,
                    Name = empl.First().Department.Name,
                    Employees = mapper.Map<List<EmployeeApiResult>>(empl)
                }
             );

            return result;
        }

    }
}
