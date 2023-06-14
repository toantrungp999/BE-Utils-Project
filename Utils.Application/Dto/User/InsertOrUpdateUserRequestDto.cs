using Utils.CrossCuttingConcerns.Enums;

namespace Utils.Application.Dto.User
{
    public class InsertOrUpdateUserRequestDto
    {
        public string Name { get; set; }

        public Sex Sex { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

    public class InsertUserRequestDto: InsertOrUpdateUserRequestDto
    {
        public string Username { get; set; }
    }

    public class UpdateUserRequestDto : InsertOrUpdateUserRequestDto
    {
    }
}
