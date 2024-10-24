import axios from 'axios';

export const reservationAPI = axios.create({
  baseURL: 'http://localhost:5009/api',
});

export const createReservation = async (reservationData, sessionId) => {
  try {
    const response = await reservationAPI.post('/Reservation', reservationData, {
      headers: {
        'SessionId': sessionId,
      },
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al crear la reserva');
  }
};
