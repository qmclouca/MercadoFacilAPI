using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IMercadoFacilDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<User> Users { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<UserAddress> UserAddresses { get; set; }        
    }
}