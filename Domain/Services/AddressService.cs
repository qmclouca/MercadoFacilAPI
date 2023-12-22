using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class AddressService: IAddressService
    {
        private readonly IRepository<Address> _addressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        }

        public async Task<bool> AddAddress(Address address)
        {
            await _addressRepository.AddAsync(address);
            return true;            
        }

        public async Task<bool> DeleteAddress(Address address)
        {
            if (address == null) return false;
            
            await _addressRepository.DeleteAsync(address.Id);
            return true;
        }

        public async Task<IEnumerable<Address>> GetAllAddresses()
        {
            return await _addressRepository.GetAllAsync();
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            await _addressRepository.UpdateAsync(address);
            return await GetAddressById(address.Id);            
        }
    }
}
