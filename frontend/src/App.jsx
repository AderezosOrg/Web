import Header from "./layouts/Header";
import { Routes, Route } from 'react-router-dom';
import DateForm from "./pages/DateForm";
import NotFound from "./pages/NotFound";
import Confirmation from "./pages/Confirmation";
import SelectRoom from "./pages/SelectRoom";
import PersonalInfoForm from "./pages/PersonalInfoForm";
import LandPage from "./pages/LandPage";

function App() {
  return (
    <div className="flex flex-col h-screen w-full">
      <Header/>
      <Routes>
      <Route path='*' element={<NotFound />} />
        <Route path='/' element={<LandPage />} />
        <Route path='/personal-info' element={<PersonalInfoForm/>} />
        <Route path='/date-form' element={<DateForm/>} />
        <Route path="/confirmation" element={<Confirmation />} />
        <Route path="/select-room" element={<SelectRoom />}/>
      </Routes>
    </div>
  );
}

export default App;
