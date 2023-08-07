using System; 

//Declaring a todoModel
namespace todoapp.Models {

    public class ToDoModel {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool TaskDone { get; set; }
    }

}