using Microsoft.AspNetCore.Mvc;

namespace ProjetoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        private static readonly string[] SummariesTeste = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        // Teste de comparação de uso de memoria 

        

   
        /// Usa muito na memoria do da maquina, pois salva le e salva pra depois retornar a resposta 

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            const int numberOfForecasts = 1000000;
            const int daysToAdd = 1;

            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(numberOfForecasts);
            var indice = 1;
            var forecasts = new List<WeatherForecast>();

            for (DateTime date = startDate; date < endDate; date = date.AddDays(daysToAdd))
            {
                Console.WriteLine($"Lendo ....{indice++}");
                forecasts.Add(new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(date) ,
                    TemperatureC = Random.Shared.Next(-20 , 55) ,
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                });
            }
            Console.WriteLine("Feito toda a leitura ....");
            return forecasts;
        }

        /// Usa  a memoria  da maquina becessária para ler a linha a a retirnar logo em seguida. mais eficiente 

        [HttpGet("teste")]
        public async IAsyncEnumerable<WeatherForecast> GetAsync()
        {
            const int numberOfForecasts = 1000000;
            const int daysToAdd = 1;

            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddDays(numberOfForecasts);
            var indice = 1;
            for (DateTime date = startDate; date < endDate; date = date.AddDays(daysToAdd))
            {
                Console.WriteLine($"Feito ....{indice++}");
                yield return new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(date) ,
                    TemperatureC = Random.Shared.Next(-20 , 55) ,
                    Summary = SummariesTeste[Random.Shared.Next(SummariesTeste.Length)]
                };
            }
        }
    
    
    }
}
