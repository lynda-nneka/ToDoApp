using System;
using ToDoApp.DAL.Enums;

namespace ToDoApp.Models.Dtos.Requests
{
    public class ToDoListRequest
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string DueDate { get; set; }
   

    }
}

