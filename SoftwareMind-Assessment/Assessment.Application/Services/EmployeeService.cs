using Assessment.API.Models;
using Assessment.Application.Dtos;
using Assessment.Application.Interfaces;
using Assessment.Application.Responses;
using Assessment.Infrastructure.Context;
using Assessment.Infrastructure.Repositories;
using AutoMapper;

namespace Assessment.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        readonly IMapper _mapper;


        public EmployeeService(AppDbContext context, IMapper mapper)
        {
            _employeeRepository = new EmployeeRepository(context);
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeRepository.GetAll();

            if (employees is null)
            {
                return new ApiResponse<IEnumerable<EmployeeDto>>
                {
                    Data = null,
                    Message = $"None employee was found",
                    Code = 404,
                    Success = false
                };
            }

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return ApiResponse<EmployeeDto>.SuccessResponseCollection(employeesDto);
        }

        public async Task<ApiResponse<EmployeeDto>> GetById(int id)
        {
            var employee = await _employeeRepository.GetById(id);

            if (employee is null)
            {
                return new ApiResponse<EmployeeDto>
                {
                    Data = null,
                    Message = $"Employee with ID {id} not found.",
                    Code = 404,
                    Success = false
                };
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return ApiResponse<EmployeeDto>.SuccessResponse(employeeDto);
        }

        public async Task<ApiResponse<EmployeeDto>> Create(EmployeeDto dto)
        {
            if (!IsRequiredFieldsFullfilled(dto))
            {
                return new ApiResponse<EmployeeDto>
                {
                    Data = null,
                    Message = "(Required) Check if FirstName, Address and Phone fields are fullfilled.",
                    Code = 400,
                    Success = false
                };
            }

            var employee = _mapper.Map<Employee>(dto);

            await _employeeRepository.Insert(employee);

            return ApiResponse<EmployeeDto>.SuccessResponse(null, "Employee created successfully", 201);
        }
        public async Task<ApiResponse<EmployeeDto>> Update(int id, EmployeeDto dto)
        {
            if (id != dto.Id)
            {
                return new ApiResponse<EmployeeDto>
                {
                    Message = $"Employee ID mismatch.",
                    Code = 400,
                    Success = false
                };
            }

            var employeeToUpdate = await _employeeRepository.GetById(id);
            if (employeeToUpdate is null)
            {
                return new ApiResponse<EmployeeDto>
                {
                    Message = "Employee not found.",
                    Code = 404,
                    Success = false
                };
            }

            employeeToUpdate = _mapper.Map<Employee>(dto);

            await _employeeRepository.Update(employeeToUpdate);

            return ApiResponse<EmployeeDto>.SuccessResponse(null, "Employee updated successfully");
        }
        public async Task<ApiResponse<EmployeeDto>> Delete(int id)
        {
            var employee = await _employeeRepository.GetById(id);

            await _employeeRepository.Delete(employee);

            return ApiResponse<EmployeeDto>.SuccessResponse(null, "Employee deleted successfully");
        }

        private bool IsRequiredFieldsFullfilled(EmployeeDto employeeDto)
        {
            if (string.IsNullOrEmpty(employeeDto.FirstName) ||
                string.IsNullOrEmpty(employeeDto.Address) ||
                string.IsNullOrEmpty(employeeDto.Phone))
            {
                return false;
            }

            return true;
        }
    }
}
