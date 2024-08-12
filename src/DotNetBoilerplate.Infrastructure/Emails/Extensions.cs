﻿using DotNetBoilerplate.Shared.Abstractions.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;


namespace DotNetBoilerplate.Infrastructure.Emails;

internal static class Extensions
{
    private const string SectionName = "emails";

    public static IServiceCollection AddEmails(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<EmailsOptions>(section);

        services.AddSingleton(configuration.GetSection(SectionName).Get<EmailsOptions>());

        services.AddHttpClient<IEmailSender, EmailSender>()
        .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))))
        .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
        // .ConfigureHttpClient((sp, client) =>
        // {
        //     var options = sp.GetRequiredService<IOptions<EmailsOptions>>().Value;
        //     // Configure the HttpClient if needed
        // })
        .AddTypedClient((httpClient, sp) =>
        {
            var options = sp.GetRequiredService<IOptions<EmailsOptions>>().Value;
            return new EmailSender(options, httpClient);
        });

        return services;
    }
}