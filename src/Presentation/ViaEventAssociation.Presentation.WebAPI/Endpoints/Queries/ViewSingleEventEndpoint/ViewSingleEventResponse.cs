namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries.ViewSingleEvent;

public record ViewSingleEventResponse(
    string Title,
    string Description,
    string Start,
    string End,
    string Visibility,
    int NumberOfGuests);