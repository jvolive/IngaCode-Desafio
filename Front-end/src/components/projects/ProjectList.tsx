import React from "react";
import { ListGroup, Button } from "react-bootstrap";
import { useProject } from "../../hooks/useProjects";
import {
  ReadProject,
  UpdateProjectRequest,
} from "../../types/projects/service";
import {
  CreateModalProject,
  DeleteModalProject,
  EditModalProject,
} from "./modals";

export const ProjectList: React.FC = () => {
  const {
    projects,
    loading,
    error,
    createProject,
    updateProject,
    deleteProject,
  } = useProject();

  const [selectedProject, setSelectedProject] =
    React.useState<ReadProject | null>(null);
  const [showCreateModal, setShowCreateModal] = React.useState<boolean>(false);
  const [showEditModal, setShowEditModal] = React.useState<boolean>(false);
  const [showDeleteModal, setShowDeleteModal] = React.useState<boolean>(false);

  const handleCreate = (name: string) => {
    createProject({ name });
    setShowCreateModal(false);
  };

  const handleUpdate = (updatedProject: UpdateProjectRequest) => {
    if (selectedProject) {
      updateProject({
        oldName: selectedProject.name,
        name: updatedProject.name,
      });
      setShowEditModal(false);
    }
  };

  const handleDelete = () => {
    if (selectedProject) {
      deleteProject(selectedProject.name);
      setShowDeleteModal(false);
    }
  };

  return (
    <div>
      {loading && <p>Loading...</p>}
      {error && <p>{error}</p>}
      {!loading && !error && (
        <ListGroup>
          <h3>Lista de Projetos</h3>
          {projects.map((project) => (
            <ListGroup.Item key={project.name}>
              {project.name}
              <Button
                variant="warning"
                onClick={() => {
                  setSelectedProject(project);
                  setShowEditModal(true);
                }}
                className="ms-2"
              >
                Edit
              </Button>
              <Button
                variant="danger"
                onClick={() => {
                  setSelectedProject(project);
                  setShowDeleteModal(true);
                }}
                className="ms-2"
              >
                Delete
              </Button>
            </ListGroup.Item>
          ))}
          <Button variant="primary" onClick={() => setShowCreateModal(true)}>
            Create Project
          </Button>
        </ListGroup>
      )}

      <CreateModalProject
        show={showCreateModal}
        onHide={() => setShowCreateModal(false)}
        onCreate={handleCreate}
      />

      {selectedProject && (
        <>
          <EditModalProject
            show={showEditModal}
            onHide={() => setShowEditModal(false)}
            project={selectedProject}
            onSave={handleUpdate}
          />

          <DeleteModalProject
            show={showDeleteModal}
            onHide={() => setShowDeleteModal(false)}
            project={selectedProject}
            onDelete={handleDelete}
          />
        </>
      )}
    </div>
  );
};
