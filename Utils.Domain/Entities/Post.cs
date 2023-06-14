namespace Utils.Domain.Entities
{
    public class Post : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public virtual ICollection<PostCategory> PostCategories { get; set; }
    }
}
