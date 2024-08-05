﻿using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Core.Users.Events;
using DotNetBoilerplate.Shared.Abstractions.Emails;
using DotNetBoilerplate.Shared.Events;

namespace DotNetBoilerplate.Application.Users.SendWelcomeEmailWhenUserRegistered;

internal sealed class SendWelcomeEmailWhenUserRegisteredHandler(
    IEmailSender emailSender,
    IUserRepository userRepository
)
    : IDomainNotificationHandler<UserCreatedEvent>
{
    public async Task HandleAsync(UserCreatedEvent notification)
    {
        var user = await userRepository.FindByIdAsync(notification.UserId);

        var emailMessage = new EmailMessage(
            user.Email,
            $"Hello {user.Username} Welcome to DotNetBoilerplate",
            "Welcome to DotNetBoilerplate"
        );

        await emailSender.SendEmailAsync(emailMessage);
    }
}