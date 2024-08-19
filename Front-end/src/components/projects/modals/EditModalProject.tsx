import React, { useState, useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import { EditProjectModal } from "../../../types/projects/application";

export const EditModalProject: React.FC<EditProjectModal> = ({
  show,
  onHide,
  project,
  onSave,
}) => {
  const [projectName, setProjectName] = useState<string>("");

  useEffect(() => {
    if (project) {
      setProjectName(project.name);
    }
  }, [project]);

  const handleSave = () => {
    if (project) {
      onSave({
        oldName: project.name,
        name: projectName,
      });
    }
  };

  return (
    <Modal
      show={show}
      onHide={onHide}
      dialogClassName="modal-90w"
      aria-labelledby="edit-project-modal-title"
    >
      <Modal.Header closeButton>
        <Modal.Title id="edit-project-modal-title">Edit Project</Modal.Title>
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
        <Button variant="primary" onClick={handleSave}>
          Save Changes
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
