using Cratebase.Domain.SharedKernel.Errors;

namespace Cratebase.Domain.Relations;

public sealed record ArtistRelationPeriod
{
    private ArtistRelationPeriod(int? startYear, int? endYear)
    {
        StartYear = startYear;
        EndYear = endYear;
    }

    public int? StartYear { get; }

    public int? EndYear { get; }

    public static ArtistRelationPeriod FromYears(int? startYear, int? endYear)
    {
        return startYear is not null && endYear is not null && startYear > endYear
            ? throw new DomainException("relation_period.invalid_range", "Relation period start year cannot be after end year")
            : new ArtistRelationPeriod(startYear, endYear);
    }
}
