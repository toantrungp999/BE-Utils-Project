using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utils.Domain.Entities
{
    public class PostCategory
    {
        [Key]
        [Column(Order = 1)]
        public Guid PostId { get; set; }
        [Key]
        [Column(Order = 2)]
        public Guid CategoryId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
