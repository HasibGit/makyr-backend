using System;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class PaginatedResponse<T>
{
    public List<T> Data { get; set; }
    public PaginationMeta Meta { get; set; }

    public PaginatedResponse(List<T> data, int totalCount, int pageNumber, int pageSize)
    {
        Data = data;
        Meta = new PaginationMeta
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public static async Task<PaginatedResponse<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var data = await source.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();

        return new PaginatedResponse<T>(data, count, pageNumber, pageSize);
    }
}

public class PaginationMeta
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}
