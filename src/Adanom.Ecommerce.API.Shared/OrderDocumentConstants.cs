namespace Adanom.Ecommerce.API
{
    public static class OrderDocumentConstantsConstants
    {
        public static class DistanceSellingContract
        {
            public static class Replacements
            {
                public static string CompanyLegalName = "{COMPANY_LEGALNAME}";
                public static string CompanyAddress = "{COMPANY_ADDRESS}";
                public static string CompanyAddressDistrict = "{COMPANY_ADDRESS_DISTRICT}";
                public static string CompanyAddressCity = "{COMPANY_ADDRESS_CITY}";
                public static string CompanyEmail = "{COMPANY_EMAIL}";
                public static string CompanyPhoneNumber = "{COMPANY_PHONE_NUMBER}";
                public static string UserFullName = "{USER_FULL_NAME}";
                public static string UserAddress = "{USER_ADDRESS}";
                public static string UserAddressDistrict = "{USER_ADDRESS_DISTRICT}";
                public static string UserAddressCity = "{USER_ADDRESS_CITY}";
                public static string UserEmail = "{USER_EMAIL}";
                public static string UserPhoneNumber = "{USER_PHONE_NUMBER}";
                public static string Products = "{PRODUCTS}";
                public static string GrandTotal = "{GRAND_TOTAL}";
                public static string OrderPaymentType = "{ORDER_PAYMENT_TYPE}";
                public static string CreatedDate = "{CREATED_DATE}";
            }

            public static string Document = @$"
                <div style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 800px; margin: 0 auto; padding: 20px;"">
                    <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">1. TARAFLAR</div>
                      <div style=""margin-bottom: 24px;"">Bu sözleşme, aşağıdaki taraflar arasında belirtilen hüküm ve koşullara uygun olarak akdedilmiştir:</div>
                      <div class=""mb-6"">
                        <div style=""font-size: 18px; font-weight: bold; margin-bottom: 8px;"">SATICI:</div>
                        <table style=""width: 100%; margin-bottom: 24px; border-collapse: collapse;"">
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Firma adı</td>
                                <td style=""padding: 8px 0;"">{Replacements.CompanyLegalName}</td>
                            </tr>
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Adres</td>
                                <td style=""padding: 8px 0;"">{Replacements.CompanyAddress}<br>{Replacements.CompanyAddressDistrict} / {Replacements.CompanyAddressCity}</td>
                            </tr>
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">E-posta</td>
                                <td style=""padding: 8px 0;"">{Replacements.CompanyEmail}</td>
                            </tr>
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Telefon numarası</td>
                                <td style=""padding: 8px 0;"">{Replacements.CompanyPhoneNumber}</td>
                            </tr>
                        </table>
                        <div style=""font-size: 18px; font-weight: bold; margin-bottom: 8px;"">ALICI:</div>
                       <table style=""width: 100%; margin-bottom: 24px; border-collapse: collapse;"">
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Ad Soyad / Ünvan</td>
                                <td style=""padding: 8px 0;"">{Replacements.UserFullName}</td>
                            </tr>
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Adres</td>
                                <td style=""padding: 8px 0;"">{Replacements.UserAddress}<br>{Replacements.UserAddressDistrict} / {Replacements.UserAddressCity}</td>
                            </tr>
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">E-posta</td>
                                <td style=""padding: 8px 0;"">{Replacements.UserEmail}</td>
                            </tr>
                            <tr>
                                <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Telefon numarası</td>
                                <td style=""padding: 8px 0;"">{Replacements.UserPhoneNumber}</td>
                            </tr>
                        </table>
                        <div style=""margin-bottom: 24px;"">
                          İş bu sözleşmeyi kabul etmekle ALICI, sözleşme konusu siparişi onayladığı takdirde sipariş konusu bedeli ve varsa kargo ücreti, vergi gibi belirtilen ek
                          ücretleri ödeme yükümlülüğü altına gireceğini ve bu konuda bilgilendirildiğini peşinen kabul eder.
                        </div>
                      </div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">2.TANIMLAR</div>
                      <div style=""margin-bottom: 24px;"">
                        İşbu sözleşmenin uygulanmasında ve yorumlanmasında aşağıda yazılı terimler karşılarındaki yazılı açıklamaları ifade edeceklerdir.
                      </div>
                      <ul style=""margin-bottom: 24px"">
                        <li style=""margin-bottom: 6px;""><span style=""font-weight: 600;"">BAKAN: </span>Gümrük ve Ticaret Bakanı’nı,</li>
                        <li style=""margin-bottom: 6px;""><span style=""font-weight: 600;"">BAKANLIK: </span>Gümrük ve Ticaret Bakanlığı’nı,</li>
                        <li style=""margin-bottom: 6px;""><span style=""font-weight: 600;"">KANUN: </span>6502 sayılı Tüketicinin Korunması Hakkında Kanun’u,</li>
                        <li style=""margin-bottom: 6px;""><span style=""font-weight: 600;"">YÖNETMELİK: </span>Mesafeli Sözleşmeler Yönetmeliği’ni (RG:27.11.2014/29188)</li>
                        <li style=""margin-bottom: 6px;"">
                          <span style=""font-weight: 600;"">HİZMET: </span> Bir ücret veya menfaat karşılığında yapılan ya da yapılması taahhüt edilen mal sağlama dışındaki her türlü
                          tüketici işleminin konusunu,
                        </li>
                        <li style=""margin-bottom: 6px;""><span style=""font-weight: 600;"">SİTE: </span>SATICI’ya ait internet sitesini,</li>
                        <li style=""margin-bottom: 6px;""><span style=""font-weight: 600;"">TARAFLAR: </span>SATICI ve ALICI’yı,</li>
                        <li style=""margin-bottom: 6px;""><span style=""font-weight: 600;"">SÖZLEŞME: </span>SATICI ve ALICI arasında akdedilen işbu sözleşmeyi,</li>
                        <li style=""margin-bottom: 6px;"">
                          <span style=""font-weight: 600;"">MAL: </span>Alışverişe konu olan taşınır eşyayı ve elektronik ortamda kullanılmak üzere hazırlanan yazılım, ses, görüntü ve
                          benzeri gayri maddi malları ifade eder.
                        </li>
                      </ul>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">3. KONU</div>
                      <div style=""margin-bottom: 24px;"">
                        İşbu Sözleşme, ALICI’nın, SATICI’ya ait internet sitesi üzerinden elektronik ortamda siparişini verdiği aşağıda nitelikleri ve satış fiyatı belirtilen
                        ürünün satışı ve teslimi ile ilgili olarak 6502 sayılı Tüketicinin Korunması Hakkında Kanun ve Mesafeli Sözleşmelere Dair Yönetmelik hükümleri gereğince
                        tarafların hak ve yükümlülüklerini düzenler.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        Listelenen ve sitede ilan edilen fiyatlar satış fiyatıdır. İlan edilen fiyatlar ve vaatler güncelleme yapılana ve değiştirilene kadar geçerlidir. Süreli
                        olarak ilan edilen fiyatlar ise belirtilen süre sonuna kadar geçerlidir.
                      </div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">4. SÖZLEŞME KONUSU ÜRÜN BİLGİLERİ</div>
                      <table style=""width: 100%; margin-bottom: 24px; border-collapse: collapse; border: 1px solid #ddd;"">
                        <thead>
                          <tr style=""background-color: #f8f9fa;"">
                            <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Ürün Adı</th>
                            <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Adet</th>
                            <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Birim Fiyatı</th>
                            <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Toplam Fiyat</th>
                          </tr>
                        </thead>
                        <tbody>
                            {Replacements.Products}
                        </tbody>
                      </table>
                      <div style=""margin-bottom: 24px;""><strong>Genel Toplam:</strong> {Replacements.GrandTotal} (Fiyatlara KDV dahildir.)</div>
                      <div style=""margin-bottom: 24px;""><strong>Ödeme yöntemi:</strong> {Replacements.OrderPaymentType}</div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">
                        5. GÜVENLİK-GİZLİLİK, KİŞİSEL VERİLER, ELEKTRONİK İLETİŞİMLER VE FİKRİ-SINAİ HAKLAR İLE İLGİLİ KURALLAR
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        INTERNET SİTESİ'nde bilgilerin korunması, gizliliği, işlenmesi-kullanımı ve iletişimler ile diğer hususlarda aşağıda cari esasları belirtilen gizlilik
                        kuralları-politikası ve şartlar geçerlidir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        5.1.ALICI tarafından İNTERNET SİTESİ'nde girilen bilgilerin ve işlemlerin güvenliği için gerekli önlemler, SATICI tarafındaki sistem altyapısında, bilgi ve
                        işlemin mahiyetine göre günümüz teknik imkanları ölçüsünde alınmıştır. Bununla beraber, söz konusu bilgiler ALICI cihazından girildiğinden ALICI tarafında
                        korunmaları ve ilgisiz kişilerce erişilememesi için, virüs ve benzeri zararlı uygulamalara ilişkin olanlar dahil, gerekli tedbirlerin alınması sorumluluğu
                        ALICI'ya aittir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        5.2. ALICI'nın sair suretle verdiği kişisel veri ve ticari elektronik iletişimlerine dair izin-onaylarının yanısıra ve teyiden; ALICI'nın İNTERNET SİTESİ'ne
                        üyeliği ve alışverişleri sırasında edinilen bilgileri SATICI, C muhtelif ürün/hizmetlerin sağlanması ve her türlü bilgilendirme, reklam-tanıtım, iletişim,
                        promosyon, satış, pazarlama, mağaza kartı, kredi kartı ve üyelik uygulamaları amaçlı yapılacak elektronik ve diğer ticari-sosyal iletişimler için,
                        belirtilenler ve halefleri nezdinde süresiz olarak veya öngörecekleri süre ile kayda alınabilir, basılı/manyetik arşivlerde saklanabilir, gerekli görülen
                        hallerde güncellenebilir, paylaşılabilir, aktarılabilir, transfer edilebilir, kullanılabilir ve sair suretlerle işlenebilir. Bu veriler ayrıca kanunen
                        gereken durumlarda ilgili Merci ve Mahkemelere iletilebilir. ALICI kişisel olan-olmayan mevcut ve yeni bilgilerinin, kişisel verilerin korunması hakkında
                        mevzuat ile elektronik ticaret mevzuatına uygun biçimde yukarıdaki kapsamda kullanımına, paylaşımına, işlenmesine ve kendisine ticari olan-olmayan
                        elektronik iletişimler ve diğer iletişimler yapılmasına muvafakat ve izin vermiştir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        5.3. ALICI SATICI'ya belirtilen iletişim kanallarından ulaşarak veri kullanımı-işlenmelerini ve/veya aynı kanallardan kanuni usulünce ulaşarak ya da
                        kendisine gönderilen elektronik iletişimlerdeki red hakkını kullanarak iletişimleri her zaman için durdurabilir. ALICI'nın bu husustaki açık bildirimine
                        göre, kişisel veri işlemleri ve/veya tarafına iletişimler yasal azami süre içinde durdurulur; ayrıca dilerse, hukuken muhafazası gerekenler ve/veya mümkün
                        olanlar haricindeki bilgileri, veri kayıt sisteminden silinir ya da kimliği belli olmayacak biçimde anonim hale getirilir. ALICI isterse kişisel verilerinin
                        işlenmesi ile ilgili işlemler, aktarıldığı kişiler, eksik veya yanlış olması halinde düzeltilmesi, düzeltilen bilgilerin ilgili üçüncü kişilere
                        bildirilmesi, verilerin silinmesi veya yok edilmesi, otomatik sistemler ile analiz edilmesi sureti ile kendisi aleyhine bir sonucun ortaya çıkmasına itiraz,
                        verilerin kanuna aykırı olarak işlenmesi sebebi ile zarara uğrama halinde giderilmesi gibi konularda SATICI'ya her zaman yukarıdaki iletişim kanallarından
                        başvurabilir ve bilgi alabilir. Bu hususlardaki başvuru ve talepleri yasal azami süreler içinde yerine getirilecek yahut hukuki gerekçesi tarafına
                        açıklanarak kabul edilmeyebilecektir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        5.4. INTERNET SİTESİ'ne ait her türlü bilgi ve içerik ile bunların düzenlenmesi, revizyonu ve kısmen/tamamen kullanımı konusunda; SATICI'nın anlaşmasına
                        göre diğer üçüncü sahıslara ait olanlar hariç; tüm fikri-sınai haklar ve mülkiyet hakları SATICI'ya aittir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        5.5. SATICI yukarıdaki konularda gerekli görebileceği her türlü değişikliği yapma hakkını saklı tutar; bu değişiklikler SATICI tarafından INTERNET
                        SİTESİ'nden veya diğer uygun yöntemler ile duyurulduğu andan itibaren geçerli olur.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        5.6. INTERNET SİTESİ'nden ulaşılan diğer sitelerde kendilerine ait gizlilik-güvenlik politikaları ve kullanım şartları geçerlidir, oluşabilecek ihtilaflar
                        ile menfi neticelerinden SATICI sorumlu değildir.
                      </div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">6. GENEL HÜKÜMLER</div>
                      <div style=""margin-bottom: 24px;"">
                        6.1. ALICI, SATICI’ya ait internet sitesinde sözleşme konusu ürünün temel nitelikleri, satış fiyatı ve ödeme şekli ile teslimata ilişkin ön bilgileri
                        okuyup, bilgi sahibi olduğunu, elektronik ortamda gerekli teyidi verdiğini kabul, beyan ve taahhüt eder. ALICI’nın; Ön Bilgilendirmeyi elektronik ortamda
                        teyit etmesi, mesafeli satış sözleşmesinin kurulmasından evvel, SATICI tarafından ALICI' ya verilmesi gereken adresi, siparişi verilen ürünlere ait temel
                        özellikleri, ürünlerin vergiler dâhil fiyatını, ödeme ve teslimat bilgilerini de doğru ve eksiksiz olarak edindiğini kabul, beyan ve taahhüt eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.2. Sözleşme konusu her bir ürün, 30 günlük yasal süreyi aşmamak kaydı ile ALICI' nın yerleşim yeri uzaklığına bağlı olarak internet sitesindeki ön
                        bilgiler kısmında belirtilen süre zarfında ALICI veya ALICI’nın gösterdiği adresteki kişi ve/veya kuruluşa teslim edilir. Bu süre içinde ürünün ALICI’ya
                        teslim edilememesi durumunda, ALICI’nın sözleşmeyi feshetme hakkı saklıdır.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.3. SATICI, Sözleşme konusu ürünü eksiksiz, siparişte belirtilen niteliklere uygun ve varsa garanti belgeleri, kullanım kılavuzları işin gereği olan bilgi
                        ve belgeler ile teslim etmeyi, her türlü ayıptan arî olarak yasal mevzuat gereklerine göre sağlam, standartlara uygun bir şekilde işi doğruluk ve dürüstlük
                        esasları dâhilinde ifa etmeyi, hizmet kalitesini koruyup yükseltmeyi, işin ifası sırasında gerekli dikkat ve özeni göstermeyi, ihtiyat ve öngörü ile hareket
                        etmeyi kabul, beyan ve taahhüt eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.4. SATICI, sözleşmeden doğan ifa yükümlülüğünün süresi dolmadan ALICI’yı bilgilendirmek ve açıkça onayını almak suretiyle eşit kalite ve fiyatta farklı
                        bir ürün tedarik edebilir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.5. SATICI, sipariş konusu ürün veya hizmetin yerine getirilmesinin imkânsızlaşması halinde sözleşme konusu yükümlülüklerini yerine getiremezse, bu durumu,
                        öğrendiği tarihten itibaren 3 gün içinde yazılı olarak tüketiciye bildireceğini, 14 günlük süre içinde toplam bedeli ALICI’ya iade edeceğini kabul, beyan ve
                        taahhüt eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.6. ALICI, Sözleşme konusu ürünün teslimatı için işbu Sözleşme’yi elektronik ortamda teyit edeceğini, herhangi bir nedenle sözleşme konusu ürün bedelinin
                        ödenmemesi ve/veya banka kayıtlarında iptal edilmesi halinde, SATICI’nın sözleşme konusu ürünü teslim yükümlülüğünün sona ereceğini kabul, beyan ve taahhüt
                        eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.7. ALICI, Sözleşme konusu ürünün ALICI veya ALICI’nın gösterdiği adresteki kişi ve/veya kuruluşa tesliminden sonra ALICI'ya ait kredi kartının yetkisiz
                        kişilerce haksız kullanılması sonucunda sözleşme konusu ürün bedelinin ilgili banka veya finans kuruluşu tarafından SATICI'ya ödenmemesi halinde, ALICI
                        Sözleşme konusu ürünü 3 gün içerisinde nakliye gideri SATICI’ya ait olacak şekilde SATICI’ya iade edeceğini kabul, beyan ve taahhüt eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.8. SATICI, tarafların iradesi dışında gelişen, önceden öngörülemeyen ve tarafların borçlarını yerine getirmesini engelleyici ve/veya geciktirici hallerin
                        oluşması gibi mücbir sebepler halleri nedeni ile sözleşme konusu ürünü süresi içinde teslim edemez ise, durumu ALICI'ya bildireceğini kabul, beyan ve
                        taahhüt eder. ALICI da siparişin iptal edilmesini, sözleşme konusu ürünün varsa emsali ile değiştirilmesini ve/veya teslimat süresinin engelleyici durumun
                        ortadan kalkmasına kadar ertelenmesini SATICI’dan talep etme hakkını haizdir. ALICI tarafından siparişin iptal edilmesi halinde ALICI’nın nakit ile yaptığı
                        ödemelerde, ürün tutarı 14 gün içinde kendisine nakden ve defaten ödenir. ALICI’nın kredi kartı ile yaptığı ödemelerde ise, ürün tutarı, siparişin ALICI
                        tarafından iptal edilmesinden sonra 14 gün içerisinde ilgili bankaya iade edilir. ALICI, SATICI tarafından kredi kartına iade edilen tutarın banka
                        tarafından ALICI hesabına yansıtılmasına ilişkin ortalama sürecin 2 ile 3 haftayı bulabileceğini, bu tutarın bankaya iadesinden sonra ALICI’nın hesaplarına
                        yansıması halinin tamamen banka işlem süreci ile ilgili olduğundan, ALICI, olası gecikmeler için SATICI’yı sorumlu tutamayacağını kabul, beyan ve taahhüt
                        eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.6. SATICININ, ALICI tarafından siteye kayıt formunda belirtilen veya daha sonra kendisi tarafından güncellenen adresi, e-posta adresi, sabit ve mobil
                        telefon hatları ve diğer iletişim bilgileri üzerinden mektup, e-posta, SMS, telefon görüşmesi ve diğer yollarla iletişim, pazarlama, bildirim ve diğer
                        amaçlarla ALICI’ya ulaşma hakkı bulunmaktadır. ALICI, işbu sözleşmeyi kabul etmekle SATICI’nın kendisine yönelik yukarıda belirtilen iletişim
                        faaliyetlerinde bulunabileceğini kabul ve beyan etmektedir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.10. ALICI, sözleşme konusu mal/hizmeti teslim almadan önce muayene edecek; ezik, kırık, ambalajı yırtılmış vb. hasarlı ve ayıplı mal/hizmeti kargo
                        şirketinden teslim almayacaktır. Teslim alınan mal/hizmetin hasarsız ve sağlam olduğu kabul edilecektir. Teslimden sonra mal/hizmetin özenle korunması
                        borcu, ALICI’ya aittir. Cayma hakkı kullanılacaksa mal/hizmet kullanılmamalıdır. Fatura iade edilmelidir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.11. ALICI ile sipariş esnasında kullanılan kredi kartı hamilinin aynı kişi olmaması veya ürünün ALICI’ya tesliminden evvel, siparişte kullanılan kredi
                        kartına ilişkin güvenlik açığı tespit edilmesi halinde, SATICI, kredi kartı hamiline ilişkin kimlik ve iletişim bilgilerini, siparişte kullanılan kredi
                        kartının bir önceki aya ait ekstresini yahut kart hamilinin bankasından kredi kartının kendisine ait olduğuna ilişkin yazıyı ibraz etmesini ALICI’dan talep
                        edebilir. ALICI’nın talebe konu bilgi/belgeleri temin etmesine kadar geçecek sürede sipariş dondurulacak olup, mezkur taleplerin 24 saat içerisinde
                        karşılanmaması halinde ise SATICI, siparişi iptal etme hakkını haizdir.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.12. ALICI, SATICI’ya ait internet sitesine üye olurken verdiği kişisel ve diğer sair bilgilerin gerçeğe uygun olduğunu, SATICI’nın bu bilgilerin gerçeğe
                        aykırılığı nedeniyle uğrayacağı tüm zararları, SATICI’nın ilk bildirimi üzerine derhal, nakden ve defaten tazmin edeceğini beyan ve taahhüt eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.13. ALICI, SATICI’ya ait internet sitesini kullanırken yasal mevzuat hükümlerine riayet etmeyi ve bunları ihlal etmemeyi baştan kabul ve taahhüt eder.
                        Aksi takdirde, doğacak tüm hukuki ve cezai yükümlülükler tamamen ve münhasıran ALICI’yı bağlayacaktır.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.14. ALICI, SATICI’ya ait internet sitesini hiçbir şekilde kamu düzenini bozucu, genel ahlaka aykırı, başkalarını rahatsız ve taciz edici şekilde, yasalara
                        aykırı bir amaç için, başkalarının maddi ve manevi haklarına tecavüz edecek şekilde kullanamaz. Ayrıca, üye başkalarının hizmetleri kullanmasını önleyici
                        veya zorlaştırıcı faaliyet (spam, virus, truva atı, vb.) işlemlerde bulunamaz.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.15. SATICI’ya ait internet sitesinin üzerinden, SATICI’nın kendi kontrolünde olmayan ve/veya başkaca üçüncü kişilerin sahip olduğu ve/veya işlettiği başka
                        web sitelerine ve/veya başka içeriklere link verilebilir. Bu linkler ALICI’ya yönlenme kolaylığı sağlamak amacıyla konmuş olup herhangi bir web sitesini
                        veya o siteyi işleten kişiyi desteklememekte ve Link verilen web sitesinin içerdiği bilgilere yönelik herhangi bir garanti niteliği taşımamaktadır.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        6.16. İşbu sözleşme içerisinde sayılan maddelerden bir ya da birkaçını ihlal eden üye işbu ihlal nedeniyle cezai ve hukuki olarak şahsen sorumlu olup,
                        SATICI’yı bu ihlallerin hukuki ve cezai sonuçlarından ari tutacaktır. Ayrıca; işbu ihlal nedeniyle, olayın hukuk alanına intikal ettirilmesi halinde,
                        SATICI’nın üyeye karşı üyelik sözleşmesine uyulmamasından dolayı tazminat talebinde bulunma hakkı saklıdır.
                      </div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">7. CAYMA HAKKI</div>
                      <div style=""margin-bottom: 24px;"">
                        7.1. ALICI; mesafeli sözleşmenin mal satışına ilişkin olması durumunda, ürünün kendisine veya gösterdiği adresteki kişi/kuruluşa teslim tarihinden itibaren
                        14 (on dört) gün içerisinde, SATICI’ya bildirmek şartıyla hiçbir hukuki ve cezai sorumluluk üstlenmeksizin ve hiçbir gerekçe göstermeksizin malı reddederek
                        sözleşmeden cayma hakkını kullanabilir. Hizmet sunumuna ilişkin mesafeli sözleşmelerde ise, bu süre sözleşmenin imzalandığı tarihten itibaren başlar. Cayma
                        hakkı süresi sona ermeden önce, tüketicinin onayı ile hizmetin ifasına başlanan hizmet sözleşmelerinde cayma hakkı kullanılamaz. Cayma hakkının
                        kullanımından kaynaklanan masraflar SATICI’ ya aittir. ALICI, iş bu sözleşmeyi kabul etmekle, cayma hakkı konusunda bilgilendirildiğini peşinen kabul eder.
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        7.2. Cayma hakkının kullanılması için 14 (ondört) günlük süre içinde SATICI' ya iadeli taahhütlü posta, faks veya eposta ile yazılı bildirimde bulunulması
                        ve ürünün işbu sözleşmede düzenlenen ""Cayma Hakkı Kullanılamayacak Ürünler"" hükümleri çerçevesinde kullanılmamış olması şarttır. Bu hakkın kullanılması
                        halinde,
                      </div>
                      <ul style=""margin-bottom: 24px;"">
                        <li style=""margin-bottom: 6px;"">
                          a. 3. kişiye veya ALICI’ ya teslim edilen ürünün faturası, (İade edilmek istenen ürünün faturası kurumsal ise, iade ederken kurumun düzenlemiş olduğu iade
                          faturası ile birlikte gönderilmesi gerekmektedir. Faturası kurumlar adına düzenlenen sipariş iadeleri İADE FATURASI kesilmediği takdirde
                          tamamlanamayacaktır.)
                        </li>
                        <li style=""margin-bottom: 6px;"">b. İade formu,</li>
                        <li style=""margin-bottom: 6px;"">
                          c. İade edilecek ürünlerin kutusu, ambalajı, varsa standart aksesuarları ile birlikte eksiksiz ve hasarsız olarak teslim edilmesi gerekmektedir.
                        </li>
                        <li style=""margin-bottom: 6px;"">
                          d. SATICI, cayma bildiriminin kendisine ulaşmasından itibaren en geç 10 günlük süre içerisinde toplam bedeli ve ALICI’yı borç altına sokan belgeleri
                          ALICI’ ya iade etmek ve 20 günlük süre içerisinde malı iade almakla yükümlüdür.
                        </li>
                        <li style=""margin-bottom: 6px;"">
                          e. ALICI’ nın kusurundan kaynaklanan bir nedenle malın değerinde bir azalma olursa veya iade imkânsızlaşırsa ALICI kusuru oranında SATICI’ nın zararlarını
                          tazmin etmekle yükümlüdür. Ancak cayma hakkı süresi içinde malın veya ürünün usulüne uygun kullanılması sebebiyle meydana gelen değişiklik ve
                          bozulmalardan ALICI sorumlu değildir.
                        </li>
                        <li style=""margin-bottom: 6px;"">
                          f. Cayma hakkının kullanılması nedeniyle SATICI tarafından düzenlenen kampanya limit tutarının altına düşülmesi halinde kampanya kapsamında faydalanılan
                          indirim miktarı iptal edilir.
                        </li>
                      </ul>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">8. CAYMA HAKKI KULLANILAMAYACAK ÜRÜNLER</div>
                      <div style=""margin-bottom: 24px;"">
                        ALICI’nın isteği veya açıkça kişisel ihtiyaçları doğrultusunda hazırlanan ve geri gönderilmeye müsait olmayan, iç giyim alt parçaları, mayo ve bikini
                        altları, makyaj malzemeleri, tek kullanımlık ürünler, çabuk bozulma tehlikesi olan veya son kullanma tarihi geçme ihtimali olan mallar, ALICI’ya teslim
                        edilmesinin ardından ALICI tarafından ambalajı açıldığı takdirde iade edilmesi sağlık ve hijyen açısından uygun olmayan ürünler, teslim edildikten sonra
                        başka ürünlerle karışan ve doğası gereği ayrıştırılması mümkün olmayan ürünler, Abonelik sözleşmesi kapsamında sağlananlar dışında, gazete ve dergi gibi
                        süreli yayınlara ilişkin mallar, Elektronik ortamda anında ifa edilen hizmetler veya tüketiciye anında teslim edilen gayrimaddi mallar, ile ses veya görüntü
                        kayıtlarının, kitap, dijital içerik, yazılım programlarının, veri kaydedebilme ve veri depolama cihazlarının, bilgisayar sarf malzemelerinin, ambalajının
                        ALICI tarafından açılmış olması halinde iadesi Yönetmelik gereği mümkün değildir. Ayrıca Cayma hakkı süresi sona ermeden önce, tüketicinin onayı ile ifasına
                        başlanan hizmetlere ilişkin cayma hakkının kullanılması da Yönetmelik gereği mümkün değildir
                      </div>
                      <div style=""margin-bottom: 24px;"">
                        Kozmetik ve kişisel bakım ürünleri, iç giyim ürünleri, mayo, bikini, kitap, kopyalanabilir yazılım ve programlar, DVD, VCD, CD ve kasetler ile kırtasiye
                        sarf malzemeleri (toner, kartuş, şerit vb.) iade edilebilmesi için ambalajlarının açılmamış, denenmemiş, bozulmamış ve kullanılmamış olmaları gerekir
                      </div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">9. TEMERRÜT HALİ VE HUKUKİ SONUÇLARI</div>
                      <div style=""margin-bottom: 24px;"">
                        ALICI, ödeme işlemlerini kredi kartı ile yaptığı durumda temerrüde düştüğü takdirde, kart sahibi banka ile arasındaki kredi kartı sözleşmesi çerçevesinde
                        faiz ödeyeceğini ve bankaya karşı sorumlu olacağını kabul, beyan ve taahhüt eder. Bu durumda ilgili banka hukuki yollara başvurabilir; doğacak masrafları ve
                        vekâlet ücretini ALICI’dan talep edebilir ve her koşulda ALICI’nın borcundan dolayı temerrüde düşmesi halinde, ALICI, borcun gecikmeli ifasından dolayı
                        SATICI’nın uğradığı zarar ve ziyanını ödeyeceğini kabul, beyan ve taahhüt eder
                      </div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">10. YETKİLİ MAHKEME</div>
                      <div style=""margin-bottom: 24px;"">
                        İşbu sözleşmeden doğan uyuşmazlıklarda şikayet ve itirazlar, Kanunda belirtilen parasal sınırlar dâhilinde tüketicinin yerleşim yerinin bulunduğu veya
                        tüketici işleminin yapıldığı yerdeki tüketici sorunları hakem heyetine veya tüketici mahkemesine yapılacaktır.
                      </div>

                      <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">11. YÜRÜRLÜK</div>
                      <div style=""margin-bottom: 24px;"">
                        ALICI, Site üzerinden verdiği siparişe ait ödemeyi gerçekleştirdiğinde işbu sözleşmenin tüm şartlarını kabul etmiş sayılır. SATICI, siparişin gerçekleşmesi
                        öncesinde işbu sözleşmenin sitede ALICI tarafından okunup kabul edildiğine dair onay alacak şekilde gerekli yazılımsal düzenlemeleri yapmakla yükümlüdür.
                      </div>

                      <table style=""width: 100%; margin-bottom: 24px; border-collapse: collapse;"">
                        <tr>
                            <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">SATICI</td>
                            <td style=""padding: 8px 0;"">{Replacements.CompanyLegalName}</td>
                        </tr>
                        <tr>
                            <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">ALICI</td>
                            <td style=""padding: 8px 0;"">{Replacements.UserFullName}</td>
                        </tr>
                        <tr>
                            <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">TARİH</td>
                            <td style=""padding: 8px 0;"">{Replacements.CreatedDate}</td>
                        </tr>
                    </table>
                </div>";
        }

        public static class PreliminaryInformationForm
        {
            public static class Replacements
            {
                public static string CompanyLegalName = "{COMPANY_LEGALNAME}";
                public static string CompanyAddress = "{COMPANY_ADDRESS}";
                public static string CompanyAddressDistrict = "{COMPANY_ADDRESS_DISTRICT}";
                public static string CompanyAddressCity = "{COMPANY_ADDRESS_CITY}";
                public static string CompanyEmail = "{COMPANY_EMAIL}";
                public static string CompanyPhoneNumber = "{COMPANY_PHONE_NUMBER}";
                public static string UserFullName = "{USER_FULL_NAME}";
                public static string UserAddress = "{USER_ADDRESS}";
                public static string UserAddressDistrict = "{USER_ADDRESS_DISTRICT}";
                public static string UserAddressCity = "{USER_ADDRESS_CITY}";
                public static string UserTaxNumber = "{USER_TAX_NUMBER}";
                public static string UserEmail = "{USER_EMAIL}";
                public static string UserPhoneNumber = "{USER_PHONE_NUMBER}";
                public static string Products = "{PRODUCTS}";
                public static string GrandTotal = "{GRAND_TOTAL}";
                public static string OrderPaymentType = "{ORDER_PAYMENT_TYPE}";
            }

            public static string Document = @$"<div style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 800px; margin: 0 auto; padding: 20px;"">
                        <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">TARAFLAR</div>
    
                        <div style=""margin-bottom: 24px;"">Bu sözleşme, aşağıdaki taraflar arasında belirtilen hüküm ve koşullara uygun olarak akdedilmiştir:</div>
    
                        <div style=""margin-bottom: 24px;"">
                            <div style=""font-size: 18px; font-weight: bold; margin-bottom: 8px;"">SATICI:</div>
                            <table style=""width: 100%; margin-bottom: 24px; border-collapse: collapse;"">
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Firma adı</td>
                                    <td style=""padding: 8px 0;"">{Replacements.CompanyLegalName}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Adres</td>
                                    <td style=""padding: 8px 0;"">{Replacements.CompanyAddress}<br>{Replacements.CompanyAddressDistrict} / {Replacements.CompanyAddressCity}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">E-posta</td>
                                    <td style=""padding: 8px 0;"">{Replacements.CompanyEmail}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Telefon numarası</td>
                                    <td style=""padding: 8px 0;"">{Replacements.CompanyPhoneNumber}</td>
                                </tr>
                            </table>

                            <div style=""font-size: 18px; font-weight: bold; margin-bottom: 8px;"">ALICI:</div>
                            <table style=""width: 100%; margin-bottom: 24px; border-collapse: collapse;"">
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Ad Soyad / Ünvan</td>
                                    <td style=""padding: 8px 0;"">{Replacements.UserFullName}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Adres</td>
                                    <td style=""padding: 8px 0;"">{Replacements.UserAddress}<br>{Replacements.UserAddressDistrict} / {Replacements.UserAddressCity}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">TC Kimlik No / VKN</td>
                                    <td style=""padding: 8px 0;"">{Replacements.UserTaxNumber}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">E-posta</td>
                                    <td style=""padding: 8px 0;"">{Replacements.UserEmail}</td>
                                </tr>
                                <tr>
                                    <td style=""padding: 8px 0; width: 30%; font-weight: 600;"">Telefon numarası</td>
                                    <td style=""padding: 8px 0;"">{Replacements.UserPhoneNumber}</td>
                                </tr>
                            </table>

                            <div style=""margin-bottom: 24px;"">
                                İş bu sözleşmeyi kabul etmekle ALICI, sözleşme konusu siparişi onayladığı takdirde sipariş konusu bedeli ve varsa kargo ücreti, vergi gibi belirtilen ek
                                ücretleri ödeme yükümlülüğü altına gireceğini ve bu konuda bilgilendirildiğini peşinen kabul eder.
                            </div>
                        </div>

                        <div style=""font-size: 18px; font-weight: 600; margin-bottom: 24px;"">ÜRÜN BİLGİLERİ</div>
                        <table style=""width: 100%; margin-bottom: 24px; border-collapse: collapse; border: 1px solid #ddd;"">
                            <thead>
                                <tr style=""background-color: #f8f9fa;"">
                                    <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Ürün Adı</th>
                                    <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Adet</th>
                                    <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Birim Fiyatı</th>
                                    <th style=""padding: 8px; border: 1px solid #ddd; text-align: left;"">Toplam Fiyat</th>
                                </tr>
                            </thead>
                            <tbody>
                                {Replacements.Products}
                            </tbody>
                        </table>
    
                        <div style=""margin-bottom: 24px;""><strong>Genel Toplam:</strong> {Replacements.GrandTotal} (Fiyatlara KDV dahildir.)</div>
                        <div style=""margin-bottom: 24px;""><strong>Ödeme yöntemi:</strong> {Replacements.OrderPaymentType}</div>

                        <div style=""margin-bottom: 24px;"">
                            Bu metnin amacı {Replacements.CompanyLegalName} (www.adanom.com) tarafından işletilmesi yapılan sitede ÜYE/MÜŞTERİ tarafından yapılan alışveriş hakkında
                            bilgilendirilmesidir. ÜYE/MÜŞTERİ nin yasal ve taraflar arasında akdedilen üyelik sözleşmesi ve mesafeli satış sözleşmesi doğan hakları saklıdır.
                        </div>
                    </div>";
        }
    }
}
