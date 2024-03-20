// TaskControllerTests.cs
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementApp.Tests
{
    public class TaskControllerTests
    {
        private TaskController _taskController;

        [SetUp]
        public void Setup()
        {
            _taskController = new TaskController();
        }

        [Test]
        public void GetTasks_ReturnsEmptyList_WhenNoTasksExist()
        {
            // Act
            var tasks = _taskController.GetTasks();

            // Assert
            Assert.IsEmpty(tasks);
        }

        [Test]
        public void CreateTask_AddsNewTask_ReturnsCreatedTask()
        {
            // Arrange
            var task = new Task { Id = 1, Title = "Test Task", Description = "This is a test task", Status = "To Do" };

            // Act
            var createdTask = _taskController.CreateTask(task);

            // Assert
            Assert.AreEqual(task, createdTask);
            Assert.AreEqual(1, _taskController.GetTasks().Count());
            Assert.AreEqual(task, _taskController.GetTasks().FirstOrDefault());
        }

        [Test]
        public void UpdateTask_UpdatesExistingTask_ReturnsUpdatedTask()
        {
            // Arrange
            var existingTask = new Task { Id = 1, Title = "Existing Task", Description = "This is an existing task", Status = "To Do" };
            _taskController.CreateTask(existingTask);

            var updatedTask = new Task { Id = 1, Title = "Updated Task", Description = "This is an updated task", Status = "In Progress" };

            // Act
            var result = _taskController.UpdateTask(1, updatedTask) as OkObjectResult;
            var updatedTaskResponse = result.Value as Task;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(updatedTaskResponse);
            Assert.AreEqual(updatedTask, updatedTaskResponse);
        }

        [Test]
        public void DeleteTask_RemovesExistingTask_ReturnsOk()
        {
            // Arrange
            var existingTask = new Task { Id = 1, Title = "Existing Task", Description = "This is an existing task", Status = "To Do" };
            _taskController.CreateTask(existingTask);

            // Act
            var result = _taskController.DeleteTask(1) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsEmpty(_taskController.GetTasks());
        }

        [Test]
        public void DeleteTask_ReturnsNotFound_WhenTaskNotFound()
        {
            // Act
            var result = _taskController.DeleteTask(1) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
