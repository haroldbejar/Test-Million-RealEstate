const API_BASE_URL = "http://localhost:8000"; //"http://localhost:5254";

export const endpoints = {
  auth: {
    login: `${API_BASE_URL}/account/login`,
    register: `${API_BASE_URL}/account/register`,
    logout: `${API_BASE_URL}/auth/logout`,
    profile: `${API_BASE_URL}/auth/profile`,
  },
  properties: {
    getAll: `${API_BASE_URL}/properties`,
    getById: (id: string) => `${API_BASE_URL}/properties/${id}`,
    create: `${API_BASE_URL}/properties`,
    update: (id: string) => `${API_BASE_URL}/properties/${id}`,
    delete: (id: string) => `${API_BASE_URL}/properties/${id}`,
    search: `${API_BASE_URL}/properties/search`,
  },
  owners: {
    getAll: `${API_BASE_URL}/owners`,
    getById: (id: string) => `${API_BASE_URL}/owners/${id}`,
    create: `${API_BASE_URL}/owners`,
    update: (id: string) => `${API_BASE_URL}/owners/${id}`,
    delete: (id: string) => `${API_BASE_URL}/owners/${id}`,
  },
  images: {
    show: "http://localhost:8003/",
  },
};
