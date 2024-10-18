import { useState, useEffect} from 'react';
import { useLocation } from 'react-router-dom';
import Button from '../components/Button';
import Container from '../components/Container';
import useSubmitReservation from './hooks/useSubmitReservation';
import RoomCard from '../components/RoomCard';
import { getPartialPrice, getTaxPrice, getTotalPrice } from '../services/priceService';

function Confirmation() {
  const location = useLocation();
  const { checkInDate, checkOutDate, email, phone, contactId, room } = location.state;

  const [formStatus, setFormStatus] = useState({ success: null, message: '' });
  const { submitCompleteReservation } = useSubmitReservation(setFormStatus);

  const [partialPrice, setPartialPrice] = useState(0);
  const [total, setTotal] = useState(0);
  const [tax, setTax] = useState(0);

  useEffect(() => {
    async function fetchPrices() {
      const reservationList = [{
        reservationDate: checkInDate,
        useDate: checkOutDate,
        roomId: room.roomID,
        contactId: contactId,
      }];
      
      console.log("Datos de la reserva que se envían al servidor:", reservationList);

      try {
        const fetchedPartialPrice = await getPartialPrice({ reservations: reservationList });
        const fetchedTotal = await getTotalPrice({ reservations: reservationList });
        const fetchedTax = await getTaxPrice({ reservations: reservationList });
        
        setPartialPrice(fetchedPartialPrice);
        setTotal(fetchedTotal);
        setTax(fetchedTax);
      } catch (error) {
        console.error("Error al obtener los precios:", error);
      }
    }

    fetchPrices();
  }, [checkInDate, checkOutDate, room.roomID, contactId]);
  
  return (
    <div className='flex flex-col items-center justify-center space-y-12'>
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 4 de 4</h1>
      <div className='w-2/3'>
        <RoomCard
          className
          key={room.index}
          bed={room?.beds?.[0]?.size || "Unknown Bed"} 
          capacity={room?.beds?.[0]?.capacity || "Unknown Capacity"}
          price={room?.pricePerNight || 0}
          floor={room?.floorNumber || "Unknown Floor"}
          code={room?.code || "Unknown Code"}
          services={room?.services || []}
          hasButton={false}
        />
      </div>
        
      <Container>
        <div className='flex col justify-between'>
          <div className="flex flex-col justify-between gap-4">
            <p className="text-[20px] font-bold font-roboto">
              Email: {email}
            </p>
            <p className="text-[20px] font-bold font-roboto">
              Phone: {phone}
            </p>
            <p className="text-[20px] font-bold font-roboto">
              Entrada: {checkInDate}
            </p>
            <p className="text-[20px] font-bold font-roboto">
              Salida: {checkOutDate}
            </p>
          </div>

          <div className="flex flex-col justify-between gap-4">
            <p className="text-[20px] font-bold font-roboto">
              Subtotal: Bs {partialPrice}
            </p>
            <p className="text-[20px] font-bold font-roboto">
              Tax: + Bs {tax}
            </p>
            <p className="text-[20px] font-bold font-roboto">
              Total: Bs {total}
            </p>
          </div>
        </div>
        <div className='mt-12 flex justify-center items-center'>
          <Button type="common" className="font-roboto text-white"
            onClick={() => {
              const reservationDetails = [{
                reservationDate: checkInDate,
                useDate: checkOutDate,
                roomId: room.roomID,
                contactId: contactId,
              }];
              submitCompleteReservation(reservationDetails);
              console.log("Back:" + reservationDetails);
            }}>
            Confirmar Reserva
          </Button>
        </div>
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
