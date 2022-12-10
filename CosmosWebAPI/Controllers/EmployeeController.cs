using CosmosWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CosmosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        private IEnumerable<Employee> _employees;
        //static int count = 1;
        public EmployeeController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            var employees = await _cosmosDbService.GetMultipleAsync();
            return employees.ToList();
        }

        //without Async keyword
        //[HttpGet("GetEmployeeDetails")]
        //public ActionResult<Employee> GetEmployeeDetails(string id)
        //{
        //    return _cosmosDbService.GetAsync(id).GetAwaiter().GetResult();
        //    //return await _cosmosDbService.GetAsync(id);
        //}


        [HttpGet("GetEmployeeDetails")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeDetails(int empid)
        {
            var employees = await _cosmosDbService.GetEmpAsync(empid);
            return employees.ToList();
        }



        [HttpPost("AddNewEmployee")]
        public async Task<ActionResult> AddNewEmployee(Employee emp)
        {
            //await _cosmosDbService.
            Employee employee = new Employee();
            employee.id = Guid.NewGuid().ToString();
            employee.EmployeeId = emp.EmployeeId;
            employee.EmployeeName = emp.EmployeeName;
            employee.Department = emp.Department;
            employee.Salary = emp.Salary;
            //employee.id = EmployeeController.count.ToString();
            await _cosmosDbService.AddAsync(employee);
            //count += 1;
            return Ok("Employee is added successfully!!!");
        }
        
        
        
        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult> UpdateEmployee(string id,string Department,int Salary)
        {
            Employee employee = await _cosmosDbService.GetAsync(id);
            employee.Salary = Salary;
            employee.Department = Department;
            await _cosmosDbService.UpdateAsync(id, employee);
            return Ok("Employee Details Updated Successfully!!");
        }
        
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await _cosmosDbService.DeleteAsync(id);
            return Ok("Employee Deleted From the Database Successfully!!!");
        }
        /*
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.id == id);
        }
        */ 

    }
}
