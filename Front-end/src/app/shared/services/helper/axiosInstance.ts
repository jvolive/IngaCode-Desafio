import axios from "axios";

const API_URL = "http://localhost:5000/api";

export const axiosInstance = axios.create({
  baseURL: API_URL,
  //   headers: {
  //     "Content-Type": "application/json",
  //     // Adicione outros cabeçalhos, como autenticação, se necessário
  //   },
});

axiosInstance.interceptors.request.use(
  (config) => {
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosInstance.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    return Promise.reject(error);
  }
);
