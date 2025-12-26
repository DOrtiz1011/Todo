import { useEffect, useState } from 'react';
import './App.css'
import type { TodoTask } from './types/TodoTask';
import TodoTaskTable from './TodoTaskTable';
import axios from 'axios';
import { CreateTask } from './CreateTask';

const API_URL = "https://localhost:7103/api/TodoTask";

const App: React.FC = () => {
    const [tasks, setTasks] = useState<TodoTask[]>([]);
    const [view, setView] = useState<'list' | 'create'>('list');

    const fetchTasks = async () => {
        const res = await axios.get(API_URL);
        setTasks(res.data);
    };

    const handleCreateTask = async (task: TodoTask) => {
        await axios.post(API_URL, task);
        setView('list'); // Go back to list
        fetchTasks();    // Refresh data
    };

    const deleteTask = async (id: number) => {
        await axios.delete(`${API_URL}/${id}`);
        fetchTasks();
    };

    useEffect(() => { fetchTasks(); }, []);

    return (
        <div style={{ padding: '40px' }}>
            {view === 'list' ? (
                <>
                    <h1>My Tasks</h1>
                    <button onClick={() => setView('create')} style={{ marginBottom: '10px' }}>
                        + Add New Task
                    </button>
                    <TodoTaskTable tasks={tasks} onDelete={deleteTask} />
                </>
            ) : (
                <CreateTask
                    onTaskCreated={handleCreateTask}
                    onCancel={() => setView('list')}
                />
            )}
        </div>
    );
}

export default App;