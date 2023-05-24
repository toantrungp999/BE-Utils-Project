using AutoMapper;
using Utils.Application.Dto;

namespace Utils.Application.Extensions
{
    public static class LinqExtension
    {
        public static PaginationDto<TDest> Paginate<TSource, TDest>(this IQueryable<TSource> source,
            IMapper mapper, int pageIndex, int pageSize, Action<IMappingOperationOptions> opts = null)
        {
            var totalCount = source.Count();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var paginatedSource = source.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return new PaginationDto<TDest>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Items = opts != null
                    ? mapper.Map<IList<TDest>>(paginatedSource, opts)
                    : mapper.Map<IList<TDest>>(paginatedSource),
                HasNextPage = pageIndex + 1 < totalPages,
                HasPreviousPage = pageIndex > 0
            };
        }
    }
}
