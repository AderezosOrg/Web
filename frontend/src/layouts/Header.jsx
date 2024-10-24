import { IoIosArrowRoundBack } from "react-icons/io";
import { useNavigate } from 'react-router-dom';

export default function Header() {
  const navigate = useNavigate();
  const lastPage = localStorage.getItem('back');

  return (
    <div className="grid grid-cols-3 items-center bg-[#881337] h-[100px] w-full">
      <div className="flex">
        <button className="px-10" onClick={() => {
          navigate(lastPage || -1)
        }}>
          <IoIosArrowRoundBack color="white" className="size-[40px]" />
        </button>
      </div>
      <p className="text-white font-roboto text-[28px] font-bold">
        Nombre Empresa Super cool
      </p>
    </div>
  );
}
