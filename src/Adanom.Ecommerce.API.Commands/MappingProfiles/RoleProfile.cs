namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleResponse>();

            CreateMap<RoleResponse, Role>();
        }
    }
}