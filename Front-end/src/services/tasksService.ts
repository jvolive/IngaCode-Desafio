import {
  ReadTask,
  CreateTaskRequest,
  UpdateTaskRequest,
} from "../types/tasks/services";
import { axiosInstance } from "./helper/axiosInstance";

/**
 * Fetches the list of tasks from the API.
 * Requires authorization.
 * @returns A promise that resolves to an array of `ReadTask` objects.
 * @throws Will throw an error if the request fails.
 */
export const getTasks = async (): Promise<ReadTask[]> => {
  try {
    const response = await axiosInstance.get("/TaskEntity");
    return response.data;
  } catch (error) {
    console.error("Error fetching tasks:", error);
    throw error;
  }
};

/**
 * Fetches a task by its ID.
 * @param id - The ID of the task.
 * @returns A promise that resolves to the `ReadTask` object.
 * @throws Will throw an error if the request fails.
 */
export const getTaskById = async (id: string): Promise<ReadTask> => {
  try {
    const response = await axiosInstance.get(`/TaskEntity/${id}`);
    return response.data;
  } catch (error) {
    console.error("Error fetching task:", error);
    throw error;
  }
};

/**
 * Creates a new task by sending a POST request to the API.
 * Requires authorization.
 * @param task - The `CreateTaskRequest` object containing the task details.
 * @returns A promise that resolves to the created `ReadTask` object.
 * @throws Will throw an error if the request fails.
 */
export const createTask = async (
  task: CreateTaskRequest
): Promise<ReadTask> => {
  try {
    const response = await axiosInstance.post("/TaskEntity", task);
    return response.data;
  } catch (error) {
    console.error("Error creating task:", error);
    throw error;
  }
};

/**
 * Updates a task by sending a PUT request to the API.
 * Requires authorization.
 * @param updateRequest - The `UpdateTaskRequest` object containing the updated task details.
 * @returns A promise that resolves to the updated `ReadTask` object.
 * @throws Will throw an error if the request fails.
 */
export const updateTask = async (
  updateRequest: UpdateTaskRequest
): Promise<ReadTask> => {
  try {
    const response = await axiosInstance.put(
      `/TaskEntity/${updateRequest.oldName}`,
      {
        name: updateRequest.name,
        description: updateRequest.description,
        projectId: updateRequest.projectId,
      }
    );
    return response.data;
  } catch (error) {
    console.error("Error updating task:", error);
    throw error;
  }
};

/**
 * Deletes a task by sending a DELETE request to the API.
 * Requires authorization.
 * @param id - The ID of the task to be deleted.
 * @returns A promise that resolves when the deletion is successful.
 * @throws Will throw an error if the request fails.
 */
export const deleteTask = async (id: string): Promise<void> => {
  try {
    await axiosInstance.delete(`/TaskEntity/${id}`);
  } catch (error) {
    console.error("Error deleting task:", error);
    throw error;
  }
};
