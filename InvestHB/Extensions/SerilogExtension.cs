﻿using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;

namespace InvestHB.Extensions
{
    public static class SerilogExtension
    {
        public static void AddSerilogApi(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
           .Enrich.FromLogContext()
           .Enrich.WithExceptionDetails()
           .Enrich.WithCorrelationId()
           .Enrich.WithProperty("ApplicationName", $"API Serilog - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
           .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
           .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
           .WriteTo.Async(wt => wt.File(
                                        path: ".//logs//log-investHB-.log",
                                        rollingInterval: RollingInterval.Day,
                                        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"
                                        ))
        .CreateLogger();
        }
    }
}