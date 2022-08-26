﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
// Dependency Injection, serilog (logging), settings
namespace BConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional:true)
                .AddEnvironmentVariables();
        }
    }
}
