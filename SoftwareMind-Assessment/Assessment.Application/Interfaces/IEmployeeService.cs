using Assessment.API.Models;
using Assessment.Application.Dtos;
using Assessment.Application.Responses;

namespace Assessment.Application.Interfaces
{
    public interface IEmployeeService
    {
        public Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAll();
        public Task<ApiResponse<EmployeeDto>> GetById(int id);
        public Task<ApiResponse<EmployeeDto>> Create(EmployeeDto employee);
        public Task<ApiResponse<EmployeeDto>> Update(int id, EmployeeDto employee);
        public Task<ApiResponse<EmployeeDto>> Delete(int id);
    }
}
