using DotNetBoilerplate.Shared.Abstractions.Emails;

namespace DotNetBoilerplate.Infrastructure.Emails;

internal sealed class EmailSender : IEmailSender
{
    private readonly EmailsOptions _emailsOptions;

    public EmailSender(EmailsOptions options)
    {
        _emailsOptions = options;
    }

    public Task SendEmailAsync(EmailMessage message)
    {
        //integration with email sender provider like Sendgrid
        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(
            "====================================================================================================="+
            $"\nSending email from {_emailsOptions.FromAddressEmail} to: {message.To} with subject ${message.Subject}\n"+
            "=====================================================================================================");
        return Task.CompletedTask;
    }
}