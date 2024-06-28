using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthApi.Services
{
    public interface IAuthService
    {
        string? Authenticate(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly string _key;

        public AuthService(string key)
        {
            _key = key;
        }

        public string? Authenticate(string email, string password)
        {
            if (email != "test@example.com" || password != "password")
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    [HttpPut("servicos/{id}")]
    [Authorize]
    public IActionResult UpdateService(int id, [FromBody] Service updatedService)
    {
        var serviceToUpdate = _serviceRepository.GetServiceById(id);

        if (serviceToUpdate == null)
        {
            return NotFound();
        }

        serviceToUpdate.Nome = updatedService.Nome;
        serviceToUpdate.Preco = updatedService.Preco;
        serviceToUpdate.Status = updatedService.Status;

        _serviceRepository.UpdateService(serviceToUpdate);

        return Ok(serviceToUpdate);
    }
    public IActionResult GetServiceById(int id)
    {
        var service = _serviceRepository.GetServiceById(id);

        if (service == null)
        {
            return NotFound(); 
        }

        return Ok(service); 
    }

}
