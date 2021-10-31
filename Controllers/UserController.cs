using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Data;
using Shop.models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Shop.Services;
using Microsoft.EntityFrameworkCore;

namespace Shop.Controllers
{
    [Route("v1/users")]
    public class UserController : Controller
    { 
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext  context){
              
              var users = await context.User.AsNoTracking().ToListAsync();
              return users;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Post([FromServices] DataContext context, [FromBody] User model){


            if(!ModelState.IsValid)
               return BadRequest(ModelState);
            try{ 

                model.Role = "employee";

                context.User.Add(model);
                await context.SaveChangesAsync(); 

                model.Password = "";
                return model;
            }
            catch(Exception){ 
                return BadRequest(new {message = "Não foi possivel criar Usuario"});

            }
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] DataContext context, [FromBody] User model){

             var user = await context.User.AsNoTracking().Where(x => x.Username == model.Username &&  x.Password == model.Password).FirstOrDefaultAsync();

             if (user == null){
                 return NotFound(new {message = ("Usuario ou senha Invalidos")});
             }

             var token  = TokenService.GenerateToken(user);
              user.Password = "";

             return new{
                 user = user,
                 token = token
             };
        } 

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<User>> Put([FromServices] DataContext context, int id, [FromBody]User model){ 

                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                if(id != model.Id){
                    return BadRequest(new {message = "Usuario não Encontrado"});
                }
                try{
                     context.Entry(model).State = EntityState.Modified;
                     await context.SaveChangesAsync();
                     return model;
                }
                catch(Exception){
                    return BadRequest(new {message = "Não foi possivel criar usuario"});

                }
        }
    }
}