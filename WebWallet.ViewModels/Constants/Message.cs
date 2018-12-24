namespace WebWallet.ViewModels.Constants
{
    public static class Message
    {
        public const string RequiredField = "Полето е задължително.";
        public const string InvalidValue = "Невалидна стойност.";
        public const string UsernameError = "Потребителското име трябва да бъде между 8 и 20 символа и може да съдържа само букви и цифри, [.-@$#^]";
        public const string PasswordError = "Паролата трябва да е между 8 и 20 символа. Трябва да съдържа главни и малки букви, цифри и специални символи [ @$!%*?& ].";
        public const string EmailError = "Невалиден имейл адрес.";
        public const string ConfirmPasswordError = "Паролите не съвпадат.";
    }
}