using AccountingDepartment.DataBase.Context.Models;
using AccountingDepartment.Web.Models.Department;
using AccountingDepartment.Web.Models.Employee;
using AutoMapper;
using System.Linq;

namespace AccountingDepartment.Web.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentView>();

            CreateMap<DepartmentView, Department>();

            CreateMap<Employee, EmployeeView>()
                .ForMember(x => x.DepartmentId, 
                    opt=> opt.MapFrom(dist => dist.Department == null ? 0 : dist.Department.Id))
                .ForMember(x => x.SkillIds,
                    opt => opt.MapFrom(dist => dist.Skills.Select(x => x.Id)));

            CreateMap<EmployeeView, Employee>()
                .ForMember(x => x.Department, 
                    opt => opt.MapFrom(dist => new Department() { Id = dist.DepartmentId }));

            CreateMap<Employee, EmployeeApiResult>()
                .ForMember(x => x.Skills,
                    opt => opt.MapFrom(dist => dist.Skills.Select(x => x.Name)));
        }
    }
}
