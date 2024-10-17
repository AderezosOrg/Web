import axios from 'axios';

export const contactAPI = axios.create({
  baseURL: 'http://localhost:5009/api/Contact',
});

export const postContact = async (contactData) => {
  try {
    const response = await contactAPI.post('/', contactData);
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al crear contacto');
  }
};

export const getContacts = async () => {
  try {
    const response = await contactAPI.get('/');
    return response.data;
  } catch (error) {
    throw new Error(error.response?.data?.message || 'Error al obtener contactos');
  }
};

export const findContactByPhoneAndEmail = async (phoneNumber, email) => {
  try {
    const contacts = await getContacts();
    
    const foundContact = contacts.find(
      (contact) => contact.phoneNumber === phoneNumber && contact.email === email
    );
    
    return foundContact;
  } catch (error) {
    throw new Error(error.message || 'Error al buscar contacto');
  }
};
