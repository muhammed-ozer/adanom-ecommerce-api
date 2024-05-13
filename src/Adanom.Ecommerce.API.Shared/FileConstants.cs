namespace Adanom.Ecommerce.API
{
    public static class FileConstants
    {
        public static class JPEG
        {
            public const string Extension = ".jpeg";

            public const string ContentType = "image/jpeg";
        }

        public static class JPG
        {
            public const string Extension = ".jpg";

            public const string ContentType = "image/jpeg";
        }
        public static class PNG
        {
            public const string Extension = ".png";

            public const string ContentType = "image/png";
        }
        public static class PDF
        {
            public const string Extension = ".pdf";

            public const string ContentType = "application/pdf";
        }

        public static class XLS
        {
            public const string Extension = ".xls";

            public const string ContentType = "application/vnd.ms-excel";
        }

        public static class XLSX
        {
            public const string Extension = ".xlsx";

            public const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
        

        public static ICollection<string> AllowedImageExtensions = new List<string>()
        {
            JPEG.Extension,
            JPG.Extension,
            PNG.Extension
        };

        public static ICollection<string> AllowedDocumentExtensions = new List<string>()
        {
            PDF.Extension
        };

        public static ICollection<string> AllowedExcelExtensions = new List<string>()
        {
            XLS.Extension,
            XLSX.Extension,
        };
    }
}
