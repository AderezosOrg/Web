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
    return <div>Loading...</div>;
  }
  return(
    <div className="flex flex-col mr-80 ml-80 space-y-[28px] items-center">
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 2 de 3</h1>
      {rooms.map((item, index) => (
          <RoomCard
              key={index}
              bed={item?.beds?.[0]?.size || "Unknown Bed"}  // Asegúrate de que item.beds[0].size existe
              capacity={item?.beds?.[0]?.capacity || "Unknown Capacity"}  // Asegúrate de que item.beds[0].capacity existe
              price={item?.pricePerNight || 0}  // Valor por defecto si pricePerNight es undefined
              floor={item?.floorNumber || "Unknown Floor"}  // Valor por defecto si floorNumber es undefined
              code={item?.code || "Unknown Code"}  // Valor por defecto si code es undefined
              services={item?.services || []}  // Asegúrate de que item.services es un array
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
