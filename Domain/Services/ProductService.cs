using Domain.Entities;
using Domain.Interfaces.Services;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductService _productService;
        public ProductService(IProductService productService)
        {
            _productService = productService;
        }

        public Task AddAsync(Product entity)
        {
            return _productService.AddAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            return _productService.DeleteAsync(id);
        }

        public Task DeleteAsync(Product entity)
        {
            return _productService.DeleteAsync(entity);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return _productService.GetAllAsync();
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            return _productService.GetByIdAsync(id);
        }

        public Task UpdateAsync(Product entity)
        {
            return _productService.UpdateAsync(entity);
        }

        public Task<Product> UpdateByIdAsync(Guid id, Product entity)
        {
            return _productService.UpdateByIdAsync(id, entity);
        }
    }
}
