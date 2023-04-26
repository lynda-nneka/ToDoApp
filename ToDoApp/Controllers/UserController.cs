using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDoApp.BLL.Infrastructure;
using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL.Entities;
using ToDoApp.Models.Dtos.Requests;
using ToDoApp.Models.Dtos.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("get-all-users")]
        [SwaggerOperation(Summary = "Gets all users ")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets all users", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            IEnumerable<UserResponse> response = await _userService.GetAllUsers();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("get-a-user")]
        [SwaggerOperation(Summary = "Gets user with id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets user with id", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUser(int Id)
        {
            UserResponse response = await _userService.GetUser(Id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("get-a-user-with-todos")]
        [SwaggerOperation(Summary = "Gets user with todos")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets user with todos", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<ToDoListResponse>>> GetUserwithTodo(int userId)
        {
            IEnumerable<ToDoListResponse> response = await _userService.GetUserWithTodoAsync(userId);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("create-a-user")]
        [SwaggerOperation(Summary = "Creates a user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Creates a user ", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<UserResponse>> CreateUser(UserRequest request)
        {
            await _userService.CreateUser(request);
            return Ok();
        }

        

        [AllowAnonymous]
        [HttpDelete("delete-user")]
        [SwaggerOperation(Summary = "Delete a user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Deletes a user", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<UserResponse>> Deleteuser(int Id)
        {
            await _userService.DeleteUser(Id);
            return Ok();
        }



        [AllowAnonymous]
        [HttpPatch("patch-user-info")]
        [SwaggerOperation(Summary = "Patches user information")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Profile patched successfully", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No profile found for user", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to update profile", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateuserProfile(int Id, JsonPatchDocument<UpdateUserRequest> request)
        {
            await _userService.PatchUser(Id, request);
            return Ok();
        }

    }

}

