using ViaEventAssociation.Core.Domain.Aggregates.Event.EventErrors;
using ViaEventAssociation.Core.Domain.Aggregates.Guest.GuestErrors;
using ViaEventAssociation.Core.Tools.OperationResult;
using Void = ViaEventAssociation.Core.Tools.OperationResult.Void;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guest.Values;

public class GuestPictureUrl : ValueObject
{
    public string PictureUrl { get; }

    internal GuestPictureUrl(string pictureUrl)
    {
        PictureUrl = pictureUrl;
    }

    public static Result<GuestPictureUrl> Create(string? pictureUrl)
    {
        var validationResult = Validate(pictureUrl);
        
        return validationResult.Match<Result<GuestPictureUrl>>(
            onPayLoad: _ => new GuestPictureUrl(pictureUrl!),
            onError: errors => errors
        );
    }

    private static Result<Void> Validate(string? pictureUrl)
    {
        if (pictureUrl is null)
        {
            return GuestAggregateErrors.PictureUrl.PictureCantBeEmpty;
        }
        
        return new Void();
    }

    public override IEnumerable<object> GetEqualityObjects()
    {
        yield return PictureUrl;
    }

    public static explicit operator string(GuestPictureUrl guestPictureUrl)
    {
        return guestPictureUrl.PictureUrl;
    }

}