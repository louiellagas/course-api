using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseApi.Data;
using CourseApi.Models;

namespace CourseApi.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : Controller
    {
        private DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")] // concatenates with the route above
        public async Task<ActionResult<List<Category>>> GetAction([FromServices] DataContext context)
        {
            // FromServices =  get dataContext in memory
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category model)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

             context.Categories.Add(model);
             await context.SaveChangesAsync();
             return Ok (model);
        }
    }
}