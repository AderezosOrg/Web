import { useLocation } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import RoomCard from "../components/RoomCard";
import {getAvailableRooms} from "../services/roomsService.js";
import {useEffect, useState} from "react";

export default function SelectRoom() {
  const [rooms, setRooms] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const location = useLocation();
  const { checkInDate, checkOutDate, numPeople } = location.state;

  useEffect(() => {
    async function fetchRooms() {
      try {
        const availableRooms = await getAvailableRooms(checkInDate, checkOutDate);
        setRooms(availableRooms);
      } catch (error) {
        console.error("Error fetching rooms:", error);
      } finally {
        setLoading(false);
      }
    }
    fetchRooms();
  }, [checkInDate, checkOutDate]);

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
    <div className="flex flex-col mr-80 ml-80 space-y-[28px] items-center">
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 2 de 3</h1>
      {rooms.map((item, index) => (
          <RoomCard
              key={index}
              bed={item?.beds?.[0]?.size || "Unknown Bed"} 
              capacity={item?.beds?.[0]?.capacity || "Unknown Capacity"}
              price={item?.pricePerNight || 0}
              floor={item?.floorNumber || "Unknown Floor"}
              code={item?.code || "Unknown Code"}
              services={item?.services || []}
              onClick={() => {
                navigate('/confirmation', {
                  state: {
                    checkInDate: checkInDate,
                    checkOutDate: checkOutDate,
                    numPeople: numPeople,
                    roomPrice: item.pricePerNight
                  }
                });
              }}
          />
      ))}
    </div>
  )
}
