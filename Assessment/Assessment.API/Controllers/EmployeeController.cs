using Microsoft.AspNetCore.Mvc;
using Assessment.Application.Interfaces;
using Assessment.Application.Responses;
using Assessment.Application.Dtos;

namespace Assessment.API.Controllers
{
    //[Authorize]
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
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetEmployees()
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
                return ApiResponse<EmployeeDto>.ErrorResponse();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> GetEmployee(int id)
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
                return ApiResponse<EmployeeDto>.ErrorResponse();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> PutEmployee(int id, [FromForm] EmployeeDto employeeDto, IFormFile? photo)
        {
            try
            {
                var response = await _employeeService.Update(id, employeeDto, photo);

                if (!response.Success)
                {
                    return StatusCode(response.Code, response); 
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return ApiResponse<EmployeeDto>.ErrorResponse();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> PostEmployee([FromForm] EmployeeDto employeeDto, IFormFile? photo)
        {
            try
            {
                var response = await _employeeService.Create(employeeDto, photo);

                if (!response.Success)
                {
                    return StatusCode(response.Code, response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return ApiResponse<EmployeeDto>.ErrorResponse();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<EmployeeDto>>> DeleteEmployee(int id)
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
                return ApiResponse<EmployeeDto>.ErrorResponse();
            }
        }
    }
}
