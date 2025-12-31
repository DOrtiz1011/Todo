import type { TodoTask } from './types/TodoTask';

interface TodoTaskTableProps {
    tasks: TodoTask[];
    onDelete: (id: number) => void;
    onEdit: (task: TodoTask) => void;
}

export const TodoTaskTable = ({ tasks, onDelete, onEdit }: TodoTaskTableProps) => {
    const formatDateTime = (dateValue: any) => {
        if (!dateValue) return 'N/A';

        console.log(dateValue);
        let dateStr = dateValue.toString();

        console.log(dateStr);

        // If the string doesn't end with 'Z' and doesn't contain a '+' (offset),
        // append 'Z' to force JavaScript to treat it as UTC.
        if (!dateStr.endsWith('Z') && !dateStr.includes('+')) {
            dateStr += 'Z';
        }

        const date = new Date(dateStr);

        if (isNaN(date.getTime()) || date.getFullYear() <= 1) return 'Not Set';

        // Now format using the local system settings
        const dayName = new Intl.DateTimeFormat('en-US', { weekday: 'short' }).format(date);
        const day = new Intl.DateTimeFormat('en-US', { day: '2-digit' }).format(date);
        const month = new Intl.DateTimeFormat('en-US', { month: 'short' }).format(date);
        const year = new Intl.DateTimeFormat('en-US', { year: 'numeric' }).format(date);

        const timeParts = new Intl.DateTimeFormat('en-US', {
            hour: 'numeric',
            minute: '2-digit',
            hour12: true,
            timeZoneName: 'short'
        }).formatToParts(date);

        const hour = timeParts.find(p => p.type === 'hour')?.value;
        const minute = timeParts.find(p => p.type === 'minute')?.value;
        const period = timeParts.find(p => p.type === 'dayPeriod')?.value;
        const tz = timeParts.find(p => p.type === 'timeZoneName')?.value;

        return `${dayName} ${day}-${month}-${year} ${hour}:${minute} ${period} ${tz}`;
    };

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
                                ? formatDateTime(task.duedatetime)
                                : 'No Date Set'}
                        </td>
                        <td style={cellStyle}>{formatDateTime(task.createdatetime)}</td>
                        <td style={cellStyle}>{formatDateTime(task.lastupdatedatetime)}</td>
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