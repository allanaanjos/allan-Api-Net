using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.models;

namespace  Backoffice.Controllers
{
    
    public class HomeController : Controller
    {
        
        public async Task<ActionResult<dynamic>> GetTask([FromServices] DataContext context){
            
            var employee = new User { Id = 1, Username = "robin", Password = "robin", Role = "employee"};
            var manager = new User { Id = 2, Username = "batman", Password = "batman", Role = "manager"};
            var category = new Category { Id = 1, Title = "Informatica"};
            var product = new Product {Id = 1, Category = category, Title = "Mouse", Price = 299, Description = "Mouse Gamer"};
            context.User.Add(employee);
            context.User.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);
            await context.SaveChangesAsync();

            return Ok(new {
                messaeg = "Dados configurados"
            });


        }
    }
}