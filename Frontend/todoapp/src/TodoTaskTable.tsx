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
                <tr style={{ backgroundColor: '#f4f4f4', textAlign: 'center', alignContent: 'center' }}>
                    <th style={headerStyle}>Title</th>
                    <th style={headerStyle}>Description</th>
                    <th style={headerStyle}>Priority</th>
                    <th style={headerStyle}>Status</th>
                    <th style={headerStyle}>Due Date</th>
                    <th style={headerStyle}>Created</th>
                    <th style={headerStyle}>Last Updated</th>
                </tr>
            </thead>
            <tbody>
                {tasks.map((task) => (
                    <tr key={task.id} style={{ borderBottom: '1px solid #ddd' }}>
                        <td style={cellStyle}>{task.title}</td>
                        <td style={cellStyle}>{task.description}</td>
                        <td style={cellStyle}>{task.priority}</td>
                        <td style={cellStyle}>{task.status}</td>
                        <td style={cellStyle}>{task.duedatetime && !isNaN(Date.parse(task.duedatetime.toString())) ? new Date(task.duedatetime).toLocaleDateString() : 'No Date Set'}</td>
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

const headerStyle = { padding: '12px', border: '3px solid #ddd', backgroundColor: '#444444', alignContent: 'center' };
const cellStyle = { padding: '12px', border: '1px solid #ddd', backgroundColor: '#000000' };

export default TodoTaskTable;