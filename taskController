// Task.cs
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}

// TaskController.cs
[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly List<Task> _tasks = new List<Task>();

    [HttpGet]
    public IEnumerable<Task> GetTasks()
    {
        return _tasks;
    }

    [HttpPost]
    public Task CreateTask(Task task)
    {
        _tasks.Add(task);
        return task;
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, Task task)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
        if (existingTask == null)
        {
            return NotFound();
        }

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.Status = task.Status;

        return Ok(existingTask);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);
        if (taskToRemove == null)
        {
            return NotFound();
        }

        _tasks.Remove(taskToRemove);
        return Ok();
    }
}
