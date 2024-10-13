import { useLocation } from 'react-router-dom';
import { Form as FormFormik, Formik } from 'formik';
import { useState } from 'react';
import * as Yup from 'yup';
import Button from '../components/Button';
import InputField from '../components/InputField';
import useSubmitReservation from './hooks/useSubmitReservation';
import FormContainer from '../components/FormContainer';

function Confirmation() {
  const location = useLocation();
  const { checkInDate, checkOutDate, numPeople, roomPrice } = location.state;

  const [formStatus, setFormStatus] = useState({ success: null, message: '' });
  const { submitCompleteReservation } = useSubmitReservation(setFormStatus);

  const totalPrice = calculateTotalPrice(checkInDate, checkOutDate, roomPrice);

  const validationSchema = Yup.object().shape({
    name: Yup.string().required('El nombre es obligatorio'),
    email: Yup.string().email('Debe ser un email válido').required('El email es obligatorio'),
  });

  return (
    <div className='flex flex-col w-screen items-center justify-center'>
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 3 de 3</h1>
      <FormContainer>
        <Formik
          initialValues={{ name: '', email: '' }}
          validationSchema={validationSchema}
          onSubmit={(values) => {
            const reservationDetails = {
              ...values,
              checkInDate,
              checkOutDate,
              numPeople,
              totalPrice,
            };
            submitCompleteReservation(reservationDetails);
            console.log(reservationDetails);
          }}
        >
          {({ errors, touched }) => (
            <FormFormik className="flex flex-col gap-4">
              <InputField
                id="name"
                name="name"
                label="Nombre"
                type="text"
                placeholder="Ingresa tu nombre completo"
                isCorrect={!touched.name || !errors.name}
              />
              <InputField
                id="email"
                name="email"
                label="Email"
                type="email"
                placeholder="Ingresa tu correo"
                isCorrect={!touched.email || !errors.email}
              />

              <div className="flex flex-row justify-between gap-4">
                <p className="text-[20px] font-bold font-roboto">
                  Entrada: {checkInDate}
                </p>
                <p className="text-[20px] font-bold font-roboto">
                  Salida: {checkOutDate}
                </p>
              </div>

              <div className="flex flex-row justify-between gap-4">
                <p className="text-[20px] font-bold font-roboto">
                  Número de personas: {numPeople}
                </p>
                <p className="text-[20px] font-bold font-roboto">
                  Precio total: {totalPrice}Bs
                </p>
              </div>

              <div className='mt-12 flex justify-center items-center'>
                <Button type="common" isSubmit className="font-roboto text-white">
                  Confirmar Reserva
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

function calculateTotalPrice(checkInDate, checkOutDate, roomPrice) {
  const date1 = new Date(checkInDate);
  const date2 = new Date(checkOutDate);
  const diffTime = Math.abs(date2 - date1);
  const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
  return diffDays * roomPrice;
}

export default Confirmation;
