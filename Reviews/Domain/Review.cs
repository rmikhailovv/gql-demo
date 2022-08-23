namespace Reviews.Domain;

public class Review
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Rate { get; set; }
}