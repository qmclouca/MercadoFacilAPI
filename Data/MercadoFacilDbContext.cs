using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MercadoFacilDbContext: DbContext, IMercadoFacilDbContext
    {
        public MercadoFacilDbContext(DbContextOptions<MercadoFacilDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Share> Shares { get; set; }

        public DbSet<T> Queryable<T>() where T : class
        {
            return Set<T>();
        }
    }
}