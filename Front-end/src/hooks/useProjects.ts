import { useState, useEffect } from "react";
import {
  getProjects,
  createProject,
  updateProject,
  deleteProject,
} from "../services/projectsService";
import {
  ReadProject,
  CreateProjectRequest,
  UpdateProjectRequest,
} from "../types/projects/service";

export const useProject = () => {
  const [projects, setProjects] = useState<ReadProject[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchProjects = async () => {
      setLoading(true);
      try {
        const data = await getProjects();
        setProjects(data);
      } catch (err) {
        setError("Failed to fetch projects.");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchProjects();
  }, []);

  const handleCreateProject = async (project: CreateProjectRequest) => {
    try {
      const newProject = await createProject(project);
      setProjects((prevProjects) => [...prevProjects, newProject]);
    } catch (err) {
      console.error("Failed to create project:", err);
    }
  };

  const handleUpdateProject = async (updateRequest: UpdateProjectRequest) => {
    try {
      const updatedProject = await updateProject(updateRequest);
      setProjects((prevProjects) =>
        prevProjects.map((project) =>
          project.name === updateRequest.oldName ? updatedProject : project
        )
      );
    } catch (err) {
      console.error("Failed to update project:", err);
    }
  };

  const handleDeleteProject = async (name: string) => {
    try {
      await deleteProject(name);
      setProjects((prevProjects) =>
        prevProjects.filter((project) => project.name !== name)
      );
    } catch (err) {
      console.error("Failed to delete project:", err);
    }
  };

  return {
    projects,
    loading,
    error,
    createProject: handleCreateProject,
    updateProject: handleUpdateProject,
    deleteProject: handleDeleteProject,
  };
};
