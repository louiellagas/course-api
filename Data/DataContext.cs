using Microsoft.EntityFrameworkCore;
using CourseApi.Models;

namespace CourseApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Course> Courses { get; set;}
        public DbSet<Category> Categories { get; set; }
    }
}