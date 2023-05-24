using FluentValidation;
using Utils.Application.Dto.User;

namespace Utils.Application.Validators.User
{
    public class AuthenticateDtoValidator : AbstractValidator<AuthenticateRequestDto>
    {
        public AuthenticateDtoValidator()
        {
            RuleFor(x => x.Username).MinimumLength(6).MaximumLength(255).NotNull().NotEmpty();

            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(255).NotNull().NotEmpty();
        }
    }
}
