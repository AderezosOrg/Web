import { Form as FormFormik, Formik } from 'formik';
import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import * as Yup from 'yup';
import Button from '../components/Button';
import FormContainer from '../components/Container';
import InputField from '../components/InputField';
import { getRandomAvailableRoom } from '../services/roomsService';

function DateForm() {
  const [formStatus, setFormStatus] = useState({ success: null, message: '' });
  const navigate = useNavigate();
  const location = useLocation();
  const { email, phone, contactId, sessionId } = location.state;
  
  const validationSchema = Yup.object().shape({
    checkInDate: Yup.date()
      .required('La fecha de entrada es obligatoria')
      .min(new Date(), 'La fecha de entrada no puede ser en el pasado'),
    checkOutDate: Yup.date()
      .required('La fecha de salida es obligatoria')
      .min(Yup.ref('checkInDate'), 'La fecha de salida debe ser después de la fecha de entrada'),
    numPeople: Yup.number()
      .required('El número de personas es obligatorio')
      .positive('El número de personas debe ser positivo')
      .integer('El número de personas debe ser un entero'),
  });

  const savedCheckIn = localStorage.getItem('checkInDate');
  const savedCheckOut = localStorage.getItem('checkOutDate');
  const savedNumPeople = localStorage.getItem('numPeople');
  const initialValues = {
    checkInDate: savedCheckIn || '',
    checkOutDate: savedCheckOut || '',
    numPeople: savedNumPeople || ''   
  };

  return (
    <div className='flex flex-col w-screen items-center justify-center'>
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 2 de 4</h1>
      <FormContainer>
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={(values) => {
            localStorage.setItem('checkInDate', values.checkInDate);
            localStorage.setItem('checkOutDate', values.checkOutDate);
            localStorage.setItem('numPeople', values.numPeople);
            setFormStatus({success: true})
            navigate('/select-room', {
              state: {
                ...values,
                email,
                phone,
                contactId,
                sessionId,
              }
            });
          }}
        >
          {({ errors, touched, values }) => (
            <FormFormik className="flex flex-col gap-2">
              <InputField
                id="checkInDate"
                name="checkInDate"
                label="Fecha de entrada"
                type="date"
                placeholder="Selecciona una fecha"
                isCorrect={!touched.checkInDate || !errors.checkInDate}
              />
              {touched.checkInDate && errors.checkInDate && (
                <div className="text-red-500 text-sm">{errors.checkInDate}</div>
              )}
              <InputField
                id="checkOutDate"
                name="checkOutDate"
                label="Fecha de salida"
                type="date"
                placeholder="Selecciona una fecha"
                isCorrect={!touched.checkOutDate || !errors.checkOutDate}
              />
              {touched.checkOutDate && errors.checkOutDate && (
                <div className="text-red-500 text-sm">{errors.checkOutDate}</div>
              )}
              <InputField
                id="numPeople"
                name="numPeople"
                label="Número de personas"
                type="number"
                placeholder="Ingresa el número de personas"
                isCorrect={!touched.numPeople || !errors.numPeople}
              />
              {touched.numPeople && errors.numPeople && (
                <div className="text-red-500 text-sm">{errors.numPeople}</div>
              )}
              <div className='mt-12 flex justify-center items-center space-x-20'>
                <Button type="common" isSubmit className="w-40 font-roboto text-white">
                  Continuar
                </Button>
                <Button 
                  type="common" 
                  onClick={async () => {
                    localStorage.setItem('checkInDate', values.checkInDate);
                    localStorage.setItem('checkOutDate', values.checkOutDate);
                    localStorage.setItem('numPeople', values.numPeople);
                    try {
                      const availabilityRequest = {
                        checkInDate: values.checkInDate,
                        checkOutDate: values.checkOutDate,
                        numPeople: values.numPeople,
                      };
                      const room = await getRandomAvailableRoom(availabilityRequest, sessionId);
                      if (room) {
                        navigate('/confirmation', {
                          state: {
                            checkInDate: values.checkInDate,
                            checkOutDate: values.checkOutDate,
                            email,
                            phone,
                            contactId,
                            sessionId,
                            room,
                          },
                        });
                      }
                    } catch (error) {
                      setFormStatus({ success: false, message: error.message });
                    }
                  }}
                  className="w-40 font-roboto text-white"
                >
                  Reserva Sorpresa
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

export default DateForm;
