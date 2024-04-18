using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YourProjectName.Models;

namespace YourProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"CALL create_employee({employee.Name}, {employee.Email}, {employee.Department})");

            return Ok("Employee created successfully.");
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"CALL update_employee({id}, {employee.Name}, {employee.Email}, {employee.Department})");

            return Ok("Employee updated successfully.");
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync(
                $"CALL delete_employee({id})");

            return Ok("Employee deleted successfully.");
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employees = await _context.Employees
                .FromSqlInterpolated($"SELECT * FROM get_employee_by_id({id})")
                .ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return NotFound("Employee not found.");
            }

            return Ok(employees[0]); // Return the first employee (should be only one)
        }
    }
}
