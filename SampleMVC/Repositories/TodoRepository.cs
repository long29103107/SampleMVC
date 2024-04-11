using SampleMVC.Data;
using SampleMVC.Models;

namespace SampleMVC.Repositories;

public class TodoRepository : GenericRepository<TodoModel>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }
}
