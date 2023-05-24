namespace Utils.Persistence.Extensions
{
    public static class CollectionExtension
    {
        /// <summary>
        /// Check if a collection should be null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<TEntity>(this IEnumerable<TEntity> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
