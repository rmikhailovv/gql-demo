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

    public IQueryable<Review> ProductReviews(Guid productId) => reviewsStates
        .Select(x => x.Value)
        .Where(x => x.ProductId == productId)
        .AsQueryable();
}