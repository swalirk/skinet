
using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;

using Core.Specifications;

using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;


public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
{
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>>  GetProducts([FromQuery]ProductSpecParams specParams)
    {
        var spec=new ProductSpecification(specParams);
        return await CreatePageResult(repo, spec, specParams.PageIndex, specParams.PageSize);
    
          
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if(product == null)
        {
            return NotFound();
        }
        return product; 
    }
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
       if(await repo.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new {id = product.Id}, product);
        }
        return BadRequest("Problem creating product");
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
       if(product.Id != id || !ProductExists(id))
        {
            return BadRequest("Cannot update product");
        }
       repo.Update(product);
        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem updating product");
        
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);
        if(product == null)
        {
            return NotFound();
        } 
        repo.Remove(product);
        if(await repo.SaveAllAsync())
        {
            return NoContent();
        }
        return BadRequest("Problem deleting product");
       
    }
    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec=new BrandListSpecifications();
        return Ok(await repo.ListAsync(spec));
    }
    [HttpGet("Types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        var spec=new TypeListSpecifications();
        return Ok(await repo.ListAsync(spec));
        
    }
}
