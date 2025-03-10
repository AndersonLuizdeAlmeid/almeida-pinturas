namespace Users.Application.Authentications.JwtToken;
public static class Key
{
    public static string Secret { get; private set; }

    public static void SetSecret(string secret)
    {
        Secret = secret;
    }
}