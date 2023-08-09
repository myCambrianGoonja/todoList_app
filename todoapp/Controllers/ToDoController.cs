using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using todoapp.Models;
using System.Diagnostics; // Add this using directive for Debug

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

        private List<ToDoModel> InitializeToDoList()
        {
            var todoListJson = TempData.Peek(TempDataKey) as string;
            if (string.IsNullOrEmpty(todoListJson))
            {
                Console.WriteLine("data empty entered");
                return new List<ToDoModel>
                {
                    new ToDoModel { Task = "Do Web assignment", Id = 0 },
                    new ToDoModel {  Task = "Do iOS assignment", Id = 1 },
                    new ToDoModel { Task = "Learn new chords", Id = 2 },
                    new ToDoModel {Task = "Get new Guitar bag",Id = 3 }
                };
            }
            return JsonSerializer.Deserialize<List<ToDoModel>>(todoListJson);
        }

        private void SaveToDoList(List<ToDoModel> todoList)
        {
            var todoListJson = JsonSerializer.Serialize(todoList);
            TempData[TempDataKey] = todoListJson;
        }

        public IActionResult Index()
        {
            var todoList = InitializeToDoList();
            Console.WriteLine(todoList[3].Id);
            return View(todoList);
        }

        [HttpPost]
        public IActionResult AddTask(string Task)
        {
            var todoList = InitializeToDoList();
            todoList.Add(new ToDoModel { Task = Task , Id = todoList.Count});

            SaveToDoList(todoList);

            return RedirectToAction("Index"); // Redirect to Index action to prevent form resubmission
        }

        [HttpPost]
        public IActionResult ToggleTask(int ToDoIndex)
        {
            var todoList = InitializeToDoList();
            Console.WriteLine(ToDoIndex);
             Console.WriteLine(todoList[ToDoIndex].Task);
            foreach (var task in todoList)
            {
                Console.WriteLine($"{task.Task} {task.Id}");
                if (task.Id == ToDoIndex)
                {
                    Console.WriteLine($"Before Toggle - Task Id: {task.Id}, Task Done: {task.TaskDone}, {task.Task}");
                    task.TaskDone = !task.TaskDone;
                    SaveToDoList(todoList);
                    Console.WriteLine($"After Toggle - Task Id: {task.Id}, Task Done: {task.TaskDone}, {task.Task}");
                    break; // Exit the loop once the task is found and updated
                }
            }

            return RedirectToAction("Index");
        }
    }
}
