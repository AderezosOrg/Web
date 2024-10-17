import axios from 'axios';

export const roomAPI = axios.create({
  baseURL: 'http://localhost:5009/api/RoomFilters/',
});

export const getAvailableRooms = async (roomData) => {
  try {
    const response = await roomAPI.post('available/', roomData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al buscar las habitaciones disponibles');
  }
};