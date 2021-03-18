using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseApi.Data;
using CourseApi.Models;

namespace CourseApi.Controllers
{
    [ApiController]
    [Route("v1/courses")]
    public class CourseController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Course>>> Get([FromServices] DataContext context)
        {
            var courses = await context.Courses.Include(c=> c.Category).ToListAsync();
            
            if(courses == null) { return NotFound(); }
            return Ok(courses);
        }

         [HttpGet]
         [Route("{id:int}")]
         public async Task<ActionResult<Course>> GetById([FromServices] DataContext context, int id)
         {
             var course = await context.Courses.Include(c=> c.Category).AsNoTracking().FirstOrDefaultAsync(c=> c.Id == id);
             
             if(course == null) { return NotFound(); }
             return Ok(course);
         }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Course>> Post([FromServices] DataContext context, [FromBody] Course model)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

             context.Courses.Add(model);
             await context.SaveChangesAsync();
             return Ok (model);
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<Course>> Delete([FromServices] DataContext context, int id)
        {
            if(id == 0) { return NotFound(); } 

              var estudante = await context.Categories.AsNoTracking().SingleOrDefaultAsync(c=> c.Id == id);

            return Ok();
        }
    }
}