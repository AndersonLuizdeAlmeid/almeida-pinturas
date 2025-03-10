using System.Text.RegularExpressions;

namespace Users.Infrastructure.Utils;
public static class EmailService
{
    public static bool ValidateEmail(string email)
    {
        const string RegexEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        if (!Regex.IsMatch(email, RegexEmail))
            return false;

        if (email == null)
            return false;

        return true;
    }
}