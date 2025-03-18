using System;
using System.Diagnostics.Contracts;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification :BaseSpecifications<Product>
{
    
        
    public ProductSpecification(string? brand, string? type,string? sort):base(x=>
        (string.IsNullOrWhiteSpace(brand) || x.Brand==brand) &&
        (string.IsNullOrWhiteSpace(type) || x.Type==type))
    {
        switch(sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }

}
