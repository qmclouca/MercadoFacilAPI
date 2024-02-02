using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IPaginationService
    {
        Task<PagedResult<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : class;
    }
}
