namespace ViaEventAssociation.Core.Tools.ObjectMapper;

public class CustomObjectMapper
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
}