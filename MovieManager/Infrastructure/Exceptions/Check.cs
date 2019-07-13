using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MovieManager.Infrastructure.Annotations;

namespace MovieManager.Infrastructure.Exceptions
{
    [DebuggerStepThrough]
    public static class Check
    {
        public static void NotNull<T>([NoEnumeration] T value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentNullException(parameterName);
            }
        }

        public static void GuidNotEmpty(Guid guid, [InvokerParameterName] [NotNull] string parameterName)
        {
            NotNull(parameterName, parameterName);
            if (guid == Guid.Empty)
            {
                throw new ArgumentException($"The value provided for argument '{parameterName}' must not be an empty guid.", parameterName);
            }
        }

        // ReSharper disable once UnusedParameter.Global
        public static void NotSupported(bool value, string message = null)
        {
            if (value)
            {
                throw new NotSupportedException(message ?? "This operation is not supported.");
            }
        }

        public static void NotNull<T>(
            [NoEnumeration] T value,
            [InvokerParameterName] [NotNull] string parameterName,
            [NotNull] string propertyName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                NotEmpty(propertyName, nameof(propertyName));

                throw new ArgumentException($"The property '{propertyName}' of the argument '{parameterName}' cannot be null.", parameterName);
            }
        }

        public static void NotEmpty<T>(IReadOnlyList<T> value, [InvokerParameterName] [NotNull] string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Count == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"The collection argument '{parameterName}' must contain at least one element.", parameterName);
            }
        }

        public static void NotEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            NotNull(value, parameterName);
            if (value.Trim().Length == 0)
            {
                NotNull(parameterName, nameof(parameterName));
                throw new ArgumentException($"The string argument '{parameterName}' cannot be empty.", parameterName);
            }
        }

        public static void NotNullOrEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!ReferenceEquals(value, null)
                && value.Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"The string argument '{parameterName}' cannot be empty.", parameterName);
            }
        }

        public static void HasNoNulls<T>(IReadOnlyList<T> value, [InvokerParameterName] [NotNull] string parameterName)
            where T : class
        {
            NotNull(value, parameterName);

            if (value.Any(e => e is null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"The collection argument '{parameterName}' cannot contain null values.", parameterName);
            }
        }

        public static void IsDefined<T>(T value, [InvokerParameterName] [NotNull] string parameterName)
            where T : struct
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException($"The value provided for argument '{parameterName}' must be a valid value of enum type '{typeof(T).Name}'.", parameterName);
            }
        }
    }
}