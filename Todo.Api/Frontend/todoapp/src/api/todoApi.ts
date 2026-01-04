import axios from 'axios';
import type { TodoTask } from '../types/TodoTask';

const API_URL = "https://localhost:7103/api/TodoTask";

export const GetAllTasks = async () => {
    try {
        const res = await axios.get(API_URL);
        return res.data;
    } catch (err) {
        console.error("Error fetching tasks:", err);
    }
};

// This handles both POST (new) and PUT (update)
export const CreateOrUpdateTask = async (task: TodoTask) => {
    try {
        //console.log(task);

        if (task.id > 0) {
            await axios.put(API_URL, task);    // UPDATE: The task has an ID
        } else {
            await axios.post(API_URL, task);   // CREATE: The task has no ID
        }
    } catch (err) {
        console.error("Error saving task:", err);
        alert("Could not save the task. Check if the backend is running.");
    }
};

export const DeleteTask = async (id: number) => {
    try {
        await axios.delete(`${API_URL}/${id}`);
    } catch (err) {
        console.error("Error deleting task:", err);
    }
};
