using FirstCrudProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstCrudProject.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("getEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _employeeService.GetEmployees();
            return result == null ? NotFound() : Ok(result);
            // turnater operator
        }

        [HttpGet("getEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
