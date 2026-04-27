using Cratebase.Domain.SharedKernel.Errors;
using Cratebase.Domain.SharedKernel.Optional;

namespace Cratebase.Domain.Relations;

public sealed record ArtistRelationPeriod
{
    private ArtistRelationPeriod(OptionalValue<int> startYear, OptionalValue<int> endYear)
    {
        StartYear = startYear;
        EndYear = endYear;
    }

    public OptionalValue<int> StartYear { get; }

    public OptionalValue<int> EndYear { get; }

    public static ArtistRelationPeriod FromYears(int startYear, int endYear)
    {
        return startYear > endYear
            ? throw new DomainException("relation_period.invalid_range", "Relation period start year cannot be after end year")
            : new ArtistRelationPeriod(Optional.From(startYear), Optional.From(endYear));
    }

    public static ArtistRelationPeriod StartingAt(int startYear)
    {
        return new ArtistRelationPeriod(Optional.From(startYear), Optional.Missing<int>());
    }

    public static ArtistRelationPeriod EndingAt(int endYear)
    {
        return new ArtistRelationPeriod(Optional.Missing<int>(), Optional.From(endYear));
    }
}
