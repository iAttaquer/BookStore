using DotNetBoilerplate.Core.BookStores;
﻿using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Core.BookStores.Events;
using DotNetBoilerplate.Shared.Abstractions.Emails;
using DotNetBoilerplate.Shared.Events;

namespace DotNetBoilerplate.Application.BookStores.SendEmailWhenBookStoreCreated;

internal sealed class SendWelcomeEmailWhenUserRegisteredHandler(
    IEmailSender emailSender,
    IUserRepository userRepository,
    IBookStoreRepository bookStoreRepository
)
    : IDomainNotificationHandler<BookStoreCreatedEvent>
{
    public async Task HandleAsync(BookStoreCreatedEvent notification)
    {
        var user = await userRepository.FindByIdAsync(notification.OwnerId);
        var bookStore = await bookStoreRepository.GetByIdAsync(notification.BookStoreId);

        var emailMessage = new EmailMessage(
            user.Email,
            $"Created Book Store!",
            $"You ({user.Username}) have created book store with name:{bookStore.Name}"
        );

        await emailSender.SendEmailAsync(emailMessage);
    }
}