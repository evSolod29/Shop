using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shared.Utils.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {

            var userName = "Guest";
            var status = string.Empty;
            var pattern = "Status: {0}|Date: {1}|Path: {2}|User: {3}";
            var path = context.Request.Path;
            var date = DateTime.Now.ToString("dd.MM.yyyy");

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);

                userName = jwtToken.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            }

            try
            {
                await _next(context);

                if (context.Response.StatusCode >= StatusCodes.Status200OK &&
                    context.Response.StatusCode < StatusCodes.Status300MultipleChoices)
                {
                    status = "Successed";
                }

                if (context.Response.StatusCode >= StatusCodes.Status400BadRequest &&
                    context.Response.StatusCode < StatusCodes.Status500InternalServerError)
                {
                    status = "Failed";
                }

                _logger.LogInformation(string.Format(pattern, status, date, path, userName));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(string.Format(pattern + "\nMessage:{4}", status, date, path, userName, ex.Message));
                throw;
            }
        }
    }
}
