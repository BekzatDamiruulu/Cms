namespace Cms.Shared.Shared.Utils;

public static class PaginationUtils
{
        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> entities ,int pageIndex = 1, int pageSize= 10)
        {
            return entities
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize);
        }
}