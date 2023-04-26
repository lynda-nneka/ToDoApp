using System;
using System.Threading.Tasks;

namespace ToDoApp.Models.Dtos.Requests
{
    public class UserWithToDoRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ToDoListRequest> ToDoListRequests { get; set; }
    }
}

