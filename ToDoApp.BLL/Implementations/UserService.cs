using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Interfaces;
using ToDoApp.Models.Dtos.Requests;
using ToDoApp.Models.Dtos.Responses;

namespace ToDoApp.BLL.Implementations
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Todo> _todoRepo;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepo = _unitOfWork.GetRepository<User>();
            _todoRepo = _unitOfWork.GetRepository<Todo>();
        }

        public async Task CreateUser(UserRequest request)
        {
            bool userExists = await _userRepo.AnyAsync(u =>
             u.FirstName.ToLower() == request.FirstName.ToLower() &&
             u.LastName.ToLower() == request.LastName.ToLower());

            if (userExists)
                throw new InvalidOperationException("user already exists");

            User newUser = _mapper.Map<User>(request);

            await _userRepo.AddAsync(newUser);
        }

        public async Task DeleteUser(int Id)
        {
            User user = await _userRepo.GetSingleByAsync(u =>
                u.Id == Id);

            if (user == null)
                throw new InvalidOperationException("user doesnt exist");

            await _userRepo.DeleteAsync(user);
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            IEnumerable<User> user = await _userRepo.GetAllAsync();

            if (!user.Any())
                throw new InvalidOperationException("empty list of users");


            return user.Select(x => new UserResponse
            {
                  Id = x.Id,
                  FirstName = x.FirstName,
                  LastName = x.LastName
            });
        }

        public async Task<UserResponse> GetUser(int Id)
        {
            User user = await _userRepo.GetSingleByAsync(u => u.Id == Id);

            if (user == null)
                throw new InvalidOperationException("no user with that Id");

            return _mapper.Map<UserResponse>(user);
        }


        public async Task<IEnumerable<ToDoListResponse>> GetUserWithTodoAsync(int userId)
        {
            User user = await _userRepo.GetSingleByAsync(u => u.Id == userId);
            if (user == null)
                throw new InvalidOperationException("No user found");
            IEnumerable<Todo> todos = await _todoRepo.GetByAsync(x => x.UserId == userId, include: t => t.Include(t => t.User));
            return todos.Select(t => new ToDoListResponse
            {
                
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
            });
        }

        public async Task PatchUser(int Id, JsonPatchDocument<UpdateUserRequest> request)
        {
            User user = await _userRepo.GetSingleByAsync(u => u.Id == Id);

            if (user == null)
                throw new InvalidOperationException("No user found");

            UpdateUserRequest DataToUpdate = _mapper.Map<UpdateUserRequest>(user);

            request.ApplyTo(DataToUpdate);

            User UpdatedUser = _mapper.Map(DataToUpdate, user);

            await _userRepo.UpdateAsync(UpdatedUser);
        }
    }
}

