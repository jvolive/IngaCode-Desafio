import React, { useState, useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import { EditTaskModalProps } from "../../../types/tasks/application";

export const EditModalTask: React.FC<EditTaskModalProps> = ({
  show,
  onHide,
  task,
  onSave,
}) => {
  const [taskName, setTaskName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [projectId, setProjectId] = useState<string>("");
  const [collaboratorName, setCollaboratorName] = useState<string>("");

  useEffect(() => {
    if (task) {
      setTaskName(task.name);
      setDescription(task.description || "");
      setProjectId(task.projectId);
      setCollaboratorName(task.collaboratorName || "");
    }
  }, [task]);

  const handleSave = () => {
    if (task) {
      onSave({
        oldName: task.name,
        name: taskName,
        description,
        projectId,
        collaboratorName,
      });
    }
  };

  return (
    <Modal
      show={show}
      onHide={onHide}
      dialogClassName="modal-90w"
      aria-labelledby="edit-task-modal-title"
    >
      <Modal.Header closeButton>
        <Modal.Title id="edit-task-modal-title">Edit Task</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Form>
          <Form.Group controlId="formTaskName">
            <Form.Label>Task Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter task name"
              value={taskName}
              onChange={(e) => setTaskName(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="formDescription">
            <Form.Label>Description</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="formProjectId">
            <Form.Label>Project ID</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter project ID"
              value={projectId}
              onChange={(e) => setProjectId(e.target.value)}
            />
          </Form.Group>
          <Form.Group controlId="formCollaboratorName">
            <Form.Label>Collaborator Name</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter collaborator name"
              value={collaboratorName}
              onChange={(e) => setCollaboratorName(e.target.value)}
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
