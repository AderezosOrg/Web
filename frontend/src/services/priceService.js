import axios from 'axios';

export const priceAPI = axios.create({
  baseURL: 'http://localhost:5009/api/Price',
});

export const getPartialPrice = async (reservationData) => {
  try {
    const response = await priceAPI.post('/partial', reservationData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al calcular el precio parcial');
  }
};

export const getTotalPrice = async (reservationData) => {
  try {
    const response = await priceAPI.post('/total', reservationData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al calcular el precio total');
  }
};

export const getTaxPrice = async (reservationData) => {
  try {
    const response = await priceAPI.post('/tax', reservationData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al calcular el tax basado en las noches');
  }
};
