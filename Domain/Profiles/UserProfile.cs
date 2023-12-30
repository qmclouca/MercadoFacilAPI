using AutoMapper;
using Domain.Entities;
using Domain.DTOs.User;

namespace Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>()
                .ForMember(destino => destino.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(destino => destino.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(destino => destino.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(destino => destino.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(destino => destino.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(destino => destino.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

           CreateMap<User, CreateUserDTO>()
                .ForMember(destino => destino.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(destino => destino.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(destino => destino.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(destino => destino.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(destino => destino.Email, opt => opt.MapFrom(src => src.Email));
           

            CreateMap<UpdateUserDTO, User>()
                .ForMember(destino => destino.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(destino => destino.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(destino => destino.Addresses, opt => opt.MapFrom(src => src.Addresses))
                .ForMember(destino => destino.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}