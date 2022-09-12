﻿using CurrencyExchangeApi.Middleware;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;

namespace CurrecyExchange.Tests.Middleware
{
    public class ExceptionHandlingTests
    {
        public async Task ShouldHandleException()
        {
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            //
                        })
                        .Configure(app =>
                        {
                            app.UseMiddleware<ExceptionHandling>();
                        });
                })
                .StartAsync();

            var response = await host.GetTestClient().GetAsync("/");

            HttpStatusCode.NotFound.Should().Be(response.StatusCode);
        }
    }
}
