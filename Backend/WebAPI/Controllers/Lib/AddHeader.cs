using Microsoft.AspNetCore.Mvc;
using WebAPI.DataAccess.Lib;

namespace WebAPI.Controllers.Lib;

public static class AddHeader
{
    private static string? AllowDomain;

    public static void AddCors(ControllerBase controller)
    {
        if(AllowDomain == null) AllowDomain = Environment.GetEnvironmentVariable("AllowDomain") ?? JsonInfoRetriever.GetValue("hide.json", "AllowDomain");
        controller.Response.Headers.TryAdd("Access-Control-Allow-Origin", AllowDomain);
    }
}