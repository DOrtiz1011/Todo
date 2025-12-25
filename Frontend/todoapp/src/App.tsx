import { useEffect, useState } from 'react';
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import api from './api';
import type { TodoTask } from './types/TodoTask';
import TodoTaskTable from './TodoTaskTable';
import axios from 'axios';

function App() {
    const [tasks, setTasks] = useState<TodoTask[]>([]);
    const [title, setTitle] = useState('');

    const fetchTasks = async () => {
        try {
            const res = await axios.get<TodoTask[]>('https://localhost:7103/api/TodoTask');
            setTasks(res.data);
        } catch (err) {
            console.error("Failed to fetch tasks", err);
        }
    };

    //const addTask = async () => {
    //    if (!title) return;
    //    const newTask: TodoTask = {
    //        title,
    //        status: TaskStatus.Todo,
    //        priority: TaskPriority.Medium
    //    };
    //    await axios.post(API_URL, newTask);
    //    setTitle('');
    //    fetchTasks();
    //};

    //const deleteTask = async (id: number) => {
    //    await axios.delete(`${API_URL}/${id}`);
    //    fetchTasks();
    //};

    useEffect(() => {
        fetchTasks();
    }, []);

    return (
        <div style={{ padding: '2rem', fontFamily: 'sans-serif' }}>
            <h1>My To-Do List</h1>

            {/*<div style={{ marginBottom: '1rem' }}>*/}
            {/*    <input*/}
            {/*        value={title}*/}
            {/*        onChange={(e) => setTitle(e.target.value)}*/}
            {/*        placeholder="New task name..."*/}
            {/*    />*/}
            {/*    <button onClick={addTask}>Add Task</button>*/}
            {/*</div>*/}

            <ul style={{ listStyle: 'none', padding: 0 }}>
                {tasks.map(task => (
                    <li key={task.id} style={{
                        borderBottom: '1px solid #ccc',
                        padding: '0.5rem',
                        display: 'flex',
                        justifyContent: 'space-between'
                    }}>
                        <span>
                            <strong>{task.title}</strong> - {task.title}
                        </span>
                        {/*<button onClick={() => deleteTask(task.id!)}>Delete</button>*/}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;