using Assessment.API.Models;
using Assessment.Application.Dtos;
using AutoMapper;


namespace Assessment.Application.Services
{
    public class MappingProfileService : Profile
    {
        public MappingProfileService()
        {
            CreateMap<Employee, EmployeeDto>()
                .ReverseMap();

            CreateMap<Department, DepartmentDto>()
                .ReverseMap();
        }
    }
}
