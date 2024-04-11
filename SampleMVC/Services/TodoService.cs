using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleMVC.Dtos;
using SampleMVC.Models;
using SampleMVC.Repositories;

namespace SampleMVC.Services;

public class TodoService : ITodoService
{
	private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    public TodoService(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TodoResponse>> GetListAsync()
    {
        return _mapper.Map<List<TodoResponse>>(await _todoRepository.FindAll().ToListAsync());
    }

    public async Task<TodoResponse> GetByIdAsync(int? id)
    {
        var todo =  await _todoRepository.FirstOrDefaultAsync(x => x.Id == id.GetValueOrDefault(0));

        if (todo == null) return null;

        return _mapper.Map<TodoResponse>(todo);
    }

    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request)
    {
        var model = _mapper.Map<TodoModel>(request);

        ValidateTodo(model);
       
        try
        {
            _todoRepository.Add(model);
            await _todoRepository.SaveAsync();
            return _mapper.Map<TodoResponse>(model);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<TodoResponse> UpdateAsync(int id, UpdateTodoRequest request)
    {
        var model = await _todoRepository.FirstOrDefaultAsync(x => x.Id == id);

        _mapper.Map<UpdateTodoRequest, TodoModel>(request, model);

        ValidateTodo(model);

        try
        {
            _todoRepository.Update(model);
            await _todoRepository.SaveAsync();

            return _mapper.Map<TodoResponse>(model);

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _todoRepository.FirstOrDefaultAsync(x => x.Id == id);
        
        try
        {
            _todoRepository.Delete(model);
            await _todoRepository.SaveAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> ExistedTodoAsync(int? id)
    {
        return await _todoRepository.AnyAsync(x => x.Id == id);
    }

    private void ValidateTodo(TodoModel model)
    {
        //Add validation for this;
    }
}
