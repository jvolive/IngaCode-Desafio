import { ReadProject, UpdateProjectRequest } from "../service";

export interface CreateProjectModalProps {
  show: boolean;
  onHide: () => void;
  onCreate: (name: string) => void;
}

export interface EditProjectModalProps {
  show: boolean;
  onHide: () => void;
  project: ReadProject | null;
  onSave: (updatedProject: UpdateProjectRequest) => void;
}

export interface DeleteProjectModalProps {
  show: boolean;
  onHide: () => void;
  project: ReadProject | null;
  onDelete: (projectName: string) => void;
}
