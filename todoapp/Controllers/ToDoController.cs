using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using todoapp.Models;

namespace todoapp.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ILogger<ToDoController> _logger;
        private const string TempDataKey = "ToDoList";

        public ToDoController(ILogger<ToDoController> logger)
        {
            _logger = logger;
        }

        // Retrieves the to-do list from TempData or initializes a new list if empty
        private List<ToDoModel> InitializeToDoList()
        {
            var todoListJson = TempData.Peek(TempDataKey) as string;

            if (string.IsNullOrEmpty(todoListJson))
            {
                return new List<ToDoModel>
                {
                    new ToDoModel { Task = "Do Web assignment", Id = 0 },
                    new ToDoModel { Task = "Do iOS assignment", Id = 1 },
                    new ToDoModel { Task = "Learn new chords", Id = 2 },
                    new ToDoModel { Task = "Get new Guitar bag", Id = 3 }
                };
            }

            return JsonSerializer.Deserialize<List<ToDoModel>>(todoListJson);
        }

        // Saves the to-do list into TempData as JSON data
        private void SaveToDoList(List<ToDoModel> todoList)
        {
            TempData[TempDataKey] = JsonSerializer.Serialize(todoList);
        }

        // Displays the to-do list on the index page
        public IActionResult Index()
        {
            var todoList = InitializeToDoList();
            return View(todoList);
        }

        // Adds a new task to the to-do list and redirects to the index page
        [HttpPost]
        public IActionResult AddTask(string Task)
        {
            var todoList = InitializeToDoList();
            todoList.Add(new ToDoModel { Task = Task, Id = todoList.Count });
            SaveToDoList(todoList);
            return RedirectToAction("Index");
        }

        // Toggles the completion status of a task in the to-do list and redirects to the index page
        [HttpPost]
        public IActionResult ToggleTask(int ToDoIndex)
        {
            var todoList = InitializeToDoList();

            foreach (var task in todoList)
            {
                if (task.Id == ToDoIndex)
                {
                    task.TaskDone = !task.TaskDone;
                    SaveToDoList(todoList);
                    break; // Exit the loop once the task is found and updated
                }
            }

            return RedirectToAction("Index");
        }
    }
}
