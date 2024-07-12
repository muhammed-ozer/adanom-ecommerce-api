namespace Adanom.Ecommerce.API.Data
{
    internal static class AddressDataScripts
    {
        internal static string AddressCitiesScript { get; set; }

        internal static string AddressDistrictsScript { get; set; }

        static AddressDataScripts()
        {
            AddressCitiesScript = @"
                INSERT INTO [dbo].[AddressCities] values ('01', '﻿Adana', '01'); 
                INSERT INTO [dbo].[AddressCities] values ('02', 'Adıyaman', '02'); 
                INSERT INTO [dbo].[AddressCities] values ('03', 'Afyon', '03'); 
                INSERT INTO [dbo].[AddressCities] values ('04', 'Ağrı', '04'); 
                INSERT INTO [dbo].[AddressCities] values ('05', 'Amasya', '05'); 
                INSERT INTO [dbo].[AddressCities] values ('06', 'Ankara', '06'); 
                INSERT INTO [dbo].[AddressCities] values ('07', 'Antalya', '07'); 
                INSERT INTO [dbo].[AddressCities] values ('08', 'Artvin', '08'); 
                INSERT INTO [dbo].[AddressCities] values ('09', 'Aydın', '09'); 
                INSERT INTO [dbo].[AddressCities] values ('10', 'Balıkesir', '10'); 
                INSERT INTO [dbo].[AddressCities] values ('11', 'Bilecik', '11'); 
                INSERT INTO [dbo].[AddressCities] values ('12', 'Bingöl', '12'); 
                INSERT INTO [dbo].[AddressCities] values ('13', 'Bitlis', '13'); 
                INSERT INTO [dbo].[AddressCities] values ('14', 'Bolu', '14'); 
                INSERT INTO [dbo].[AddressCities] values ('15', 'Burdur', '15'); 
                INSERT INTO [dbo].[AddressCities] values ('16', 'Bursa', '16'); 
                INSERT INTO [dbo].[AddressCities] values ('17', 'Çanakkale', '17'); 
                INSERT INTO [dbo].[AddressCities] values ('18', 'Çankırı', '18'); 
                INSERT INTO [dbo].[AddressCities] values ('19', 'Çorum', '19'); 
                INSERT INTO [dbo].[AddressCities] values ('20', 'Denizli', '20'); 
                INSERT INTO [dbo].[AddressCities] values ('21', 'Diyarbakır', '21'); 
                INSERT INTO [dbo].[AddressCities] values ('22', 'Edirne', '22'); 
                INSERT INTO [dbo].[AddressCities] values ('23', 'Elazığ', '23'); 
                INSERT INTO [dbo].[AddressCities] values ('24', 'Erzincan', '24'); 
                INSERT INTO [dbo].[AddressCities] values ('25', 'Erzurum', '25'); 
                INSERT INTO [dbo].[AddressCities] values ('26', 'Eskişehir', '26'); 
                INSERT INTO [dbo].[AddressCities] values ('27', 'Gaziantep', '27'); 
                INSERT INTO [dbo].[AddressCities] values ('28', 'Giresun', '28'); 
                INSERT INTO [dbo].[AddressCities] values ('29', 'Gümüşhane', '29'); 
                INSERT INTO [dbo].[AddressCities] values ('30', 'Hakkari', '30'); 
                INSERT INTO [dbo].[AddressCities] values ('31', 'Hatay', '31'); 
                INSERT INTO [dbo].[AddressCities] values ('32', 'Isparta', '32'); 
                INSERT INTO [dbo].[AddressCities] values ('33', 'Mersin', '33'); 
                INSERT INTO [dbo].[AddressCities] values ('34', 'İstanbul', '34'); 
                INSERT INTO [dbo].[AddressCities] values ('35', 'İzmir', '35'); 
                INSERT INTO [dbo].[AddressCities] values ('36', 'Kars', '36'); 
                INSERT INTO [dbo].[AddressCities] values ('37', 'Kastamonu', '37'); 
                INSERT INTO [dbo].[AddressCities] values ('38', 'Kayseri', '38'); 
                INSERT INTO [dbo].[AddressCities] values ('39', 'Kırklareli', '39'); 
                INSERT INTO [dbo].[AddressCities] values ('40', 'Kırşehir', '40'); 
                INSERT INTO [dbo].[AddressCities] values ('41', 'Kocaeli', '41'); 
                INSERT INTO [dbo].[AddressCities] values ('42', 'Konya', '42'); 
                INSERT INTO [dbo].[AddressCities] values ('43', 'Kütahya', '43'); 
                INSERT INTO [dbo].[AddressCities] values ('44', 'Malatya', '44'); 
                INSERT INTO [dbo].[AddressCities] values ('45', 'Manisa', '45'); 
                INSERT INTO [dbo].[AddressCities] values ('46', 'K.Maraş', '46'); 
                INSERT INTO [dbo].[AddressCities] values ('47', 'Mardin', '47'); 
                INSERT INTO [dbo].[AddressCities] values ('48', 'Muğla', '48'); 
                INSERT INTO [dbo].[AddressCities] values ('49', 'Muş', '49'); 
                INSERT INTO [dbo].[AddressCities] values ('50', 'Nevşehir', '50'); 
                INSERT INTO [dbo].[AddressCities] values ('51', 'Niğde', '51'); 
                INSERT INTO [dbo].[AddressCities] values ('52', 'Ordu', '52'); 
                INSERT INTO [dbo].[AddressCities] values ('53', 'Rize', '53'); 
                INSERT INTO [dbo].[AddressCities] values ('54', 'Sakarya', '54'); 
                INSERT INTO [dbo].[AddressCities] values ('55', 'Samsun', '55'); 
                INSERT INTO [dbo].[AddressCities] values ('56', 'Siirt', '56'); 
                INSERT INTO [dbo].[AddressCities] values ('57', 'Sinop', '57'); 
                INSERT INTO [dbo].[AddressCities] values ('58', 'Sivas', '58'); 
                INSERT INTO [dbo].[AddressCities] values ('59', 'Tekirdağ', '59'); 
                INSERT INTO [dbo].[AddressCities] values ('60', 'Tokat', '60'); 
                INSERT INTO [dbo].[AddressCities] values ('61', 'Trabzon', '61'); 
                INSERT INTO [dbo].[AddressCities] values ('62', 'Tunceli', '62'); 
                INSERT INTO [dbo].[AddressCities] values ('63', 'Şanlıurfa', '63'); 
                INSERT INTO [dbo].[AddressCities] values ('64', 'Uşak', '64'); 
                INSERT INTO [dbo].[AddressCities] values ('65', 'Van', '65'); 
                INSERT INTO [dbo].[AddressCities] values ('66', 'Yozgat', '66'); 
                INSERT INTO [dbo].[AddressCities] values ('67', 'Zonguldak', '67'); 
                INSERT INTO [dbo].[AddressCities] values ('68', 'Aksaray', '68'); 
                INSERT INTO [dbo].[AddressCities] values ('69', 'Bayburt', '69'); 
                INSERT INTO [dbo].[AddressCities] values ('70', 'Karaman', '70'); 
                INSERT INTO [dbo].[AddressCities] values ('71', 'Kırıkkale', '71'); 
                INSERT INTO [dbo].[AddressCities] values ('72', 'Batman', '72'); 
                INSERT INTO [dbo].[AddressCities] values ('73', 'Şırnak', '73'); 
                INSERT INTO [dbo].[AddressCities] values ('74', 'Bartın', '74'); 
                INSERT INTO [dbo].[AddressCities] values ('75', 'Ardahan', '75'); 
                INSERT INTO [dbo].[AddressCities] values ('76', 'Iğdır', '76'); 
                INSERT INTO [dbo].[AddressCities] values ('77', 'Yalova', '77'); 
                INSERT INTO [dbo].[AddressCities] values ('78', 'Karabük', '78'); 
                INSERT INTO [dbo].[AddressCities] values ('79', 'Kilis', '79'); 
                INSERT INTO [dbo].[AddressCities] values ('80', 'Osmaniye', '80'); 
                INSERT INTO [dbo].[AddressCities] values ('81', 'Düzce', '81');";

            AddressDistrictsScript = @"
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (2, 'ALADAĞ',1),
                (3, 'CEYHAN',1),
                (4, 'ÇUKUROVA',1),
                (5, 'FEKE',1),
                (6, 'İMAMOĞLU',1),
                (7, 'KARAİSALI',1),
                (8, 'KARATAŞ',1),
                (9, 'KOZAN',1),
                (10, 'POZANTI',1),
                (11, 'SAİMBEYLİ',1),
                (12, 'SARIÇAM',1),
                (13, 'SEYHAN',1),
                (14, 'TUFANBEYLİ',1),
                (15, 'YUMURTALIK',1),
                (16, 'YÜREĞİR',1);

 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (20, 'BESNİ',2),
                (21, 'ÇELİKHAN',2),
                (22, 'GERGER',2),
                (23, 'GÖLBAŞI',2),
                (24, 'KAHTA',2),
                (25, 'MERKEZ',2),
                (26, 'SAMSAT',2),
                (27, 'SİNCİK',2),
                (28, 'TUT',2);

 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (32, 'BAŞMAKÇI',3),
                (33, 'BAYAT',3),
                (34, 'BOLVADİN',3),
                (35, 'ÇAY',3),
                (36, 'ÇOBANLAR',3),
                (37, 'DAZKIRI',3),
                (38, 'DİNAR',3),
                (39, 'EMİRDAĞ',3),
                (40, 'EVCİLER',3),
                (41, 'HOCALAR',3),
                (42, 'İHSANİYE',3),
                (43, 'İSCEHİSAR',3),
                (44, 'KIZILÖREN',3),
                (45, 'MERKEZ',3),
                (46, 'SANDIKLI',3),
                (47, 'SİNANPAŞA',3),
                (48, 'SULTANDAĞI',3),
                (49, 'ŞUHUT',3);

 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (53, 'DİYADİN',4),
                (54, 'DOĞUBAYAZIT',4),
                (55, 'ELEŞKİRT',4),
                (56, 'HAMUR',4),
                (57, 'MERKEZ',4),
                (58, 'PATNOS',4),
                (59, 'TAŞLIÇAY',4),
                (60, 'TUTAK',4);

 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (64, 'AĞAÇÖREN',68),
                (65, 'ESKİL',68),
                (66, 'GÜLAĞAÇ',68),
                (67, 'GÜZELYURT',68),
                (68, 'MERKEZ',68),
                (69, 'ORTAKÖY',68),
                (70, 'SARIYAHŞİ',68),
                (71, 'SULTANHANI',68);

 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (75, 'GÖYNÜCEK',5),
                (76, 'GÜMÜŞHACIKÖY',5),
                (77, 'HAMAMÖZÜ',5),
                (78, 'MERKEZ',5),
                (79, 'MERZİFON',5),
                (80, 'SULUOVA',5),
                (81, 'TAŞOVA',5);

 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (85, 'AKYURT',6),
                (86, 'ALTINDAĞ',6),
                (87, 'AYAŞ',6),
                (88, 'BALA',6),
                (89, 'BEYPAZARI',6),
                (90, 'ÇAMLIDERE',6),
                (91, 'ÇANKAYA',6),
                (92, 'ÇUBUK',6),
                (93, 'ELMADAĞ',6),
                (94, 'ETİMESGUT',6),
                (95, 'EVREN',6),
                (96, 'GÖLBAŞI',6),
                (97, 'GÜDÜL',6),
                (98, 'HAYMANA',6),
                (99, 'KAHRAMANKAZAN',6),
                (100, 'KALECİK',6),
                (101, 'KEÇİÖREN',6),
                (102, 'KIZILCAHAMAM',6),
                (103, 'MAMAK',6),
                (104, 'NALLIHAN',6),
                (105, 'POLATLI',6),
                (106, 'PURSAKLAR',6),
                (107, 'SİNCAN',6),
                (108, 'ŞEREFLİKOÇHİSAR',6),
                (109, 'YENİMAHALLE',6);

 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (113, 'AKSEKİ',7),
                (114, 'AKSU',7),
                (115, 'ALANYA',7),
                (116, 'DEMRE',7),
                (117, 'DÖŞEMEALTI',7),
                (118, 'ELMALI',7),
                (119, 'FİNİKE',7),
                (120, 'GAZİPAŞA',7),
                (121, 'GÜNDOĞMUŞ',7),
                (122, 'İBRADI',7),
                (123, 'KAŞ',7),
                (124, 'KEMER',7),
                (125, 'KEPEZ',7),
                (126, 'KONYAALTI',7),
                (127, 'KORKUTELİ',7),
                (128, 'KUMLUCA',7),
                (129, 'MANAVGAT',7),
                (130, 'MURATPAŞA',7),
                (131, 'SERİK',7);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (135, 'ÇILDIR',75),
                (136, 'DAMAL',75),
                (137, 'GÖLE',75),
                (138, 'HANAK',75),
                (139, 'MERKEZ',75),
                (140, 'POSOF',75);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (144, 'ARDANUÇ',8),
                (145, 'ARHAVİ',8),
                (146, 'BORÇKA',8),
                (147, 'HOPA',8),
                (148, 'KEMALPAŞA',8),
                (149, 'MERKEZ',8),
                (150, 'MURGUL',8),
                (151, 'ŞAVŞAT',8),
                (152, 'YUSUFELİ',8);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (156, 'BOZDOĞAN',9),
                (157, 'BUHARKENT',9),
                (158, 'ÇİNE',9),
                (159, 'DİDİM',9),
                (160, 'EFELER',9),
                (161, 'GERMENCİK',9),
                (162, 'İNCİRLİOVA',9),
                (163, 'KARACASU',9),
                (164, 'KARPUZLU',9),
                (165, 'KOÇARLI',9),
                (166, 'KÖŞK',9),
                (167, 'KUŞADASI',9),
                (168, 'KUYUCAK',9),
                (169, 'NAZİLLİ',9),
                (170, 'SÖKE',9),
                (171, 'SULTANHİSAR',9),
                (172, 'YENİPAZAR',9);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (176, 'ALTIEYLÜL',10),
                (177, 'AYVALIK',10),
                (178, 'BALYA',10),
                (179, 'BANDIRMA',10),
                (180, 'BİGADİÇ',10),
                (181, 'BURHANİYE',10),
                (182, 'DURSUNBEY',10),
                (183, 'EDREMİT',10),
                (184, 'ERDEK',10),
                (185, 'GÖMEÇ',10),
                (186, 'GÖNEN',10),
                (187, 'HAVRAN',10),
                (188, 'İVRİNDİ',10),
                (189, 'KARESİ',10),
                (190, 'KEPSUT',10),
                (191, 'MANYAS',10),
                (192, 'MARMARA',10),
                (193, 'SAVAŞTEPE',10),
                (194, 'SINDIRGI',10),
                (195, 'SUSURLUK',10);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (199, 'AMASRA',74),
                (200, 'KURUCAŞİLE',74),
                (201, 'MERKEZ',74),
                (202, 'ULUS',74);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (206, 'BEŞİRİ',72),
                (207, 'GERCÜŞ',72),
                (208, 'HASANKEYF',72),
                (209, 'KOZLUK',72),
                (210, 'MERKEZ',72),
                (211, 'SASON',72);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (215, 'AYDINTEPE',69),
                (216, 'DEMİRÖZÜ',69),
                (217, 'MERKEZ',69);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (221, 'BOZÜYÜK',11),
                (222, 'GÖLPAZARI',11),
                (223, 'İNHİSAR',11),
                (224, 'MERKEZ',11),
                (225, 'OSMANELİ',11),
                (226, 'PAZARYERİ',11),
                (227, 'SÖĞÜT',11),
                (228, 'YENİPAZAR',11);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (232, 'ADAKLI',12),
                (233, 'GENÇ',12),
                (234, 'KARLIOVA',12),
                (235, 'KİĞI',12),
                (236, 'MERKEZ',12),
                (237, 'SOLHAN',12),
                (238, 'YAYLADERE',12),
                (239, 'YEDİSU',12);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (243, 'ADİLCEVAZ',13),
                (244, 'AHLAT',13),
                (245, 'GÜROYMAK',13),
                (246, 'HİZAN',13),
                (247, 'MERKEZ',13),
                (248, 'MUTKİ',13),
                (249, 'TATVAN',13);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (253, 'DÖRTDİVAN',14),
                (254, 'GEREDE',14),
                (255, 'GÖYNÜK',14),
                (256, 'KIBRISCIK',14),
                (257, 'MENGEN',14),
                (258, 'MERKEZ',14),
                (259, 'MUDURNU',14),
                (260, 'SEBEN',14),
                (261, 'YENİÇAĞA',14);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (265, 'AĞLASUN',15),
                (266, 'ALTINYAYLA',15),
                (267, 'BUCAK',15),
                (268, 'ÇAVDIR',15),
                (269, 'ÇELTİKÇİ',15),
                (270, 'GÖLHİSAR',15),
                (271, 'KARAMANLI',15),
                (272, 'KEMER',15),
                (273, 'MERKEZ',15),
                (274, 'TEFENNİ',15),
                (275, 'YEŞİLOVA',15);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (279, 'BÜYÜKORHAN',16),
                (280, 'GEMLİK',16),
                (281, 'GÜRSU',16),
                (282, 'HARMANCIK',16),
                (283, 'İNEGÖL',16),
                (284, 'İZNİK',16),
                (285, 'KARACABEY',16),
                (286, 'KELES',16),
                (287, 'KESTEL',16),
                (288, 'MUDANYA',16),
                (289, 'MUSTAFAKEMALPAŞA',16),
                (290, 'NİLÜFER',16),
                (291, 'ORHANELİ',16),
                (292, 'ORHANGAZİ',16),
                (293, 'OSMANGAZİ',16),
                (294, 'YENİŞEHİR',16),
                (295, 'YILDIRIM',16);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (299, 'AYVACIK',17),
                (300, 'BAYRAMİÇ',17),
                (301, 'BİGA',17),
                (302, 'BOZCAADA',17),
                (303, 'ÇAN',17),
                (304, 'ECEABAT',17),
                (305, 'EZİNE',17),
                (306, 'GELİBOLU',17),
                (307, 'GÖKÇEADA',17),
                (308, 'LAPSEKİ',17),
                (309, 'MERKEZ',17),
                (310, 'YENİCE',17);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (314, 'ATKARACALAR',18),
                (315, 'BAYRAMÖREN',18),
                (316, 'ÇERKEŞ',18),
                (317, 'ELDİVAN',18),
                (318, 'ILGAZ',18),
                (319, 'KIZILIRMAK',18),
                (320, 'KORGUN',18),
                (321, 'KURŞUNLU',18),
                (322, 'MERKEZ',18),
                (323, 'ORTA',18),
                (324, 'ŞABANÖZÜ',18),
                (325, 'YAPRAKLI',18);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (329, 'ALACA',19),
                (330, 'BAYAT',19),
                (331, 'BOĞAZKALE',19),
                (332, 'DODURGA',19),
                (333, 'İSKİLİP',19),
                (334, 'KARGI',19),
                (335, 'LAÇİN',19),
                (336, 'MECİTÖZÜ',19),
                (337, 'MERKEZ',19),
                (338, 'OĞUZLAR',19),
                (339, 'ORTAKÖY',19),
                (340, 'OSMANCIK',19),
                (341, 'SUNGURLU',19),
                (342, 'UĞURLUDAĞ',19);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (346, 'ACIPAYAM',20),
                (347, 'BABADAĞ',20),
                (348, 'BAKLAN',20),
                (349, 'BEKİLLİ',20),
                (350, 'BEYAĞAÇ',20),
                (351, 'BOZKURT',20),
                (352, 'BULDAN',20),
                (353, 'ÇAL',20),
                (354, 'ÇAMELİ',20),
                (355, 'ÇARDAK',20),
                (356, 'ÇİVRİL',20),
                (357, 'GÜNEY',20),
                (358, 'HONAZ',20),
                (359, 'KALE',20),
                (360, 'MERKEZEFENDİ',20),
                (361, 'PAMUKKALE',20),
                (362, 'SARAYKÖY',20),
                (363, 'SERİNHİSAR',20),
                (364, 'TAVAS',20);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (368, 'BAĞLAR',21),
                (369, 'BİSMİL',21),
                (370, 'ÇERMİK',21),
                (371, 'ÇINAR',21),
                (372, 'ÇÜNGÜŞ',21),
                (373, 'DİCLE',21),
                (374, 'EĞİL',21),
                (375, 'ERGANİ',21),
                (376, 'HANİ',21),
                (377, 'HAZRO',21),
                (378, 'KAYAPINAR',21),
                (379, 'KOCAKÖY',21),
                (380, 'KULP',21),
                (381, 'LİCE',21),
                (382, 'SİLVAN',21),
                (383, 'SUR',21),
                (384, 'YENİŞEHİR',21);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (388, 'AKÇAKOCA',81),
                (389, 'CUMAYERİ',81),
                (390, 'ÇİLİMLİ',81),
                (391, 'GÖLYAKA',81),
                (392, 'GÜMÜŞOVA',81),
                (393, 'KAYNAŞLI',81),
                (394, 'MERKEZ',81),
                (395, 'YIĞILCA',81);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (399, 'ENEZ',22),
                (400, 'HAVSA',22),
                (401, 'İPSALA',22),
                (402, 'KEŞAN',22),
                (403, 'LALAPAŞA',22),
                (404, 'MERİÇ',22),
                (405, 'MERKEZ',22),
                (406, 'SÜLOĞLU',22),
                (407, 'UZUNKÖPRÜ',22);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (411, 'AĞIN',23),
                (412, 'ALACAKAYA',23),
                (413, 'ARICAK',23),
                (414, 'BASKİL',23),
                (415, 'KARAKOÇAN',23),
                (416, 'KEBAN',23),
                (417, 'KOVANCILAR',23),
                (418, 'MADEN',23),
                (419, 'MERKEZ',23),
                (420, 'PALU',23),
                (421, 'SİVRİCE',23);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (425, 'ÇAYIRLI',24),
                (426, 'İLİÇ',24),
                (427, 'KEMAH',24),
                (428, 'KEMALİYE',24),
                (429, 'MERKEZ',24),
                (430, 'OTLUKBELİ',24),
                (431, 'REFAHİYE',24),
                (432, 'TERCAN',24),
                (433, 'ÜZÜMLÜ',24);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (437, 'AŞKALE',25),
                (438, 'AZİZİYE',25),
                (439, 'ÇAT',25),
                (440, 'HINIS',25),
                (441, 'HORASAN',25),
                (442, 'İSPİR',25),
                (443, 'KARAÇOBAN',25),
                (444, 'KARAYAZI',25),
                (445, 'KÖPRÜKÖY',25),
                (446, 'NARMAN',25),
                (447, 'OLTU',25),
                (448, 'OLUR',25),
                (449, 'PALANDÖKEN',25),
                (450, 'PASİNLER',25),
                (451, 'PAZARYOLU',25),
                (452, 'ŞENKAYA',25),
                (453, 'TEKMAN',25),
                (454, 'TORTUM',25),
                (455, 'UZUNDERE',25),
                (456, 'YAKUTİYE',25);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (460, 'ALPU',26),
                (461, 'BEYLİKOVA',26),
                (462, 'ÇİFTELER',26),
                (463, 'GÜNYÜZÜ',26),
                (464, 'HAN',26),
                (465, 'İNÖNÜ',26),
                (466, 'MAHMUDİYE',26),
                (467, 'MİHALGAZİ',26),
                (468, 'MİHALIÇÇIK',26),
                (469, 'ODUNPAZARI',26),
                (470, 'SARICAKAYA',26),
                (471, 'SEYİTGAZİ',26),
                (472, 'SİVRİHİSAR',26),
                (473, 'TEPEBAŞI',26);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (477, 'ARABAN',27),
                (478, 'İSLAHİYE',27),
                (479, 'KARKAMIŞ',27),
                (480, 'NİZİP',27),
                (481, 'NURDAĞI',27),
                (482, 'OĞUZELİ',27),
                (483, 'ŞAHİNBEY',27),
                (484, 'ŞEHİTKAMİL',27),
                (485, 'YAVUZELİ',27);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (489, 'ALUCRA',28),
                (490, 'BULANCAK',28),
                (491, 'ÇAMOLUK',28),
                (492, 'ÇANAKÇI',28),
                (493, 'DERELİ',28),
                (494, 'DOĞANKENT',28),
                (495, 'ESPİYE',28),
                (496, 'EYNESİL',28),
                (497, 'GÖRELE',28),
                (498, 'GÜCE',28),
                (499, 'KEŞAP',28),
                (500, 'MERKEZ',28),
                (501, 'PİRAZİZ',28),
                (502, 'ŞEBİNKARAHİSAR',28),
                (503, 'TİREBOLU',28),
                (504, 'YAĞLIDERE',28);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (508, 'KELKİT',29),
                (509, 'KÖSE',29),
                (510, 'KÜRTÜN',29),
                (511, 'MERKEZ',29),
                (512, 'ŞİRAN',29),
                (513, 'TORUL',29);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (517, 'ÇUKURCA',30),
                (518, 'DERECİK',30),
                (519, 'MERKEZ',30),
                (520, 'ŞEMDİNLİ',30),
                (521, 'YÜKSEKOVA',30);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (525, 'ALTINÖZÜ',31),
                (526, 'ANTAKYA',31),
                (527, 'ARSUZ',31),
                (528, 'BELEN',31),
                (529, 'DEFNE',31),
                (530, 'DÖRTYOL',31),
                (531, 'ERZİN',31),
                (532, 'HASSA',31),
                (533, 'İSKENDERUN',31),
                (534, 'KIRIKHAN',31),
                (535, 'KUMLU',31),
                (536, 'PAYAS',31),
                (537, 'REYHANLI',31),
                (538, 'SAMANDAĞ',31),
                (539, 'YAYLADAĞI',31);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (543, 'ARALIK',76),
                (544, 'KARAKOYUNLU',76),
                (545, 'MERKEZ',76),
                (546, 'TUZLUCA',76);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (550, 'AKSU',32),
                (551, 'ATABEY',32),
                (552, 'EĞİRDİR',32),
                (553, 'GELENDOST',32),
                (554, 'GÖNEN',32),
                (555, 'KEÇİBORLU',32),
                (556, 'MERKEZ',32),
                (557, 'SENİRKENT',32),
                (558, 'SÜTÇÜLER',32),
                (559, 'ŞARKİKARAAĞAÇ',32),
                (560, 'ULUBORLU',32),
                (561, 'YALVAÇ',32),
                (562, 'YENİŞARBADEMLİ',32);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (566, 'ADALAR',34),
                (567, 'ARNAVUTKÖY',34),
                (568, 'ATAŞEHİR',34),
                (569, 'AVCILAR',34),
                (570, 'BAĞCILAR',34),
                (571, 'BAHÇELİEVLER',34),
                (572, 'BAKIRKÖY',34),
                (573, 'BAŞAKŞEHİR',34),
                (574, 'BAYRAMPAŞA',34),
                (575, 'BEŞİKTAŞ',34),
                (576, 'BEYKOZ',34),
                (577, 'BEYLİKDÜZÜ',34),
                (578, 'BEYOĞLU',34),
                (579, 'BÜYÜKÇEKMECE',34),
                (580, 'ÇATALCA',34),
                (581, 'ÇEKMEKÖY',34),
                (582, 'ESENLER',34),
                (583, 'ESENYURT',34),
                (584, 'EYÜPSULTAN',34),
                (585, 'FATİH',34),
                (586, 'GAZİOSMANPAŞA',34),
                (587, 'GÜNGÖREN',34),
                (588, 'KADIKÖY',34),
                (589, 'KAĞITHANE',34),
                (590, 'KARTAL',34),
                (591, 'KÜÇÜKÇEKMECE',34),
                (592, 'MALTEPE',34),
                (593, 'PENDİK',34),
                (594, 'SANCAKTEPE',34),
                (595, 'SARIYER',34),
                (596, 'SİLİVRİ',34),
                (597, 'SULTANBEYLİ',34),
                (598, 'SULTANGAZİ',34),
                (599, 'ŞİLE',34),
                (600, 'ŞİŞLİ',34),
                (601, 'TUZLA',34),
                (602, 'ÜMRANİYE',34),
                (603, 'ÜSKÜDAR',34),
                (604, 'ZEYTİNBURNU',34);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (608, 'ALİAĞA',35),
                (609, 'BALÇOVA',35),
                (610, 'BAYINDIR',35),
                (611, 'BAYRAKLI',35),
                (612, 'BERGAMA',35),
                (613, 'BEYDAĞ',35),
                (614, 'BORNOVA',35),
                (615, 'BUCA',35),
                (616, 'ÇEŞME',35),
                (617, 'ÇİĞLİ',35),
                (618, 'DİKİLİ',35),
                (619, 'FOÇA',35),
                (620, 'GAZİEMİR',35),
                (621, 'GÜZELBAHÇE',35),
                (622, 'KARABAĞLAR',35),
                (623, 'KARABURUN',35),
                (624, 'KARŞIYAKA',35),
                (625, 'KEMALPAŞA',35),
                (626, 'KINIK',35),
                (627, 'KİRAZ',35),
                (628, 'KONAK',35),
                (629, 'MENDERES',35),
                (630, 'MENEMEN',35),
                (631, 'NARLIDERE',35),
                (632, 'ÖDEMİŞ',35),
                (633, 'SEFERİHİSAR',35),
                (634, 'SELÇUK',35),
                (635, 'TİRE',35),
                (636, 'TORBALI',35),
                (637, 'URLA',35);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (641, 'AFŞİN',46),
                (642, 'ANDIRIN',46),
                (643, 'ÇAĞLAYANCERİT',46),
                (644, 'DULKADİROĞLU',46),
                (645, 'EKİNÖZÜ',46),
                (646, 'ELBİSTAN',46),
                (647, 'GÖKSUN',46),
                (648, 'NURHAK',46),
                (649, 'ONİKİŞUBAT',46),
                (650, 'PAZARCIK',46),
                (651, 'TÜRKOĞLU',46);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (655, 'EFLANİ',78),
                (656, 'ESKİPAZAR',78),
                (657, 'MERKEZ',78),
                (658, 'OVACIK',78),
                (659, 'SAFRANBOLU',78),
                (660, 'YENİCE',78);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (664, 'AYRANCI',70),
                (665, 'BAŞYAYLA',70),
                (666, 'ERMENEK',70),
                (667, 'KAZIMKARABEKİR',70),
                (668, 'MERKEZ',70),
                (669, 'SARIVELİLER',70);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (673, 'AKYAKA',36),
                (674, 'ARPAÇAY',36),
                (675, 'DİGOR',36),
                (676, 'KAĞIZMAN',36),
                (677, 'MERKEZ',36),
                (678, 'SARIKAMIŞ',36),
                (679, 'SELİM',36),
                (680, 'SUSUZ',36);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (684, 'ABANA',37),
                (685, 'AĞLI',37),
                (686, 'ARAÇ',37),
                (687, 'AZDAVAY',37),
                (688, 'BOZKURT',37),
                (689, 'CİDE',37),
                (690, 'ÇATALZEYTİN',37),
                (691, 'DADAY',37),
                (692, 'DEVREKANİ',37),
                (693, 'DOĞANYURT',37),
                (694, 'HANÖNÜ',37),
                (695, 'İHSANGAZİ',37),
                (696, 'İNEBOLU',37),
                (697, 'KÜRE',37),
                (698, 'MERKEZ',37),
                (699, 'PINARBAŞI',37),
                (700, 'SEYDİLER',37),
                (701, 'ŞENPAZAR',37),
                (702, 'TAŞKÖPRÜ',37),
                (703, 'TOSYA',37);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (707, 'AKKIŞLA',38),
                (708, 'BÜNYAN',38),
                (709, 'DEVELİ',38),
                (710, 'FELAHİYE',38),
                (711, 'HACILAR',38),
                (712, 'İNCESU',38),
                (713, 'KOCASİNAN',38),
                (714, 'MELİKGAZİ',38),
                (715, 'ÖZVATAN',38),
                (716, 'PINARBAŞI',38),
                (717, 'SARIOĞLAN',38),
                (718, 'SARIZ',38),
                (719, 'TALAS',38),
                (720, 'TOMARZA',38),
                (721, 'YAHYALI',38),
                (722, 'YEŞİLHİSAR',38);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (726, 'BAHŞILI',71),
                (727, 'BALIŞEYH',71),
                (728, 'ÇELEBİ',71),
                (729, 'DELİCE',71),
                (730, 'KARAKEÇİLİ',71),
                (731, 'KESKİN',71),
                (732, 'MERKEZ',71),
                (733, 'SULAKYURT',71),
                (734, 'YAHŞİHAN',71);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (738, 'BABAESKİ',39),
                (739, 'DEMİRKÖY',39),
                (740, 'KOFÇAZ',39),
                (741, 'LÜLEBURGAZ',39),
                (742, 'MERKEZ',39),
                (743, 'PEHLİVANKÖY',39),
                (744, 'PINARHİSAR',39),
                (745, 'VİZE',39);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (749, 'AKÇAKENT',40),
                (750, 'AKPINAR',40),
                (751, 'BOZTEPE',40),
                (752, 'ÇİÇEKDAĞI',40),
                (753, 'KAMAN',40),
                (754, 'MERKEZ',40),
                (755, 'MUCUR',40);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (759, 'ELBEYLİ',79),
                (760, 'MERKEZ',79),
                (761, 'MUSABEYLİ',79),
                (762, 'POLATELİ',79);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (766, 'BAŞİSKELE',41),
                (767, 'ÇAYIROVA',41),
                (768, 'DARICA',41),
                (769, 'DERİNCE',41),
                (770, 'DİLOVASI',41),
                (771, 'GEBZE',41),
                (772, 'GÖLCÜK',41),
                (773, 'İZMİT',41),
                (774, 'KANDIRA',41),
                (775, 'KARAMÜRSEL',41),
                (776, 'KARTEPE',41),
                (777, 'KÖRFEZ',41);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (781, 'AHIRLI',42),
                (782, 'AKÖREN',42),
                (783, 'AKŞEHİR',42),
                (784, 'ALTINEKİN',42),
                (785, 'BEYŞEHİR',42),
                (786, 'BOZKIR',42),
                (787, 'CİHANBEYLİ',42),
                (788, 'ÇELTİK',42),
                (789, 'ÇUMRA',42),
                (790, 'DERBENT',42),
                (791, 'DEREBUCAK',42),
                (792, 'DOĞANHİSAR',42),
                (793, 'EMİRGAZİ',42),
                (794, 'EREĞLİ',42),
                (795, 'GÜNEYSINIR',42),
                (796, 'HADİM',42),
                (797, 'HALKAPINAR',42),
                (798, 'HÜYÜK',42),
                (799, 'ILGIN',42),
                (800, 'KADINHANI',42),
                (801, 'KARAPINAR',42),
                (802, 'KARATAY',42),
                (803, 'KULU',42),
                (804, 'MERAM',42),
                (805, 'SARAYÖNÜ',42),
                (806, 'SELÇUKLU',42),
                (807, 'SEYDİŞEHİR',42),
                (808, 'TAŞKENT',42),
                (809, 'TUZLUKÇU',42),
                (810, 'YALIHÜYÜK',42),
                (811, 'YUNAK',42);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (815, 'ALTINTAŞ',43),
                (816, 'ASLANAPA',43),
                (817, 'ÇAVDARHİSAR',43),
                (818, 'DOMANİÇ',43),
                (819, 'DUMLUPINAR',43),
                (820, 'EMET',43),
                (821, 'GEDİZ',43),
                (822, 'HİSARCIK',43),
                (823, 'MERKEZ',43),
                (824, 'PAZARLAR',43),
                (825, 'SİMAV',43),
                (826, 'ŞAPHANE',43),
                (827, 'TAVŞANLI',43);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (831, 'AKÇADAĞ',44),
                (832, 'ARAPGİR',44),
                (833, 'ARGUVAN',44),
                (834, 'BATTALGAZİ',44),
                (835, 'DARENDE',44),
                (836, 'DOĞANŞEHİR',44),
                (837, 'DOĞANYOL',44),
                (838, 'HEKİMHAN',44),
                (839, 'KALE',44),
                (840, 'KULUNCAK',44),
                (841, 'PÜTÜRGE',44),
                (842, 'YAZIHAN',44),
                (843, 'YEŞİLYURT',44);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (847, 'AHMETLİ',45),
                (848, 'AKHİSAR',45),
                (849, 'ALAŞEHİR',45),
                (850, 'DEMİRCİ',45),
                (851, 'GÖLMARMARA',45),
                (852, 'GÖRDES',45),
                (853, 'KIRKAĞAÇ',45),
                (854, 'KÖPRÜBAŞI',45),
                (855, 'KULA',45),
                (856, 'SALİHLİ',45),
                (857, 'SARIGÖL',45),
                (858, 'SARUHANLI',45),
                (859, 'SELENDİ',45),
                (860, 'SOMA',45),
                (861, 'ŞEHZADELER',45),
                (862, 'TURGUTLU',45),
                (863, 'YUNUSEMRE',45);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (867, 'ARTUKLU',47),
                (868, 'DARGEÇİT',47),
                (869, 'DERİK',47),
                (870, 'KIZILTEPE',47),
                (871, 'MAZIDAĞI',47),
                (872, 'MİDYAT',47),
                (873, 'NUSAYBİN',47),
                (874, 'ÖMERLİ',47),
                (875, 'SAVUR',47),
                (876, 'YEŞİLLİ',47);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (880, 'AKDENİZ',33),
                (881, 'ANAMUR',33),
                (882, 'AYDINCIK',33),
                (883, 'BOZYAZI',33),
                (884, 'ÇAMLIYAYLA',33),
                (885, 'ERDEMLİ',33),
                (886, 'GÜLNAR',33),
                (887, 'MEZİTLİ',33),
                (888, 'MUT',33),
                (889, 'SİLİFKE',33),
                (890, 'TARSUS',33),
                (891, 'TOROSLAR',33),
                (892, 'YENİŞEHİR',33);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (896, 'BODRUM',48),
                (897, 'DALAMAN',48),
                (898, 'DATÇA',48),
                (899, 'FETHİYE',48),
                (900, 'KAVAKLIDERE',48),
                (901, 'KÖYCEĞİZ',48),
                (902, 'MARMARİS',48),
                (903, 'MENTEŞE',48),
                (904, 'MİLAS',48),
                (905, 'ORTACA',48),
                (906, 'SEYDİKEMER',48),
                (907, 'ULA',48),
                (908, 'YATAĞAN',48);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (912, 'BULANIK',49),
                (913, 'HASKÖY',49),
                (914, 'KORKUT',49),
                (915, 'MALAZGİRT',49),
                (916, 'MERKEZ',49),
                (917, 'VARTO',49);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (921, 'ACIGÖL',50),
                (922, 'AVANOS',50),
                (923, 'DERİNKUYU',50),
                (924, 'GÜLŞEHİR',50),
                (925, 'HACIBEKTAŞ',50),
                (926, 'KOZAKLI',50),
                (927, 'MERKEZ',50),
                (928, 'ÜRGÜP',50);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (932, 'ALTUNHİSAR',51),
                (933, 'BOR',51),
                (934, 'ÇAMARDI',51),
                (935, 'ÇİFTLİK',51),
                (936, 'MERKEZ',51),
                (937, 'ULUKIŞLA',51);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (941, 'AKKUŞ',52),
                (942, 'ALTINORDU',52),
                (943, 'AYBASTI',52),
                (944, 'ÇAMAŞ',52),
                (945, 'ÇATALPINAR',52),
                (946, 'ÇAYBAŞI',52),
                (947, 'FATSA',52),
                (948, 'GÖLKÖY',52),
                (949, 'GÜLYALI',52),
                (950, 'GÜRGENTEPE',52),
                (951, 'İKİZCE',52),
                (952, 'KABADÜZ',52),
                (953, 'KABATAŞ',52),
                (954, 'KORGAN',52),
                (955, 'KUMRU',52),
                (956, 'MESUDİYE',52),
                (957, 'PERŞEMBE',52),
                (958, 'ULUBEY',52),
                (959, 'ÜNYE',52);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (963, 'BAHÇE',80),
                (964, 'DÜZİÇİ',80),
                (965, 'HASANBEYLİ',80),
                (966, 'KADİRLİ',80),
                (967, 'MERKEZ',80),
                (968, 'SUMBAS',80),
                (969, 'TOPRAKKALE',80);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (973, 'ARDEŞEN',53),
                (974, 'ÇAMLIHEMŞİN',53),
                (975, 'ÇAYELİ',53),
                (976, 'DEREPAZARI',53),
                (977, 'FINDIKLI',53),
                (978, 'GÜNEYSU',53),
                (979, 'HEMŞİN',53),
                (980, 'İKİZDERE',53),
                (981, 'İYİDERE',53),
                (982, 'KALKANDERE',53),
                (983, 'MERKEZ',53),
                (984, 'PAZAR',53);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (988, 'ADAPAZARI',54),
                (989, 'AKYAZI',54),
                (990, 'ARİFİYE',54),
                (991, 'ERENLER',54),
                (992, 'FERİZLİ',54),
                (993, 'GEYVE',54),
                (994, 'HENDEK',54),
                (995, 'KARAPÜRÇEK',54),
                (996, 'KARASU',54),
                (997, 'KAYNARCA',54),
                (998, 'KOCAALİ',54),
                (999, 'PAMUKOVA',54),
                (1000, 'SAPANCA',54),
                (1001, 'SERDİVAN',54),
                (1002, 'SÖĞÜTLÜ',54),
                (1003, 'TARAKLI',54);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1007, '19 MAYIS',55),
                (1008, 'ALAÇAM',55),
                (1009, 'ASARCIK',55),
                (1010, 'ATAKUM',55),
                (1011, 'AYVACIK',55),
                (1012, 'BAFRA',55),
                (1013, 'CANİK',55),
                (1014, 'ÇARŞAMBA',55),
                (1015, 'HAVZA',55),
                (1016, 'İLKADIM',55),
                (1017, 'KAVAK',55),
                (1018, 'LADİK',55),
                (1019, 'SALIPAZARI',55),
                (1020, 'TEKKEKÖY',55),
                (1021, 'TERME',55),
                (1022, 'VEZİRKÖPRÜ',55),
                (1023, 'YAKAKENT',55);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1027, 'BAYKAN',56),
                (1028, 'ERUH',56),
                (1029, 'KURTALAN',56),
                (1030, 'MERKEZ',56),
                (1031, 'PERVARİ',56),
                (1032, 'ŞİRVAN',56),
                (1033, 'TİLLO',56);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1037, 'AYANCIK',57),
                (1038, 'BOYABAT',57),
                (1039, 'DİKMEN',57),
                (1040, 'DURAĞAN',57),
                (1041, 'ERFELEK',57),
                (1042, 'GERZE',57),
                (1043, 'MERKEZ',57),
                (1044, 'SARAYDÜZÜ',57),
                (1045, 'TÜRKELİ',57);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1049, 'AKINCILAR',58),
                (1050, 'ALTINYAYLA',58),
                (1051, 'DİVRİĞİ',58),
                (1052, 'DOĞANŞAR',58),
                (1053, 'GEMEREK',58),
                (1054, 'GÖLOVA',58),
                (1055, 'GÜRÜN',58),
                (1056, 'HAFİK',58),
                (1057, 'İMRANLI',58),
                (1058, 'KANGAL',58),
                (1059, 'KOYULHİSAR',58),
                (1060, 'MERKEZ',58),
                (1061, 'SUŞEHRİ',58),
                (1062, 'ŞARKIŞLA',58),
                (1063, 'ULAŞ',58),
                (1064, 'YILDIZELİ',58),
                (1065, 'ZARA',58);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1069, 'AKÇAKALE',63),
                (1070, 'BİRECİK',63),
                (1071, 'BOZOVA',63),
                (1072, 'CEYLANPINAR',63),
                (1073, 'EYYÜBİYE',63),
                (1074, 'HALFETİ',63),
                (1075, 'HALİLİYE',63),
                (1076, 'HARRAN',63),
                (1077, 'HİLVAN',63),
                (1078, 'KARAKÖPRÜ',63),
                (1079, 'SİVEREK',63),
                (1080, 'SURUÇ',63),
                (1081, 'VİRANŞEHİR',63);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1085, 'BEYTÜŞŞEBAP',73),
                (1086, 'CİZRE',73),
                (1087, 'GÜÇLÜKONAK',73),
                (1088, 'İDİL',73),
                (1089, 'MERKEZ',73),
                (1090, 'SİLOPİ',73),
                (1091, 'ULUDERE',73);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1095, 'ÇERKEZKÖY',59),
                (1096, 'ÇORLU',59),
                (1097, 'ERGENE',59),
                (1098, 'HAYRABOLU',59),
                (1099, 'KAPAKLI',59),
                (1100, 'MALKARA',59),
                (1101, 'MARMARAEREĞLİSİ',59),
                (1102, 'MURATLI',59),
                (1103, 'SARAY',59),
                (1104, 'SÜLEYMANPAŞA',59),
                (1105, 'ŞARKÖY',59);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1109, 'ALMUS',60),
                (1110, 'ARTOVA',60),
                (1111, 'BAŞÇİFTLİK',60),
                (1112, 'ERBAA',60),
                (1113, 'MERKEZ',60),
                (1114, 'NİKSAR',60),
                (1115, 'PAZAR',60),
                (1116, 'REŞADİYE',60),
                (1117, 'SULUSARAY',60),
                (1118, 'TURHAL',60),
                (1119, 'YEŞİLYURT',60),
                (1120, 'ZİLE',60);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1124, 'AKÇAABAT',61),
                (1125, 'ARAKLI',61),
                (1126, 'ARSİN',61),
                (1127, 'BEŞİKDÜZÜ',61),
                (1128, 'ÇARŞIBAŞI',61),
                (1129, 'ÇAYKARA',61),
                (1130, 'DERNEKPAZARI',61),
                (1131, 'DÜZKÖY',61),
                (1132, 'HAYRAT',61),
                (1133, 'KÖPRÜBAŞI',61),
                (1134, 'MAÇKA',61),
                (1135, 'OF',61),
                (1136, 'ORTAHİSAR',61),
                (1137, 'SÜRMENE',61),
                (1138, 'ŞALPAZARI',61),
                (1139, 'TONYA',61),
                (1140, 'VAKFIKEBİR',61),
                (1141, 'YOMRA',61);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1145, 'ÇEMİŞGEZEK',62),
                (1146, 'HOZAT',62),
                (1147, 'MAZGİRT',62),
                (1148, 'MERKEZ',62),
                (1149, 'NAZIMİYE',62),
                (1150, 'OVACIK',62),
                (1151, 'PERTEK',62),
                (1152, 'PÜLÜMÜR',62);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1156, 'BANAZ',64),
                (1157, 'EŞME',64),
                (1158, 'KARAHALLI',64),
                (1159, 'MERKEZ',64),
                (1160, 'SİVASLI',64),
                (1161, 'ULUBEY',64);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1165, 'BAHÇESARAY',65),
                (1166, 'BAŞKALE',65),
                (1167, 'ÇALDIRAN',65),
                (1168, 'ÇATAK',65),
                (1169, 'EDREMİT',65),
                (1170, 'ERCİŞ',65),
                (1171, 'GEVAŞ',65),
                (1172, 'GÜRPINAR',65),
                (1173, 'İPEKYOLU',65),
                (1174, 'MURADİYE',65),
                (1175, 'ÖZALP',65),
                (1176, 'SARAY',65),
                (1177, 'TUŞBA',65);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1181, 'ALTINOVA',77),
                (1182, 'ARMUTLU',77),
                (1183, 'ÇINARCIK',77),
                (1184, 'ÇİFTLİKKÖY',77),
                (1185, 'MERKEZ',77),
                (1186, 'TERMAL',77);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1190, 'AKDAĞMADENİ',66),
                (1191, 'AYDINCIK',66),
                (1192, 'BOĞAZLIYAN',66),
                (1193, 'ÇANDIR',66),
                (1194, 'ÇAYIRALAN',66),
                (1195, 'ÇEKEREK',66),
                (1196, 'KADIŞEHRİ',66),
                (1197, 'MERKEZ',66),
                (1198, 'SARAYKENT',66),
                (1199, 'SARIKAYA',66),
                (1200, 'SORGUN',66),
                (1201, 'ŞEFAATLİ',66),
                (1202, 'YENİFAKILI',66),
                (1203, 'YERKÖY',66);
 
 
                INSERT INTO [dbo].[AddressDistricts](Id, Name, AddressCityId) VALUES 
                (1207, 'ALAPLI',67),
                (1208, 'ÇAYCUMA',67),
                (1209, 'DEVREK',67),
                (1210, 'EREĞLİ',67),
                (1211, 'GÖKÇEBEY',67),
                (1212, 'KİLİMLİ',67),
                (1213, 'KOZLU',67),
                (1214, 'MERKEZ',67);";
        }
    }
}
