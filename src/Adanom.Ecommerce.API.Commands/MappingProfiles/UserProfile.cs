namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<UserResponse, User>();
        }
    }
}