using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using todoapp.Models;

namespace todoapp.Controllers;

public class ToDoController : Controller
{
    private readonly ILogger<ToDoController> _logger;

    public ToDoController(ILogger<ToDoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
            var todoList = new List<ToDoModel>
        {
            new ToDoModel { Task = "Do Web assignment" },
            new ToDoModel { Task = "Do iOS assignment" },
            new ToDoModel { Task = "Learn new chords" },
            new ToDoModel { Task = "Get new Guitar bag" }
        };

        return View(todoList);
    }

}
