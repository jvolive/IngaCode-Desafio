import {
  ReadProject,
  CreateProjectRequest,
  UpdateProjectRequest,
} from "../types/projects/service";
import { axiosInstance } from "./helper/axiosInstance";

/**
 * Fetches the list of projects from the API.
 * @returns A promise that resolves to an array of `ReadProject` objects.
 * @throws Will throw an error if the request fails.
 */
export const getProjects = async (): Promise<ReadProject[]> => {
  try {
    const response = await axiosInstance.get("/Projects");
    return response.data; // Retorna a lista completa de projetos
  } catch (error) {
    console.error("Error fetching projects:", error);
    throw error;
  }
};

/**
 * Fetches a project by its name.
 * @param name - The name of the project.
 * @returns A promise that resolves to the `ReadProject` object.
 * @throws Will throw an error if the request fails.
 */
export const getProjectByName = async (name: string): Promise<ReadProject> => {
  try {
    const response = await axiosInstance.get(`/Projects/${name}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching project:", error);
    throw error;
  }
};

/**
 * Creates a new project by sending a POST request to the API.
 * @param project - The `CreateProjectRequest` object containing the project details.
 * @returns A promise that resolves to the created `ReadProject` object.
 * @throws Will throw an error if the request fails.
 */
export const createProject = async (
  project: CreateProjectRequest
): Promise<ReadProject> => {
  try {
    const response = await axiosInstance.post("/Projects", project);
    return response.data;
  } catch (error) {
    console.error("Error creating project:", error);
    throw error;
  }
};

/**
 * Updates a project by sending a PUT request to the API.
 * @param updateRequest - The `UpdateProjectRequest` object containing the old and new names.
 * @returns A promise that resolves to the updated `ReadProject` object.
 * @throws Will throw an error if the request fails.
 */
export const updateProject = async (
  updateRequest: UpdateProjectRequest
): Promise<ReadProject> => {
  try {
    const response = await axiosInstance.put(
      `/Projects/${updateRequest.oldName}`,
      {
        name: updateRequest.name,
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error updating project:", error);
    throw error;
  }
};

/**
 * Deletes a project by sending a DELETE request to the API.
 * @param name - The name of the project to be deleted.
 * @returns A promise that resolves when the deletion is successful.
 * @throws Will throw an error if the request fails.
 */
export const deleteProject = async (name: string): Promise<void> => {
  try {
    await axiosInstance.delete(`/Projects/${name}`);
  } catch (error) {
    console.error("Error deleting project:", error);
    throw error;
  }
};
