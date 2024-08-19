import React from "react";
import { Modal, Button } from "react-bootstrap";
import { DeleteTaskModalProps } from "../../../types/tasks/application";

export const DeleteModalTask: React.FC<DeleteTaskModalProps> = ({
  show,
  onHide,
  task,
  onDelete,
}) => {
  const handleDelete = () => {
    if (task) {
      onDelete(task.name);
    }
  };

  return (
    <Modal
      show={show}
      onHide={onHide}
      dialogClassName="modal-90w"
      aria-labelledby="delete-task-modal-title"
    >
      <Modal.Header closeButton>
        <Modal.Title id="delete-task-modal-title">Delete Task</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        Are you sure you want to delete the task "{task?.name}"?
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>
          Cancel
        </Button>
        <Button variant="danger" onClick={handleDelete}>
          Delete
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
