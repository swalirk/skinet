using System;
using Core.Entities;

namespace Core.Intergaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type,string? sort);
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();
    
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);

    bool productExists(int id);

    Task<bool> SaveChangesAsync();

}
