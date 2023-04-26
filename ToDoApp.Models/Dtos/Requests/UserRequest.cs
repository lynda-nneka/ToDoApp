using System;
namespace ToDoApp.Models.Dtos.Requests
{
    public class UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

