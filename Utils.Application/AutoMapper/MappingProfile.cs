using AutoMapper;
using Utils.Application.Dto.User;
using Utils.Domain.Entities;

namespace Utils.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            MapUser();
        }

        private void MapUser()
        {
            CreateMap<User, UserResponsesDto>();
            CreateMap<User, AuthenticateResponsesDto>();
        }
    }
}
