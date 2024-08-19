import { CreateTaskRequest, ReadTask, UpdateTaskRequest } from "../services";

export interface CreateTaskModalProps {
  show: boolean;
  onHide: () => void;
  onCreate: (task: CreateTaskRequest) => void;
}

export interface EditTaskModalProps {
  show: boolean;
  onHide: () => void;
  task: ReadTask | null;
  onSave: (updatedTask: UpdateTaskRequest) => void;
}

export interface DeleteTaskModalProps {
  show: boolean;
  onHide: () => void;
  task: ReadTask | null;
  onDelete: (taskName: string) => void;
}
