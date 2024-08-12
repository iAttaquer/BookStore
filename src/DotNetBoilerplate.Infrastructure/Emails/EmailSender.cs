using DotNetBoilerplate.Shared.Abstractions.Emails;
using Microsoft.Extensions.Options;
using Resend;

namespace DotNetBoilerplate.Infrastructure.Emails;

internal sealed class EmailSender : IEmailSender
{
    private readonly EmailsOptions _emailsOptions;
    private readonly ResendClient _resendClient;

    public EmailSender(EmailsOptions emailsOptions, HttpClient httpClient)
    {
        _emailsOptions = emailsOptions;
        var resendOptions = Options.Create(new ResendClientOptions { ApiToken = _emailsOptions.ApiKey });
        _resendClient = new ResendClient(resendOptions, httpClient);
    }

    public async Task SendEmailAsync(EmailMessageInfo message)
    {
        var emailMessage = new EmailMessage
        {
            From = _emailsOptions.FromAddressEmail,
            To = message.To,
            Subject = message.Subject,
            TextBody = message.TextBody
        };

        await _resendClient.EmailSendAsync(emailMessage);
    }
}