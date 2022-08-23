namespace Reviews.Domain;

public record AddReviewCommand(Guid ProductId, int Rate);
public record UpdateReviewCommand(Guid Id, int Rate);