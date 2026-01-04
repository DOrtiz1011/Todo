import { useEffect, useState } from 'react';
import './App.css'
import type { TodoTask } from './types/TodoTask';
import TodoTaskTable from './components/TodoTaskTable';
import { TaskForm } from './components/TaskForm';
import { GetAllTasks, CreateOrUpdateTask, DeleteTask } from './api/todoApi';

const App: React.FC = () => {
    const [tasks, setTasks] = useState<TodoTask[]>([]);
    const [view, setView] = useState<'list' | 'form'>('list');
    const [taskToEdit, setTaskToEdit] = useState<TodoTask | undefined>(undefined);

    const handleGetAllTasks = async () => {
        try {
            setTasks(await GetAllTasks());
        } catch (err) {
            console.error("Error fetching tasks:", err);
        }
    };

    const handleSaveTask = async (task: TodoTask) => {
        await CreateOrUpdateTask(task);

        setView('list');
        setTaskToEdit(undefined);
        handleGetAllTasks();
    };

    const handleDeleteTask = async (id: number) => {
        await DeleteTask(id);
        handleGetAllTasks();
    };

    const startEdit = (task: TodoTask) => {
        setTaskToEdit(task);
        setView('form');
    };

    const startCreate = () => {
        setTaskToEdit(undefined);
        setView('form');
    };

    useEffect(() => { handleGetAllTasks(); }, []);

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
                        onDelete={handleDeleteTask}
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