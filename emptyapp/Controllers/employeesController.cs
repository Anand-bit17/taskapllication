using emptyapp.Data;
using emptyapp.Models;
using emptyapp.Models.domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emptyapp.Controllers
{
    public class employeesController : Controller
    {
        private readonly MvcdemoDBContext mvcdemoDBContext;
        public employeesController(MvcdemoDBContext mvcdemoDBContext)
        {
            this.mvcdemoDBContext = mvcdemoDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Employees = await mvcdemoDBContext.employees.ToListAsync();
            return View(Employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddemployeeviewModel addEmployeeRequest)
        {
            var employee = new employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                salary = addEmployeeRequest.salary,
                Department = addEmployeeRequest.Department,
                Dateofbirth = addEmployeeRequest.Dateofbirth
                
            };
            await mvcdemoDBContext.employees.AddAsync(employee);
            await mvcdemoDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult>view(Guid Id)
        {
          var employee= await mvcdemoDBContext.employees.FirstOrDefaultAsync(x => x.Id == Id);
            if(employee!=null)
            {
                var viewModel = new updateemployeeviewmodel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    salary = employee.salary,
                    Department = employee.Department,
                    Dateofbirth = employee.Dateofbirth

                };
                return await Task.Run(()=>View("view",viewModel));

            }
           
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(updateemployeeviewmodel model)
        {
            var employee = await mvcdemoDBContext.employees.FindAsync(model.Id);
            if(employee!=null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.salary = model.salary;
                employee.Dateofbirth = model.Dateofbirth;
                employee.Department = model.Department;

               await mvcdemoDBContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult>Delete(updateemployeeviewmodel model)
        {
            var employee = mvcdemoDBContext.employees.Find(model.Id);
            if(employee!=null)
            {
                mvcdemoDBContext.employees.Remove(employee);
                await mvcdemoDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
