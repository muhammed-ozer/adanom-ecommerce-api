using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Data
{
    internal static class SeedMailTemplateData
    {
        internal static ICollection<MailTemplate> MailTemplates { get; set; }

        static SeedMailTemplateData()
        {
            MailTemplates = new List<MailTemplate>()
            {
                new()
                {
                    Key = MailTemplateKey.AUTH_NEW_USER,
                    Description = "Yeni üye olan kullanıcıya gönderilir.",
                    Subject = "Adanom'a Hoşgeldiniz!",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Adanom&apos;a hoşgeldiniz.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.AUTH_EMAIL_CONFIRMATION,
                    Description = "E-posta hesabı onaylamak için kullanıcıya gönderilir.",
                    Subject = "Adanom e-posta hesabınızı onaylayınız",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Adanom e-posta hesabınızı onaylamak i&ccedil;in buraya <a href=""{AUTH_EMAIL_CONFIRMATION_URL}""
                                target=""_blank"" rel=""noopener noreferrer"">tıklayınız.</a></p>"
                },
                new()
                {
                    Key = MailTemplateKey.AUTH_EMAIL_CONFIRMED_SUCCESSFUL,
                    Description = "E-posta hesabı onaylanan kullanıcıya gönderilir.",
                    Subject = "Adanom e-posta hesabınız onaylandı",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Adanom e-posta hesabınız onaylandı.</p>
                                <p>Alışverişin keyfini &ccedil;ıkarmanız dileğiyle</p>"
                },
                new()
                {
                    Key = MailTemplateKey.AUTH_PASSWORD_CHANGED_SUCCESSFUL,
                    Description = "Şifresini değiştiren kullanıcıya gönderilir.",
                    Subject = "Adanom şifreniz değişti",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                Adanom şifreniz değişti.</p>
                                <p>Bu işlemden bilginiz yok ise bizimle iletişime ge&ccedil;ebilirsiniz.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.AUTH_PASSWORD_RESET,
                    Description = "Şifresini sıfırlamak isteyen kullanıcıya gönderilir",
                    Subject = "Adanom şifrenizi sıfırlayın",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Adanom şifrenizi sıfırlamak i&ccedil;in buraya <a href=""{AUTH_PASSWORD_RESET_URL}""
                                target=""_blank"" rel=""noopener noreferrer"">tıklayınız</a>.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.AUTH_PASSWORD_RESET_SUCCESSFUL,
                    Description = "Şifresini sıfırlayan kullanıcıya gönderilir",
                    Subject = "Adanom şifreniz sıfırlandı",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                Adanom şifreniz sıfırlandı.</p>
                                <p>Bu işlemden bilginiz yok ise bizimle iletişime ge&ccedil;ebilirsiniz.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_IN_PROGRESS,
                    Description = "İşleme alınan sipariş için kullanıcıya gönderilir.",
                    Subject = "Siparişiniz işleme alındı",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Siparişiniz işleme alınmıştır.</p>
                                <p>Sipariş Numarası: <strong>{ORDER_NUMBER}</strong></p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_READY_PICK_UP_FROM_STORE,
                    Description = "Mağazadan teslim alınacak ve hazırlanan sipariş için kullanıcıya gönderilir.",
                    Subject = "Siparişiniz teslime hazır",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Siparişiniz teslime hazır.</p>
                                <p>Sipariş Numarası: <strong>{ORDER_NUMBER}</strong></p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_DELIVERED_TO_SHIPPING_PROVIDER_CARGO_SHIPMENT,
                    Description = "Siparişin kargo firmasına teslim edilmesi durumunda kullanıcıya gönderilir.",
                    Subject = "Siparişiniz kargoya teslim edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Siparişiniz kargo firmasına teslim edildi</p>
                                <p>Kargo firması: <strong>{ORDER_SHIPPING_PROVIDER_NAME}</strong></p>
                                <p>Sipariş Numarası: <strong>{ORDER_NUMBER}</strong></p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_DELIVERED_TO_CUSTOMER,
                    Description = "Siparişi teslim edilen müşteriye gönderilir.",
                    Subject = "Siparişiniz teslim edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Siparişiniz teslim edildi.</p>
                                <p>Sipariş Numarası: <strong>{ORDER_NUMBER}</strong></p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_CANCEL,
                    Description = "Siparişi iptal edilen müşteriye gönderilir",
                    Subject = "Siparişiniz iptal edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p>Siparişiniz iptal edildi.</p>
                                <p>Sipariş Numarası: <strong>{ORDER_NUMBER}</strong></p>
                                <p>En kısa sürede sizinle iletişime geçilecektir.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_RECEIVED,
                    Description = "İade talebi oluşturan kullanıcıya gönderilir",
                    Subject = "İade talebiniz oluşturuldu",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz oluşturuldu.</p>
                                <p>......... İade kargo bilgileri........</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_IN_PROGRESS,
                    Description = "İade talebi bize ulaştığı zaman kullanıcıya gönderilir",
                    Subject = "İade talebiniz elimize ulaştı",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz elimize ulaştı.</p>
                                <p>Gerekli incelemeler sonrası en kısa sürede sizlere dönüş yapılacaktır.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_APPROVED,
                    Description = "İade talebi onaylandığı zaman kullanıcıya gönderilir",
                    Subject = "İade talebiniz onaylandı",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz onaylandı.</p>
                                <p>En kısa sürede geri ödemesi yapılacaktır.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_DISAPPROVED,
                    Description = "İade talebi onaylanmadığı zaman kullanıcıya gönderilir",
                    Subject = "İade talebiniz onaylanmadı",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz onaylanmadı.</p>
                                <p>En kısa sürede sizinle iletişime geçeceğiz.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_REFUND_MADE,
                    Description = "İade talebi ödemesi yapıldığı zaman kullanıcıya gönderilir",
                    Subject = "İade talebiniz için geri ödeme gerçekleşti",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebinizin geri ödemesi yapıldı.</p>
                                <p>Bu geri ödemenin kartınıza/hesabınıza yansıması bankanıza göre değişiklik gösterebilir. Lütfen bankanızla iletişime geçiniz.</p>"
                }
            };
        }
    }
}
