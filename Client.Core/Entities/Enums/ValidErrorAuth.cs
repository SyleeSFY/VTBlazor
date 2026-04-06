namespace Client.Core.Entities.Enums
{
    public enum ValidErrorAuth : byte
    {
        InvalidCredentials = 0,
        PasswordTooShort,
        InvalidEmail,
        RequiredField,
        PasswordTooLong,
        EmailRequired,
        PasswordRequired
    }
}
