using LAB5_Fatima.Models;

namespace LAB5_Fatima.Services.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginModel login);
    Task<bool> RegisterAsync(RegisterModel model);
}