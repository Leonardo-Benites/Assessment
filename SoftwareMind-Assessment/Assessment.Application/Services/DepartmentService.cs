using Assessment.Application.Dtos;
using Assessment.Application.Interfaces;
using Assessment.Application.Responses;
using Assessment.Infrastructure.Context;
using Assessment.Infrastructure.Repositories;
using AutoMapper;

namespace Assessment.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;
        readonly IMapper _mapper;

        public DepartmentService(AppDbContext context, IMapper mapper) 
        {
            _departmentRepository = new DepartmentRepository(context);
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<DepartmentDto>>> GetAll()
        {
            var departments = await _departmentRepository.GetAll();

            if (departments is null)
            {
                return new ApiResponse<IEnumerable<DepartmentDto>>
                {
                    Data = null,
                    Message = $"None department was found",
                    Code = 404,
                    Success = false
                };
            }

            var departmentsDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return ApiResponse<DepartmentDto>.SuccessResponseCollection(departmentsDto);
        }
    }
}
