export interface TodoTask {
    id: number;
    createdatetime: string; // Dates from JSON are strings until parsed
    lastupdatedatetime: string;
    title: string;
    description: string;
    duedatetime: string;
    status: string;   // Changed from number
    priority: string; // Changed from number
}