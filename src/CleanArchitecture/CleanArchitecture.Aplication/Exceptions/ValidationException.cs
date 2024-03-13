﻿using CleanArchitecture.Aplication.Abstractions.Behaviors;

namespace CleanArchitecture.Aplication.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    public IEnumerable<ValidationError> Errors { get; }

}