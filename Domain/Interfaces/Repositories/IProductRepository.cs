using Domain.Entities;

namespace Domain.Interfaces.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<Product> UpdateByIdAsync(Guid id, Product entity); 
    }
}
