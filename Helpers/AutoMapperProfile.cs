using AutoMapper;
using VacationBookerAPI.DtoModels;
using VacationBookerAPI.Entities;

namespace VacationBookerAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
            
            CreateMap<DepartmentDto, Department>();
            CreateMap<Department, DepartmentDto>();
            
            CreateMap<BookingDto, Booking>();
            CreateMap<Booking, BookingDto>();

            CreateMap<User, UserModel>();
            CreateMap<UserModel,User>();
        }
    }
}