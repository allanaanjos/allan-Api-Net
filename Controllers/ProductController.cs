using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.models; 

namespace Shop.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
    {     
       
        var products = await  context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();
         return products;
    } 

    [HttpGet]
    [Route("{id=int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Product>>  GetById(int id, [FromServices] DataContext context)
    {
        var products = await context.Products.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return products;
    } 
     
    [HttpGet]
    [Route("categories/{id=int}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Product>>>  GetByCategory(int id, [FromServices] DataContext context)
    {
        var products = await context.Products.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();

        return products;
    }  

    [HttpPost]
    [Route("")]
    [Authorize (Roles = "employee")]
    public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product model)
    {
        if(ModelState.IsValid){
            context.Products.Add(model);
            await context.SaveChangesAsync();
            return model;
        }
        else
        {
           return BadRequest(new { message = "Produto invalido"});
        }
    }  
    

    }
}