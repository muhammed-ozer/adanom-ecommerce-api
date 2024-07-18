namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();

            CreateMap<UserResponse, User>();

            CreateMap<UpdateUserDefaultDiscountRateRequest, UpdateUserDefaultDiscountRate>();

            CreateMap<UpdateUserDefaultDiscountRate, User>();

            CreateMap<UpdateUserRolesRequest, UpdateUserRoles>();

            CreateMap<UpdateUserPermissionsRequest, UpdateUserPermissions>();
        }
    }
}