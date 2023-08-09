using System; 

namespace todoapp.Models 
{
    // Represents a single task in the to-do list
    public class ToDoModel 
    {
        // Gets or sets the unique identifier of the task
        public int Id { get; set; }
        
        // Gets or sets the description of the task
        public string? Task { get; set; }
        
        // Gets or sets a value indicating whether the task is marked as done or not
        public bool TaskDone { get; set; }
    }
}
