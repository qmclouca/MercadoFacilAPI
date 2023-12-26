using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class AddressService: IAddressService
    {
        private readonly IRepository<Address> _addressRepository;
        private readonly IRepository<UserAddress> _userAddressRepository;

        public AddressService(IRepository<Address> addressRepository, IRepository<UserAddress> userAddressRepository)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _userAddressRepository = userAddressRepository ?? throw new ArgumentNullException(nameof(userAddressRepository)); 
        }

        public async Task<bool> AddAddress(Address address)
        {
            try
            {
                await _addressRepository.AddAsync(address);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
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

        public async Task<bool> DeleteAddressByUserId(Guid userId)
        {            
            IEnumerable<UserAddress> lstAllUserAddresses = await _userAddressRepository.GetAllAsync();
            IEnumerable<UserAddress> lstUserAddresses = lstAllUserAddresses.Where(x => x.UserId == userId);

            IEnumerable<Address> lstAllAddresses = await _addressRepository.GetAllAsync();
            IEnumerable<Address> lstAddresses = lstAllAddresses.Where(x => lstUserAddresses.Any(y => y.AddressId == x.Id));

            if (lstAddresses.Count() > 1)
            {
                foreach (Address address in lstAddresses)
                {
                    await _addressRepository.DeleteAsync(address.Id);
                    await _userAddressRepository.DeleteAsync(address.Id);
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            await _addressRepository.UpdateAsync(address);
            return await GetAddressById(address.Id);            
        }
    }
}
