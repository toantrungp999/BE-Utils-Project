namespace Utils.Application.Dto.User
{
    public class UpdateUserRuleRequestDto
    {
        public Guid UserId { get; set; }

        public string Role { get; set; }
    }
}
