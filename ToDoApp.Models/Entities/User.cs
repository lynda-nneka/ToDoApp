using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

namespace ToDoApp.DAL.Entities
{
    public class User : BaseEntity
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Todo> Todos { get; set; } = new List<Todo>();

    }
}

