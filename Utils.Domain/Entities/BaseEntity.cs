namespace Utils.Domain.Entities
{
    public class BaseEntity<TKey> : TrackableEntity
    {
        public TKey Id { get; set; }
    }
}
