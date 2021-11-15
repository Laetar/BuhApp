using AccountingDepartment.DataBase.Context;
using AccountingDepartment.DataBase.Context.Models;
using AccountingDepartment.Web.Models.Department;
using AccountingDepartment.Web.Models.Employee;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingDepartment.Web.Controllers
{
    public class DepartmentController : Controller
    {
       
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public DepartmentController(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        // GET: DepartmentController1
        public async Task<ActionResult> Index()
        {
            var entities = await dbContext.Departments.ToListAsync();


            var model = mapper.Map<List<DepartmentView>>(entities);
            return View(model);
        }

        // GET: DepartmentController1/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var depart = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            var users = await dbContext.Employees.Include(x => x.Department)
                .Where(x => x.Department == depart).ToListAsync(); 

            var model = new DepartmentDetailsView()
            {
                Id = id,
                Employees = mapper.Map<List<EmployeeView>>(users),
                Name = depart.Name
            };

            return View(model);
        }

        // GET: DepartmentController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DepartmentView model)
        {
            try
            {
                var entity = mapper.Map<Department>(model);
                await dbContext.Departments.AddAsync(entity);

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
            var dbEntity = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
            var model = mapper.Map<DepartmentView>(dbEntity);

            return View(model);
        }

        // POST: DepartmentController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DepartmentView entity)
        {
            try
            {
                var dbEntity = await dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);

                if (dbEntity == null)
                {
                    return View("Error");
                }

                mapper.Map<DepartmentView, Department>(entity, dbEntity);

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var entity = dbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
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
