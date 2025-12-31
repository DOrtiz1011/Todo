import React, { useState } from 'react';
import type { TodoTask } from './types/TodoTask';

interface TaskFormProps {
    initialData?: TodoTask;
    onSave: (task: Partial<TodoTask>) => void; // Use Partial because new tasks lack IDs
    onCancel: () => void;
}

export const TaskForm = ({ initialData, onSave, onCancel }: TaskFormProps) => {
    const [title, setTitle] = useState(initialData?.title || '');
    const [description, setDescription] = useState(initialData?.description || '');
    const [status, setStatus] = useState(initialData?.status ?? 'NotStarted');
    const [priority, setPriority] = useState(initialData?.priority ?? 'Low');

    // Format Date for the input (YYYY-MM-DDTHH:mm)
    const formatForInput = (date?: Date | string) => {
        if (!date) return '';
        const d = new Date(date);
        return d.toISOString().slice(0, 16);
    };

    const [dueDateTime, setDueDateTime] = useState(formatForInput(initialData?.duedatetime));

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const taskData: Partial<TodoTask> = {
            ...initialData, // Spreading preserves id, createdatetime, etc.
            title,
            description,
            status,
            priority,
            duedatetime: new Date(dueDateTime),
        };

        onSave(taskData);
    };

    return (
        <form onSubmit={handleSubmit} style={formStyle}>
            <h2>{initialData ? "Edit Task" : "New Task"}</h2>

            <label>Title</label>
            <input value={title} onChange={e => setTitle(e.target.value)} required />

            <label>Description</label>
            <textarea value={description} onChange={e => setDescription(e.target.value)} />

            <label>Priority</label>
            <select value={priority} onChange={(e) => setPriority(e.target.value)}>
                <option value='0'>Low</option>
                <option value='1'>Medium</option>
                <option value='2'>High</option>
            </select>

            <label>Status</label>
            <select value={status} onChange={(e) => setStatus(e.target.value)}>
                <option value='0'>Not Started</option>
                <option value='1'>In Progress</option>
                <option value='2'>Completed</option>
                <option value='3'>Blocked</option>
                <option value='4'>Cancelled</option>
            </select>

            <label>Due Date & Time</label>
            <input
                type="datetime-local"
                value={dueDateTime}
                onChange={e => setDueDateTime(e.target.value)}
            />

            <div style={{ marginTop: '10px' }}>
                <button type="submit">Save</button>
                <button type="button" onClick={onCancel}>Cancel</button>
            </div>
        </form>
    );
};

const formStyle: React.CSSProperties = { display: 'flex', flexDirection: 'column', gap: '8px', maxWidth: '300px' };