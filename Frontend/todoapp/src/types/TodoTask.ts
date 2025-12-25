export interface TodoTask {
    id: number;
    createdatetime: Date;
    lastupdatedatetime: Date;
    title: string;
    description: string;
    duedatetime: Date;
    status: number;
    priority: number;
}
