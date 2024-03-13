using CleanArchitecture.Aplication.Abstractions.Clock;

namespace CleanArchitecture.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime currentTime => DateTime.UtcNow;
}
