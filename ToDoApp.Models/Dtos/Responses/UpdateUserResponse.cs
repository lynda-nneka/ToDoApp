using System;
namespace ToDoApp.Models.Dtos.Responses
{
    public class UpdateUserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

