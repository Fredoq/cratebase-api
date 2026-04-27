using Cratebase.Domain.Catalog;
using Cratebase.Domain.SharedKernel.Optional;

namespace Cratebase.Domain.Ratings;

public static class ReleaseTrackRatingCalculator
{
    public static ReleaseTrackRatingSummary Calculate(Release release, IReadOnlyCollection<Track> tracks)
    {
        ArgumentNullException.ThrowIfNull(release);
        ArgumentNullException.ThrowIfNull(tracks);

        var tracksById = tracks
            .GroupBy(track => track.Id)
            .ToDictionary(group => group.Key, group => group.First());
        List<int> ratings = [];

        foreach (ReleaseTrack releaseTrack in release.Tracklist)
        {
            if (!tracksById.ContainsKey(releaseTrack.TrackId))
            {
                continue;
            }

            Track track = tracksById[releaseTrack.TrackId];

            if (track.Details.Rating is PresentOptionalValue<Rating> rating)
            {
                ratings.Add(rating.Value.Value);
            }
        }

        return ratings.Count == 0
            ? ReleaseTrackRatingSummary.Unrated()
            : ReleaseTrackRatingSummary.FromAverage(decimal.Divide(ratings.Sum(), ratings.Count), ratings.Count);
    }
}
