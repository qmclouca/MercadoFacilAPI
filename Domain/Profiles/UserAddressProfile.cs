using AutoMapper;
using Domain.DTOs.Address;
using Domain.Entities;

namespace Domain.Profiles
{
    public class UserAddressProfile: Profile
    {
        public UserAddressProfile()
        {
            CreateMap<CreateAddressDTO, UserAddress>();
            CreateMap<UpdateAddressDTO, UserAddress>();
        }
    }
}
