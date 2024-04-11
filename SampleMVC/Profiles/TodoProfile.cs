using AutoMapper;
using SampleMVC.Dtos;
using SampleMVC.Models;

namespace SampleMVC.Profiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<TodoModel, TodoResponse>().ReverseMap();
        CreateMap<TodoModel, CreateTodoRequest>().ReverseMap();
        CreateMap<TodoModel, UpdateTodoRequest>().ReverseMap();
    }
}
