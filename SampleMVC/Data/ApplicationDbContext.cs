using Microsoft.EntityFrameworkCore;
using SampleMVC.Models;

namespace SampleMVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<TodoModel> Todos { get; set; }
}
