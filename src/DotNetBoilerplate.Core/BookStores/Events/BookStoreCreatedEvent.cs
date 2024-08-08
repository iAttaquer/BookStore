using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Core.BookStores.Events;

public class BookStoreCreatedEvent : IDomainEvent
{
    public Guid BookStoreId { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public DateTimeOffset OccuredOn { get; set; }
}