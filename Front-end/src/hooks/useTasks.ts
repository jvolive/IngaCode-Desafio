import { useState, useEffect } from "react";
import {
  CreateTaskRequest,
  ReadTask,
  UpdateTaskRequest,
} from "../types/tasks/services";
import {
  deleteTask,
  getTasks,
  updateTask,
  createTask,
} from "../services/tasksService";

export const useTasks = () => {
  const [tasks, setTasks] = useState<ReadTask[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTasks = async () => {
      try {
        setLoading(true);
        const fetchedTasks = await getTasks();
        setTasks(fetchedTasks);
      } catch (error) {
        console.error("Error fetching tasks:", error);
        setError("Failed to fetch tasks.");
      } finally {
        setLoading(false);
      }
    };

    fetchTasks();
  }, []);

  const handleCreate = async (task: CreateTaskRequest) => {
    try {
      const newTask = await createTask(task);
      setTasks((prevTasks) => [...prevTasks, newTask]);
    } catch (error) {
      console.error("Error creating task:", error);
      setError("Failed to create task.");
    }
  };

  const handleUpdate = async (updateRequest: UpdateTaskRequest) => {
    try {
      const updatedTask = await updateTask(updateRequest);
      setTasks((prevTasks) =>
        prevTasks.map((task) =>
          task.name === updateRequest.oldName ? updatedTask : task
        )
      );
    } catch (err) {
      console.error("Failed to update project:", err);
    }
  };

  const handleDelete = async (name: string) => {
    try {
      await deleteTask(name);
      setTasks((prevTasks) => prevTasks.filter((task) => task.name !== name));
    } catch (error) {
      console.error("Error deleting task:", error);
      setError("Failed to delete task.");
    }
  };

  return {
    tasks,
    loading,
    error,
    createTask: handleCreate,
    updateTask: handleUpdate,
    deleteTask: handleDelete,
  };
};
