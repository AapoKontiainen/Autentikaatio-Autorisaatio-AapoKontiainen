using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Autentikaatio___Autorisaatio.Services;



namespace Autentikaatio___Autorisaatio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        public class UserCredentials
        {
        public string Username { get; set; }
        public string Password { get; set; }
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("AuthGet")]
        [Authorize]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("OpenGet")]
        public IEnumerable<WeatherForecast> GetOpen()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserCredentials credentials)
        {
            // Tarkista tässä credentials-olion arvot, esimerkiksi tietokantahakujen kautta
            if (credentials.Username == "testuser" && credentials.Password == "testpassword")
            {
                // Jos tunnistetiedot ovat oikein, generoi JWT-token ja palauta se
                var tokenService = new TokenService();
                var token = tokenService.GenerateToken(credentials.Username, false); 
                return Ok(new { Token = token });
            }
            else if (credentials.Username == "adminuser" && credentials.Password == "adminpassword")
            {
                var tokenService = new TokenService();
                var token = tokenService.GenerateToken(credentials.Username, true); 
                return Ok(new { Token = token });
            }
            {
                // Jos tunnistetiedot ovat väärin, palauta virheilmoitus
                return Unauthorized("Käyttäjätunnus tai salasana on väärin.");
            }
            
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("GetSecret")]
        public IActionResult GetSecret()
        {
            return Ok("Tämä on suojattua tietoa Admineilta vain muille Admineille..");
        }

        
    }
}
