using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

public class CustomBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(decimal))
        {
            return new DecimalModelBinder();
        }

        return null;
    }
}