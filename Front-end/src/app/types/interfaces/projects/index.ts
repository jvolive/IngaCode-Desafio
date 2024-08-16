export interface Project {
  id: string;
  name: string;
  description?: string;
}

export interface CreateProjectRequest {
  name: string;
  description?: string;
}
