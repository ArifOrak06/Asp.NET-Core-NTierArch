using Microsoft.AspNetCore.Diagnostics;
using NTierApp.Core.ResultPattern;
using NTierApp.Service.Exceptions;
using System.Text.Json;

namespace NTierApp.WebAPI.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    { // Hata Client tabanlı bir hata ise 400, uygulama kaynaklı bir hata ise 500
                        ClientSideException => 400,
                        _ => 500
                    };
                    // Yukarıda oluşturulan statusCode Response'a ekleyelim.
                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    // Oluşturduğumuz response'u Apiden Json formatlı data dönmemiz gerektiği için JSON'a çevirelim.
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                });

            });
        }
    }
}
