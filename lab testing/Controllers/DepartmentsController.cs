using lab_testing.Model;
using lab_testing.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace lab_testing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await departmentRepository.GetDepartments());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            try
            {
                var result = await departmentRepository.GetDepartment(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            try
            {
                if (department == null)
                    return BadRequest("Department cannot be null");

                var createdDepartment = await departmentRepository.AddDepartment(department);
                return CreatedAtAction(nameof(GetDepartment), new { id = createdDepartment.DepartmentId }, createdDepartment);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new department record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, Department department)
        {
            try
            {
                if (id != department.DepartmentId)
                    return BadRequest("Department ID mismatch");

                var updatedDepartment = await departmentRepository.UpdateDepartment(department);
                if (updatedDepartment == null)
                    return NotFound($"Department with Id = {id} not found");

                return updatedDepartment;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            try
            {
                var success = await departmentRepository.DeleteDepartment(id);
                if (!success)
                    return NotFound($"Department with Id = {id} not found");

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting department");
            }
        }
    }
}
