import React from "react";
import { ListGroup, Button } from "react-bootstrap";
import { useTasks } from "../../hooks/useTasks";
import {
  CreateTaskRequest,
  UpdateTaskRequest,
  ReadTask,
} from "../../types/tasks/services";
import { CreateModalTask, DeleteModalTask, EditModalTask } from "./modals";

export const TaskList: React.FC = () => {
  const { tasks, loading, error, createTask, updateTask, deleteTask } =
    useTasks();
  const [selectedTask, setSelectedTask] = React.useState<ReadTask | null>(null);
  const [showCreateModal, setShowCreateModal] = React.useState<boolean>(false);
  const [showEditModal, setShowEditModal] = React.useState<boolean>(false);
  const [showDeleteModal, setShowDeleteModal] = React.useState<boolean>(false);

  const handleCreate = async (task: CreateTaskRequest) => {
    try {
      await createTask(task);
    } catch (error) {
      console.error("Failed to create task:", error);
    }
    setShowCreateModal(false);
  };

  const handleUpdate = async (updatedTask: UpdateTaskRequest) => {
    if (selectedTask) {
      try {
        await updateTask(updatedTask);
      } catch (error) {
        console.error("Failed to update task:", error);
      }
      setShowEditModal(false);
    }
  };

  const handleDelete = async () => {
    if (selectedTask) {
      try {
        await deleteTask(selectedTask.name);
      } catch (error) {
        console.error("Failed to delete task:", error);
      }
      setShowDeleteModal(false);
    }
  };

  return (
    <div>
      {loading && <p>Loading...</p>}
      {error && <p>{error}</p>}
      {!loading && !error && (
        <>
          <ListGroup>
            <h3>Task List</h3>
            {tasks.map((task) => (
              <ListGroup.Item key={task.name}>
                {task.name}
                <Button
                  variant="warning"
                  onClick={() => {
                    setSelectedTask(task);
                    setShowEditModal(true);
                  }}
                  className="ms-3"
                >
                  Edit
                </Button>
                <Button
                  variant="danger"
                  onClick={() => {
                    setSelectedTask(task);
                    setShowDeleteModal(true);
                  }}
                  className="ms-3"
                >
                  Delete
                </Button>
              </ListGroup.Item>
            ))}
            <Button
              variant="primary"
              onClick={() => setShowCreateModal(true)}
              disabled={loading}
              className="mt-3"
            >
              Create Task
            </Button>
          </ListGroup>

          <CreateModalTask
            show={showCreateModal}
            onHide={() => setShowCreateModal(false)}
            onCreate={handleCreate}
          />

          {selectedTask && (
            <>
              <EditModalTask
                show={showEditModal}
                onHide={() => setShowEditModal(false)}
                task={selectedTask}
                onSave={handleUpdate}
              />

              <DeleteModalTask
                show={showDeleteModal}
                onHide={() => setShowDeleteModal(false)}
                task={selectedTask}
                onDelete={handleDelete}
              />
            </>
          )}
        </>
      )}
    </div>
  );
};
