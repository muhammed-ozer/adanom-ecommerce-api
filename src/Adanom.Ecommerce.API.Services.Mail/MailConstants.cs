namespace Adanom.Ecommerce.API.Services.Mail
{
    public static class MailConstants
    {
        public static class Replacements
        {
            public static class User
            {
                public const string FullName = "{USER_FULL_NAME}";
            }

            public static class Auth
            {
                public const string EmailConfirmationUrl = "{AUTH_EMAIL_CONFIRMATION_URL}";
                public const string PasswordResetUrl = "{AUTH_PASSWORD_RESET_URL}";
            }

            public static class Order
            {
                public const string Number = "{ORDER_NUMBER}";
                public const string ShippingTrackingCode = "{ORDER_SHIPPING_TRACKING_CODE}";
                public const string ShippingProviderName = "{ORDER_SHIPPING_PROVIDER_NAME}";
            }
        }
    }
}
