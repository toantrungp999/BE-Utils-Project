using Microsoft.Extensions.Options;
using System.Security.Claims;
using Utils.Application.Dto.User;
using Utils.Application.Dto;
using Utils.Application.Services.Interfaces;
using Utils.CrossCuttingConcerns.Constants;
using Utils.CrossCuttingConcerns.Exceptions;
using Utils.Domain.Entities;
using Utils.Domain.Repositories;
using AutoMapper;
using Utils.CrossCuttingConcerns.Configurations;
using Utils.CrossCuttingConcerns.Utilities;
using Utils.Application.Extensions;
using Utils.CrossCuttingConcerns.Extensions;

namespace Utils.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uowProvider;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly AppSetting _appSettings;

        public UserService(IUnitOfWork uowProvider,
            IMapper mapper,
            IUserRepository userRepository,
            IOptions<AppSetting> appSettings)
        {
            _uowProvider = uowProvider;
            _mapper = mapper;
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public UserResponsesDto GetUserById(Guid userId)
        {
            var user = _userRepository.FindById(userId);

            Guard.ThrowIfNull<NotFoundException>(user, string.Format(ExceptionConstant.NotFound, nameof(User)));

            return _mapper.Map<UserResponsesDto>(user);
        }

        public PaginationDto<UserResponsesDto> GetUsers(PaginationRequestDto paginationRequest)
        {
            var users = _userRepository.GetAll(true)
                .Paginate<User, UserResponsesDto>(_mapper, paginationRequest.PageIndex, paginationRequest.PageSize);

            return users;
        }

        public AuthenticateResponsesDto Authenticate(AuthenticateRequestDto authenticateRequest)
        {

            var user = _userRepository.GetAll(true).FirstOrDefault(
                           x => (x.UserName.ContainKeyWordInvariant(authenticateRequest.Username) || x.Email.ContainKeyWordInvariant(authenticateRequest.Username))
                            && x.Password == authenticateRequest.Password.ConvertToMD5());

            Guard.ThrowIfNull<NotFoundException>(user, ExceptionConstant.LoginFail);

            var claims = new List<Claim>();

            claims.AddRange(new[]
            {
                  new Claim(ClaimTypes.Name, user.Id.ToString()),
                  new Claim(ClaimTypes.Role, user.Role)
            });

            var jwtToken = JwtToken.GenerateToken(_appSettings.Secret, claims);

            AuthenticateResponsesDto result = _mapper.Map<AuthenticateResponsesDto>(user);

            result.Token = jwtToken;

            return result;
        }

        public async Task<Guid> InsertUser(InsertUserRequestDto request)
        {
            var userByUserName = _userRepository.GetAll(true).FirstOrDefault(
                            x => x.UserName.ContainKeyWordInvariant(request.Username));

            Guard.ThrowByCondition<BusinessLogicException>(userByUserName != null, ExceptionConstant.UserNameExist);

            var userByEmail = _userRepository.GetAll(true).FirstOrDefault(
                           x => x.Email.ContainKeyWordInvariant(request.Email));

            Guard.ThrowByCondition<BusinessLogicException>(userByEmail != null, ExceptionConstant.EmailExist);

            var userByPhoneNumber = _userRepository.GetAll(true).FirstOrDefault(
                           x => x.PhoneNumber.ContainKeyWordInvariant(request.PhoneNumber));

            Guard.ThrowByCondition<BusinessLogicException>(userByPhoneNumber != null, ExceptionConstant.PhoneNumberExist);

            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Sex = request.Sex,
                UserName = request.Username,
                Password = request.Password.ConvertToMD5()
            };

            await _userRepository.AddAsync(entity);
            await _uowProvider.SaveChangesAsync();

            return entity.Id;
        }
    }
}
