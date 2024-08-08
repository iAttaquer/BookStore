using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Core.Users.Events;
// using DotNetBoilerplate.Shared.Abstractions.Emails;
using Resend;
using DotNetBoilerplate.Shared.Events;

namespace DotNetBoilerplate.Application.Users.SendWelcomeEmailWhenUserRegistered;

internal sealed class SendWelcomeEmailWhenUserRegisteredHandler(
    IUserRepository userRepository)
    : IDomainNotificationHandler<UserCreatedEvent>
{
    public async Task HandleAsync(UserCreatedEvent notification)
    {
        var user = await userRepository.FindByIdAsync(notification.UserId);
        var apiKey = "re_URwuinr2_ATqH87ETwzfUEpATnzVXbzTL";
        var options = Microsoft.Extensions.Options.Options.Create(new ResendClientOptions { ApiToken = apiKey });
        var httpClient = new HttpClient();
        var client = new ResendClient(options, httpClient);

        var emailMessage = new EmailMessage
        {
            From = "example@resend.dev",
            To = user.Email.Value,
            Subject = $"Hello {user.Username} Welcome to Book Store!",
            TextBody = "\nWelcome to Book Store\n"
        };

        await client.EmailSendAsync(emailMessage);
    }
}