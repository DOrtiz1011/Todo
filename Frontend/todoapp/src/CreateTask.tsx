import React, { useState } from 'react';
//import { TaskPriority } from '../src/types';
import type { TodoTask } from '../src/types/TodoTask';

interface CreateTaskProps {
    onTaskCreated: (task: TodoTask) => void;
    onCancel: () => void;
}

export const CreateTask = ({ onTaskCreated, onCancel }: CreateTaskProps) => {
    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    //const [priority, setPriority] = useState<TaskPriority>(TaskPriority.Medium);
    const [dueDate, setDueDate] = useState('');

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!title) return alert("Title is required");

        const newTask: TodoTask = {
            id: 0,
            title,
            priority: 0,
            status: 0,
            createdatetime: new Date,
            duedatetime: new Date,
            description: 'DESCRIPTION',
            lastupdatedatetime: new Date
            // Convert empty string to undefined so C# handles it as null
            //duedatetime: dueDate ? new Date(dueDate).toISOString() : undefined,
        };

        onTaskCreated(newTask);
    };

    return (
        <div style={formContainerStyle}>
            <h2>Create New Task</h2>
            <form onSubmit={handleSubmit} style={formStyle}>
                <label>Title</label>
                <input
                    type="text"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    placeholder="What needs to be done?"
                />
                <label>Description</label>
                <input
                    type="text"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    placeholder="Any details?"
                />

                <label>Priority</label>
                <select value='0' onChange={(e) => setPriority(Number(e.target.value))}>
                    <option value='0'>Low</option>
                    <option value='1'>Medium</option>
                    <option value='2'>High</option>
                </select>

                <label>Due Date (Optional)</label>
                <input
                    type="date"
                    value={dueDate}
                    onChange={(e) => setDueDate(e.target.value)}
                />

                <div style={{ marginTop: '20px', display: 'flex', gap: '10px' }}>
                    <button type="submit" style={saveButtonStyle}>Save Task</button>
                    <button type="button" onClick={onCancel} style={cancelButtonStyle}>Cancel</button>
                </div>
            </form>
        </div>
    );
};

// Simple Styles
const formContainerStyle: React.CSSProperties = { padding: '20px', border: '1px solid #ccc', borderRadius: '8px', maxWidth: '400px', background: '#000000' };
const formStyle: React.CSSProperties = { display: 'flex', flexDirection: 'column', gap: '10px' };
const saveButtonStyle = { backgroundColor: '#28a745', color: 'white', border: 'none', padding: '10px', cursor: 'pointer' };
const cancelButtonStyle = { backgroundColor: '#dc3545', color: 'white', border: 'none', padding: '10px', cursor: 'pointer' };