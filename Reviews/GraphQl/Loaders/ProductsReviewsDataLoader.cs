using GreenDonut;
using Reviews.Domain;

namespace Reviews.GraphQl.Loaders
{
    public class ProductsReviewsDataLoader : BatchDataLoader<Guid, Review[]>
    {
        private readonly IBatchScheduler batchScheduler;
        private readonly Dictionary<Guid, Review> reviewsStates;

        public ProductsReviewsDataLoader(IBatchScheduler batchScheduler, Dictionary<Guid, Review> reviewsStates) : base(batchScheduler, new DataLoaderOptions())
        {
            this.batchScheduler = batchScheduler;
            this.reviewsStates = reviewsStates;
        }

        protected override Task<IReadOnlyDictionary<Guid, Review[]>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            var reviews = reviewsStates
                .Select(x => x.Value)
                .Where(x => keys.Any(k => k == x.ProductId))
                .GroupBy(x => x.ProductId)
                .ToDictionary(x => x.Key, x => x.ToArray());

            return Task.FromResult((IReadOnlyDictionary<Guid, Review[]>)reviews!);
        }
    }
}