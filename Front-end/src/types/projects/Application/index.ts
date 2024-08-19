import { ReadProject, UpdateProjectRequest } from "../service";

export interface CreateProjectModal {
  show: boolean;
  onHide: () => void;
  onCreate: (name: string) => void;
}

export interface EditProjectModal {
  show: boolean;
  onHide: () => void;
  project: ReadProject | null;
  onSave: (updatedProject: UpdateProjectRequest) => void;
}

export interface DeleteProjectModal {
  show: boolean;
  onHide: () => void;
  project: ReadProject | null;
  onDelete: (projectName: string) => void;
}
