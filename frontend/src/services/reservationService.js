import axios from 'axios';

export const reservationAPI = axios.create({
  baseURL: 'http://localhost:5009/api/Reservation',
});

export const createReservation = async (reservationData) => {
  try {
    const response = await reservationAPI.post('/Reservation', reservationData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al crear la reserva');
  }
};
