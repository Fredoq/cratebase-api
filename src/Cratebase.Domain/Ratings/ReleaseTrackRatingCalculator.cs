using Cratebase.Domain.Catalog;

namespace Cratebase.Domain.Ratings;

public static class ReleaseTrackRatingCalculator
{
    public static ReleaseTrackRatingSummary Calculate(Release release, IReadOnlyCollection<Track> tracks)
    {
        ArgumentNullException.ThrowIfNull(release);
        ArgumentNullException.ThrowIfNull(tracks);

        var tracksById = tracks.ToDictionary(track => track.Id);
        List<int> ratings = [];

        foreach (ReleaseTrack releaseTrack in release.Tracklist)
        {
            if (tracksById.TryGetValue(releaseTrack.TrackId, out Track? track) && track.Rating is { } rating)
            {
                ratings.Add(rating.Value);
            }
        }

        return ratings.Count == 0
            ? new ReleaseTrackRatingSummary
            {
                AverageRating = null,
                RatedTrackCount = 0
            }
            : new ReleaseTrackRatingSummary
            {
                AverageRating = decimal.Divide(ratings.Sum(), ratings.Count),
                RatedTrackCount = ratings.Count
            };
    }
}
