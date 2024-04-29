namespace ViaEventAssociation.Core.Tools.ObjectMapper;

public class CustomObjectMapper : IMapper
{
    public TDestination Map<TSource, TDestination>(TSource sourceObject)
    {
        var destinationObject = Activator.CreateInstance<TDestination>();
        if (sourceObject == null) return destinationObject;
        
        foreach (var sourceProperty in typeof(TSource).GetProperties())
        {
            var destinationProperty = typeof(TDestination).GetProperty(sourceProperty.Name);
            if (destinationProperty != null)
            {
                destinationProperty.SetValue(destinationObject, sourceProperty.GetValue(sourceObject));
            }
        }
        return destinationObject;
    }
    
    public TOutput Map<TOutput>(object? sourceObject) where TOutput : class
    {
        var destinationObject = Activator.CreateInstance<TOutput>();
        if (sourceObject == null) return destinationObject;

        foreach (var sourceProperty in sourceObject.GetType().GetProperties())
        {
            var destinationProperty = typeof(TOutput).GetProperty(sourceProperty.Name);
            if (destinationProperty != null)
            {
                destinationProperty.SetValue(destinationObject, sourceProperty.GetValue(sourceObject));
            }
        }

        return destinationObject;
    }
}