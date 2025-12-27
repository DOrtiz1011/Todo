import { useEffect, useState } from 'react';
import './App.css'
import type { TodoTask } from './types/TodoTask';
import TodoTaskTable from './TodoTaskTable';
import { TaskForm } from './TaskForm';
import axios from 'axios';

const API_URL = "https://localhost:7103/api/TodoTask";

const App: React.FC = () => {
    const [tasks, setTasks] = useState<TodoTask[]>([]);
    const [view, setView] = useState<'list' | 'form'>('list');
    const [taskToEdit, setTaskToEdit] = useState<TodoTask | undefined>(undefined);

    const fetchTasks = async () => {
        try {
            const res = await axios.get(API_URL);
            setTasks(res.data);
        } catch (err) {
            console.error("Error fetching tasks:", err);
        }
    };

    // This handles both POST (new) and PUT (update)
    const handleSaveTask = async (task: Partial<TodoTask>) => {
        try {
            console.log(task);
            if (task.id > 0) {
                // UPDATE: The task has an ID
                await axios.put(API_URL, task);
            } else {
                // CREATE: The task has no ID
                await axios.post(API_URL, task);
            }

            setView('list');
            setTaskToEdit(undefined);
            fetchTasks();
        } catch (err) {
            console.error("Error saving task:", err);
            alert("Could not save the task. Check if the backend is running.");
        }
    };

    const deleteTask = async (id: number) => {
        await axios.delete(`${API_URL}/${id}`);
        fetchTasks();
    };

    const startEdit = (task: TodoTask) => {
        setTaskToEdit(task);
        setView('form');
    };

    const startCreate = () => {
        setTaskToEdit(undefined);
        setView('form');
    };

    useEffect(() => { fetchTasks(); }, []);

    return (
        <div style={{ padding: '40px' }}>
            {view === 'list' ? (
                <>
                    <header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <h1>My Todos</h1>
                        <button onClick={startCreate} style={addButtonStyle}>
                            + Create New Task
                        </button>
                    </header>

                    <TodoTaskTable
                        tasks={tasks}
                        onDelete={deleteTask}
                        onEdit={startEdit}
                    />
                </>
            ) : (
                <TaskForm
                    initialData={taskToEdit}
                    onSave={handleSaveTask}
                    onCancel={() => setView('list')}
                />
            )}
        </div>
    );
}

const addButtonStyle = {
    backgroundColor: '#28a745',
    color: 'white',
    padding: '10px 20px',
    border: 'none',
    borderRadius: '4px',
    cursor: 'pointer',
    fontWeight: 'bold'
};

export default App;