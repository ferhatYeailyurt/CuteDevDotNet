
namespace CuteDev
{
    /// <summary>
    /// Sistem hataları
    /// </summary>
    public static class Exceptions
    {
        /// <summary>
        /// Hata kodları
        /// </summary>
        public static class Codes
        {
            public const string Parameter = "Parameter";
            public const string Exist = "Exist";
            public const string ExistUserName = "ExistUserName";
            public const string NotExist = "NotExist";
            public const string ApiNotFound = "ApiNotFound";
            public const string Session = "Session";
            public const string Method = "Operation";
            public const string Login = "Login";
            public const string ValidPassword = "ValidPassword";
        }

        /// <summary>
        /// Hata Mesajları
        /// </summary>
        public static class Messages
        {
            /// <summary>
            /// Parametre hatası
            /// </summary>
            public const string Parameter = "Eksik ya da hatalı parametre girdiniz!";

            /// <summary>
            /// Var olan kayıt hatası
            /// </summary>
            public const string Exist = "İşlem yapmak istediğiniz kayıt daha önceden sisteme eklenmiştir!";

            /// <summary>
            /// Var olan kayıt hatası
            /// </summary>
            public const string ExistUserName = "İşlem yapmak istediğiniz kullanıcı adı daha önceden sisteme eklenmiştir!";

            /// <summary>
            /// Olmayan kayıt hatası
            /// </summary>
            public const string NotExist = "İşlem yapmak istediğiniz kayıt bulunamadı!";

            /// <summary>
            /// Olmayan kayıt hatası
            /// </summary>
            public const string ApiNotFound = "İşlem yapmak istediğiniz api bulunamadı!";

            /// <summary>
            /// Oturum hatası
            /// </summary>
            public const string Session = "Geçerli bir oturum bulunamadı!";

            /// <summary>
            /// Operasyon hatası
            /// </summary>
            public const string Method = "Geçerli bir metot bulunamadı!";

            /// <summary>
            /// Oturum açma hatası
            /// </summary>
            public const string Login = "Kullanıcı adınızı veya şifrenizi hatalı girdiniz!";


            public const string ValidPassword = "Geçerli şifrenizi hatalı girdiniz!";


            public const string ValidNewPassword = "Şifreler eşleşmiyor!";

        }

        public static ProcessException Parameter(string message = Messages.Parameter, params object[] prms)
        {
            return new ProcessException(Codes.Parameter, message, prms);
        }

        public static ProcessException Exist(string message = Messages.Exist)
        {
            return new ProcessException(Codes.Exist, message);
        }

        public static ProcessException ExistUserName()
        {
            return new ProcessException(Codes.ExistUserName, Messages.ExistUserName);
        }

        public static ProcessException NotExist(string message = Messages.NotExist)
        {
            return new ProcessException(Codes.NotExist, message);
        }

        public static ProcessException Session(string message = Messages.Session)
        {
            return new ProcessException(Codes.Session, message);
        }

        public static ProcessException Session(string code, string message = Messages.Session)
        {
            return new ProcessException(code, message);
        }

        public static ProcessException Operation(string message = Messages.Method, string code = null)
        {
            return new ProcessException(Codes.Method, message);
        }

        public static ProcessException ValidPassword(string message = Messages.ValidPassword)
        {
            return new ProcessException(Codes.ValidPassword, message);
        }

        public static ProcessException ValidNewPassword(string message = Messages.ValidNewPassword)
        {
            return new ProcessException(Codes.ValidPassword, message);
        }
    }
}
