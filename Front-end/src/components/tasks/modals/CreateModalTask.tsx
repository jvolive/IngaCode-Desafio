import React, { useState } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import { CreateTaskModalProps } from "../../../types/tasks/application";

export const CreateModalTask: React.FC<CreateTaskModalProps> = ({
  show,
  onHide,
  onCreate,
}) => {
  const [taskName, setTaskName] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [projectId, setProjectId] = useState<string>("");

  const handleCreate = () => {
    onCreate({
      name: taskName,
      description,
      projectId,
    });
    setTaskName("");
    setDescription("");
    setProjectId("");
  };

  return (
    <Modal
      show={show}
      onHide={onHide}
      dialogClassName="modal-90w"
      aria-labelledby="create-task-modal-title"
    >
      <Modal.Header closeButton>
        <Modal.Title id="create-task-modal-title">Create New Task</Modal.Title>
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
        </Form>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onHide}>
          Cancel
        </Button>
        <Button variant="primary" onClick={handleCreate}>
          Create Task
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
