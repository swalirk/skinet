using System;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecifications:BaseSpecifications<Product,string>
{
    public BrandListSpecifications()
    {
        AddSelect(x=>x.Brand);
        ApplyDistinct();
    }

}
