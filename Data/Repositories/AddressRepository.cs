using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;

namespace Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IRepository<Address> _AddressRepository;       

        public AddressRepository(IRepository<Address> repository)
        {
            _AddressRepository = repository;
        }

        public Task AddAsync(Address entity)
        {                     
           return _AddressRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            return _AddressRepository.DeleteAsync(id);
        }

        public Task DeleteAsync(Address entity)
        {
            return _AddressRepository.DeleteAsync(entity);            
        }

        public Task<IEnumerable<Address>> GetAllAsync()
        {
            return _AddressRepository.GetAllAsync();
        }

        public Task<Address> GetByIdAsync(Guid id)
        {
            return _AddressRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Address entity)
        {
            return _AddressRepository.UpdateAsync(entity);
        }
    }
}