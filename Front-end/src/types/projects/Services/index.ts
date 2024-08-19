export interface ReadProject {
  name: string;
}

export interface CreateProjectRequest {
  name: string;
}

export interface UpdateProjectRequest {
  name: string;
  oldName: string;
}
