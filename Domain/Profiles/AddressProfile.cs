using AutoMapper;
using Domain.DTOs.Address;
using Domain.Entities;

namespace Domain.Profiles
{
    public class AddressProfile: Profile
    {
        public AddressProfile()
        {
            CreateMap<CreateAddressDTO, Address>();
            CreateMap<UpdateAddressDTO, Address>();
        }
    }
}
