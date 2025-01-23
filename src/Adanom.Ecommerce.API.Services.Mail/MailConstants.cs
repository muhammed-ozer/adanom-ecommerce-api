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
                public const string GrandTotal = "{GRAND_TOTAL}";
                public const string ShippingTrackingCode = "{ORDER_SHIPPING_TRACKING_CODE}";
                public const string ShippingProviderName = "{ORDER_SHIPPING_PROVIDER_NAME}";
                public const string DeliveryType = "{DELIVERY_TYPE}";
                public const string OrderPaymentType = "{ORDER_PAYMENT_TYPE}";
            }

            public static class ReturnRequest
            {
                public const string Number = "{RETURN_REQUEST_NUMBER}";

                public const string DisapprovedReasonMessage = "{DISAPPROVED_REASON_MESSAGE}";

                public const string DeliveryInformations = "{RETURN_REQUEST_DELIVERY_INFORMATIONS}";
            }
        }
    }
}
