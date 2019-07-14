using System;

namespace MovieManager.Infrastructure.Annotations
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class NoEnumerationAttribute : Attribute
    {
    }
}