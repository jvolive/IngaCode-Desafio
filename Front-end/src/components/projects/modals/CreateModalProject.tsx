import React, { useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import { CreateProjectModalProps } from "../../../types/projects/Application";

export const CreateProjectModal: React.FC<CreateProjectModalProps> = ({
  show,
  onHide,
  onCreate,
}) => {
  const [projectName, setProjectName] = useState<string>("");

  const handleCreate = () => {
    onCreate(projectName);
    setProjectName("");
  };

  return (
    <Modal
      show={show}
      onHide={onHide}
      dialogClassName="modal-90w"
      aria-labelledby="create-project-modal-title"
    >
      <Modal.Header closeButton>
        <Modal.Title id="create-project-modal-title">
          Create New Project
        </Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="formProjectName">
            <Form.Label>Project Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter project name"
              value={projectName}
              onChange={(e) => setProjectName(e.target.value)}
            />
          </Form.Group>
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>
          Cancel
        </Button>
        <Button variant="primary" onClick={handleCreate}>
          Create Project
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
