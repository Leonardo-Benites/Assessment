using Assessment.Application.Dtos;
using Assessment.Application.Responses;
using Microsoft.AspNetCore.Http;

namespace Assessment.Application.Interfaces
{
    public interface IEmployeeService
    {
        public Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAll();
        public Task<ApiResponse<EmployeeDto>> GetById(int id);
        public Task<ApiResponse<EmployeeDto>> Create(EmployeeDto employee, IFormFile? photo);
        public Task<ApiResponse<EmployeeDto>> Update(int id, EmployeeDto employee, IFormFile? photo);
        public Task<ApiResponse<EmployeeDto>> Delete(int id);
    }
}
