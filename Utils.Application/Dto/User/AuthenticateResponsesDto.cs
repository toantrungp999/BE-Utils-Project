namespace Utils.Application.Dto.User
{
    public class AuthenticateResponsesDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}
