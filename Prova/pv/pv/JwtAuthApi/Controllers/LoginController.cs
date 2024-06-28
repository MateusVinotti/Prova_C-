using Microsoft.AspNetCore.Mvc;
using JwtAuthApi.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using JwtAuthApi.Services;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService; 

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials userCredentials)
        {
         
            if (userCredentials.Email == "user@example.com" && userCredentials.Password == "password")
            {
                var token = _authService.GenerateJwtToken(userCredentials.Email);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPost("servicos")]
        [Authorize] 
        public IActionResult CreateService([FromBody] Service service)
        {
           
            return Ok(service);
        }

        [HttpPut("servicos/{id}")]
        [Authorize]
        public IActionResult UpdateService(int id, [FromBody] Service updatedService)
        {
            return Ok(updatedService);
        }

        [HttpGet("servicos/{id}")]
        [Authorize]
        public IActionResult GetServiceById(int id)
        {
            var service = new Service { Id = id, Nome = "Serviço de Exemplo", Preco = 100.00m, Status = true };
            return Ok(service);
        }

        [HttpPost("contratos")]
        [Authorize] 
        public IActionResult CreateContract([FromBody] Contract contract)
        {
            return Ok(contract);
        }

        [HttpGet("clientes/{clienteId}/servicos")]
        [Authorize]
        public IActionResult GetClientServices(int clienteId)
        {
            var clientServices = new[]
            {
                new ClientService { ClienteId = clienteId, ServicoId = 1, PrecoCobrado = 100.00m, DataContratacao = DateTime.UtcNow, NomeServico = "Serviço A", Status = true },
                new ClientService { ClienteId = clienteId, ServicoId = 2, PrecoCobrado = 150.00m, DataContratacao = DateTime.UtcNow, NomeServico = "Serviço B", Status = true }
            };

            return Ok(clientServices);
        }
    }
}
