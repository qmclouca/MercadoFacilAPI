using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IMercadoFacilDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
