using System;
using ToDoApp.DAL.Enums;

namespace ToDoApp.Models.Dtos.Responses
{
    public class ToDoListResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; } = Priority.Important;
        public DateTime DueDate { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}

