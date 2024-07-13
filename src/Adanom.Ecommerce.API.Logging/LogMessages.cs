namespace Adanom.Ecommerce.API.Logging;

public static class LogMessages
{
    public static class Auth
    {
        /// <summary>
        /// 0: user id
        /// 1: user email
        /// </summary>
        public const string UserNotFound = "Kullanıcı bulunamadı. Kullanıcı Id: {0} --- Kullanıcı E-posta: {1}";

        /// <summary>
        /// 0: new value
        /// 1: old value
        /// </summary>
        public const string UserChangesCommercialEmailsPreference = "Kullanıcı reklam e-postaları izin tercihi. Yeni tercih: {0} --- Eski tercih: {1}";

        /// <summary>
        /// 0: new value
        /// 1: old value
        /// </summary>
        public const string UserChangesCommercialSMSPreference = "Kullanıcı reklam SMS'leri izin tercihi. Yeni tercih: {0} --- Eski tercih: {1}";
    }

    public static class AdminTransaction
    {
        public const string DatabaseSaveChangesSuccessful = @"Veri tabanı işlemi başarılı bir şekilde gerçekleştirildi. Id: {0}";

        public const string DatabaseSaveChangesHasFailed = "Veri tabanı işlemi sırasında hata meydana geldi.";

        public const string DatabaseTransactionHasFailed = "Veri tabanı transaction sırasında hata meydana geldi.";

        public const string BatchUpdateProductSKUNotFound = @"Toplu işlem sırasında ürün kodu bulunamadı. Kod: {0}";

        public const string BatchCreateProductsError = @"Toplu işlem sırasında hata meydana geldi. ExcelRow: {0}";
    }

    public static class CustomerTransaction
    {
        public const string DatabaseSaveChangesSuccessful = @"Veri tabanı işlemi başarılı bir şekilde gerçekleştirildi. Id: {0}";

        public const string DatabaseSaveChangesHasFailed = "Veri tabanı işlemi sırasında hata meydana geldi.";

        public const string DatabaseTransactionHasFailed = "Veri tabanı transaction sırasında hata meydana geldi.";
    }
}
