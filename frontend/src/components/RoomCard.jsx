import PropTypes from 'prop-types';
import { useState } from 'react'
import { IoIosArrowDropleftCircle } from "react-icons/io";
import { IoIosArrowDropdownCircle } from "react-icons/io";
import Button from './Button';

export default function RoomCard({bed, capacity, price, floor, number, services})
{
  const [details, setDetails] = useState(false);

  
  return(
    <div className='flex flex-col font-roboto w-full'>
      <div className={`flex flex-row justify-between bg-white p-5 rounded${details? '-t': ''}-[20px] border-[#881337] border-2`}>
        <div className='flex flex-row justify justify-between'>
          <div className='flex flex-row px-3'>
            <p className='text-[20px] font-bold'>Cama: </p>
            <p className='text-[20px] px-1'>{bed}</p>
          </div>
          <div className='flex flex-row px-3'>
            <p className='text-[20px] font-bold'>Ocupacion Max:</p>
            <p className='text-[20px] px-1'>{capacity}</p>
          </div>
        </div>
        <div className='flex flex-row'>
          <p className='px-3 text-[20px] font-bold'>{price}Bs</p>
          <button onClick={() => setDetails((presed) => !presed)}>
            {details ?  <IoIosArrowDropdownCircle size={30} /> : <IoIosArrowDropleftCircle size={30} /> }
          </button>
        </div>
      </div>
      {details ? <div className='grid grid-cols-3 border-2 rounded-b-[20px] border-[#881337] bg-[#EEEEEE] p-5'>
        <div>
          <div className='flex flex-row px-3 py-2'>
            <p className='text-[20px] font-bold'>Piso: </p>
            <p className='text-[20px] px-1'>{floor}</p>
          </div>
          <div className='flex flex-row px-3 py-2'>
            <p className='text-[20px] font-bold'>Numero de Habitación:</p>
            <p className='text-[20px] px-1'>{number}</p>
          </div>
        </div>
        <div className='items-center text-center'>
            <p className='text-[20px] font-bold'>Servicios:</p>
            {services.map((item, index) => (
              <p key={index} className='text-[20px]'>{item}</p>
            ))}
          </div>
        <div className='flex justify-end items-end h-full' >
          <Button type={'common'} className={'h-[40px]'}>Seleccionar</Button>
        </div>
      </div> : <></>}
    </div>
    
  )
}

RoomCard.propTypes = {
  bed: PropTypes.string,
  capacity: PropTypes.number,
  price: PropTypes.number,
  floor: PropTypes.number,
  number: PropTypes.number,
  services: PropTypes.array
}
