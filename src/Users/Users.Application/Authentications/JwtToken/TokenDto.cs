namespace Users.Application.Authentications.JwtToken;
public class TokenDto
{
    public long UserID { get; set; }
    public bool IsAuthenticated { get; set; }
    public DateTime ExpirationDate { get; set; }
}