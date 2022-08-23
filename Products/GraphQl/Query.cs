using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data;
using HotChocolate.Types;
using Products.Domain;

namespace Products.GraphQl
{
    public class Query
    {
        private readonly Dictionary<Guid, Product> productsStates;

        public Query(Dictionary<Guid, Product> productsStates)
        {
            this.productsStates = productsStates;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> Products() => productsStates.Select(x => x.Value).AsQueryable();
    }
}