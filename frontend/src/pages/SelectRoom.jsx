import { useLocation, useNavigate } from 'react-router-dom';
import RoomCard from "../components/RoomCard";
import {getAvailableRooms} from "../services/roomsService.js";
import {useEffect, useState} from "react";

export default function SelectRoom() {
  const [rooms, setRooms] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const location = useLocation();
  const { checkInDate, checkOutDate, numPeople, email, phone, contactId, sessionId } = location.state;

  useEffect(() => {
    async function fetchRooms() {
      if (!checkInDate || !checkOutDate || !numPeople) {
        console.error("Faltan datos importantes: checkInDate, checkOutDate o numPeople");
        setLoading(false);
        return;
      }
      try {
        const reservationDetails = {
          checkInDate,
          checkOutDate,
          capacity: numPeople
        }
        const availableRooms = await getAvailableRooms(reservationDetails, sessionId);
        setRooms(availableRooms);
      } catch (error) {
        console.error("Error fetching rooms:", error);
      } finally {
        setLoading(false);
      }
    }
    fetchRooms();
  }, [checkInDate, checkOutDate, numPeople, sessionId]);

  if (loading) {
    return <div className='flex justify-center items-center h-full'>
      <p className="text-[28px] font-roboto font-bold">Loading. . .</p>
    </div>;
  }
  
  if ((rooms.length <= 0)) {
    return <div className='flex justify-center items-center h-full'>
        <p className="text-[28px] font-roboto font-bold">Rooms not found. . .</p>
      </div>;
  }
  
return(
    <div className="flex flex-col space-y-[28px] items-center">
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 3 de 4</h1>
      <div className='space-y-[28px] w-2/3'>
        {rooms.map((item, index) => (
          <RoomCard
            className
            key={index}
            bed={item?.beds?.[0]?.size || "Unknown Bed"} 
            capacity={item?.beds?.[0]?.capacity || "Unknown Capacity"}
            price={item?.pricePerNight || 0}
            floor={item?.floorNumber || "Unknown Floor"}
            code={item?.code || "Unknown Code"}
            services={item?.services || []}
            hasButton={true}
            onClick={() => {
              console.log('Navegando con los siguientes datos:', {
                checkInDate,
                checkOutDate,
                email,
                phone,
                contactId,
                sessionId,
                room: item,
              });

              navigate('/confirmation', {
                state: {
                  checkInDate,
                  checkOutDate,
                  email,
                  phone,
                  contactId,
                  sessionId,
                  room: item,
                }
              });
            }}
          />
        ))}
      </div>
    </div>
  )
}
