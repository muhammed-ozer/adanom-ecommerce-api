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
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_NEW,
                    Description = "Yeni sipariş için kullanıcıya gönderilir.",
                    Subject = "Siparişiniz oluşturuldu",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz oluşturulmuştur.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_NEW_ORDERPAYMENTTYPE_BANK_TRANSFER,
                    Description = "Banka havalesi yöntemi ile oluşturulan yeni sipariş için kullanıcıya gönderilir.",
                    Subject = "Siparişiniz oluşturuldu",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz oluşturulmuştur.</p>
                                <p>Banka havalesi ödemesi için aşağıdaki hesap numarasına sipariş tutarını göndermeniz beklenmektedir. Sipariş numarasını açıklama kısmına yazmayı unutmayınız.</p>
                                <p><strong>Toplam tutar</strong>: {GRAND_TOTAL}</p>
                                <p>BANKA</p>
                                <p>IBAN</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_READY_PICK_UP_FROM_STORE,
                    Description = "Mağazadan teslim alınacak ve hazırlanan sipariş için kullanıcıya gönderilir.",
                    Subject = "Siparişiniz teslime hazır",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz seçtiğiniz mağazada teslime hazır.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_ON_DELIVERY_CARGO_SHIPMENT,
                    Description = "Siparişin kargo firmasına teslim edilmesi durumunda kullanıcıya gönderilir.",
                    Subject = "Siparişiniz kargoya teslim edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz kargo firmasına teslim edildi</p>
                                <p>Kargo firması: <strong>{ORDER_SHIPPING_PROVIDER_NAME}</strong></p>
                                <p>Kargo takip numarası: <strong>{ORDER_SHIPPING_TRACKING_CODE}</strong></p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_ON_DELIVERY_LOCAL_DELIVERY,
                    Description = "Sipariş yerel teslimat ile teslimata çıktığı zaman kullanıcıya gönderilir.",
                    Subject = "Siparişiniz teslimata çıktı",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz teslimata çıktı, en kısa sürede adresinize teslim edilecektir.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_DONE,
                    Description = "Siparişi teslim edilen müşteriye gönderilir.",
                    Subject = "Siparişiniz teslim edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz teslim edildi.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_ORDERSTATUSTYPE_CANCEL,
                    Description = "Siparişi iptal edilen müşteriye gönderilir",
                    Subject = "Siparişiniz iptal edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz iptal edildi. En kısa sürede sizinle iletişime geçilecektir.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ORDER_USER_CANCEL,
                    Description = "Siparişi iptal eden müşteriye gönderilir",
                    Subject = "Siparişiniz iptal edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{ORDER_NUMBER}</strong> numaralı siparişiniz iptal edildi. Ödemeniz en kısa süre içerisinde size iade edilecektir.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ADMIN_ORDER_RECEIVED,
                    Description = "Sipariş oluştuktan sonra yöneticiye gönderilir",
                    Subject = "Yeni sipariş",
                    Content = @"<p><strong>{USER_FULL_NAME}&nbsp;</strong> tarafından <strong>{ORDER_NUMBER}</strong> numaralı sipariş oluşturdu.</p>
                                <p><strong>Teslimat yöntemi:</strong> {DELIVERY_TYPE}</p>
                                <p><strong>Ödeme yöntemi:</strong> {ORDER_PAYMENT_TYPE}</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ADMIN_ORDER_USER_CANCEL,
                    Description = "Müşteri sipariş iptal ettiği zaman yöneticiye gönderilir.",
                    Subject = "Sipariş iptali",
                    Content = @"<p><strong>{USER_FULL_NAME}&nbsp;</strong> tarafından <strong>{ORDER_NUMBER}</strong> numaralı sipariş iptal edildi.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_RECEIVED,
                    Description = "İade talebi oluşturan kullanıcıya gönderilir",
                    Subject = "İade talebiniz oluşturuldu",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz oluşturuldu.</p>
                                <p>{RETURN_REQUEST_DELIVERY_INFORMATIONS}</p>"
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
                    Description = "İade talebi reddeildiği zaman kullanıcıya gönderilir",
                    Subject = "İade talebiniz reddedildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz reddeildi.</p>
                                <p><strong>Red sebebi</strong>: {DISAPPROVED_REASON_MESSAGE}</p>
                                <p>En kısa sürede sizinle iletişime geçeceğiz.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_REFUND_MADE,
                    Description = "İade talebi ödemesi yapıldığı zaman kullanıcıya gönderilir",
                    Subject = "İade talebiniz için geri ödeme gerçekleşti",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebinizin geri ödemesi yapıldı.</p>
                                <p>Bu geri ödemenin kartınıza/hesabınıza yansıması bankanıza göre değişiklik gösterebilir.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_CANCEL,
                    Description = "İade talebi iptal edildiği zaman kullanıcıya gönderilir",
                    Subject = "İade talebiniz iptal edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz iptal edildi. En kısa sürede sizinle iletişime geçilecektir.</p></p>"
                },
                new()
                {
                    Key = MailTemplateKey.RETURN_REQUEST_USER_CANCEL,
                    Description = "İade talebini iptal eden müşteriye gönderilir",
                    Subject = "İade talebiniz iptal edildi",
                    Content = @"<p>Sayın <strong>{USER_FULL_NAME}&nbsp;</strong></p>
                                <p><strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebiniz iptal edildi.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ADMIN_RETURN_REQUEST_RECEIVED,
                    Description = "İade talebi oluştuktan sonra yöneticiye gönderilir",
                    Subject = "Yeni iade talebi",
                    Content = @"<p><strong>{USER_FULL_NAME}&nbsp;</strong> tarafından <strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebi oluşturuldu.</p>"
                },
                new()
                {
                    Key = MailTemplateKey.ADMIN_RETURN_REQUEST_USER_CANCEL,
                    Description = "Müşteri iade talebini iptal ettiği zaman yöneticiye gönderilir.",
                    Subject = "İade talebi iptali",
                    Content = @"<p><strong>{USER_FULL_NAME}&nbsp;</strong> tarafından <strong>{RETURN_REQUEST_NUMBER}</strong> numaralı iade talebi iptal edildi.</p>"
                }
            };
        }
    }
}
