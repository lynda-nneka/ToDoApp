using System;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Models.Dtos.Requests;
using ToDoApp.Models.Dtos.Responses;

namespace ToDoApp.BLL.Implementations
{
    public class ToDoListService : IToDoListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Todo> _todoRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;
        public ToDoListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           _todoRepo = _unitOfWork.GetRepository<Todo>();
            _userRepo = _unitOfWork.GetRepository<User>();
        }

        public async Task CreateToDo(ToDoListRequest request)
        {
            bool toDoItemExists = await _todoRepo.AnyAsync(c =>
                 c.isCompleted && c.Description.ToLower() == request.Description.ToLower());

            if (toDoItemExists)
                throw new InvalidOperationException("Item already exists");

            Todo newToDo = _mapper.Map<Todo>(request);

            newToDo.CreatedAt = DateTime.Now;
            await _todoRepo.AddAsync(newToDo);
        }

        public async Task DeleteToDo(int Id)
        {
            Todo toDoItem = await _todoRepo.GetSingleByAsync(c =>
                c.Id == Id);

            if (toDoItem == null)
                throw new InvalidOperationException("Item doesnt exist");

            await _todoRepo.DeleteAsync(toDoItem);
        }

        public async Task<IEnumerable<GetAllToDoResponse>> GetAllToDos()
        {
            IEnumerable<Todo> toDoList = await _todoRepo.GetAllAsync();

            if (!toDoList.Any())
                throw new InvalidOperationException("To do list is empty");


            return toDoList.Select(x => new GetAllToDoResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate.ToLongDateString(),
                Priority = x.Priority,
                isCompleted = x.isCompleted

            });
        }

        public async Task<ToDoListResponse> GetToDo(int Id)
        {
            Todo toDoItem = await _todoRepo.GetSingleByAsync(x => x.Id == Id);

            if (toDoItem == null)
                throw new InvalidOperationException("no item with that Id");
            return  _mapper.Map<ToDoListResponse>(toDoItem);
            
        }

        public async Task PatchToDo(int Id, JsonPatchDocument<UpdateTodoRequest> request)
        {
            Todo toDoItem = await _todoRepo.GetSingleByAsync(x => x.Id == Id);

            if (toDoItem == null)
                throw new InvalidOperationException("No to do item found");

            UpdateTodoRequest DataToUpdate = _mapper.Map<UpdateTodoRequest>(toDoItem);

            request.ApplyTo(DataToUpdate);

            Todo UpdatedToDoItem = _mapper.Map(DataToUpdate, toDoItem);

            await _todoRepo.UpdateAsync(UpdatedToDoItem);


        }

        public async Task ToggleToDo(int Id)
        {
            Todo toDoItem = await _todoRepo.GetSingleByAsync(c =>
                c.Id == Id);

            if (toDoItem == null)
                throw new InvalidOperationException("Item doesnt exist");
            toDoItem.isCompleted = !toDoItem.isCompleted;
            await _todoRepo.UpdateAsync(toDoItem);
        }

        
    }
}

