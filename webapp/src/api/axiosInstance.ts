import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: '/api', // Puedes cambiar esto a la URL base de tu API
  timeout: 10000, // Tiempo de espera de la petición
});

// Interceptor para añadir el token a las cabeceras de las peticiones
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('authToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosInstance;
