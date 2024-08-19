// src/context/AuthContext.tsx
import React, { createContext, useState, useEffect } from "react";
import { IContext, IAuthProvider, IUser } from "../types/auth";
import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../services/helper/axiosInstance";

export const AuthContext = createContext<IContext | undefined>(undefined);

export const AuthProvider: React.FC<IAuthProvider> = ({ children }) => {
  const [user, setUser] = useState<IUser | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const authenticate = async (username: string, password: string) => {
    try {
      const response = await axiosInstance.post(
        "/auth/login",
        {
          username,
          password,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      const token = response.data;
      if (!token) {
        throw new Error("No token received from API");
      }

      const userData = { username, token };
      setUser(userData);

      localStorage.setItem("user", JSON.stringify({ username }));
      localStorage.setItem("token", token);

      navigate("/dashboard");
    } catch (error) {
      console.error("Authentication failed:", error);
    }
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem("user");
    localStorage.removeItem("token");
    navigate("/login");
  };

  return (
    <AuthContext.Provider value={{ ...user, authenticate, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
