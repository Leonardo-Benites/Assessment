using Assessment.API.Models;
using Assessment.Application.Dtos;
using Assessment.Application.Interfaces;
using Assessment.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<DepartmentDto>>> GetDepartments()
        {
            try
            {
                var response = await _departmentService.GetAll();

                if (!response.Success)
                {
                    return StatusCode(response.Code, response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return ApiResponse<DepartmentDto>.ErrorResponse();
            }
        }
    }
}
