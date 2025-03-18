using System;
using Core.Entities;

namespace Core.Specifications;

public class TypeListSpecifications:BaseSpecifications<Product,string>
{
    public TypeListSpecifications()
    {
        AddSelect(x=>x.Type);
        ApplyDistinct();
    }

}

