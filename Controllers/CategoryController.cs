using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.models;

[Route("v1/categories")]
public class CategoryController : ControllerBase
{ 
    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context){     
       
        var categories = await context.Categories.AsNoTracking().ToListAsync();
         return Ok(categories);
    }
    
    [HttpGet]
    [Route("{id=int}")]
    [AllowAnonymous]
    public async Task<ActionResult<Category>>  GetById(int id, [FromServices] DataContext context)
    {
        var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        return category;
    } 

    [HttpPost]
    [Route("")]
    [Authorize (Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Post([FromBody]Category model, [FromServices]DataContext context)
    { 
        if(!ModelState.IsValid)
           return BadRequest(ModelState);

        try{
           context.Categories.Add(model);
           await context.SaveChangesAsync();
           return Ok(model);
        }
        catch
        {
            return BadRequest(new { message = "Não foi possivel criar Categoria"});
        }
    }  

    [HttpPut]
    [Route("{id=int}")]
    [Authorize (Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Put(int id, [FromBody] Category model, [FromServices] DataContext context)
    {
        if(id != model.Id)
            return NotFound(new{ message = "Categoria não Encontrada"});

        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
           context.Entry<Category>(model).State = EntityState.Modified;
           await context.SaveChangesAsync();
           return Ok(model); 
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(new{ message = "Esse Registro ja foi Atualizado"});
        }
        catch (Exception)
        {
            return BadRequest(new{ message = "Não foi Possivel atualizar a Categoria"});
        }
    }

    [HttpDelete]
    [Route("{id=int}")]
    [Authorize (Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Delete(int id, [FromServices] DataContext context)
    {
        var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if(category == null){
               return BadRequest(new { message = "Categoria nao encontrada"});
        }

        try
        {
             context.Categories.Remove(category);
             await context.SaveChangesAsync();
             return Ok(new { message = "Categoria removida com sucesso"});
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Não foi possivel remover a categoria"});
        }
    }
}