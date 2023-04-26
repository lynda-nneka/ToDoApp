using System;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.DAL.Entities;
using ToDoApp.Models.Dtos.Requests;
using ToDoApp.Models.Dtos.Responses;

namespace ToDoApp.BLL.Interfaces
{
    public interface IToDoListService
    {
        Task<IEnumerable<GetAllToDoResponse>> GetAllToDos();
        Task<ToDoListResponse> GetToDo(int Id);
        Task CreateToDo(ToDoListRequest request);
        //Task UpdateToDo(int Id, CreateToDoListRequest request);
        Task DeleteToDo(int Id);
        Task ToggleToDo(int Id);
        Task PatchToDo(int Id, JsonPatchDocument<UpdateTodoRequest> request);
    }
}


