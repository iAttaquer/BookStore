using DotNetBoilerplate.Core.BookStores;
﻿using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Core.BookStores.Events;
// using DotNetBoilerplate.Shared.Abstractions.Emails;
using DotNetBoilerplate.Shared.Events;
using Resend;

namespace DotNetBoilerplate.Application.BookStores.SendEmailWhenBookStoreCreated;

internal sealed class SendEmailWhenBookStoreCreatedHandler(
    IUserRepository userRepository,
    IBookStoreRepository bookStoreRepository
)
    : IDomainNotificationHandler<BookStoreCreatedEvent>
{
    public async Task HandleAsync(BookStoreCreatedEvent notification)
    {
        var user = await userRepository.FindByIdAsync(notification.OwnerId);
        var bookStore = await bookStoreRepository.GetByIdAsync(notification.BookStoreId);

        var apiKey = "re_URwuinr2_ATqH87ETwzfUEpATnzVXbzTL";
        var options = Microsoft.Extensions.Options.Options.Create(new ResendClientOptions { ApiToken = apiKey });
        var httpClient = new HttpClient();
        var client = new ResendClient(options, httpClient);

        var emailMessage = new EmailMessage
        {
            From = "example@resend.dev",
            To = user.Email.Value,
            Subject = "Created Book Store!",
            TextBody = $"You ({user.Username}) have created book store with name:{bookStore.Name}"
        };

        await client.EmailSendAsync(emailMessage);
    }
}