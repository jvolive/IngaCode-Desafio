export interface ReadTask {
  name: string;
  description: string;
  projectId: string;
}

export interface CreateTaskRequest {
  name: string;
  description?: string;
  projectId: string;
}

export interface UpdateTaskRequest {
  name: string;
  oldName: string;
  description?: string;
  projectId: string;
}
