namespace DotNetBoilerplate.Shared.Abstractions.Emails;

public record EmailMessageInfo(string To, string Subject, string TextBody);