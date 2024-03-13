namespace CleanArchitecture.Aplication.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime currentTime { get; }
}
