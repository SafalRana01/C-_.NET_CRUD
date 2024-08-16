using FirstCrudProject.DAL; // Importing the namespace for Data Access Layer
using FirstCrudProject.Models; // Importing the namespace where the models are defined
using FirstCrudProject.Models.DBEntities;
using FirstCrudProject.Services;
using Microsoft.AspNetCore.Mvc; // Importing the namespace for ASP.NET Core MVC functionalities
using System.ComponentModel.DataAnnotations; // Importing the namespace for data annotations

namespace FirstCrudProject.Controllers // Defining the namespace for controllers
{
    // Defining the EmployeeController class which inherits from the Controller base class
    public class EmployeeController : Controller
    {
        // Private readonly field to hold the database context instance
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeService _employeeService;


        //ApplicationDbContext context works as dependency injection 
        // Constructor for EmployeeController with dependency injection of ApplicationDbContext
        public EmployeeController(ApplicationDbContext context, IEmployeeService employeeService)
        {
            _context = context; // Assigning the injected context to the private field
            _employeeService = employeeService;
        }

        // Action method for handling GET requests to the index page
        public async Task<IActionResult> Index()
        {
            var employeeList = await _employeeService.GetEmployees();
            return View(employeeList);
        }


        [HttpGet]
        // by default it willl be httpget even when we don't specify do we have write httpGet
        public IActionResult Create() 
        {
            // display the creation form only
            return View();
        }


        // after submit button this create is called from action button with method post
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _employeeService.CreateEmployee(employeeViewModel);
                    if (result) 
                    {
                        TempData["successMessage"] = "Employee created successfully";
                        return RedirectToAction("Index");
                    } else {
                        TempData["errorMessage"] = "Model data is not valid";
                        return View();
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();

                }
                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {

                var employee = _context.Employees.SingleOrDefault(x => x.Id == id);
                if (employee != null)
                {
                    var employeeViewModel = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfbirth = employee.DateOfbirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeViewModel);
                }
                else
                {
                    TempData["errorMessage"] = $"Employee details not available for the id: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");

            }
        }



        //[HttpPost]
        //public IActionResult Edit(int id, EmployeeViewModel employeeViewModel)
        //{
        //    try
        //    {
        //        var employee = _context.Employees.SingleOrDefault(x => x.Id == employeeViewModel.Id);
        //        if (employee != null)
        //        {
        //            employee.FirstName = employeeViewModel.FirstName;
        //            employee.LastName = employeeViewModel.LastName;
        //            employee.DateOfbirth = employeeViewModel.DateOfbirth;
        //            employee.Email = employeeViewModel.Email;
        //            employee.Salary = employeeViewModel.Salary;

        //            _context.Employees.Update(employee);
        //            _context.SaveChanges();

        //            TempData["successMessage"] = "Employee detail edited successfully";
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {

        //            TempData["errorMessage"] = $"Employee details not available for the id: {id}";
        //            return RedirectToAction("Index");

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["errorMessage"] = ex.Message;
        //        return View(employeeViewModel);
        //    }
        //}



        [HttpPost]
        public IActionResult Edit(int id, EmployeeViewModel employeeViewModel)
        {
            try
            {
                if (ModelState.IsValid) 
                {

                    var result = _employeeService.EditEmployee(employeeViewModel);
                    if (result)
                    {
                        TempData["successMessage"] = "Employee detail edited successfully";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Model data is not valid";
                        return View();
                    }     

                }
                else
                {
                    TempData["errorMessage"] = $"Model data is invalid.";
                    return View();

                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
               return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                // Retrieve the employee from the database using the provided ID.
                var employee = _context.Employees.SingleOrDefault(x => x.Id == id);

                // If the employee is not found, set an error message and redirect to the index page.
                if (employee != null)
                {
                    var employeeViewModel = new EmployeeViewModel
                    {
                        Id = employee.Id,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfbirth = employee.DateOfbirth,
                        Email = employee.Email,
                        Salary = employee.Salary
                    };
                    return View(employeeViewModel);
                }
                else
                {
                    TempData["errorMessage"] = $"Employee details not available for the ID: {id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult Delete(int id, EmployeeViewModel employeeViewModel)
        {
            try
            {
                var result = _employeeService.DeleteEmployee(employeeViewModel);
                if (result) 
                {
                    TempData["successMessage"] = "Employee deleted successfully";
                    return RedirectToAction("Index");
                } 
                else 
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }


    }
}
