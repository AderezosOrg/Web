import axios from 'axios';

export const userAPI = axios.create({
  baseURL: 'http://localhost:5009/api/User',
});

export const postUser = async (userData) => {
  try {
    const response = await userAPI.post('/', userData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al crear usuario');
  }
};
