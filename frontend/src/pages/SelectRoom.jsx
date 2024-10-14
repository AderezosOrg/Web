import { useLocation } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
import RoomCard from "../components/RoomCard";

export default function SelectRoom()
{
  const navigate = useNavigate();
  const location = useLocation();
  const { checkInDate, checkOutDate, numPeople } = location.state;

  const rooms = [{
    Beds: [
      {
        Size: "Queen",
        Capacity: "2"
      }
    ],
    PricePerNight: 100,
    FloorNumber: 1,
    Code: "abc",
    Services: ["a", "b"]
  },
  {
    Beds: [
      {
        Size: "King",
        Capacity: "3"
      }
    ],
    PricePerNight: 150,
    FloorNumber: 2,
    Code: "ijk",
    Services: ["b", "c"]
  }]
  return(
    <div className="flex flex-col mr-80 ml-80 space-y-[28px] items-center">
      <h1 className="text-[28px] font-roboto font-bold mt-8 mb-4">Paso 2 de 3</h1>
      {rooms.map((item, index) => (
        <RoomCard 
        key={index} 
        bed={item.Beds[0].Size} 
        capacity={item.Beds[0].Capacity} 
        price={item.PricePerNight}
        floor={item.FloorNumber}
        code={item.Code}
        services={item.Services}
        onClick={() => {
          navigate('/confirmation', {
            state: {
              checkInDate: checkInDate,
              checkOutDate: checkOutDate,
              numPeople: numPeople,
              roomPrice: item.PricePerNight
            }
          });
        }}
        />
      ))}
    </div>
  )
}

