import axios from 'axios';

export const contactAPI = axios.create({
  baseURL: 'http://localhost:5009/api/Contact',
});

export const putContact = async (contactData, sessionId) => {
  try {
    const response = await contactAPI.put(`/${contactData.contactID}`, contactData, {
      headers: {
        'SessionId': sessionId,
      },
    });
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al actualizar contacto');
  }
};
