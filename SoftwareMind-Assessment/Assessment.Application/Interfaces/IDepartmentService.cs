using Assessment.Application.Dtos;
using Assessment.Application.Responses;

namespace Assessment.Application.Interfaces
{
    public interface IDepartmentService
    {
        public Task<ApiResponse<IEnumerable<DepartmentDto>>> GetAll();
    }
}
