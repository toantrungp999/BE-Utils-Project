
using Utils.Application.Dto;
using Utils.Application.Dto.User;

namespace Utils.Application.Services.Interfaces
{
    public interface IUserService
    {
        UserResponsesDto GetUserById(Guid userId);

        PaginationDto<UserResponsesDto> GetUsers(PaginationRequestDto paginationRequest);

        AuthenticateResponsesDto Authenticate(AuthenticateRequestDto authenticateRequest);

        Task<Guid> InsertUser(InsertUserRequestDto request);

        Task<Guid> UpdateUserInfo(Guid userId, UpdateUserRequestDto request);

        Task<Guid> UpdateUserRule(UpdateUserRuleRequestDto request);
    }
}
