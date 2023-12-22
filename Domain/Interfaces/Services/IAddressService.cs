using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IAddressService
    {
        Task<bool> AddAddress(Address address);
        Task<Address> UpdateAddress(Address address);
        Task<bool> DeleteAddress(Address address);
        Task<Address> GetAddressById(Guid id);
        Task<IEnumerable<Address>> GetAllAddresses();
    }
}
