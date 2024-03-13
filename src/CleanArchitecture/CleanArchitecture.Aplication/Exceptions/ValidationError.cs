namespace CleanArchitecture.Aplication.Exceptions;

public sealed record ValidationError(
    string PropertyName,
    string ErrorMessage
    );

