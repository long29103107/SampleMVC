using SampleMVC.Dtos;

namespace SampleMVC.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoResponse>> GetListAsync();
    Task<TodoResponse> GetByIdAsync(int? id);
    Task<bool> ExistedTodoAsync(int? id);
    Task<TodoResponse> CreateAsync(CreateTodoRequest request);
    Task<TodoResponse> UpdateAsync(int id, UpdateTodoRequest request);
    Task DeleteAsync(int id);
}
