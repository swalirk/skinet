using System;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController :ControllerBase
{
    private readonly StoreContext storeContext;
    public ProductsController(StoreContext storeContext)
    {
        this.storeContext = storeContext;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>>  GetProducts()
    {
        return await storeContext.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await storeContext.Products.FindAsync(id);
        if(product == null)
        {
            return NotFound();
        }
        return product; 
    }
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        storeContext.Products.Add(product);
        await storeContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
    }
    [HttpPut("update/{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
       if(product.Id != id || !ProductExists(id))
        {
            return BadRequest("Cannot update product");
        }
        storeContext.Entry(product).State = EntityState.Modified;
        await storeContext.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await storeContext.Products.FindAsync(id);
        if(product == null)
        {
            return NotFound();
        }
        storeContext.Products.Remove(product);
        await storeContext.SaveChangesAsync();
        return NoContent();
    }
    private bool ProductExists(int id)
    {
        return storeContext.Products.Any(e => e.Id == id);
    }
}
