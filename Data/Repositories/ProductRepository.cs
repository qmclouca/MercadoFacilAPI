using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly IRepository<Product> _productRepository;
        public ProductRepository(IRepository<Product> repository)
        {
            _productRepository = repository;
        }

        public Task AddAsync(Product entity)
        {
            return _productRepository.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            return _productRepository.DeleteAsync(id);
        }

        public Task DeleteAsync(Product entity)
        {
            return _productRepository.DeleteAsync(entity);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _productRepository.GetAllAsync();
        }

        public Task<IQueryable<Product>> GetAllQueryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByEmailAsync(Expression<Func<Product, bool>> value)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            return _productRepository.GetByIdAsync(id);
        }

        public Task<Product> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> UpdateByIdAsync(Guid id, Product novoProduto)
        {
            Product atual = await _productRepository.GetByIdAsync(id);

            if (atual == null)
            {
                return null;
            }

            atual.Name = novoProduto.Name;
            atual.Description = novoProduto.Description;
            atual.Price = novoProduto.Price;
            atual.Periodicidade = novoProduto.Periodicidade;
            atual.UpdatedAt = DateTime.Now;

            await _productRepository.UpdateAsync(atual!);

            return atual;
        }
    }
}
