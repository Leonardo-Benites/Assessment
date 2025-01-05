using Microsoft.AspNetCore.Mvc;
using Assessment.Application.Interfaces;
using Assessment.Application.Responses;
using Assessment.Application.Dtos;
using Assessment.API.Models; 

namespace Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<Employee>>> GetEmployees()
        {
            try
            {
                var response = await _employeeService.GetAll();
                
                if (!response.Success)
                {
                    return StatusCode(response.Code, response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return ApiResponse<Employee>.ErrorResponse();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Employee>>> GetEmployee(int id)
        {
            try
            {
                var response = await _employeeService.GetById(id);

                if (!response.Success)
                {
                    return StatusCode(response.Code, response); 
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return ApiResponse<Employee>.ErrorResponse();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Employee>>> PutEmployee(int id, [FromBody] EmployeeDto employeeUpdateDto)
        {
            try
            {
                var response = await _employeeService.Update(id, employeeUpdateDto);

                if (!response.Success)
                {
                    return StatusCode(response.Code, response); 
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return ApiResponse<Employee>.ErrorResponse();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<Employee>>> PostEmployee([FromBody] EmployeeDto employeeCreateDto)
        {
            try
            {
                var response = await _employeeService.Create(employeeCreateDto);

                if (!response.Success)
                {
                    return StatusCode(response.Code, response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return ApiResponse<Employee>.ErrorResponse();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Employee>>> DeleteEmployee(int id)
        {
            try
            {
                var response = await _employeeService.Delete(id);

                if (!response.Success)
                {
                    return StatusCode(response.Code, response); 
                }

                return Ok(response); 
            }
            catch (Exception)
            {
                return ApiResponse<Employee>.ErrorResponse();
            }
        }
    }
}
