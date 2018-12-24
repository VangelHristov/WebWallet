namespace WebWallet.ViewModels.Constants
{
    public static class Pattern
    {
        public const string Username = @"^[a-zA-Z0-9.\-@#^]{8,20}$";
        public const string Password = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
    }
}