using FirstCrudProject.DAL;
using FirstCrudProject.Models;
using FirstCrudProject.Models.DBEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Serilog.Data;
using System.Reflection.Metadata.Ecma335;

namespace FirstCrudProject.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> GetEmployees();

        Task<EmployeeViewModel?> GetEmployeeById(int id);



        bool CreateEmployee(EmployeeViewModel employeeViewModel);
        bool EditEmployee(EmployeeViewModel employeeViewModel);

        bool DeleteEmployee(EmployeeViewModel employeeViewModel);

    }
    public class EmployeeService : IEmployeeService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public EmployeeService(ApplicationDbContext context, ILogger<EmployeeService> logger)
        {
            _context = context;
            _logger = logger;
        }



        public async Task<List<EmployeeViewModel>> GetEmployees()
        {
            // Creating a list to hold EmployeeViewModel instances
            List<EmployeeViewModel> employeeViewModelList = new List<EmployeeViewModel>();

            // Retrieving all employees from the database and converting them to a list
            var employees = await _context.Employees.ToListAsync();

            // Checking if the employees list is not null
            if (employees != null)
            {
                // Iterating over each employee in the list
                foreach (var emp in employees)
                {
                    // Creating a new EmployeeViewModel instance and setting its properties
                    var employee = new EmployeeViewModel()
                    {
                        Id = emp.Id,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        Email = emp.Email,
                        DateOfbirth = emp.DateOfbirth,
                        Salary = emp.Salary,
                    };

                    // Adding the EmployeeViewModel instance to the list
                    employeeViewModelList.Add(employee);
                }
            }
            return employeeViewModelList;   

            
        }


        public async Task<EmployeeViewModel?> GetEmployeeById(int id)
        {
            try
            {
                var dbResult = await _context.Employees.SingleOrDefaultAsync(r => r.Id == id);
                if (dbResult != null)
                {
                    return new EmployeeViewModel
                    {
                        FirstName = dbResult.FirstName,
                        LastName = dbResult.LastName,
                        DateOfbirth = dbResult.DateOfbirth,
                        Salary = dbResult.Salary,
                        Email = dbResult.Email
                    };
                }
                return null;

            }
            catch (Exception ex)
            {
                _logger.LogError($"EmployeeService-GetEmployeeById -- {ex.Message}");
                return null;
            }
        }


        public bool CreateEmployee(EmployeeViewModel employeeViewModel) 
        {

            try
            {
                var employee = new Employee()
                {
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    DateOfbirth = employeeViewModel.DateOfbirth,
                    Salary = employeeViewModel.Salary,
                    Email = employeeViewModel.Email
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                // loggin in database
                _logger.LogInformation("EmployeeService - CreateEmployee, Successfull Created");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"(EmployeeService - Method=>CreateEmployee), {ex.Message}");
                return false;
            }

        }


        public bool EditEmployee(EmployeeViewModel employeeViewModel)
        {

            try
            {
                var employee = new Employee()
                {
                    Id = employeeViewModel.Id,
                    FirstName = employeeViewModel.FirstName,
                    LastName = employeeViewModel.LastName,
                    Salary = employeeViewModel.Salary,
                    DateOfbirth = employeeViewModel.DateOfbirth,
                    Email = employeeViewModel.Email

                };

                _context.Employees.Update(employee);
                _context.SaveChanges();

                _logger.LogInformation("EmployeeService - EditEmployee, Successfull Edited");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"(EmployeeService - Method=>EditEmployee), {ex.Message}");
                return false;
            }

            
        }


        public bool DeleteEmployee(EmployeeViewModel employeeViewModel) 
        {

            try
            {

                var employee = _context.Employees.SingleOrDefault(x => x.Id == employeeViewModel.Id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    _context.SaveChanges();
                }
                _logger.LogInformation("EmployeeService - DeleteEmployee, Successfull Deleted");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"(EmployeeService - Method=>DeleteEmployee), {ex.Message}");

                return false;
            }
                  
        
        
        }


    }
}
