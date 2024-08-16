import {
  Project,
  CreateProjectRequest,
} from "../../types/interfaces/projects/index";
import { axiosInstance } from "./helper/axiosInstance";

export const getProjects = async (): Promise<Project[]> => {
  try {
    const response = await axiosInstance.get("/project");
    return response.data;
  } catch (error) {
    console.error("Error fetching projects:", error);
    throw error;
  }
};

export const createProject = async (
  project: CreateProjectRequest
): Promise<Project> => {
  try {
    const response = await axiosInstance.post("/project", project);
    return response.data;
  } catch (error) {
    console.error("Error creating project:", error);
    throw error;
  }
};
