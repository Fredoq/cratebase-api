using Cratebase.Domain.SharedKernel.Validation;

namespace Cratebase.Domain.Collection;

public sealed record ItemCondition
{
    private ItemCondition(string description)
    {
        Description = description;
    }

    public string Description { get; }

    public static ItemCondition Mint { get; } = FromDescription("mint");

    public static ItemCondition NearMint { get; } = FromDescription("near_mint");

    public static ItemCondition VeryGoodPlus { get; } = FromDescription("very_good_plus");

    public static ItemCondition VeryGood { get; } = FromDescription("very_good");

    public static ItemCondition Good { get; } = FromDescription("good");

    public static ItemCondition Fair { get; } = FromDescription("fair");

    public static ItemCondition Poor { get; } = FromDescription("poor");

    public static ItemCondition FromDescription(string description)
    {
        return new ItemCondition(Guard.RequiredText(description, nameof(description), "item_condition.description_required"));
    }
}
