using System;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.DAL.Entities;
using ToDoApp.Models.Dtos.Requests;
using ToDoApp.Models.Dtos.Responses;

namespace ToDoApp.BLL.Interfaces
{
    public interface IUserService
    {
      
        Task<IEnumerable<UserResponse>> GetAllUsers();
        Task<UserResponse> GetUser(int Id);
        Task<IEnumerable<ToDoListResponse>> GetUserWithTodoAsync(int userId);
        Task CreateUser(UserRequest request);
        Task DeleteUser(int Id);
        Task PatchUser(int Id, JsonPatchDocument<UpdateUserRequest> request);
    }
}

