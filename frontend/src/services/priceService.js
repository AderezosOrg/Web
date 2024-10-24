import axios from 'axios';

export const priceAPI = axios.create({
  baseURL: 'http://localhost:5009/api/Price',
});

export const getPartialPrice = async (reservationData, sessionId) => {
  try {
    const response = await priceAPI.post('/partial', reservationData, {
      headers: {
        'SessionId': sessionId,
      },
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al calcular el precio parcial');
  }
};

export const getTotalPrice = async (reservationData, sessionId) => {
  try {
    const response = await priceAPI.post('/total', reservationData, {
      headers: {
        'SessionId': sessionId,
      },
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al calcular el precio total');
  }
};

export const getTaxPrice = async (reservationData, sessionId) => {
  try {
    const response = await priceAPI.post('/tax', reservationData, {
      headers: {
        'SessionId': sessionId,
      },
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al calcular el tax basado en las noches');
  }
};
