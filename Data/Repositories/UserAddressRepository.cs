using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class UserAddressRepository: IUserAddressRepository
    {
        private readonly IRepository<UserAddress> _userAddressRepository;

        public UserAddressRepository(IRepository<UserAddress> repository)
        {
            _userAddressRepository = repository;
        }

        public Task AddAsync(UserAddress entity)
        {
            return _userAddressRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            return _userAddressRepository.DeleteAsync(id);
        }

        public Task DeleteAsync(UserAddress entity)
        {
            return _userAddressRepository.DeleteAsync(entity);            
        }

        public Task<IEnumerable<UserAddress>> GetAllAsync()
        {
            return _userAddressRepository.GetAllAsync(); 
        }

        public Task<IQueryable<UserAddress>> GetAllQueryAsync()
        {
            return _userAddressRepository.GetAllQueryAsync();
        }

        public Task<UserAddress> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> GetByEmailAsync(Expression<Func<UserAddress, bool>> value)
        {
            throw new NotImplementedException();
        }

        public Task<UserAddress> GetByIdAsync(Guid id)
        {
           return _userAddressRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(UserAddress entity)
        {
            return _userAddressRepository.UpdateAsync(entity);
        }
    }
}