using HotChocolate.Data;
using Reviews.Domain;

namespace Reviews.GraphQl;

public class Query
{
    private readonly Dictionary<Guid, Review> reviewsStates;

    public Query(Dictionary<Guid, Review> reviewsStates)
    {
        this.reviewsStates = reviewsStates;
    }

    [UseFiltering]
    [UseSorting]
    public IQueryable<Review> Reviews() => reviewsStates
        .Select(x => x.Value)
        .AsQueryable();

    public Review[] ProductReviews(Guid productId, CancellationToken cancellationToken) => reviewsStates
        .Select(x => x.Value)
        .Where(x => x.ProductId == productId)
        .ToArray();

    public decimal ProductAverageRate(Guid productId)
    {
        var reviews = reviewsStates.Select(x => x.Value).Where(x => x.ProductId == productId).ToArray();
        if (!reviews.Any()) return 0;
        return Math.Round((decimal)(reviews.Sum(x => x.Rate) / reviews.Length), 2);
    }
}