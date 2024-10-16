import { Form as FormFormik, Formik } from 'formik';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import * as Yup from 'yup';
import Button from '../components/Button';
import InputField from '../components/InputField';
import FormContainer from '../components/FormContainer';
import { parsePhoneNumberFromString } from 'libphonenumber-js';

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
    email: Yup.string()
      .email('Debe ser un email válido')
      .required('El email es obligatorio'),
    phone: Yup.string()
      .required('El celular es obligatorio')
      .test('isValidPhone', 'Número de teléfono no válido', (value) => validatePhoneNumber(value)),
  });

  return (
    <div className='flex flex-col w-screen items-center justify-center'>
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 1 de 4</h1>
      <FormContainer>
        <Formik
          initialValues={{ name: '', email: '', phone: '' }}
          validationSchema={validationSchema}
          onSubmit={(values) => {
            setFormStatus({success: true})
            navigate('/date-form', {
              state: {
                name: values.name,
                email: values.email,
                phone: values.phone,
              }
            });
          }}
        >
          {({ errors, touched }) => (
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
                <Button type="common" isSubmit className="font-roboto text-white">
                  Continuar
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
