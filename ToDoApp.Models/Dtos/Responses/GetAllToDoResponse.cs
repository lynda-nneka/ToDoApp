using System;
using ToDoApp.DAL.Entities;
using ToDoApp.DAL.Enums;

namespace ToDoApp.Models.Dtos.Responses
{
    public class GetAllToDoResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; } = Priority.Important;
        public string DueDate { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}

