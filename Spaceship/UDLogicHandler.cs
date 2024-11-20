﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payload_Ops;
using Uplink_Downlink;
using System.Text.Json;
using Proj5Spaceship.Filters;


namespace Proj5Spaceship
{
    public class UDLogicHandler
    {
        private Spaceship? _spaceship;
        public UDLogicHandler(Spaceship? spaceship)
        {
            _spaceship = spaceship;
        }
        public async Task StartServerAsync(string[]? args)
        {
            args ??= Array.Empty<string>();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<AppLogger>(new ServerLogger("server_logs.txt"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddScoped<AuthenticateFilter>();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints?.MapControllers();
            });
            await app.RunAsync();
        }
        public async Task StartClientAsync(string HostAndPort)
        {
            var connectionManager = new ConnectionManager(HostAndPort);

            while (true)
            {
                if (!connectionManager.IsAuthenticated)
                {
                    if (await connectionManager.AuthenticateAsync(SetUpAuth()))
                    {
                        Console.WriteLine("Authentication successful.");
                    }
                    else
                    {
                        Console.WriteLine("Authentication Failed.");
                    }
                }
            }
        }
        private string SetUpAuth()
        {
            var authPacket = new AuthPacket
            {
                Username = "user",
                Password = "pass"
            };
            return JsonSerializer.Serialize(authPacket);
        }
        private class AuthPacket
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }
    }
}
