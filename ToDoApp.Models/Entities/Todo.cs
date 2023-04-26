using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.DAL.Enums;

namespace ToDoApp.DAL.Entities
{
    public class Todo : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; } = Priority.Important;
        public DateTime DueDate { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}

