import Header from "./layouts/Header";
import { Routes, Route } from 'react-router-dom';
import DateForm from "./pages/DateForm";
import NotFound from "./pages/NotFound";
import Confirmation from "./pages/Confirmation";
import SelectRoom from "./pages/SelectRoom";
import PersonalInfoForm from "./pages/PersonalInfoForm";
import LandPage from "./pages/LandPage";
import { Toaster } from "sonner";

function App() {
  return (
    <div className="flex flex-col min-h-screen w-full">
      <Toaster position="bottom-right" expand visibleToasts={3}
        richColors
        toastOptions={{
          success: {
            style: {
              backgroundColor: '#d1e7dd',
              color: '#0f5132',
              borderLeft: '4px solid #0f5132',
              padding: '8px 16px',
              fontWeight: 'bold',
            },
          },
        }}
      />
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
