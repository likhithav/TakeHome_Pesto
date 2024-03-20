// script.js
const taskForm = document.getElementById('taskForm');
const taskList = document.getElementById('taskList');

taskForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const title = document.getElementById('title').value;
    const description = document.getElementById('description').value;
    const status = document.getElementById('status').value;

    const response = await fetch('/Task', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ title, description, status })
    });

    const task = await response.json();
    addTaskToList(task);
    taskForm.reset();
});

async function fetchTasks() {
    const response = await fetch('/Task');
    const tasks = await response.json();
    tasks.forEach(task => addTaskToList(task));
}

function addTaskToList(task) {
    const li = document.createElement('li');
    li.innerHTML = `
        <strong>${task.title}</strong>
        <p>${task.description}</p>
        <span>Status: ${task.status}</span>
        <button onclick="updateTask(${task.id})">Update</button>
        <button onclick="deleteTask(${task.id})">Delete</button>
    `;
    taskList.appendChild(li);
}

async function updateTask(id) {
    const title = prompt('Enter new title:');
    const description = prompt('Enter new description:');
    const status = prompt('Enter new status (To Do, In Progress, Done):');

    const response = await fetch(`/Task/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ title, description, status })
    });

    const updatedTask = await response.json();
    location.reload();
}

async function deleteTask(id) {
    const response = await fetch(`/Task/${id}`, { method: 'DELETE' });
    if (response.ok) {
        location.reload();
    } else {
        alert('Failed to delete task');
    }
}

fetchTasks();
