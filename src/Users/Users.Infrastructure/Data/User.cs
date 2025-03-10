using System;

namespace Users.Infrastructure.Data;
public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime BirthdayDate { get; set; }
    public string Address { get; set; } = string.Empty;
    public short IsActive { get; set; }
}