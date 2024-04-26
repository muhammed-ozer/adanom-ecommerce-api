using Adanom.Ecommerce.API.Data.Models;
using AutoMapper;

namespace Adanom.Ecommerce.API.Commands.Models.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUserRequest, RegisterUser>();

            CreateMap<RegisterUser, User>();

            CreateMap<ChangePasswordRequest, ChangePassword>();

            CreateMap<ConfirmEmailRequest, ConfirmEmail>();

            CreateMap<LoginRequest, Login>();

            CreateMap<ResetPasswordRequest, ResetPassword>();

            CreateMap<SendEmailConfirmationEmailRequest, SendEmailConfirmationEmail>();

            CreateMap<SendPasswordResetEmailRequest, SendPasswordResetEmail>();
        }
    }
}