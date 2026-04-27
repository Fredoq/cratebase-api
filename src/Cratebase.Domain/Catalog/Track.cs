using Cratebase.Domain.Ratings;
using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Catalog;

public sealed class Track : IEntity<TrackId>, ICreditTarget
{
    private Track(
        TrackId id,
        string title,
        TrackDetails details,
        Cataloging cataloging)
    {
        Id = id;
        Title = title;
        Details = details;
        Cataloging = cataloging;
    }

    public TrackId Id { get; }

    public string Title { get; }

    public string DisplayName => Title;

    public TrackDetails Details { get; }

    public Cataloging Cataloging { get; }

    public static Track Create(TrackId id, string title)
    {
        return new Track(
            id,
            Guard.RequiredText(title, nameof(title), "track.title_required"),
            TrackDetails.Empty,
            Cataloging.Empty);
    }

    public Track WithDetails(TrackDetails details)
    {
        ArgumentNullException.ThrowIfNull(details);

        return new Track(Id, Title, details, Cataloging);
    }

    public Track WithDuration(TimeSpan duration)
    {
        return WithDetails(Details.WithDuration(duration));
    }

    public Track WithRating(Rating rating)
    {
        return WithDetails(Details.WithRating(rating));
    }

    public Track WithCataloging(Cataloging cataloging)
    {
        ArgumentNullException.ThrowIfNull(cataloging);

        return new Track(Id, Title, Details, cataloging);
    }
}
