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
        private readonly IOptionsMonitor<AppSettings> _optionsDelegate;

        public UserService(IUnitOfWork uowProvider,
            IMapper mapper,
            IUserRepository userRepository,
            IOptionsMonitor<AppSettings> optionsDelegate)
        {
            _uowProvider = uowProvider;
            _mapper = mapper;
            _userRepository = userRepository;
            _optionsDelegate = optionsDelegate;
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
            var userName = authenticateRequest.Username.ToLower().Trim();
            var user = _userRepository.GetAll(true).FirstOrDefault(
                           x => (x.UserName.ToLower().Equals(userName) || x.Email.ToLower().Equals(userName))
                            && x.Password == authenticateRequest.Password.ConvertToMD5());

            Guard.ThrowIfNull<NotFoundException>(user, ExceptionConstant.LoginFail);

            var claims = new List<Claim>();

            claims.AddRange(new[]
            {
                  new Claim(ClaimTypes.Name, user.Id.ToString()),
                  new Claim(ClaimTypes.Role, user.Role)
            });

            var jwtToken = JwtToken.GenerateToken(_optionsDelegate.CurrentValue.Secret, claims);

            AuthenticateResponsesDto result = _mapper.Map<AuthenticateResponsesDto>(user);

            result.Token = jwtToken;

            return result;
        }

        public async Task<Guid> InsertUser(InsertUserRequestDto request)
        {
            var existUser = _userRepository.GetAll(true).FirstOrDefault(
                x => x.UserName.ToLower().Equals(request.Username.ToLower()) ||
                x.Email.ToLower().Equals(request.Email.ToLower()) ||
                x.PhoneNumber.ToLower().Equals(request.PhoneNumber.ToLower()));

            Guard.ThrowByCondition<BusinessLogicException>(
                existUser != null && existUser.UserName.ContainKeyWordInvariant(request.Username),
                ExceptionConstant.UserNameExist);
            Guard.ThrowByCondition<BusinessLogicException>(
                existUser != null && existUser.Email.ContainKeyWordInvariant(request.Email),
                ExceptionConstant.EmailExist);
            Guard.ThrowByCondition<BusinessLogicException>(
                existUser != null && existUser.PhoneNumber.ContainKeyWordInvariant(request.PhoneNumber),
                ExceptionConstant.PhoneNumberExist);

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

        public async Task<Guid> UpdateUserInfo(Guid userId, UpdateUserRequestDto request)
        {
            var user = _userRepository.GetAll().FirstOrDefault(
                user => user.Id == userId
                && user.Password == request.Password.ConvertToMD5());

            Guard.ThrowIfNull<NotFoundException>(user, string.Format(ExceptionConstant.NotFound, nameof(User)));

            user.Name = request.Name;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;
            user.Sex = request.Sex;

            await _uowProvider.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid> UpdateUserRule(UpdateUserRuleRequestDto request)
        {
            var user = _userRepository.GetAll().FirstOrDefault(
                user => user.Id == request.UserId);

            Guard.ThrowIfNull<NotFoundException>(user, string.Format(ExceptionConstant.NotFound, nameof(User)));
            Guard.ThrowByCondition<NotFoundException>(!RoleConstant.Roles.Contains(request.Role), string.Format(ExceptionConstant.RoleNotExist, request.Role));

            user.Role = request.Role;

            await _uowProvider.SaveChangesAsync();

            return user.Id;
        }

        //    ExpressionStarter<User> FindUserByInfomation(string userName, string email, string phoneNumber)
        //    {
        //        var filterStatusPredicate = PredicateBuilder.New<User>();

        //        filterStatusPredicate.Or(x => x.UserName.ContainKeyWordInvariant(userName));
        //        filterStatusPredicate.Or(x => x.Email.ContainKeyWordInvariant(email));
        //        filterStatusPredicate.Or(x => x.PhoneNumber.ContainKeyWordInvariant(phoneNumber));

        //        return filterStatusPredicate;
        //    }
    }
}
