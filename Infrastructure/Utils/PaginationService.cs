using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public class PaginationService : IPaginationService
    {
        public async Task<PagedResult<T>> PaginateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize) where T : class
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var result = new PagedResult<T>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };

            var skipAmount = (pageNumber - 1) * pageSize;
            result.Items = await query.Skip(skipAmount).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
