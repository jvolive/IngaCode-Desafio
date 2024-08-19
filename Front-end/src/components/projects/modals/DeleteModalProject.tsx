import React from "react";
import { Modal, Button } from "react-bootstrap";
import { DeleteProjectModal } from "../../../types/projects/application";

export const DeleteModalProject: React.FC<DeleteProjectModal> = ({
  show,
  onHide,
  project,
  onDelete,
}) => {
  const handleDelete = () => {
    if (project) {
      onDelete(project.name);
    }
  };

  return (
    <Modal
      show={show}
      onHide={onHide}
      dialogClassName="modal-90w"
      aria-labelledby="delete-project-modal-title"
    >
      <Modal.Header closeButton>
        <Modal.Title id="delete-project-modal-title">
          Delete Project
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        Are you sure you want to delete the project "{project?.name}"?
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
