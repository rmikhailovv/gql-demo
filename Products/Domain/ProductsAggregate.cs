namespace Products.Domain;

public class ProductsAggregate
{
    private readonly Dictionary<Guid, Product> productsStates;

    public ProductsAggregate(Dictionary<Guid, Product> productsStates)
    {
        this.productsStates = productsStates;
    }

    public Guid AddProduct(AddProductCommand command)
    {
        var id = Guid.NewGuid();
        productsStates.Add(id, new Product { Id = id, Name = command.Name });
        return id;
    }

    public void UpdateProduct(UpdateProductCommand command)
    {
        if (!productsStates.TryGetValue(command.Id, out var product)) throw new InvalidOperationException($"Product {command.Id} not found");
        product.Name = command.Name;
    }

    public void DeleteProduct(Guid id)
    {
        if (!productsStates.TryGetValue(id, out var product)) throw new InvalidOperationException($"Product {id} not found");
        productsStates.Remove(id);
    }
}