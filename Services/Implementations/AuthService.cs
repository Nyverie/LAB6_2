using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LAB5_Fatima.Models;
using LAB5_Fatima.Repositories.Interfaces;
using LAB5_Fatima.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LAB5_Fatima.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<string?> LoginAsync(LoginModel login)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetFirstOrDefaultAsync(u => u.Username == login.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            return null;

        return CrearToken(user);
    }

    public async Task<bool> RegisterAsync(RegisterModel model)
    {
        if (await _unitOfWork.Repository<User>().ExistsAsync(u => u.Username == model.Username))
            return false;

        var user = new User
        {
            Username = model.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
            Role = model.Role
        };

        await _unitOfWork.Repository<User>().InsertAsync(user);
        await _unitOfWork.Complete();
        return true;
    }

    private string CrearToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}