using FluentValidation;
using Utils.Application.Dto.User;

namespace Utils.Application.Validators.User
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserRequestDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();

            RuleFor(x => x.Email).MinimumLength(6).MaximumLength(255).EmailAddress().NotNull().NotEmpty();

            RuleFor(x => x.PhoneNumber).MinimumLength(6).MaximumLength(12).NotNull().NotEmpty();

            RuleFor(x => x.Password).MinimumLength(6).MaximumLength(255).NotNull().NotEmpty();
        }
    }
}
