using Cratebase.Domain.Catalog;
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
            if (!tracksById.TryGetValue(releaseTrack.TrackId, out Track? track))
            {
                continue;
            }

            _ = track.Details.Rating.Match(
                whenPresent: rating =>
                {
                    ratings.Add(rating.Value);
                    return true;
                },
                whenMissing: () => false);
        }

        return ratings.Count == 0
            ? ReleaseTrackRatingSummary.Unrated()
            : ReleaseTrackRatingSummary.FromAverage(decimal.Divide(ratings.Sum(), ratings.Count), ratings.Count);
    }
}
