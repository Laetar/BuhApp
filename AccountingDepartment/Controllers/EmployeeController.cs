using AccountingDepartment.DataBase.Context;
using AccountingDepartment.DataBase.Context.Models;
using AccountingDepartment.Web.Models.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingDepartment.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public EmployeeController(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // GET: DepartmentController1
        public async Task<ActionResult> Index()
        {
            var entity = await dbContext.Employees.ToListAsync();
            var model = mapper.Map<List<EmployeeView>>(entity);
            return View(model);
        }

        // GET: DepartmentController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var entity = await dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            var model = mapper.Map<EmployeeView>(entity);
            return View(model);
        }

        // GET: DepartmentController1/Create
        public async Task<ActionResult> Create()
        {
            var departs = await dbContext.Departments.ToListAsync();
            var skill = await dbContext.Skills.ToListAsync();
            IEnumerable<SelectListItem> itemDepartList = departs.Select(x => {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                };
            });

            IEnumerable<SelectListItem> itemSkillList = skill.Select(x => {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                };
            });

            ViewBag.Departments = itemDepartList;
            ViewBag.Skills = itemSkillList;

            return View();
        }

        // POST: DepartmentController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeView model)
        {
            try
            {
                var department = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == model.DepartmentId);

                List<Skill> skills = model.SkillIds.Any() ?
                    await dbContext.Skills.Where(x => model.SkillIds.Contains(x.Id)).ToListAsync()
                    : new();

                var entity = mapper.Map<Employee>(model);

                entity.Department = department;
                entity.Skills = skills;

                dbContext.Employees.Add(entity);
                await dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }
        }

        // GET: DepartmentController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var entity = await dbContext.Employees.Include(x => x.Department).Include(x => x.Skills).FirstOrDefaultAsync(x => x.Id == id);
            var model = mapper.Map<EmployeeView>(entity);
            var skill = await dbContext.Skills.ToListAsync();

            var departs = await dbContext.Departments.ToListAsync();
            IEnumerable<SelectListItem> itemList = departs.Select(x => {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (model.DepartmentId == x.Id)
                };
            });

            IEnumerable<SelectListItem> itemSkillList = skill.Select(x => {
                return new SelectListItem()
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = model.SkillIds != null && model.SkillIds.Any(y => y == x.Id)
                };
            });

            ViewBag.Departments = itemList;
            ViewBag.Skills = itemSkillList;

            return View(model);
        }

        // POST: DepartmentController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeView entity)
        {
            try
            {
                var dbEntity = await dbContext.Employees.Include(x => x.Skills).FirstOrDefaultAsync(x => x.Id == id);

                var skills = await dbContext.Skills.Where(x => entity.SkillIds.Any(y => y == x.Id)).ToListAsync();
                var department = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == entity.DepartmentId);

                if (dbEntity == null)
                {
                    return View("Error");
                }

                mapper.Map<EmployeeView, Employee>(entity, dbEntity);
                dbEntity.Department = department;
                dbEntity.Skills = skills;

                await dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }
        }

        // POST: DepartmentController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var entity = new Employee()
                {
                    Id = id
                };

                dbContext.Remove(entity);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }
        }
    }
}
