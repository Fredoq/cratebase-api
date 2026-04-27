using Cratebase.Domain.SharedKernel.Ids;
using Cratebase.Domain.SharedKernel.Interfaces;
using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Credits;

public sealed class Credit : IEntity<CreditId>
{
    private Credit(CreditId id, CreditContributor contributor, CreditTarget target, CreditRole role)
    {
        Id = id;
        Contributor = contributor;
        Target = target;
        Role = role;
    }

    public CreditId Id { get; }

    public CreditContributor Contributor { get; }

    public CreditTarget Target { get; }

    public CreditRole Role { get; }

    public static Credit Create(CreditId id, CreditContributor contributor, CreditTarget target, CreditRole role)
    {
        ArgumentNullException.ThrowIfNull(contributor);
        ArgumentNullException.ThrowIfNull(target);

        CreditRole validatedRole = Guard.DefinedEnum(role, nameof(role), "credit.role_invalid");

        return new Credit(id, contributor, target, validatedRole);
    }
}
