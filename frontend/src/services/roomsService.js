import axios from 'axios';

export const roomAPI = axios.create({
  baseURL: 'http://localhost:5009/api/Room',
});

export const getAvailableRooms = async (startDate, endDate) => {
  try {
    const response = await roomAPI.get(`available/${startDate}/${endDate}`);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al buscar las habitaciones disponibles');
  }
};
