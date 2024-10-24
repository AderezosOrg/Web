import { Form as FormFormik, Formik } from 'formik';
import { parsePhoneNumberFromString } from 'libphonenumber-js';
import { useState, useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import * as Yup from 'yup';
import Button from '../components/Button';
import FormContainer from '../components/Container';
import InputField from '../components/InputField';
import { putContact } from '../services/contactService';
import { getDecodedToken } from '../services/authService';

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
  const [initialValues, setInitialValues] = useState({
    ci: '',
    email: '',
    phone: '',
    contactId: '',
    sessionId: '',
  });

  useEffect(() => {
    const decodedToken = getDecodedToken();
    const savedCI = localStorage.getItem('ci');
    const savedPhone = localStorage.getItem('phone');
  
    if (decodedToken || savedCI || savedPhone) {
      setInitialValues({
        ci: savedCI || '',
        email: decodedToken.email || '',
        phone: savedPhone || '',
        contactId: decodedToken.ContactId || '',
        sessionId: decodedToken.SessionId || ''        
      });
    }
  }, []);

  const validationSchema = Yup.object().shape({
    ci: Yup.string()
      .required('El CI es obligatorio')
      .matches(/^\d+$/, 'El CI solo debe contener números')
      .min(6, 'El CI debe tener al menos 6 dígitos'),
    phone: Yup.string()
      .required('El celular es obligatorio')
      .test('isValidPhone', 'Número de teléfono no válido', (value) => validatePhoneNumber(value)),
  });

  const handleSubmit = async (values, { setSubmitting }) => {
    try {
      const contactResponse = await putContact({
        contactID: values.contactId,
        phoneNumber: values.phone,
        email: values.email,
      }, values.sessionId);

      localStorage.setItem('ci', values.ci);
      localStorage.setItem('phone', values.phone);
      localStorage.setItem('back', '');      
      
      setFormStatus({ success: true, message: 'Contacto y usuario enviados con éxito' });
      navigate('/date-form', {
        state: {
          email: values.email,
          phone: values.phone,
          contactId: contactResponse.contactID,
          sessionId: values.sessionId
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
          initialValues={initialValues}
          enableReinitialize={true}
          validationSchema={validationSchema}
          onSubmit={handleSubmit}
        >
          {({ errors, touched, isSubmitting }) => (
            <FormFormik className="flex flex-col gap-4">
              <InputField
                id="ci"
                name="ci"
                label="CI"
                type="text"
                placeholder="Ingrese su CI"
                isCorrect={!touched.ci || !errors.ci}
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
