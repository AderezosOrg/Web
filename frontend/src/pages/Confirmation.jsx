import { Form as FormFormik, Formik } from 'formik';
import { useState } from 'react';
import { useLocation } from 'react-router-dom';
import Button from '../components/Button';
import Container from '../components/Container';
import useSubmitReservation from './hooks/useSubmitReservation';
import RoomCard from '../components/RoomCard';

function Confirmation() {
  const location = useLocation();
  const { checkInDate, checkOutDate, numPeople, room, email, phone, contactId } = location.state;

  const [formStatus, setFormStatus] = useState({ success: null, message: '' });
  const { submitCompleteReservation } = useSubmitReservation(setFormStatus);

  return (
    <div className='flex flex-col w-screen items-center justify-center space-y-12'>
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 4 de 4</h1>
      <RoomCard
        className
        key={room.index}
        bed={room?.beds?.[0]?.size || "Unknown Bed"} 
        capacity={room?.beds?.[0]?.capacity || "Unknown Capacity"}
        price={room?.pricePerNight || 0}
        floor={room?.floorNumber || "Unknown Floor"}
        code={room?.code || "Unknown Code"}
        services={room?.services || []}
      />
        
      <Container>
      <Formik
          onSubmit={() => {
            const reservationDetails = {
              checkInDate,
              checkOutDate,
              roomId: room.id,
              contactId,
            };
            submitCompleteReservation(reservationDetails);
            console.log(reservationDetails);
          }}
        >
          <FormFormik className="flex flex-col gap-4">
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
                Precio total: {room.pricePerNight}Bs
              </p>
              <p className="text-[20px] font-bold font-roboto">
                Email: {email}
              </p>
              <p className="text-[20px] font-bold font-roboto">
                Phone: {phone}
              </p>
              <p className="text-[20px] font-bold font-roboto">
                ContactID: {contactId}
              </p>
            </div>

            <div className='mt-12 flex justify-center items-center'>
              <Button type="common" isSubmit className="font-roboto text-white">
                Confirmar Reserva
              </Button>
            </div>
          </FormFormik>
        </Formik>
        {formStatus.message && (
          <p className={`mt-4 ${formStatus.success ? 'text-green-500' : 'text-red-500'}`}>
            {formStatus.message}
          </p>
        )}
      </Container>
    </div>
  );
}

export default Confirmation;
