export interface ReadTask {
  name: string;
  description: string;
  projectId: string;
  collaboratorName: string;
}

export interface CreateTaskRequest {
  name: string;
  description?: string;
  projectId: string;
  collaboratorName: string;
}

export interface UpdateTaskRequest {
  name: string;
  oldName: string;
  description?: string;
  projectId?: string;
  collaboratorName?: string;
}
