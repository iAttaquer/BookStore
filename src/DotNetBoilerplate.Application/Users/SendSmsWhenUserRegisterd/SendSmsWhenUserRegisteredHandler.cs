using DotNetBoilerplate.Core.Users.Events;
using DotNetBoilerplate.Shared.Events;

namespace DotNetBoilerplate.Application.Users.SendSmsWhenUserRegisterd;

internal sealed class SendSmsWhenUserRegisteredHandler : IDomainNotificationHandler<UserCreatedEvent>
{
    public Task HandleAsync(UserCreatedEvent notification)
    {
        // Send SMS
        Console.WriteLine($"SMS sent to {notification.UserId}");
        return Task.CompletedTask;
    }
}