namespace DotNetBoilerplate.Shared.Abstractions.Emails;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessageInfo message);
}