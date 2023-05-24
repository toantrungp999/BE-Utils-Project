using Utils.CrossCuttingConcerns.Enums;

namespace Utils.Application.Dto.User
{
    public class UserResponsesDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Sex Sex { get; set; }

        public string Role { get; set; }
    }
}
