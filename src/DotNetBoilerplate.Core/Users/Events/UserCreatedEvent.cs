using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Core.Users.Events;

public class UserCreatedEvent : IDomainEvent
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public DateTimeOffset OccuredOn { get; set; }
}