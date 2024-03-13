using CleanArchitecture.Aplication.Abstractions.Email;
using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infrastructure.Email;


internal sealed class EmailService : IEmailService
{
    public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
    {
        return Task.CompletedTask;
    }
}