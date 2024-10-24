import Hotel from "../assets/Hotel.jpg"
import HotelRoom from "../assets/HotelRoom.jpeg"
import Google from "../assets/googleLogo.png"
import Discord from "../assets/discordLogo.png"
import Microsoft from "../assets/microsoftLogo.png"
export default function LandPage() {

  const handleGoogleLogin = () => {
    window.location.href = "http://localhost:8000/auth/google";
  };

  const handleDiscordLogin = () => {
    window.location.href = "http://localhost:8000/auth/discord";
  };

  const handleMicrosoftLogin = () => {
    window.location.href = "http://localhost:8000/auth/microsoftonline";
  };

  return (
    <div className="font-roboto flex flex-row h-screen">
      <div className="bg-[#D7D7D7] flex flex-col h-full w-1/2 items-center min-h-screen">
        <div className="flex flex-col h-full m-10 space-y-20 items-center">
          <img
            className=""
            src={Hotel}
            alt="Hotel"
          />
          <p className="text-[16px] leading-loose text-justify">
            En nuestro hotel, nos dedicamos a brindarte una experiencia única con el equilibrio perfecto entre confort y atención personalizada. Ubicados en el corazón de la ciudad, ofrecemos habitaciones modernas y servicios diseñados tanto para quienes viajan por negocios como por placer. Nuestro equipo se compromete a hacer que tu estancia sea inolvidable, asegurando que disfrutes de una atención cálida y todas las comodidades necesarias para relajarte o explorar la ciudad. ¡Te esperamos para darte la bienvenida y hacer de tu visita algo especial!
          </p>
        </div>
      </div>

      <div className="flex flex-col w-1/2 items-center">
        <div className="flex flex-col h-full m-10 space-y-10 items-center">
          <img
            className=""
            src={HotelRoom}
            alt="Hotel room"
          />
          <p className="text-[20px] font-bold">¿Quieres hacer una reserva?</p>
          <div className="flex-col space-y-4">
            <button 
              className="flex p-4 border border-rose-950 rounded-[20px] items-center justify-center space-x-2 bg-white hover:bg-[#D7D7D7] transition-transform duration-150 active:scale-95"
              onClick={handleGoogleLogin}>
              <img src={Google} 
                  alt="Google logo" 
                  className="w-6 h-6" />
              <span className="text-[20px]">Iniciar sesión con Google</span>
            </button>
            <button 
              className="flex p-4 border border-rose-950 rounded-[20px] items-center justify-center space-x-2 bg-white hover:bg-[#D7D7D7] transition-transform duration-150 active:scale-95"
              onClick={handleDiscordLogin}>
              <img src={Discord} 
                  alt="Discord logo" 
                  className="w-9 h-9" />
              <span className="text-[20px]">Iniciar sesión con Discord</span>
            </button>
            <button 
              className="flex p-4 border border-rose-950 rounded-[20px] items-center justify-center space-x-2 bg-white hover:bg-[#D7D7D7] transition-transform duration-150 active:scale-95"
              onClick={handleMicrosoftLogin}>
              <img src={Microsoft} 
                  alt="Microsoft logo" 
                  className="w-7 h-7" />
              <span className="text-[20px]">Iniciar sesión con Microsoft</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
