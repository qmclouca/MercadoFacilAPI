using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MercadoFacilDbContext: DbContext
    {
        public MercadoFacilDbContext(DbContextOptions<MercadoFacilDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

    }
}
