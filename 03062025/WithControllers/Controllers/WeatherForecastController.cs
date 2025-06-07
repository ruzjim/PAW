using Microsoft.AspNetCore.Mvc;

namespace WithControllers.Controllers;

[ApiController]
[Route("api")]
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

    [HttpGet(Name = "GetWeatherForecast")]
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

    [HttpGet(template: "GetHttpOk", Name = " Este es el nombre del endpoint")]
    public IActionResult GetOk()
    {
        return Ok("test");
    }


    [HttpGet("days/{days}")]

    public IEnumerable<WeatherForecast> Get(int days)
    {
        return Enumerable.Range(1, days).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost("PostNotFound")]
    public IActionResult PostNotFound()
    {
        return NotFound("No se ha encontrado el recurso solicitado.");
    }


    [HttpGet("GetrequestInfo")]
    public IActionResult GetRequestInfo()
    {
        var requestInfo = new
        {
            Method = HttpContext.Request.Method,
            Path = HttpContext.Request.Path,
            QueryString = HttpContext.Request.QueryString.ToString(),
            Headers = HttpContext.Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
            Host = HttpContext.Request.Host.ToString(),
            Protocol = HttpContext.Request.Protocol,
            ContentType = HttpContext.Request.ContentType,
            ContentLength = HttpContext.Request.ContentLength,
            Scheme = HttpContext.Request.Scheme,
            IsHttps = HttpContext.Request.IsHttps,
            ClientIp = HttpContext.Connection.RemoteIpAddress?.ToString(),
            UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
        };

        return Ok(requestInfo);
    }




    [HttpGet("SuccessOK")]
    public IActionResult SuccessOK()
    {
        return Ok("Operación exitosa.");
    }

    [HttpGet("PermanentRedirect")]
    public IActionResult PermanentRedirect()
    {
        return RedirectPermanent("https://www.example.com");
    }

    [HttpGet("TemporaryRedirect")]
    public IActionResult TemporaryRedirect()
    {
        return Redirect("https://www.example.com");
    }

    [HttpGet("NotModified")]
    public IActionResult NotModified()
    {
        return StatusCode(304, "No se ha modificado el recurso desde la última solicitud.");
    }

    [HttpGet("unauthorizedError")]
    public IActionResult UnauthorizedError()
    {
        return Unauthorized("No tienes permiso para acceder a este recurso.");
    }

    [HttpGet("forbiddenError")]
    public IActionResult ForbiddenError()
    {
        return Forbid("No tienes permiso para acceder a este recurso.");
    }

    [HttpGet("notFound")]
    public IActionResult NotFoundError()
    {
        return NotFound("No se ha encontrado el recurso solicitado.");
    }

    [HttpGet("MethodNotAllowed")]
    public IActionResult MethodNotAllowed()
    {
        return StatusCode(405, "El método HTTP utilizado no está permitido para este recurso.");
    }

    [HttpGet("NotImplemented")]
    public IActionResult NotImplemented()
    {
        return StatusCode(501, "El servidor no admite la funcionalidad requerida para completar la solicitud.");
    }

    [HttpGet("BadGateway")]
    public IActionResult BadGateway()
    {
        return StatusCode(502, "El servidor recibió una respuesta no válida de un servidor ascendente.");
    }

    [HttpGet("ServiceUnavailable")]
    public IActionResult ServiceUnavailable()
    {
        return StatusCode(503, "El servicio no está disponible temporalmente. Inténtalo más tarde.");
    }

    [HttpGet("GatewayTimeout")]
    public IActionResult GatewayTimeout()
    {
        return StatusCode(504, "El servidor no recibió una respuesta a tiempo de un servidor ascendente.");
    }

    [HttpGet("InternalServerError")]
    public IActionResult InternalServerError()
    {
        return StatusCode(500, "Se ha producido un error interno en el servidor.");
    }


    [HttpPost("ExampleForm")]
    public IActionResult ExampleForm([FromForm] bool flag)
    {
        var response = new
        {
            Message = "Formulario recibido correctamente.",
            Flag = flag,
            Timestamp = DateTime.UtcNow
        };
        return Ok(response);
    }

}
