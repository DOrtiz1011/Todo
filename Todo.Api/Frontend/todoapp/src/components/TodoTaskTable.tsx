import type { TodoTask } from '../types/TodoTask';
import { formatDateTimeForDisplay } from '../utils/dateUtils';

interface TodoTaskTableProps {
    tasks: TodoTask[];
    onDelete: (id: number) => void;
    onEdit: (task: TodoTask) => void;
}

export const TodoTaskTable = ({ tasks, onDelete, onEdit }: TodoTaskTableProps) => {
    return (
        <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '20px', color: 'white' }}>
            <thead>
                <tr style={{ backgroundColor: '#444444', color: 'white' }}>
                    <th style={headerStyle}>Title</th>
                    <th style={headerStyle}>Description</th>
                    <th style={headerStyle}>Priority</th>
                    <th style={headerStyle}>Status</th>
                    <th style={headerStyle}>Due Date</th>
                    <th style={headerStyle}>Created</th>
                    <th style={headerStyle}>Last Updated</th>
                    <th style={headerStyle}>Actions</th>
                </tr>
            </thead>
            <tbody>
                {tasks.map((task) => (
                    <tr key={task.id} style={{ borderBottom: '1px solid #ddd' }}>
                        <td style={cellStyle}>{task.title}</td>
                        <td style={cellStyle}>{task.description}</td>
                        <td style={cellStyle}>{task.priority}</td>
                        <td style={cellStyle}>{task.status}</td>
                        <td style={cellStyle}>
                            {task.duedatetime && !isNaN(Date.parse(task.duedatetime.toString()))
                                ? formatDateTimeForDisplay(task.duedatetime)
                                : 'No Date Set'}
                        </td>
                        <td style={cellStyle}>{formatDateTimeForDisplay(task.createdatetime)}</td>
                        <td style={cellStyle}>{formatDateTimeForDisplay(task.lastupdatedatetime)}</td>
                        <td style={cellStyle}>
                            <div style={{ display: 'flex', gap: '5px' }}>
                                <button onClick={() => onEdit(task)} style={editButtonStyle}>Update</button>
                                <button onClick={() => onDelete(task.id)} style={deleteButtonStyle}>Delete</button>
                            </div>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

// Styles
const headerStyle: React.CSSProperties = { padding: '12px', border: '1px solid #ddd', textAlign: 'left' };
const cellStyle: React.CSSProperties = { padding: '12px', border: '1px solid #ddd', backgroundColor: '#1a1a1a', color: 'white' };
const editButtonStyle = { backgroundColor: '#007bff', color: 'white', border: 'none', padding: '5px 10px', cursor: 'pointer', borderRadius: '4px' };
const deleteButtonStyle = { backgroundColor: '#dc3545', color: 'white', border: 'none', padding: '5px 10px', cursor: 'pointer', borderRadius: '4px' };

export default TodoTaskTable;