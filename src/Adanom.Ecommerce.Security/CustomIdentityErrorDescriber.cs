using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.Security.Identity
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        #region Fields


        #endregion

        #region Ctor

        public CustomIdentityErrorDescriber()
        {
        }

        #endregion

        public override IdentityError DefaultError() => new IdentityError
        {
            Code = nameof(DefaultError),
            Description = "Bilinmeyen bir hata meydana geldi."
        };

        public override IdentityError ConcurrencyFailure() => new IdentityError
        {
            Code = nameof(ConcurrencyFailure),
            Description = "Bilinmeyen bir hata meydana geldi."
        };

        public override IdentityError PasswordMismatch() => new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = "Şifre yanlış."
        };

        public override IdentityError InvalidToken() => new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = "Geçersiz token."
        };

        public override IdentityError LoginAlreadyAssociated() => new IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = "Hesabınıza giriş yapılmış durumda"
        };

        public override IdentityError InvalidUserName(string userName) => new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = string.Format("Geçersiz '{0}' kullanıcı adı. Kullanıcı adı sadece harf ve rakamlardan oluşmalıdır.", userName)
        };

        public override IdentityError InvalidEmail(string email) => new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = string.Format("Geçersiz '{0}' e-posta.", email)
        };

        public override IdentityError DuplicateUserName(string userName) => new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = string.Format("E-posta '{0}' kullanımda.", userName)
        };

        public override IdentityError DuplicateEmail(string email) => new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = string.Format("E-posta '{0}' kullanımda.", email)
        };

        public override IdentityError InvalidRoleName(string role) => new IdentityError
        {
            Code = nameof(InvalidRoleName),
            Description = string.Format("Rol {0} geçerisiz.", role)
        };

        public override IdentityError DuplicateRoleName(string role) => new IdentityError
        {
            Code = nameof(DuplicateRoleName),
            Description = string.Format("Rol {0} zaten mevcut.", role)
        };

        public override IdentityError UserAlreadyHasPassword() => new IdentityError
        {
            Code = nameof(UserAlreadyHasPassword),
            Description = "Kullanıcı zaten parola belirlemiş durumdadır."
        };

        public override IdentityError UserLockoutNotEnabled() => new IdentityError
        {
            Code = nameof(UserLockoutNotEnabled),
            Description = "Kullanıcı kilitleme aktif değil."
        };

        public override IdentityError UserAlreadyInRole(string role) => new IdentityError
        {
            Code = nameof(UserAlreadyInRole),
            Description = "Kullanıcı zaten bu roldedir."
        };

        public override IdentityError UserNotInRole(string role) => new IdentityError
        {
            Code = nameof(UserNotInRole),
            Description = "Kullanıcı bu rolde değildir."
        };

        public override IdentityError PasswordTooShort(int length) => new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = string.Format("Şifre en az {0} karakter içermelidir.", length)
        };

        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = "Şifre en az bir alfanumerik olmayan karakter içermelidir."
        };
        public override IdentityError PasswordRequiresDigit() => new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = "Şifre en az bir rakam içermelidir ('0'-'9')."
        };

        public override IdentityError PasswordRequiresLower() => new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = "Şifre en az bir küçük harf içermelidir ('a'-'z')."
        };

        public override IdentityError PasswordRequiresUpper() => new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = "Şifre en az bir büyük harf içermelidir ('A'-'Z')."
        };
    }
}
