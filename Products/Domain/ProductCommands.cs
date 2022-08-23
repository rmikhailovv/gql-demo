namespace Products.Domain;

public record AddProductCommand(string Name);
public record UpdateProductCommand(Guid Id, string Name);