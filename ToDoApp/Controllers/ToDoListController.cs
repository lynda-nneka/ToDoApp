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
    public class ToDoAppController : ControllerBase
    {
        private readonly IToDoListService _toDoService;
        public ToDoAppController(IToDoListService toDoService)
        {
            _toDoService = toDoService;
        }


        [HttpGet("get-all-to-do-items")]
        [SwaggerOperation(Summary = "Gets all items in the to do list")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets all items in the to do list", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<GetAllToDoResponse>>> GetAllToDoItems()
        {
            IEnumerable<GetAllToDoResponse> response = await _toDoService.GetAllToDos();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("get-to-do-item")]
        [SwaggerOperation(Summary = "Gets item with id")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Gets item with id", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<IEnumerable<ToDoListResponse>>> GetToDoItem(int Id)
        {
            ToDoListResponse response = await _toDoService.GetToDo(Id);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("create-to-do-item")]
        [SwaggerOperation(Summary = "Creates a to do item")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Creates a to do item", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ToDoListResponse>> CreateToDoItem(ToDoListRequest request)
        {
            await _toDoService.CreateToDo(request);
            return Ok();
        }

        //[AllowAnonymous]
        //[HttpPut("update-to-do-item")]
        //[SwaggerOperation(Summary = "Update a to do item")]
        //[SwaggerResponse(StatusCodes.Status200OK, Description = "Updates a to do item", Type = typeof(SuccessResponse))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        //public async Task<ActionResult<ToDoListResponse>> UpdateToDoItem(int Id, CreateToDoListRequest request)
        //{
        //    await _toDoService.UpdateToDo(Id, request);
        //    return Ok();
        //}

        [AllowAnonymous]
        [HttpDelete("delete-to-do-item")]
        [SwaggerOperation(Summary = "Delete a to do item")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Deletes a to do item", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ToDoListResponse>> DeleteToDoItem(int Id)
        {
            await _toDoService.DeleteToDo(Id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("toggle-to-do-item")]
        [SwaggerOperation(Summary = "toggles a to do item")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "toggles a to do item", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ToDoListResponse>> ToggleToDoItem(int Id)
        {
            await _toDoService.ToggleToDo(Id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPatch("patch-to-do-list")]
        [SwaggerOperation(Summary = "Patches to do item")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Profile patched successfully", Type = typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No profile found for user", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to update profile", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateToDo(int Id, JsonPatchDocument<UpdateTodoRequest> request)
        {
            await _toDoService.PatchToDo(Id, request);
            return Ok();
        }

    }
}

