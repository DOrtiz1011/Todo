import type { TodoTask } from './types/TodoTask';
//import { TaskStatus, TaskPriority } from './types';

interface TodoTaskTableProps {
    tasks: TodoTask[];
    //onDelete: (id: number) => void;
    //onStatusChange?: (task: TodoTask) => void;
}

export const TodoTaskTable = ({ tasks/*, onDelete, onStatusChange*/ }: TodoTaskTableProps) => {
    return (
        <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '20px' }}>
            <thead>
                <tr style={{ backgroundColor: '#f4f4f4', textAlign: 'left' }}>
                    <th style={cellStyle}>Title</th>
                    <th style={cellStyle}>Description</th>
                    <th style={cellStyle}>Priority</th>
                    <th style={cellStyle}>Status</th>
                    <th style={cellStyle}>Due Date</th>
                    <th style={cellStyle}>Created</th>
                    <th style={cellStyle}>Last Updated</th>
                </tr>
            </thead>
            <tbody>
                {tasks.map((task) => (
                    <tr key={task.id} style={{ borderBottom: '1px solid #ddd' }}>
                        <td style={cellStyle}>{task.title}</td>
                        <td style={cellStyle}>{task.description}</td>
                        <td style={cellStyle}>{task.priority}</td>
                        <td style={cellStyle}>{task.status}</td>
                        <td style={cellStyle}>{task.duedatetime ? new Date(task.duedatetime).toLocaleDateString() : 'N/A'}</td>
                        <td style={cellStyle}>{new Date(task.createdatetime).toLocaleDateString()}</td>
                        <td style={cellStyle}>{new Date(task.lastupdatedatetime).toLocaleDateString()}</td>
                        {/*<td style={cellStyle}>*/}
                        {/*    <button onClick={() => onDelete(task.id!)}>Delete</button>*/}
                        {/*    {onStatusChange && (*/}
                        {/*        <button onClick={() => onStatusChange(task)}>Update Status</button>*/}
                        {/*    )}*/}
                        {/*</td>*/}
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

const cellStyle = { padding: '12px', border: '1px solid #ddd' };

export default TodoTaskTable;