namespace Reviews.Domain;

public class ReviewsAggregate
{
    private readonly Dictionary<Guid, Review> reviewsStates;

    public ReviewsAggregate(Dictionary<Guid, Review> reviewsStates)
    {
        this.reviewsStates = reviewsStates;
    }

    public Guid AddReview(AddReviewCommand command)
    {
        ValidateRate(command.Rate);
        var id = Guid.NewGuid();
        reviewsStates.Add(id, new Review
        {
            Id = id,
            ProductId = command.ProductId,
            Rate = command.Rate
        });
        return id;
    }

    public void UpdateReview(UpdateReviewCommand command)
    {
        if (!reviewsStates.TryGetValue(command.Id, out var review)) throw new InvalidOperationException($"Unable to find review {command.Id}");
        ValidateRate(command.Rate);
        review.Rate = command.Rate;
    }

    private void ValidateRate(int rate)
    {
        if (rate < 0 || rate > 10) throw new ArgumentOutOfRangeException($"Expected rate to be betwen 1 and 10 but found {rate}");
    }
}