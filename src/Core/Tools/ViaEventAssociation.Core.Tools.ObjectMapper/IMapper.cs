namespace ViaEventAssociation.Core.Tools.ObjectMapper;

public interface IMapper
{
    TOutput Map<TOutput>(object sourceObject) where TOutput : class;
    public TDestination Map<TSource, TDestination>(TSource sourceObject);
}