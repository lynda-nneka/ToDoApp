using System;
using AutoMapper;
using ToDoApp.DAL.Entities;
using ToDoApp.Models.Dtos.Requests;
using ToDoApp.Models.Dtos.Responses;

namespace ToDoApp.Models.MappingConfiguration
{
    public class ToDoListMappingProfile : Profile
    {
        public ToDoListMappingProfile()
        {
            CreateMap<ToDoListRequest, Todo>();
            CreateMap<Todo, ToDoListResponse>();
            CreateMap<UpdateTodoRequest, Todo>().ReverseMap();
        }
    }
}

