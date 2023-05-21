using Utils.CrossCuttingConcerns.Constants;
using Utils.CrossCuttingConcerns.Enums;
using System.ComponentModel.DataAnnotations;

namespace Utils.Domain.Entities
{
    public class User : BaseEntity<Guid>
    {
        [StringLength(450)]
        [Required]
        public string UserName { get; set; }

        [StringLength(450)]
        [Required]
        public string Password { get; set; }

        [StringLength(450)]
        [Required]
        public string Name { get; set; }

        public Sex Sex { get; set; }

        [StringLength(24)]
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(450)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(450)]
        public string Role { get; set; } = RoleConstant.User;

        public bool IsLoginGoogle { get; set; }

        public bool IsLoginFacebook { get; set; }
    }
}
