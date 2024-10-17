import { Form as FormFormik, Formik } from 'formik';
import { parsePhoneNumberFromString } from 'libphonenumber-js';
import { useState} from "react";
import { useNavigate } from 'react-router-dom';
import * as Yup from 'yup';
import Button from '../components/Button';
import FormContainer from '../components/Container';
import InputField from '../components/InputField';
import { postUser } from '../services/userService';
import { postContact, findContactByPhoneAndEmail } from '../services/contactService';

const validatePhoneNumber = (phone) => {
  const phoneNumber = parsePhoneNumberFromString(phone);
  if (!phoneNumber) {
    return false;
  }
  return phoneNumber.isValid();
};

function PersonalInfoForm() {
  const [formStatus, setFormStatus] = useState({ success: null, message: '' });
  const navigate = useNavigate();

  const validationSchema = Yup.object().shape({
    name: Yup.string()
      .required('El nombre es obligatorio')
      .min(2, 'El nombre debe tener al menos 2 caracteres'),
    ci: Yup.string()
      .required('El CI es obligatorio')
      .matches(/^\d+$/, 'El CI solo debe contener números')
      .min(6, 'El CI debe tener al menos 6 dígitos'),
    email: Yup.string()
      .email('Debe ser un email válido')
      .required('El email es obligatorio'),
    phone: Yup.string()
      .required('El celular es obligatorio')
      .test('isValidPhone', 'Número de teléfono no válido', (value) => validatePhoneNumber(value)),
  });

  const handleSubmit = async (values, { setSubmitting }) => {
    try {
      const contactResponse = await postContact({
        phoneNumber: values.phone,
        email: values.email,
        reservationList: []
      });
      
      const userResponse = await postUser({
        name: values.name,
        ciNumber: values.ci,
        phoneNumber: values.phone,
        email: values.email,
        hotelList: []
      });

      const contactId = await findContactByPhoneAndEmail(values.phone, values.email);
      console.log(contactId.id),

      setFormStatus({ success: true, message: 'Contacto y usuario enviados con éxito' });
      navigate('/date-form', {
        state: {
          email: values.email,
          phone: values.phone,
          contactId: contactId,
          userName: userResponse.name,
          contact: contactResponse,
        },
      });
    } catch (error) {
      setFormStatus({ success: false, message: error.message });
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className='flex flex-col w-screen items-center justify-center'>
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 1 de 4</h1>
      <FormContainer>
        <Formik
          initialValues={{ name: '', ci: '', email: '', phone: '' }}
          validationSchema={validationSchema}
          onSubmit={handleSubmit}
        >
          {({ errors, touched, isSubmitting }) => (
            <FormFormik className="flex flex-col gap-4">
              <InputField
                id="name"
                name="name"
                label="Nombre"
                type="text"
                placeholder="Ingrese su nombre"
                isCorrect={!touched.name || !errors.name}
              />
              <InputField
                id="ci"
                name="ci"
                label="CI"
                type="text"
                placeholder="Ingrese su CI"
                isCorrect={!touched.ci || !errors.ci}
              />
              <InputField
                id="email"
                name="email"
                label="Email"
                type="email"
                placeholder="Ingrese su email"
                isCorrect={!touched.email || !errors.email}
              />
              <InputField
                id="phone"
                name="phone"
                label="Celular"
                type="text"
                placeholder="Ingrese su número de celular"
                isCorrect={!touched.phone || !errors.phone}
              />
              <div className='mt-12 flex justify-center items-center'>
                <Button type="common" isSubmit className="font-roboto text-white" disabled={isSubmitting}>
                  {isSubmitting ? 'Enviando...' : 'Continuar'}
                </Button>
              </div>
            </FormFormik>
          )}
        </Formik>
        {formStatus.message && (
          <p className={`mt-4 ${formStatus.success ? 'text-green-500' : 'text-red-500'}`}>
            {formStatus.message}
          </p>
        )}
      </FormContainer>
    </div>
  );
}

export default PersonalInfoForm;
